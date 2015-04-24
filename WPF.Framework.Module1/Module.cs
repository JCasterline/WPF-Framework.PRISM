using System;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using WPF.Framework.Infrastructure;
using WPF.Framework.Infrastructure.Models;
using WPF.Framework.Infrastructure.PubSubEvents;
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

            //Register views with region manager
            _regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof (HelloWorld));
            _regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(UserControl1));

            //Since RegisterViewWithRegion does not activate a view, navigate to a view
            var mainRegion = _regionManager.Regions[RegionNames.MainRegion];
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

            //var baseAddress = "http://localhost:8080/";
            //using (var client = new HttpClient())
            //{
            //    //var response = client.GetAsync(baseAddress + "api/values").Result;

            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //    var form = new Dictionary<string, string>
            //    {
            //        {"grant_type", "password"},
            //        {"username", "Test"},
            //        {"password", "Password123!"},
            //    };

            //    var result = client.PostAsync(baseAddress + "token", new FormUrlEncodedContent(form)).Result;
            //    if (result.IsSuccessStatusCode)
            //    {
            //        var token = JsonConvert.DeserializeObject<Token>(result.Content.ReadAsStringAsync().Result);

            //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            //        var result2 = client.GetAsync(baseAddress + "api/values").Result;
            //        var values = JsonConvert.DeserializeObject<List<string>>(result2.Content.ReadAsStringAsync().Result);
            //    }
            //    else
            //    {
                    
            //    }
            //}
        }
    }
}