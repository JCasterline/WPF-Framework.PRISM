using System;
using System.Diagnostics;
using System.Windows.Controls;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Mvvm;
using WPF.Framework.Infrastructure.Services.Interfaces;
using WPF.Framework.Module1.ViewModels;
using WPF.Framework.Module1.ViewModels.Interfaces;

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
        }

        public HelloWorld(IHelloWorldViewModel vm) : this()
        {
            //Create viewmodel programmatically
            DataContext = vm;
        }
    }
}
