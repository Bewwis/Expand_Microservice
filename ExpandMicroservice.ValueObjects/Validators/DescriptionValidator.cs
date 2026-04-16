using ExpandMicroservice.ValueObjects.Base;
using ExpandMicroservice.ValueObjects.Exceptions;

namespace ExpandMicroservice.ValueObjects.Validators;

public class DescriptionValidator : IValidator<string?>
{
    public static int MaxLength => 300;

    public void Validate(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return;

        if (value.Length > MaxLength)
            throw new ArgumentLongValueException(nameof(value), value, MaxLength);
    }
}