using Microsoft.Practices.Prism.PubSubEvents;
using WPF.Framework.Infrastructure.PubSubEvents;
using WPF.Framework.Infrastructure.ViewModelBases;
using WPF.Framework.Prism.ViewModels.Interfaces;

namespace WPF.Framework.Prism.ViewModels
{
    public class ShellViewModel : ViewModelBase, IShellViewModel
    {
        public ShellViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<ShowWindowRegion>().Subscribe(OnShowWindowRegion);
        }

        public bool ShowWindowRegion
        {
            get { return _showWindowRegion; }
            set { SetProperty(ref _showWindowRegion, value); }
        }

        private void OnShowWindowRegion(bool show)
        {
            ShowWindowRegion = show;
        }

        private bool _showWindowRegion;
    }
}
