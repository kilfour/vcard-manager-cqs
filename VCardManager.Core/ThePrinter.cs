using VCardManager.Core.Abstractions;

namespace VCardManager.Core;

public interface IAmAPrinter
{
    void PrintContactCards(IEnumerable<ContactCard> cards);
    void PrintConfirmDeletion();
    void PrintCardsDeleted();
}

public class ThePrinter : IAmAPrinter
{
    private readonly IConsole console;

    public ThePrinter(IConsole console)
    {
        this.console = console;
    }

    public void PrintContactCards(IEnumerable<ContactCard> cards)
    {
        cards.Select(a => $"{a.FullName}: {a.Email}, {a.Phone}")
            .ToList()
            .ForEach(console.WriteLine);
    }

    public void PrintConfirmDeletion()
    {
        console.WriteLine("Do you want to delete these Contact Cards (y/n) ?");
    }

    public void PrintCardsDeleted()
    {
        console.WriteLine("The Contact Cards have been deleted.");
    }
}
