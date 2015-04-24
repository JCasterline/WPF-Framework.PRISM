using System.Collections.Generic;
using System.Windows.Controls;
using Microsoft.Practices.Prism.PubSubEvents;
using WPF.Framework.Infrastructure.Models;
using WPF.Framework.Infrastructure.PubSubEvents;
using WPF.Framework.Prism.ViewModels.Interfaces;

namespace WPF.Framework.Prism.Views.Shell
{
    /// <summary>
    /// Interaction logic for NavigationView.xaml
    /// </summary>
    public partial class NavigationView : UserControl
    {
        public NavigationView()
        {
            InitializeComponent();
        }
        public NavigationView(INavigationViewModel vm) : this()
        {
            DataContext = vm;
        }
    }
}
