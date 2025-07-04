
using VCardManager.Core.Abstractions;

namespace VCardManager.Core;

public interface IAmInquisitive
{
    bool Confirm();
    ContactCard GetContactInformation();
    string GetSearchString();
}

public class Inquisitor : IAmInquisitive
{
    private readonly IConsole console;
    private readonly IAmAReceptionist receptionist;

    public Inquisitor(IConsole console, IAmAReceptionist receptionist)
    {
        this.console = console;
        this.receptionist = receptionist;
    }

    public bool Confirm()
    {
        if (console.ReadLine().Equals("y", StringComparison.OrdinalIgnoreCase))
            return true;
        return false;
    }

    private string GetAnswerFor(string prompt)
    {
        console.Write(prompt);
        return console.ReadLine();
    }

    private string GetValidPhone()
    {
        while (true)
        {
            var phone = GetAnswerFor("Phone: ");
            if (receptionist.IsValidPhoneNumber(phone))
                return phone;
        }
    }

    public ContactCard GetContactInformation()
    {
        var firstName = GetAnswerFor("First name: ");
        var lastName = GetAnswerFor("Last name: ");
        var phone = GetValidPhone();
        var email = GetAnswerFor("Email: ");
        return new ContactCard(firstName, lastName, phone, email);
    }

    public string GetSearchString()
    {
        return GetAnswerFor("Enter a search string: ");
    }
}
