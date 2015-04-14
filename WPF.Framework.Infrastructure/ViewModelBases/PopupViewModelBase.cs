using System;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace WPF.Framework.Infrastructure.ViewModelBases
{
    public class PopupViewModelBase : ViewModelBase, IInteractionRequestAware
    {
        // Both the FinishInteraction and Notification properties will be set by the PopupWindowAction
        // when the popup is shown.
        private INotification _notification;
        public Action FinishInteraction { get; set; }

        public INotification Notification
        {
            get { return _notification; }
            set
            {
                _notification = value;
                Title = _notification.Title;
                Content = _notification.Content;
            }
        }

        private string _title;
        private object _content;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public object Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }
    }
}