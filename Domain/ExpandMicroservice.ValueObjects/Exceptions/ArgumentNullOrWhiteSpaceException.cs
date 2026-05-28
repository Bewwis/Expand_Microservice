namespace ExpandMicroservice.ValueObjects.Exceptions;

public class ArgumentNullOrWhiteSpaceException(string paramName)
    : ArgumentNullException(paramName, $"Параметр \"{paramName}\" не может быть null, пустым или состоять только из пробелов.");