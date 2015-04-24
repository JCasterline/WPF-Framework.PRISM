using System.Windows.Controls;
using WPF.Framework.Module1.ViewModels.Interfaces;

namespace WPF.Framework.Module1.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class HelloWorld : UserControl
    {
        public HelloWorld()
        {
            InitializeComponent();   
        }

        public HelloWorld(IHelloWorldViewModel vm) : this()
        {
            //Create viewmodel programmatically
            DataContext = vm;
        }
    }
}
