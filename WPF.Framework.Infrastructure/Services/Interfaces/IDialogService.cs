namespace WPF.Framework.Infrastructure.Services.Interfaces
{
    public interface IDialogService
    {
        string OpenFileDialog(string defaultPath = null);
        string SaveFileDialog(string defaultPath = null);
        string FolderBrowserDialog(string defaultPath = null);
        //Other similar untestable IO operations
        //Stream OpenFile(string path);
    }
}
