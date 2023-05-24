using System.ComponentModel;

namespace JackCraftLauncher.CrossPlatform.Class.ListTemplate;

public class DefaultDownloadList : INotifyPropertyChanged
{
    public string time;

    public string version;

    public DefaultDownloadList()
    {
    }

    public DefaultDownloadList(string version, string time)
    {
        this.version = version;
        this.time = time;
    }

    public string Version
    {
        get => version;
        set
        {
            version = value;
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Age"));
        }
    }

    public string Time
    {
        get => time;
        set
        {
            time = value;
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Age"));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
}