using VCardManager.Core.Abstractions;
using VCardManager.Core.Handlers.AddContact;
using VCardManager.Core.Handlers.DeleteContact;
using VCardManager.Core.Handlers.SearchContact;
using VCardManager.Core.Handlers.ShowAllContacts;

namespace VCardManager.Core;

public interface IMenu
{
    void Run();
}

public class Menu : IMenu
{
    private readonly IDispatcher dispatcher;
    private readonly IConsole console;

    public Menu(IDispatcher dispatcher, IConsole console)
    {
        this.dispatcher = dispatcher;
        this.console = console;
    }

    public void Run()
    {
        var running = true;
        while (running)
        {
            ShowMenu();
            running = HandleChoice(console.ReadLine());
        }
    }

    private void ShowMenu()
    {
        console.WriteLine("1. Show Contacts");
        console.WriteLine("2. Add Contact");
        console.WriteLine("3. Search Contact");
        console.WriteLine("4. Delete Contact");
        console.WriteLine("5. Export Contact");
        console.WriteLine("6. Exit");
        console.Write("Choose an option: ");
    }

    private bool HandleChoice(string choice)
    {
        switch (choice)
        {
            case "1": dispatcher.Dispatch(new ShowAllContactsCommand()); break;
            case "2": dispatcher.Dispatch(new AddContactCommand()); break;
            case "3": dispatcher.Dispatch(new SearchContactCommand()); break;
            case "4": dispatcher.Dispatch(new DeleteContactCommand()); break;
            case "5": break;
            case "6": return false;
            default: console.WriteLine("Invalid choice."); break;
        }
        return true;
    }
}
