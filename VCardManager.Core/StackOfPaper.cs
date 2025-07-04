using VCardManager.Core.Abstractions;

namespace VCardManager.Core;

public interface IAmAStackOfPaper
{
    IEnumerable<ContactCard> GetAllContactCards();
    void Add(ContactCard contact);
    IEnumerable<ContactCard> FindAllContactCards(string searchString);
    void DeleteAllThese(string searchString);
}

public class StackOfPaper : IAmAStackOfPaper
{
    private const string FilePath = "./data/contacts.vcf";
    private readonly IFileStore fileStore;

    public StackOfPaper(IFileStore fileStore)
    {
        this.fileStore = fileStore;
    }

    public IEnumerable<ContactCard> GetAllContactCards()
    {
        var input = fileStore.ReadAllText(FilePath);
        var vCardStrings = input.Split("BEGIN:VCARD").Where(a => !string.IsNullOrWhiteSpace(a));
        return vCardStrings.Select(ContactCard.FromVCardFormatString);
    }

    public void Add(ContactCard contact)
    {
        fileStore.AppendAllText(FilePath, contact.ToVCardFormatString());
    }

    public IEnumerable<ContactCard> FindAllContactCards(string searchString)
    {
        return GetAllContactCards().Where(a => a.ContainsThisString(searchString));
    }

    public void DeleteAllThese(string searchString)
    {
        var cards = GetAllContactCards().Where(a => !a.ContainsThisString(searchString));
        var result = string.Join(Environment.NewLine, cards.Select(a => a.ToVCardFormatString()));
        fileStore.WriteAllText(FilePath, result);
    }
}



