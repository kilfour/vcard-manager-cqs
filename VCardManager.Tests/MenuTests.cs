using Moq;
using VCardManager.Core;
using VCardManager.Core.Handlers.AddContact;
using VCardManager.Core.Handlers.DeleteContact;
using VCardManager.Core.Handlers.SearchContact;
using VCardManager.Core.Handlers.ShowAllContacts;
using VCardManager.Tests._tools;

namespace VCardManager.Tests;

public class MenuTests
{

    [Fact]
    public void Menu_Six_Exits()
    {
        var dispatcher = new Mock<IDispatcher>();
        var console = new ConsoleSpy();
        console.AddInput("6");
        new Menu(dispatcher.Object, console).Run();
    }

    private Mock<IDispatcher> PickOnceFromMenu(string choice)
    {
        var dispatcher = new Mock<IDispatcher>();
        var console = new ConsoleSpy();
        console.AddInput(choice);
        console.AddInput("6"); // <= QUIT
        new Menu(dispatcher.Object, console).Run();
        return dispatcher;
    }

    [Fact]
    public void Menu_One()
    {
        PickOnceFromMenu("1").Verify(a => a.Dispatch(It.IsAny<ShowAllContactsCommand>()), Times.Once);
    }

    [Fact]
    public void Menu_Two()
    {
        PickOnceFromMenu("2").Verify(a => a.Dispatch(It.IsAny<AddContactCommand>()), Times.Once);
    }

    [Fact]
    public void Menu_Three()
    {
        PickOnceFromMenu("3").Verify(a => a.Dispatch(It.IsAny<SearchContactCommand>()), Times.Once);
    }

    [Fact]
    public void Menu_Four()
    {
        PickOnceFromMenu("4").Verify(a => a.Dispatch(It.IsAny<DeleteContactCommand>()), Times.Once);
    }

    [Fact(Skip = "not implemented")]
    public void Menu_Five()
    {
        //PickOnceFromMenu("5").Verify(a => a.ExportContact(), Times.Once);
    }
}