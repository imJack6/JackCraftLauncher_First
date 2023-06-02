namespace JackCraftLauncher.Desktop.Interface;

public interface IPlugin
{
    string Name { get; }
    string Description { get; }
    string Author { get; }
    string Version { get; }
    void Execute();
}