using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using WPF.Framework.Infrastructure.ViewModelBases;

namespace WPF.Framework.Module1.ViewModels
{
    public class CustomPopupViewModel : PopupViewModelBase
    {
        public ICommand RaiseCloseCommand { get; private set; }

        public CustomPopupViewModel()
        {
            RaiseCloseCommand = new DelegateCommand(RaiseClose);
        }

        private void RaiseClose()
        {
            if (FinishInteraction != null)
                FinishInteraction();
        }
    }
}