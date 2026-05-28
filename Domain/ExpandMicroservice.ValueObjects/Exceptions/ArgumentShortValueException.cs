namespace ExpandMicroservice.ValueObjects.Exceptions;

public class ArgumentShortValueException(string paramName, string value, int minLength)
    : FormatException($"Длина параметра \"{paramName}\" ({value.Length} символов) меньше минимально допустимой ({minLength}).")
{
    public string Value => value;
    public int MinLength => minLength;
}