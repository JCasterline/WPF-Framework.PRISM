using System;
using System.Windows.Forms;
using WPF.Framework.Infrastructure.Services.Interfaces;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace WPF.Framework.Infrastructure.Services
{
    public class DialogService : IDialogService
    {
        public string OpenFileDialog(string defaultPath = null)
        {
            var dialog = new OpenFileDialog {InitialDirectory = defaultPath};
            var dialogResult = dialog.ShowDialog();
            if (dialogResult.HasValue && dialogResult.Value)
            {
                return dialog.FileName;
            }
            return null;
        }

        public string SaveFileDialog(string defaultPath = null)
        {
            var dialog = new SaveFileDialog {InitialDirectory = defaultPath};
            var dialogResult = dialog.ShowDialog();
            if (dialogResult.HasValue && dialogResult.Value)
            {
                return dialog.FileName;
            }
            return null;
        }

        public string FolderBrowserDialog(string defaultPath = null)
        {
            var dialog = new FolderBrowserDialog {SelectedPath = defaultPath};
            var dialogResult = dialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                return dialog.SelectedPath;
            }
            return null;
        }

        public void ExceptionMessageBox(Exception ex, string title=null)
        {
            var msgbox = new Common.WPF.ExceptionMessageBox(ex, title);
            msgbox.ShowDialog();
        }
    }
}