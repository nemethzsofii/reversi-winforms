namespace WinForms.Data
{
    public interface IReversiDataAccess
    {
        WinForm_Data LoadState(string path);
        bool SaveState(WinForm_Data data, string path);
    }
}