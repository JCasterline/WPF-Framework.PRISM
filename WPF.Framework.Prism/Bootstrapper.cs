using System;
using System.IO;
using System.Windows;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using WPF.Framework.Module1;

namespace WPF.Framework.Prism
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
        }
        
        protected override DependencyObject CreateShell()
        {
            return new Shell();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            //Register modules in code
            //ModuleCatalog moduleCatalog = (ModuleCatalog)ModuleCatalog;
            //moduleCatalog.AddModule(typeof(WPF.Framework.Module1.Module));
        }

        #region Overrides of Bootstrapper

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

            return base.CreateModuleCatalog();
        }

        #endregion
    }
}
