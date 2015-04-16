using System;
using System.Dynamic;
using System.Windows.Input;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using WPF.Framework.Infrastructure;
using WPF.Framework.Infrastructure.PubSubEvents;
using WPF.Framework.Module1.ViewModels;
using WPF.Framework.Module1.ViewModels.Interfaces;
using WPF.Framework.Module1.Views;
using Microsoft.Practices.Prism.Commands;
using WPF.Framework.Infrastructure.Models;

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
        private readonly IEventAggregator _eventAggregator;

        public Module(IRegionManager regionManager, ILoggerFacade logger, IUnityContainer container, IEventAggregator eventAggregator)
        {
            _container = container;
            _regionManager = regionManager;
            _logger = logger;
            _eventAggregator = eventAggregator;
        }

        public void Initialize()
        {
            //Register viewmodels
            _container.RegisterType<IHelloWorldViewModel, HelloWorldViewModel>();

            var mainRegion = _regionManager.Regions[RegionNames.MainRegion];

            //Register views with region manager
            //Register useing view injection since there can be multiple views in the main region
            var view = _container.Resolve <HelloWorld>();
            mainRegion.Add(view);
            mainRegion.Deactivate(view);

            var view2 = _container.Resolve<UserControl1>();
            mainRegion.Add(view2);
            mainRegion.Deactivate(view2);

            //To change view
            mainRegion.RequestNavigate(new Uri("HelloWorld", UriKind.Relative));

            try
            {
                throw new Exception("Test", new Exception("Test inner"));
            }
            catch (Exception ex)
            {
                _logger.Log(ex.ToString(), Category.Exception, Priority.None);
            }
            _logger.Log("Message", Category.Debug, Priority.None);

            var navButton = new NavigationButton(){ButtonText = "Show UserControl1", ButtonAction = new DelegateCommand(
                () =>
                {
                    mainRegion.RequestNavigate(new Uri("UserControl1", UriKind.Relative));
                })};
            _eventAggregator.GetEvent<AddNavigationButton>().Publish(navButton);

            navButton = new NavigationButton() { ButtonText = "Show HelloWorld", ButtonAction = new DelegateCommand(
                () =>
                {
                    mainRegion.RequestNavigate(new Uri("HelloWorld", UriKind.Relative));
                }) };
            _eventAggregator.GetEvent<AddNavigationButton>().Publish(navButton);
        }
    }
}