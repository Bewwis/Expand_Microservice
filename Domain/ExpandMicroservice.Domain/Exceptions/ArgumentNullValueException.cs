namespace ExpandMicroservice.Domain.Exceptions;

public class ArgumentNullValueException(string paramName)
    : ArgumentNullException(paramName, $"Аргумент \"{paramName}\" имеет значение null.");