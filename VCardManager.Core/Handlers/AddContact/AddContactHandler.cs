using VCardManager.Core.Abstractions;

namespace VCardManager.Core.Handlers.AddContact;

public class AddContactHandler : IHandler<AddContactCommand>
{
    private readonly IAmAStackOfPaper stackOfPaper;
    private readonly IAmInquisitive inquisitor;

    public AddContactHandler(IAmAStackOfPaper stackOfPaper, IAmInquisitive inquisitor)
    {
        this.stackOfPaper = stackOfPaper;
        this.inquisitor = inquisitor;
    }

    public void ExecuteCommand(AddContactCommand command)
    {
        stackOfPaper.Add(inquisitor.GetContactInformation());
    }
}