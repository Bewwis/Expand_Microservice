using ExpandMicroservice.ValueObjects.Base;
using ExpandMicroservice.ValueObjects.Validators;

namespace ExpandMicroservice.ValueObjects;

public class CategoryName(string value) : ValueObject<string>(new CategoryNameValidator(), value);