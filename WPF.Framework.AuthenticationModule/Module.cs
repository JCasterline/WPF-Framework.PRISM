using System;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using WPF.Framework.AuthenticationModule.ViewModels;
using WPF.Framework.AuthenticationModule.ViewModels.Interfaces;
using WPF.Framework.AuthenticationModule.Views;
using WPF.Framework.Infrastructure;
using WPF.Framework.Infrastructure.Models;
using WPF.Framework.Infrastructure.PubSubEvents;

namespace WPF.Framework.AuthenticationModule
{
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
            _container.RegisterType<ILoginViewModel, LoginViewModel>();
            _container.RegisterType<ILockViewModel, LockViewModel>();

            //Register views with region manager
            _regionManager.RegisterViewWithRegion(RegionNames.WindowRegion, typeof(Login));

            var view = _container.Resolve<Lock>();
            _regionManager.Regions[RegionNames.WindowRegion].Add(view, "Lock");
            //_regionManager.RegisterViewWithRegion(RegionNames.WindowRegion, typeof(Lock));

            //Since RegisterViewWithRegion does not activate a view, navigate to a view
            var windowRegion = _regionManager.Regions[RegionNames.WindowRegion];
            windowRegion.RequestNavigate(new Uri("Login", UriKind.Relative));

            var navButton = new NavigationButton()
            {
                ButtonText = "Lock Application",
                ButtonAction = new DelegateCommand(
                    () =>
                    {
                        _eventAggregator.GetEvent<LockApplication>().Publish(null);
                    })
            };
            _eventAggregator.GetEvent<AddNavigationButton>().Publish(navButton);

            _eventAggregator.GetEvent<ShowWindowRegion>().Publish(true);
        }
    }
}
