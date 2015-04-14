using System;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using WPF.Framework.Infrastructure;
using WPF.Framework.Infrastructure.ViewModelBases;

namespace WPF.Framework.Module1.ViewModels
{
    public class ItemSelectionViewModel : ViewModelBase, IInteractionRequestAware
    {
        private ItemSelectionNotification _notification;

        public ItemSelectionViewModel()
        {
            SelectItemCommand = new DelegateCommand(AcceptSelectedItem);
            CancelCommand = new DelegateCommand(CancelInteraction);
        }

        // Both the FinishInteraction and Notification properties will be set by the PopupWindowAction
        // when the popup is shown.
        public Action FinishInteraction { get; set; }

        public INotification Notification
        {
            get
            {
                return _notification;
            }
            set
            {
                if (!(value is ItemSelectionNotification)) return;
                // To keep the code simple, this is the only property where we are raising the PropertyChanged event,
                // as it's required to update the bindings when this property is populated.
                // Usually you would want to raise this event for other properties too.
                _notification = (ItemSelectionNotification) value;
                OnPropertyChanged(() => Notification);
            }
        }

        public string SelectedItem { get; set; }

        public ICommand SelectItemCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public void AcceptSelectedItem()
        {
            if (_notification != null)
            {
                _notification.SelectedItem = this.SelectedItem;
                _notification.Confirmed = true;
            }

            FinishInteraction();
        }

        public void CancelInteraction()
        {
            if (_notification != null)
            {
                _notification.SelectedItem = null;
                _notification.Confirmed = false;
            }

            FinishInteraction();
        }
    }
}
