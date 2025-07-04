using System.Text;
using VCardManager.Core;
using VCardManager.Tests._tools;

namespace VCardManager.Tests;

public class ContactCardTests
{
    [Fact]
    public void ContactCard_ToVCardFormatString()
    {
        var contactCard = new ContactCard("Mark", "Meyers", "0487221133", "mark@meyers.com");
        var result = contactCard.ToVCardFormatString();
        var reader = LinesReader.FromText(result);
        Assert.Equal("BEGIN:VCARD", reader.NextLine());
        Assert.Equal("FN:Mark Meyers", reader.NextLine());
        Assert.Equal("TEL:0487221133", reader.NextLine());
        Assert.Equal("EMAIL:mark@meyers.com", reader.NextLine());
        Assert.Equal("END:VCARD", reader.NextLine());
        Assert.True(reader.EndOfContent());
    }

    [Fact]
    public void ContactCard_FromVCardFormatString()
    {
        var builder = new StringBuilder();
        builder.AppendLine($"BEGIN:VCARD");
        builder.AppendLine($"FN:Mark Meyers");
        builder.AppendLine($"TEL:0487221133");
        builder.AppendLine($"EMAIL:mark@meyers.com");
        builder.Append($"END:VCARD");
        var vCardFormatString = builder.ToString();
        var contactCard = ContactCard.FromVCardFormatString(vCardFormatString);
        Assert.Equal("Mark", contactCard.FirstName);
    }

    [Fact]
    public void ContactCard_ContainsThisString_Nope()
    {
        var contactCard = new ContactCard("", "", "", "");
        Assert.False(contactCard.ContainsThisString("str"));
    }

    [Fact]
    public void ContactCard_ContainsThisString_FirstName()
    {
        var contactCard = new ContactCard("THE_STRING", "", "", "");
        Assert.True(contactCard.ContainsThisString("str"));
    }

    [Fact]
    public void ContactCard_ContainsThisString_LastName()
    {
        var contactCard = new ContactCard("", "THE_STRING", "", "");
        Assert.True(contactCard.ContainsThisString("str"));
    }

    [Fact]
    public void ContactCard_ContainsThisString_Phone()
    {
        var contactCard = new ContactCard("", "", "THE_STRING", "");
        Assert.True(contactCard.ContainsThisString("str"));
    }

    [Fact]
    public void ContactCard_ContainsThisString_Email()
    {
        var contactCard = new ContactCard("", "", "", "THE_STRING");
        Assert.True(contactCard.ContainsThisString("str"));
    }
}