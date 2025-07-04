using Moq;
using VCardManager.Core;
using VCardManager.Core.Handlers.AddContact;

namespace VCardManager.Tests;

public class AddContactHandlerTests
{
    private readonly Mock<IAmAStackOfPaper> stackOfPaper;
    private readonly Mock<IAmInquisitive> inquisitor;
    private readonly AddContactHandler handler;

    public AddContactHandlerTests()
    {
        stackOfPaper = new Mock<IAmAStackOfPaper>();
        inquisitor = new Mock<IAmInquisitive>();
        handler = new AddContactHandler(stackOfPaper.Object, inquisitor.Object);
    }

    [Fact]
    public void AddContact()
    {
        var contact = new ContactCard("the test contact", "m", "33", "@");
        inquisitor.Setup(a => a.GetContactInformation()).Returns(contact);

        handler.ExecuteCommand(new AddContactCommand());

        stackOfPaper.Verify(a => a.Add(contact), Times.Once);
    }
}