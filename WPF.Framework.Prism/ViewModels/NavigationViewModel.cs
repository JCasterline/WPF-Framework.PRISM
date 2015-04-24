using System.Collections.Generic;
using Microsoft.Practices.Prism.PubSubEvents;
using WPF.Framework.Infrastructure.Models;
using WPF.Framework.Infrastructure.PubSubEvents;
using WPF.Framework.Prism.ViewModels.Interfaces;

namespace WPF.Framework.Prism.ViewModels
{
    public class NavigationViewModel : INavigationViewModel
    {
        public NavigationViewModel(IEventAggregator eventAggregator)
        {
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
