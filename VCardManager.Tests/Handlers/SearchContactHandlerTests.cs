using Moq;
using VCardManager.Core;
using VCardManager.Core.Handlers.SearchContact;

namespace VCardManager.Tests;

public class SearchContactHandlerTests
{
    private readonly Mock<IAmAStackOfPaper> stackOfPaper;
    private readonly Mock<IAmInquisitive> inquisitor;
    private readonly Mock<IAmAPrinter> printer;
    private readonly SearchContactHandler handler;

    public SearchContactHandlerTests()
    {
        stackOfPaper = new Mock<IAmAStackOfPaper>();
        inquisitor = new Mock<IAmInquisitive>();
        printer = new Mock<IAmAPrinter>();
        handler = new SearchContactHandler(stackOfPaper.Object, inquisitor.Object, printer.Object);
    }

    [Fact]
    public void SearchContact()
    {
        var contact = new ContactCard("the test contact", "m", "33", "@");
        inquisitor.Setup(a => a.GetSearchString()).Returns("33");
        stackOfPaper.Setup(a => a.FindAllContactCards("33")).Returns([contact]);

        handler.ExecuteCommand(new SearchContactCommand());

        inquisitor.Verify(a => a.GetSearchString(), Times.Once);
        stackOfPaper.Verify(a => a.FindAllContactCards("33"), Times.Once);
        printer.Verify(a => a.PrintContactCards(
            It.Is<IEnumerable<ContactCard>>(b => b.First().FirstName == "the test contact")), Times.Once);
    }
}