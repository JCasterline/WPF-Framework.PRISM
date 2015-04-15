using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using WPF.Framework.Infrastructure.Services;
using WPF.Framework.Infrastructure.Services.Interfaces;
using WPF.Framework.Infrastructure.ViewModelBases;
using WPF.Framework.Module1.ViewModels.Interfaces;

namespace WPF.Framework.Module1.ViewModels
{
    public class HelloWorldViewModel : ViewModelBase, IHelloWorldViewModel
    {

        //RaiseOpenFileDialogCommand


        private string _resultMessage;

        public InteractionRequest<INotification> NotificationRequest { get; private set; }
        public InteractionRequest<IConfirmation> ConfirmationRequest { get; private set; }
        public InteractionRequest<INotification> CustomPopupViewRequest { get; private set; }
        public InteractionRequest<ItemSelectionNotification> ItemSelectionRequest { get; private set; }

        private readonly IDialogService _dialogService;

        public HelloWorldViewModel()
        {
            // To setup the interaction request we only need to create instances of the InteractionRequest class
            // and expose them through properties. The InteractionRequestTriggers will bind to them and subscribe
            // to the corresponding events automatically.
            // The InteractionRequest class requires a parameter that has to inherit from the INotification class.
            NotificationRequest = new InteractionRequest<INotification>();
            ConfirmationRequest = new InteractionRequest<IConfirmation>();
            CustomPopupViewRequest = new InteractionRequest<INotification>();
            ItemSelectionRequest = new InteractionRequest<ItemSelectionNotification>();

            RaiseNotificationCommand = new DelegateCommand(RaiseNotification);
            RaiseConfirmationCommand = new DelegateCommand(RaiseConfirmation);
            RaiseCustomPopupViewCommand = new DelegateCommand(RaiseCustomPopupView);
            RaiseItemSelectionCommand = new DelegateCommand(RaiseItemSelection);

            RaiseOpenFileDialogCommand = new DelegateCommand(RaiseOpenFileDialog);
            RaiseExceptionDialogCommand = new DelegateCommand(RaiseExceptionDialog);
        }

        public HelloWorldViewModel(IDialogService dialogService) : this()
        {
            _dialogService = dialogService;
        }

        public string InteractionResultMessage
        {
            get { return _resultMessage; }
            set { SetProperty(ref _resultMessage, value); }
        }

        private string _helloWorld = "Hello World!";

        public string HelloWorld
        {
            get { return _helloWorld; }
            set { SetProperty(ref _helloWorld, value); }
        }

        public ICommand RaiseNotificationCommand { get; private set; }
        public ICommand RaiseConfirmationCommand { get; private set; }
        public ICommand RaiseCustomPopupViewCommand { get; private set; }
        public ICommand RaiseItemSelectionCommand { get; private set; }

        public ICommand RaiseOpenFileDialogCommand { get; private set; }
        public ICommand RaiseExceptionDialogCommand { get; private set; }

        private void RaiseNotification()
        {
            // By invoking the Raise method we are raising the Raised event and triggering any InteractionRequestTrigger that
            // is subscribed to it.
            // As parameters we are passing a Notification, which is a default implementation of INotification provided by Prism
            // and a callback that is executed when the interaction finishes.
            NotificationRequest.Raise(
                new Notification {Content = "Notification Message", Title = "Notification"},
                n => { InteractionResultMessage = "The user was notified."; });
        }

        private void RaiseConfirmation()
        {
            // By invoking the Raise method we are raising the Raised event and triggering any InteractionRequestTrigger that
            // is subscribed to it.
            // As parameters we are passing a Confirmation, which is a default implementation of IConfirmation (which inherits
            // from INotification) provided by Prism and a callback that is executed when the interaction finishes.
            ConfirmationRequest.Raise(
                new Confirmation {Content = "Confirmation Message", Title = "Confirmation"},
                c => { InteractionResultMessage = c.Confirmed ? "The user accepted." : "The user cancelled."; });
        }

        private void RaiseCustomPopupView()
        {
            // In this case we are passing a simple notification as a parameter.
            // The custom popup view we are using for this interaction request does not have a DataContext of its own
            // so it will inherit the DataContext of the window, which will be this same notification.
            InteractionResultMessage = "";
            CustomPopupViewRequest.Raise(
                new Notification {Content = "Message for the CustomPopupView", Title = "Custom Popup"});
        }

        private void RaiseItemSelection()
        {
            // Here we have a custom implementation of INotification which allows us to pass custom data in the 
            // parameter of the interaction request. In this case, we are passing a list of items.
            var notification = new ItemSelectionNotification();
            notification.Items.Add("Item1");
            notification.Items.Add("Item2");
            notification.Items.Add("Item3");
            notification.Items.Add("Item4");
            notification.Items.Add("Item5");
            notification.Items.Add("Item6");

            notification.Title = "Items";

            // The custom popup view in this case has its own view model which implements the IInteractionRequestAware interface
            // therefore, its Notification property will be automatically populated with this notification by the PopupWindowAction.
            // Like this that view model is able to recieve data from this one without knowing each other.
            InteractionResultMessage = "";
            ItemSelectionRequest.Raise(notification,
                returned =>
                {
                    if (returned != null && returned.Confirmed && returned.SelectedItem != null)
                    {
                        InteractionResultMessage = "The user selected: " + returned.SelectedItem;
                    }
                    else
                    {
                        InteractionResultMessage = "The user cancelled the operation or didn't select an item.";
                    }
                });
        }

        private void RaiseOpenFileDialog()
        {
            var filePath = _dialogService.OpenFileDialog();
        }

        private void RaiseExceptionDialog()
        {
            try
            {
                ThrowException();
            }
            catch (Exception ex)
            {
                _dialogService.ExceptionMessageBox(ex, "Hello World");
            }
        }

        private void ThrowException()
        {
            try
            {
                ThrowInnerException();
            }
            catch (Exception ex)
            {
                Exception exc = new Exception("Cannot escape throwing error.", ex);
                throw exc;
            }
        }
        private void ThrowInnerException()
        {
            Int32.Parse("I");
        }
    }
}
