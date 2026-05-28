using ExpandMicroservice.ValueObjects.Base;
using ExpandMicroservice.ValueObjects.Validators;

namespace ExpandMicroservice.ValueObjects;

public class Description(string? value) : ValueObject<string?>(new DescriptionValidator(), value);