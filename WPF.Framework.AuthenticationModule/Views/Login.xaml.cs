using System.Windows.Controls;
using WPF.Framework.AuthenticationModule.ViewModels.Interfaces;

namespace WPF.Framework.AuthenticationModule.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl, IHavePassword
    {
        public Login()
        {
            InitializeComponent();
        }

        public Login(ILoginViewModel vm) : this()
        {
            DataContext = vm;
        }

        public System.Security.SecureString Password
        {
            get { return PasswordBox.SecurePassword; }
        }
    }
}
