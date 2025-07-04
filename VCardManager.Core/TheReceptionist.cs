using Superpower;
using Superpower.Parsers;

// This one was just for fun, regex'll do
public interface IAmAReceptionist
{
    bool IsValidPhoneNumber(string number);
}

public class TheReceptionist : IAmAReceptionist
{
    public bool IsValidPhoneNumber(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            return false;
        var result = PhoneNumber.TryParse(number);
        return result.HasValue;
    }

    private static readonly TextParser<string> TwoDigits =
        from d1 in Character.Digit
        from d2 in Character.Digit
        select $"{d1}{d2}";

    private static readonly TextParser<string> ThreeDigits =
        from d1 in Character.Digit
        from d2 in Character.Digit
        from d3 in Character.Digit
        select $"{d1}{d2}{d3}";

    private static readonly TextParser<char> LeadingZero =
        Character.EqualTo('0').Try().OptionalOrDefault();
    private static readonly TextParser<char> Separator =
        Character.EqualTo('/').Or(Character.EqualTo('.')).Try().OptionalOrDefault();

    private static readonly TextParser<char> Dot =
        Character.EqualTo('.').Try().OptionalOrDefault();

    private static readonly TextParser<string> PhoneNumber =
        from leadingZero in LeadingZero
        from prefix in ThreeDigits
        from maybeSlash in Separator
        from group1 in TwoDigits
        from dot1 in Dot
        from group2 in TwoDigits
        from dot2 in Dot
        from group3 in TwoDigits
        select string.Concat(
            leadingZero,
            prefix,
            maybeSlash,
            group1,
            dot1,
            group2,
            dot2,
            group3
        );
}
