using System;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using WPF.Framework.Module1.ViewModels;
using WPF.Framework.Module1.ViewModels.Interfaces;
using WPF.Framework.Module1.Views;

namespace WPF.Framework.Module1
{
    //Specify dependencies using unity
    //[Module(ModuleName = "Module1")]
    //[ModuleDependency("Module2")]
    public class Module : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;
        private readonly ILoggerFacade _logger;

        public Module(IRegionManager regionManager, ILoggerFacade logger, IUnityContainer container)
        {
            _container = container;
            _regionManager = regionManager;
            _logger = logger;
        }

        public void Initialize()
        {
            //Register viewmodels
            _container.RegisterType<IHelloWorldViewModel, HelloWorldViewModel>();

            //Register views with region manager
            _regionManager.RegisterViewWithRegion("MainRegion", typeof (HelloWorld));

            try
            {
                throw new Exception("Test", new Exception("Test inner"));
            }
            catch (Exception ex)
            {
                _logger.Log(ex.ToString(), Category.Exception, Priority.None);
            }
            _logger.Log("Message", Category.Debug, Priority.None);
        }
    }
}