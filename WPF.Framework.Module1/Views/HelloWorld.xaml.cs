using System.Windows.Controls;
using Microsoft.Practices.Prism.Mvvm;
using WPF.Framework.Module1.ViewModels;

namespace WPF.Framework.Module1.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class HelloWorld : UserControl, IView
    {
        public HelloWorld()
        {
            InitializeComponent();
            //Create viewmodel programmatically
            //DataContext = new HelloWorldViewModel();
        }
    }
}
