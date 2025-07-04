using System.Text;

namespace VCardManager.Core;

public class ContactCard
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Phone { get; init; }
    public string Email { get; init; }

    public ContactCard(string firstName, string lastName, string phone, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
    }

    public string FullName => $"{FirstName} {LastName}";

    public string ToVCardFormatString()
    {
        var builder = new StringBuilder();
        builder.AppendLine($"BEGIN:VCARD");
        builder.AppendLine($"FN:{FullName}");
        builder.AppendLine($"TEL:{Phone}");
        builder.AppendLine($"EMAIL:{Email}");
        builder.Append($"END:VCARD");
        return builder.ToString();
    }

    public static ContactCard FromVCardFormatString(string vCardFormatString)
    {
        var lines = vCardFormatString.Split(Environment.NewLine);
        var names = GetNames(lines[1].Replace("FN:", ""));
        var firstName = names.FirstName;
        var lastName = names.LastName;
        var phone = lines[2].Replace("TEL:", "");
        var email = lines[3].Replace("EMAIL:", "");
        return new ContactCard(firstName, lastName, phone, email);
    }

    public bool ContainsThisString(string searchString)
    {
        string[] toSearch = [FullName, Phone, Email];
        return toSearch.Any(a => a.Contains(searchString, StringComparison.OrdinalIgnoreCase));
    }

    private static (string FirstName, string LastName) GetNames(string fullName)
    {
        var namesList = fullName.Split(" ");
        return (namesList[0], namesList.Length > 1 ? namesList[0] : string.Empty);
    }
}