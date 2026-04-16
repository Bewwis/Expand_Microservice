using ExpandMicroservice.ValueObjects.Base;
using ExpandMicroservice.ValueObjects.Validators;

namespace ExpandMicroservice.ValueObjects;

public class Username(string value) : ValueObject<string>(new UsernameValidator(), value);