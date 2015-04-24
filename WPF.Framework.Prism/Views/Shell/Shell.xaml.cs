using System.Windows;
using WPF.Framework.Prism.ViewModels.Interfaces;

namespace WPF.Framework.Prism.Views.Shell
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell : Window, IShell
    {
        public Shell()
        {
            InitializeComponent();
        }

        public Shell(IShellViewModel vm) : this()
        {
            DataContext = vm;
        }
    }
}
