using System.Windows.Controls;
using WPF.Framework.Module1.ViewModels;

namespace WPF.Framework.Module1.Views
{
    /// <summary>
    /// Interaction logic for ItemSelectionView.xaml
    /// </summary>
    public partial class ItemSelectionView : UserControl
    {
        public ItemSelectionView()
        {
            DataContext = new ItemSelectionViewModel();
            InitializeComponent();
        }
    }
}
