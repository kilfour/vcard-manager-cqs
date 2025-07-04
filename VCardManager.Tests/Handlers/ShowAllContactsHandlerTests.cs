using Moq;
using VCardManager.Core;
using VCardManager.Core.Handlers.ShowAllContacts;

namespace VCardManager.Tests;

public class ShowAllContactsHandlerTests
{
    private readonly Mock<IAmAStackOfPaper> stackOfPaper;
    private readonly Mock<IAmAPrinter> printer;
    private readonly ShowAllContactsHandler handler;

    public ShowAllContactsHandlerTests()
    {
        stackOfPaper = new Mock<IAmAStackOfPaper>();
        printer = new Mock<IAmAPrinter>();
        handler = new ShowAllContactsHandler(stackOfPaper.Object, printer.Object);
    }

    [Fact]
    public void ShowAllContacts()
    {
        var contact = new ContactCard("the test contact", "", "", "");
        stackOfPaper.Setup(a => a.GetAllContactCards()).Returns([contact]);
        handler.ExecuteCommand(new ShowAllContactsCommand());
        stackOfPaper.Verify(a => a.GetAllContactCards(), Times.Once);
        printer.Verify(a => a.PrintContactCards(
            It.Is<IEnumerable<ContactCard>>(b => b.First().FirstName == "the test contact")), Times.Once);
    }
}