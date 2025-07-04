namespace VCardManager.Core.Abstractions;

public interface IFileStore
{
    bool Exists(string path);
    string ReadAllText(string path);
    void WriteAllText(string path, string contents);
    void AppendAllText(string path, string contents);
}
