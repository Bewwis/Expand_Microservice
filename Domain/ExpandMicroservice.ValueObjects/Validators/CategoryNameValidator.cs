using ExpandMicroservice.ValueObjects.Base;
using ExpandMicroservice.ValueObjects.Exceptions;

namespace ExpandMicroservice.ValueObjects.Validators;

public class CategoryNameValidator : IValidator<string>
{
    public static int MinLength => 2;
    public static int MaxLength => 30;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));

        if (value.Length > MaxLength)
            throw new ArgumentLongValueException(nameof(value), value, MaxLength);

        if (value.Length < MinLength)
            throw new ArgumentShortValueException(nameof(value), value, MinLength);
    }
}