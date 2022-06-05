using ModuleCommon;
using ModuleCommon.UI;
using ModuleCommon.VM;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA_V1
{
    public class ModuleA : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            ContainerCommon.NagivateToAll(containerProvider, "V1");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ContainerCommon.RegisterViewModels(containerRegistry, "V1");
            ContainerCommon.RegisterAllControlsForNavigationWithTheirViewModels(containerRegistry, "V1");

        }
    }
}
