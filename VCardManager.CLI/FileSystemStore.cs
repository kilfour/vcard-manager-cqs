using VCardManager.Core.Abstractions;

namespace VCardManager.CLI;

public class FileSystemStore : IFileStore
{
    public bool Exists(string path) => File.Exists(path);
    public string ReadAllText(string path) => File.ReadAllText(path);
    public void WriteAllText(string path, string contents) => File.WriteAllText(path, contents);
    public void AppendAllText(string path, string contents) => File.AppendAllText(path, contents);
}