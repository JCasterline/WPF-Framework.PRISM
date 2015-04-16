using Microsoft.Practices.Prism.Commands;

namespace WPF.Framework.Infrastructure.Models
{
    public class NavigationButton
    {
        public string ButtonText { get; set; }
        public DelegateCommand ButtonAction { get; set; }
    }
}
