using System;
using System.Windows;
using Common.Application;

namespace WPF.Framework.Prism
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private SingleGlobalInstance _instance;

        #region Overrides of Application

        protected override void OnStartup(StartupEventArgs e)
        {
            _instance = SingleGlobalInstance.AcquireWithin(TimeSpan.FromSeconds(3));
            if (!_instance.HasHandle)
            {
                MessageBox.Show(
                    "Another instance of the application is running. " +
                    "Only a single instance is allowed to run at a time.");
                return;
            }

            base.OnStartup(e);

            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }

        #region Overrides of Application

        protected override void OnExit(ExitEventArgs e)
        {
            _instance.Dispose();
            base.OnExit(e);
        }

        #endregion

        #endregion
    }
}