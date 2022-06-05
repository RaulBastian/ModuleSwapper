using ModuleCommon.UI;
using ModuleCommon.VM;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;

namespace ModuleCommon
{
    public class ContainerCommon
    {
        public static void RegisterViewModels(IContainerRegistry containerRegistry, string pVersion)
        {
            //We register the view models, we use the callback so we can pass in the version so it can be shown in the UI, this is for testing
            //not really needed
            containerRegistry.Register<BottomContentViewModel>(() => { return new BottomContentViewModel($"Bottom {pVersion}"); });
            containerRegistry.Register<MiddleContentViewModel>(() => { return new MiddleContentViewModel($"Middle {pVersion}"); });
            containerRegistry.Register<TopContentViewModel>(() => { return new TopContentViewModel($"Top {pVersion}"); });
        }

        public static void RegisterAllControlsForNavigationWithTheirViewModels(IContainerRegistry containerRegistry, string pVersion)
        {
            //Registers the navigation.
            //- We have registered how we want the view models to be created with 'RegisterViewModels'
            //- Now we want to define which control is injected in the region and with which viewmodel
            //- As we are working with the same MODULEA to diferentiate between them I add a suffix with the version (not sure if this is needed)
            containerRegistry.RegisterForNavigation<BottomLabelContent, BottomContentViewModel>(ReturnFormattedName(nameof(BottomLabelContent), pVersion));
            containerRegistry.RegisterForNavigation<MiddleLabelContent, MiddleContentViewModel>(ReturnFormattedName(nameof(MiddleLabelContent), pVersion));
            containerRegistry.RegisterForNavigation<TopLabelContent, TopContentViewModel>(ReturnFormattedName(nameof(TopLabelContent), pVersion));
        }

        public static void NagivateToAll(IContainerProvider containerProvider, string pVersion)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();

            //If we don't remove, new UI instantes are created and no view models are instantiated, we need to clean up first
            RemoveViewForRegionName(regionManager, ModuleCommon.RegionNames.BottomRegionName);
            RemoveViewForRegionName(regionManager, ModuleCommon.RegionNames.MiddleRegionName);
            RemoveViewForRegionName(regionManager, ModuleCommon.RegionNames.TopRegionName);

            //We navigate using the suffix with the version
            regionManager.RequestNavigate(ModuleCommon.RegionNames.BottomRegionName, ReturnFormattedName(nameof(BottomLabelContent), pVersion));
            regionManager.RequestNavigate(ModuleCommon.RegionNames.MiddleRegionName, ReturnFormattedName(nameof(MiddleLabelContent), pVersion));
            regionManager.RequestNavigate(ModuleCommon.RegionNames.TopRegionName, ReturnFormattedName(nameof(TopLabelContent), pVersion));
        }

        private static string ReturnFormattedName(string name, string version)
        {
            return $"{name}_{version}";
        }

        private static void RemoveViewForRegionName(IRegionManager regionManager, string regionName)
        {
            regionManager.Regions[regionName].RemoveAll();
        }

    }
}
