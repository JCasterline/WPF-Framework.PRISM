using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Framework.AuthenticationModule.ViewModels.Interfaces
{
    public interface IHavePassword
    {
        System.Security.SecureString Password { get; }
    }
}
