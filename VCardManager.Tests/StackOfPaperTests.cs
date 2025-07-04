using System.Text;
using VCardManager.Core;
using VCardManager.Tests._tools;

namespace VCardManager.Tests;

public class StackOfPaperTests
{
    [Fact]
    public void GetAllContacts()
    {
        var builder = new StringBuilder();
        builder.Append(new ContactCard("a", "a", "1", "1@1").ToVCardFormatString());
        builder.AppendLine();
        builder.Append(new ContactCard("b", "b", "2", "2@2").ToVCardFormatString());
        var fileStore = new FileStoreSpy { Text = builder.ToString() };
        var stackOfPaper = new StackOfPaper(fileStore);

        var contactCards = stackOfPaper.GetAllContactCards().ToList();

        Assert.Equal("./data/contacts.vcf", fileStore.LastPath);
        Assert.Equal(2, contactCards.Count);
        Assert.Equal("a", contactCards[0].FirstName);
        Assert.Equal("a", contactCards[0].LastName);
        Assert.Equal("1", contactCards[0].Phone);
        Assert.Equal("1@1", contactCards[0].Email);
        Assert.Equal("b", contactCards[1].FirstName);
        Assert.Equal("b", contactCards[1].LastName);
        Assert.Equal("2", contactCards[1].Phone);
        Assert.Equal("2@2", contactCards[1].Email);
    }

    [Fact]
    public void AddContact()
    {
        var fileStore = new FileStoreSpy();
        var stackOfPaper = new StackOfPaper(fileStore);
        var contact = new ContactCard("m", "m", "33", "@");

        stackOfPaper.Add(contact);

        Assert.Equal("./data/contacts.vcf", fileStore.LastPath);
        var reader = LinesReader.FromText(fileStore.Text);
        Assert.Equal("BEGIN:VCARD", reader.NextLine());
        Assert.Equal("FN:m m", reader.NextLine());
        Assert.Equal("TEL:33", reader.NextLine());
        Assert.Equal("EMAIL:@", reader.NextLine());
        Assert.Equal("END:VCARD", reader.NextLine());
        Assert.True(reader.EndOfContent());
    }

    [Fact]
    public void FindAllContactCards()
    {
        var builder = new StringBuilder();
        builder.Append(new ContactCard("a", "a", "1", "1@1").ToVCardFormatString());
        builder.AppendLine();
        builder.Append(new ContactCard("b", "b", "2", "2@2").ToVCardFormatString());
        var fileStore = new FileStoreSpy { Text = builder.ToString() };
        var stackOfPaper = new StackOfPaper(fileStore);

        var contactCards = stackOfPaper.FindAllContactCards("1@1").ToList();

        Assert.Equal("./data/contacts.vcf", fileStore.LastPath);
        Assert.Single(contactCards);
        Assert.Equal("a", contactCards[0].FirstName);
        Assert.Equal("a", contactCards[0].LastName);
        Assert.Equal("1", contactCards[0].Phone);
        Assert.Equal("1@1", contactCards[0].Email);
    }

    [Fact]
    public void DeleteAllThese()
    {
        var builder = new StringBuilder();
        builder.Append(new ContactCard("a", "a", "1", "1@1").ToVCardFormatString());
        builder.AppendLine();
        builder.Append(new ContactCard("b", "b", "2", "2@2").ToVCardFormatString());
        var fileStore = new FileStoreSpy { Text = builder.ToString() };
        var stackOfPaper = new StackOfPaper(fileStore);

        stackOfPaper.DeleteAllThese("2@2");

        Assert.Equal("./data/contacts.vcf", fileStore.LastPath);
        var reader = LinesReader.FromText(fileStore.Text);
        Assert.Equal("BEGIN:VCARD", reader.NextLine());
        Assert.Equal("FN:a a", reader.NextLine());
        Assert.Equal("TEL:1", reader.NextLine());
        Assert.Equal("EMAIL:1@1", reader.NextLine());
        Assert.Equal("END:VCARD", reader.NextLine());
        Assert.True(reader.EndOfContent());
    }
}