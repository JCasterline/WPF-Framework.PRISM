using System;

namespace WPF.Framework.Infrastructure.Services.Interfaces
{
    public interface IDialogService
    {
        string OpenFileDialog(string defaultPath = null);
        string SaveFileDialog(string defaultPath = null);
        string FolderBrowserDialog(string defaultPath = null);

        void ExceptionMessageBox(Exception ex, string title);
    }
}
