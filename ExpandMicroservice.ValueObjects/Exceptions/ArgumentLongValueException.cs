namespace ExpandMicroservice.ValueObjects.Exceptions;

public class ArgumentLongValueException(string paramName, string value, int maxLength)
    : FormatException($"Длина параметра \"{paramName}\" ({value.Length} символов) превышает максимально допустимую ({maxLength}).")
{
    public string Value => value;
    public int MaxLength => maxLength;
}