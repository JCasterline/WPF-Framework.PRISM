using System.Windows;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using WPF.Framework.Infrastructure;
using WPF.Framework.Infrastructure.Services;
using WPF.Framework.Infrastructure.Services.Interfaces;
using WPF.Framework.Prism.UnityExtensions;
using WPF.Framework.Prism.Views;

namespace WPF.Framework.Prism
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            //Add extensions for getting the correct named logger
            Container.AddNewExtension<BuildTracking>().AddNewExtension<LogCreation>();

            //Resolve default instance of ILoggerFacade
            Container.Resolve<ILoggerFacade>();

            //Register types as singletons
            Container.RegisterType<IShell, Shell>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IDialogService, DialogService>(new ContainerControlledLifetimeManager());
        }

        protected override ILoggerFacade CreateLogger()
        {
            //Create default instance of logger for logging bootstrapper items
            //This happens before the container is created.
            return new LoggerService(typeof (Bootstrapper));
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<IShell>() as DependencyObject;
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Shell) Shell;
            Application.Current.MainWindow.Show();

            var regionManager = Container.Resolve<RegionManager>();

            //Register views with view discover since only one view will be displayed in these regions
            regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, () => Container.Resolve<NavigationView>());
            regionManager.RegisterViewWithRegion(RegionNames.StatusRegion, () => Container.Resolve<StatusView>());
            regionManager.RegisterViewWithRegion(RegionNames.MenuRegion, () => Container.Resolve<MenuView>());


        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            //Register modules in code
            //ModuleCatalog moduleCatalog = (ModuleCatalog)ModuleCatalog;
            //moduleCatalog.AddModule(typeof(WPF.Framework.Module1.Module));
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            //Register modules using a XAML file
            //using (var catalogStream = new FileStream(@".\ModuleCatalog.xaml", FileMode.Open))
            //{
            //    var catalog = Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(catalogStream);
            //    return catalog;
            //}

            //Discover modules in a directory
            //var di = new DirectoryInfo(@".");
            //if(di.Exists)
            //    return new DirectoryModuleCatalog(){ModulePath = di.FullName};

            //Register modules using a configuration file
            return new ConfigurationModuleCatalog();

            //return base.CreateModuleCatalog();
        }
    }
}