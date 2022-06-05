using ModuleCommon;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ModuleSwapper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Prism.Unity.PrismApplication
    {
        protected override Window CreateShell()
        {
            return new ShellWindow();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);

        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new CustomDirectoryModuleCatalog(Container) { ModulePath = @"..\\..\\..\\..\\ModulesOutput\Debug\net6.0-windows" };
        }
    }
}
