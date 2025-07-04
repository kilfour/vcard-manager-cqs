
namespace VCardManager.Tests;

public class TheRectionistTests
{
    private readonly IAmAReceptionist _receptionist = new TheReceptionist();

    [Theory]
    [InlineData("0487112233", true)]
    [InlineData("487112233", true)]
    [InlineData("0487/11.22.33", true)]
    [InlineData("487/11.22.33", true)]
    [InlineData("0487.11.22.33", true)]
    [InlineData("487.11.22.33", true)]
    [InlineData("0487-11-22-33", false)]
    [InlineData("0487/112233", true)]
    [InlineData("123", false)]
    [InlineData("", false)]
    public void IsValidPhoneNumber_ValidatesCorrectly(string input, bool expected)
    {
        var result = _receptionist.IsValidPhoneNumber(input);
        Assert.Equal(expected, result);
    }
}