using ExpandMicroservice.ValueObjects.Base;
using ExpandMicroservice.ValueObjects.Validators;

namespace ExpandMicroservice.ValueObjects;

public class Amount(decimal value) : ValueObject<decimal>(new AmountValidator(), value);