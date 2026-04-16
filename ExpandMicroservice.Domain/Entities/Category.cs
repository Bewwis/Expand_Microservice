using ExpandMicroservice.Domain.Base;
using ExpandMicroservice.ValueObjects;
using ExpandMicroservice.Domain.Exceptions;

namespace ExpandMicroservice.Domain.Entities;

public class Category : Entity<Guid>
{
    public CategoryName Name { get; private set; }

    protected Category() : base(Guid.NewGuid()) { }

    public Category(CategoryName name) : base(Guid.NewGuid())
    {
        Name = name ?? throw new ArgumentNullValueException(nameof(name));
    }

    public bool ChangeName(CategoryName newName)
    {
        if (newName == null) throw new ArgumentNullValueException(nameof(newName));
        if (Name.Equals(newName)) return false;

        Name = newName;
        return true;
    }
}