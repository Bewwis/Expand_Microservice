using ExpandMicroservice.Domain.Base;
using ExpandMicroservice.ValueObjects;
using ExpandMicroservice.Domain.Exceptions;

namespace ExpandMicroservice.Domain.Entities;

public class Category : Entity<Guid>
{
    public CategoryName Name { get; private set; }
    public User User { get; private set; }
    public Guid UserId { get; private set; }

    protected Category() : base() { }

    public Category(User user, CategoryName name) : base()
    {
        User = user ?? throw new ArgumentNullValueException(nameof(user));
        UserId = user.Id;
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
        