using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using WPF.Framework.AuthenticationModule.ViewModels.Interfaces;
using WPF.Framework.Infrastructure;
using WPF.Framework.Infrastructure.PubSubEvents;
using WPF.Framework.Infrastructure.ViewModelBases;

namespace WPF.Framework.AuthenticationModule.ViewModels
{
    public class LockViewModel : ViewModelBase, ILockViewModel
    {
        public ICommand RaiseUnlockCommand { get; private set; }

        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

        public LockViewModel()
        {
            RaiseUnlockCommand = new DelegateCommand(RaiseUnlock);
        }

        public LockViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
            : this()
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;

            _eventAggregator.GetEvent<LockApplication>().Subscribe(LockApplication);
        }

        private void LockApplication(object obj)
        {
            var windowRegion = _regionManager.Regions[RegionNames.WindowRegion];
            var view = windowRegion.GetView("Lock");
            windowRegion.Activate(view);
            _eventAggregator.GetEvent<ShowWindowRegion>().Publish(true);
        }
        private void RaiseUnlock()
        {
           _eventAggregator.GetEvent<ShowWindowRegion>().Publish(false);
        }
    }
}
