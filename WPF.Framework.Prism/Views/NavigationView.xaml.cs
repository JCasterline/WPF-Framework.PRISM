using System.Collections.Generic;
using System.Windows.Controls;
using Microsoft.Practices.Prism.PubSubEvents;
using WPF.Framework.Infrastructure.Models;
using WPF.Framework.Infrastructure.PubSubEvents;

namespace WPF.Framework.Prism.Views
{
    /// <summary>
    /// Interaction logic for NavigationView.xaml
    /// </summary>
    public partial class NavigationView : UserControl
    {
        public NavigationView(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            DataContext = this;
            eventAggregator.GetEvent<AddNavigationButton>().Subscribe(OnAddNavigationButton);
        }
        List<NavigationButton> _navButtons = new List<NavigationButton>();

        public List<NavigationButton> NavButtons
        {
            get { return _navButtons; }
            set { _navButtons = value; }
        }

        private void OnAddNavigationButton(NavigationButton obj)
        {
            _navButtons.Add(obj);
        }
    }
}
