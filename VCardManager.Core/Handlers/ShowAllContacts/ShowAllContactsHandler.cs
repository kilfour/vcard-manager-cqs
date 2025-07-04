using VCardManager.Core.Abstractions;

namespace VCardManager.Core.Handlers.ShowAllContacts;

public class ShowAllContactsHandler : IHandler<ShowAllContactsCommand>
{
    private readonly IAmAStackOfPaper stackOfPaper;
    private readonly IAmAPrinter printer;

    public ShowAllContactsHandler(IAmAStackOfPaper stackOfPaper, IAmAPrinter printer)
    {
        this.stackOfPaper = stackOfPaper;
        this.printer = printer;
    }

    public void ExecuteCommand(ShowAllContactsCommand command)
    {
        printer.PrintContactCards(stackOfPaper.GetAllContactCards());
    }
}