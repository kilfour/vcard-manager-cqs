using Moq;
using VCardManager.Core;
using VCardManager.Core.Handlers.DeleteContact;

namespace VCardManager.Tests;

public class DeleteContactHandlerTests
{
    private readonly Mock<IAmAStackOfPaper> stackOfPaper;
    private readonly Mock<IAmInquisitive> inquisitor;
    private readonly Mock<IAmAPrinter> printer;
    private readonly DeleteContactHandler handler;

    public DeleteContactHandlerTests()
    {
        stackOfPaper = new Mock<IAmAStackOfPaper>();
        inquisitor = new Mock<IAmInquisitive>();
        printer = new Mock<IAmAPrinter>();
        handler = new DeleteContactHandler(stackOfPaper.Object, inquisitor.Object, printer.Object);
    }

    [Fact]
    public void DeleteContact_Confirmed()
    {
        var contact = new ContactCard("the test contact", "m", "33", "@");
        inquisitor.Setup(a => a.GetSearchString()).Returns("33");
        inquisitor.Setup(a => a.Confirm()).Returns(true); // <= CONFIRMED
        stackOfPaper.Setup(a => a.FindAllContactCards("33")).Returns([contact]);

        handler.ExecuteCommand(new DeleteContactCommand());

        inquisitor.Verify(a => a.GetSearchString(), Times.Once);
        stackOfPaper.Verify(a => a.FindAllContactCards("33"), Times.Once);
        printer.Verify(a => a.PrintConfirmDeletion(), Times.Once);
        printer.Verify(a => a.PrintContactCards(
            It.Is<IEnumerable<ContactCard>>(b => b.First().FirstName == "the test contact")));
        inquisitor.Verify(a => a.Confirm(), Times.Once);
        stackOfPaper.Verify(a => a.DeleteAllThese("33"), Times.Once);
        printer.Verify(a => a.PrintCardsDeleted(), Times.Once);
    }
}