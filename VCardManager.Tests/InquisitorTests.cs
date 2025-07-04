using Moq;
using VCardManager.Core;
using VCardManager.Tests._tools;

namespace VCardManager.Tests;

public class InquisitorTests
{
    private readonly ConsoleSpy console;
    private readonly Mock<IAmAReceptionist> receptionist;
    private readonly IAmInquisitive inquisitor;

    public InquisitorTests()
    {
        console = new ConsoleSpy();
        receptionist = new Mock<IAmAReceptionist>();
        inquisitor = new Inquisitor(console, receptionist.Object);
    }

    [Fact]
    public void GetContactInformation_AsksForContactInformation()
    {
        console.AddInput("");
        console.AddInput("");
        console.AddInput("");
        console.AddInput("");
        receptionist.Setup(a => a.IsValidPhoneNumber(It.IsAny<string>())).Returns(true);

        inquisitor.GetContactInformation();

        Assert.Equal("First name: ", console.Output[0]);
        Assert.Equal("Last name: ", console.Output[1]);
        Assert.Equal("Phone: ", console.Output[2]);
        Assert.Equal("Email: ", console.Output[3]);
    }

    [Fact]
    public void GetContactInformation_ChecksWithTheReceptionistForValidPhoneNumber()
    {
        console.AddInput("");
        console.AddInput("");
        console.AddInput("phone");
        console.AddInput("");
        receptionist.Setup(a => a.IsValidPhoneNumber(It.IsAny<string>())).Returns(true);

        inquisitor.GetContactInformation();

        receptionist.Verify(a => a.IsValidPhoneNumber("phone"), Times.Once);
    }

    [Fact]
    public void GetContactInformation_ChecksWithTheReceptionistForValidPhoneNumber_AndAsksAgainIfInvalid()
    {
        console.AddInput("");
        console.AddInput("");
        console.AddInput("phone");
        console.AddInput("better");
        console.AddInput("");
        receptionist.Setup(a => a.IsValidPhoneNumber("phone")).Returns(false);
        receptionist.Setup(a => a.IsValidPhoneNumber("better")).Returns(true);

        inquisitor.GetContactInformation();

        receptionist.Verify(a => a.IsValidPhoneNumber("phone"), Times.Once);
        receptionist.Verify(a => a.IsValidPhoneNumber("better"), Times.Once);
    }

    [Fact]
    public void GetContactInformation_BuildsContact()
    {
        console.AddInput("Mark");
        console.AddInput("Meyers");
        console.AddInput("487/11.22.33");
        console.AddInput("mark@meyers.com");
        receptionist.Setup(a => a.IsValidPhoneNumber(It.IsAny<string>())).Returns(true);

        var contactCard = inquisitor.GetContactInformation();

        Assert.Equal("Mark", contactCard.FirstName);
        Assert.Equal("Meyers", contactCard.LastName);
        Assert.Equal("487/11.22.33", contactCard.Phone);
        Assert.Equal("mark@meyers.com", contactCard.Email);
    }


    [Fact]
    public void GetSearchString()
    {
        console.AddInput("Mark");

        var searchString = inquisitor.GetSearchString();

        Assert.Equal("Enter a search string: ", console.Output[0]);
        Assert.Equal("Mark", searchString);
    }

    [Fact]
    public void Confirm_Yes()
    {
        console.AddInput("Y");
        var flag = inquisitor.Confirm();
        Assert.True(flag);
    }

    [Fact]
    public void Confirm_Yes_Lower()
    {
        console.AddInput("y");
        var flag = inquisitor.Confirm();
        Assert.True(flag);
    }

    [Fact]
    public void Confirm_Other_Is_No()
    {
        console.AddInput("");
        var flag = inquisitor.Confirm();
        Assert.False(flag);
    }
}
