using VCardManager.Core;
using VCardManager.Tests._tools;

namespace VCardManager.Tests;

public class ThePrinterTests
{
    [Fact]
    public void PrintContactCards()
    {
        var console = new ConsoleSpy();
        var printer = new ThePrinter(console);
        List<ContactCard> cards = [new ContactCard("a", "a", "1", "1@1"), new ContactCard("b", "b", "2", "2@2")];
        printer.PrintContactCards(cards);

        var reader = LinesReader.FromStringList([.. console.Output]);
        Assert.Equal("a a: 1@1, 1", reader.NextLine());
        Assert.Equal("b b: 2@2, 2", reader.NextLine());
    }

    [Fact]
    public void PrintConfirmDeletion()
    {
        var console = new ConsoleSpy();
        var printer = new ThePrinter(console);
        printer.PrintConfirmDeletion();
        Assert.Equal("Do you want to delete these Contact Cards (y/n) ?", console.Output.Single());
    }

    [Fact]
    public void PrintCardsDeleted()
    {
        var console = new ConsoleSpy();
        var printer = new ThePrinter(console);
        printer.PrintCardsDeleted();
        Assert.Equal("The Contact Cards have been deleted.", console.Output.Single());
    }
}