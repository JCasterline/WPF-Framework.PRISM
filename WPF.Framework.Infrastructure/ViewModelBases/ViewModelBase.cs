using System.ComponentModel;
using Microsoft.Practices.Prism.Mvvm;

namespace WPF.Framework.Infrastructure.ViewModelBases
{
    public class ViewModelBase : BindableBase
    {
        public new event PropertyChangedEventHandler PropertyChanged;

        public virtual void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
