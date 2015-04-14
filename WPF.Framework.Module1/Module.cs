using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace WPF.Framework.Module1
{
    //Specify dependencies using unity
    //[Module(ModuleName = "Module1")]
    //[ModuleDependency("Module2")]
    public class Module : IModule
    {
        private readonly IRegionManager _regionManager;

        public Module(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("MainRegion", typeof (Views.HelloWorld));
        }
    }
}