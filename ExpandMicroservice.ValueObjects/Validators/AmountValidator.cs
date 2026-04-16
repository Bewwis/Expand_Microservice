using ExpandMicroservice.ValueObjects.Base;

namespace ExpandMicroservice.ValueObjects.Validators;

public class AmountValidator : IValidator<decimal>
{
    public static decimal MinAmount => 0;
    public static decimal MaxAmount => 1000000;

    public void Validate(decimal value)
    {
        if (value < MinAmount)
            throw new ArgumentOutOfRangeException(nameof(value), $"Сумма не может быть отрицательной. Указано: {value}");

        if (value > MaxAmount)
            throw new ArgumentOutOfRangeException(nameof(value), $"Сумма превышает максимально допустимую ({MaxAmount}). Указано: {value}");
    }
}