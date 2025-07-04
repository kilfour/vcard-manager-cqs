using VCardManager.Core.Abstractions;

namespace VCardManager.Core.Handlers.DeleteContact;

public class DeleteContactHandler : IHandler<DeleteContactCommand>
{
    private readonly IAmAStackOfPaper stackOfPaper;
    private readonly IAmInquisitive inquisitor;
    private readonly IAmAPrinter printer;

    public DeleteContactHandler(IAmAStackOfPaper stackOfPaper, IAmInquisitive inquisitor, IAmAPrinter printer)
    {
        this.stackOfPaper = stackOfPaper;
        this.inquisitor = inquisitor;
        this.printer = printer;
    }

    public void ExecuteCommand(DeleteContactCommand command)
    {
        var searchString = inquisitor.GetSearchString();
        var cards = stackOfPaper.FindAllContactCards(searchString);
        printer.PrintConfirmDeletion();
        printer.PrintContactCards(cards);
        if (inquisitor.Confirm())
        {
            stackOfPaper.DeleteAllThese(searchString);
            printer.PrintCardsDeleted();
        }
    }
}