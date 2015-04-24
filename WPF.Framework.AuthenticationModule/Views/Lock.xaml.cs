using System.Windows.Controls;
using WPF.Framework.AuthenticationModule.ViewModels.Interfaces;

namespace WPF.Framework.AuthenticationModule.Views
{
    /// <summary>
    /// Interaction logic for Lock.xaml
    /// </summary>
    public partial class Lock : UserControl
    {
        public Lock()
        {
            InitializeComponent();
        }

        public Lock(ILockViewModel vm) : this()
        {
            DataContext = vm;
        }
    }
}
