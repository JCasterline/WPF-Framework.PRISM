using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;
using WPF.Framework.AuthenticationModule.ViewModels.Interfaces;
using WPF.Framework.Infrastructure.PubSubEvents;
using WPF.Framework.Infrastructure.ViewModelBases;

namespace WPF.Framework.AuthenticationModule.ViewModels
{
    public class LoginViewModel : ViewModelBase, ILoginViewModel
    {
        public ICommand RaiseLoginCommand { get; private set; }

        private readonly IEventAggregator _eventAggregator;

        public string Username { get; set; }
        public LoginViewModel()
        {
            RaiseLoginCommand = new DelegateCommand<object>(RaiseLogin);
        }

        public LoginViewModel(IEventAggregator eventAggregator) : this()
        {
            _eventAggregator = eventAggregator;
        }

        private void RaiseLogin(object parameter)
        {
            var passwordContainer = parameter as IHavePassword;
            if (passwordContainer != null)
            {
                var secureString = passwordContainer.Password;
            }
           _eventAggregator.GetEvent<ShowWindowRegion>().Publish(false);
        }
    }
}
