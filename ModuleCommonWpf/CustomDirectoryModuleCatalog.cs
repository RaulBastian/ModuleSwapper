using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ModuleCommon
{
    public class CustomDirectoryModuleCatalog: DirectoryModuleCatalog
    {
        private FileSystemWatcher watcher = null;
        private readonly IContainerProvider container;

        public CustomDirectoryModuleCatalog(IContainerProvider container)
        {
            this.container = container;
        }

        protected override void InnerLoad()
        {
            base.InnerLoad();

            watcher = new FileSystemWatcher(System.IO.Path.GetFullPath(this.ModulePath));

            watcher.EnableRaisingEvents = true;
            watcher.Created += Watcher_Created;
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
           //It could happen that this event fires before the copying finishes, it was throw errors when trying to read
           while(IsFileLocked(new FileInfo(e.FullPath)))            {

           }
  
           //We obtain the copied assembly
            var a = Assembly.Load(File.ReadAllBytes(e.FullPath));

            //And retrieve the existing IModules in the assembly
            foreach(var moduleType in a.GetTypes().Where(t => typeof(IModule).IsAssignableFrom(t) == true))
            {
                //As they could possibly share the same names, we rename the existing one so when loading by name it only finds the new one
                if(this.Modules.Where(n=> n.ModuleName == moduleType.Name).Any())
                {
                    var moduleInfoInCollection = this.Modules.Where(n => n.ModuleName == moduleType.Name).First();
                    moduleInfoInCollection.ModuleName = $"UNLOADED_{moduleInfoInCollection.ModuleName}";
                }

                //We add the module info
                //Ref, it's needed by PRISM to locate the assembly when instantiating the module
                //As a note, there are some classes responsible for this instantiation, it was raising errors prefixing with file:// solves
                //the problem
                this.AddModule(new ModuleInfo()
                {
                    ModuleName = moduleType.Name,
                    ModuleType = moduleType.AssemblyQualifiedName,
                    Ref = $"file://{a.Location}",
                });

                var manager = container.Resolve<IModuleManager>();

                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    //We most likely are on another thread, how the system watcher works, we dispatch to the UI thread
                    //so the interface can be updated
                    manager.LoadModule(moduleType.Name);

                }), DispatcherPriority.ApplicationIdle);
            }

            this.EnsureCatalogValidated();
        }

        private bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }


        public override IModuleCatalog AddModule(IModuleInfo moduleInfo)
        {
            return base.AddModule(moduleInfo);
        }


        protected override IEnumerable<IModuleInfo> Sort(IEnumerable<IModuleInfo> modules)
        {
            return base.Sort(modules);
        }
    }
}
