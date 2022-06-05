using ModuleCommon;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA_V2
{
    public class ModuleA: IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            ContainerCommon.NagivateToAll(containerProvider, "V2");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ContainerCommon.RegisterViewModels(containerRegistry, "V2");
            ContainerCommon.RegisterAllControlsForNavigationWithTheirViewModels(containerRegistry, "V2");

        }
    }
}
