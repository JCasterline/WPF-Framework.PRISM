using System.Windows.Controls;
using WPF.Framework.Module1.ViewModels;

namespace WPF.Framework.Module1.Views
{
    /// <summary>
    /// Interaction logic for CustomPopupView.xaml
    /// </summary>
    public partial class CustomPopupView : UserControl
    {
        public CustomPopupView()
        {
            InitializeComponent();
            DataContext = new CustomPopupViewModel();
        }
    }
}
