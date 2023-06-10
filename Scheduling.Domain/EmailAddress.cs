using System.Text.RegularExpressions;
using ErrorOr;
using Framework.Domain;

namespace Scheduling.Domain;

public sealed class EmailAddress : ValueObject
{
    public const int MaxLength = 128;

    public static Regex EmailRegex = new(
        @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
        RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.Compiled);

    private EmailAddress(string value)
    {

        Value = value.ToLower();
    }
    public static ErrorOr<EmailAddress> Create(string value)
    {
        value = Framework.Core.String.Fix(value);

        if (value is null)
            //return Errors.General.ValueIsRequired(nameof(EmailAddress));
            return Error.Validation(description: "null"); ;

        if (EmailRegex.IsMatch(value) == false)
            return Error.Validation(description: "not valid");

        return new EmailAddress(value);
    }

    public string Value { get; init; }

    public override string ToString()
    {
        return Value;
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}