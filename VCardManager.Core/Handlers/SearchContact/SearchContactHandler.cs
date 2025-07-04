using VCardManager.Core.Abstractions;

namespace VCardManager.Core.Handlers.SearchContact;

public class SearchContactHandler : IHandler<SearchContactCommand>
{
    private readonly IAmAStackOfPaper stackOfPaper;
    private readonly IAmInquisitive inquisitor;
    private readonly IAmAPrinter printer;

    public SearchContactHandler(IAmAStackOfPaper stackOfPaper, IAmInquisitive inquisitor, IAmAPrinter printer)
    {
        this.stackOfPaper = stackOfPaper;
        this.inquisitor = inquisitor;
        this.printer = printer;
    }

    public void ExecuteCommand(SearchContactCommand command)
    {
        printer.PrintContactCards(stackOfPaper.FindAllContactCards(inquisitor.GetSearchString()));
    }
}