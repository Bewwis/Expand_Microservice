using ExpandMicroservice.Domain.Base;
using ExpandMicroservice.ValueObjects;
using ExpandMicroservice.Domain.Exceptions;

namespace ExpandMicroservice.Domain.Entities;

public class Expense : Entity<Guid>
{
    public Amount Amount { get; private set; }
    public Category Category { get; private set; }
    public Description? Description { get; private set; }
    public DateTime CreationDate { get; }
    public DateTime? ModificationDate { get; private set; }
    public User User { get; set; }
    public Guid UserId { get; private set; }

    protected Expense() : base(Guid.NewGuid()) { }

    public Expense(User user, Amount amount, Category category, Description? description = null)
        : base(Guid.NewGuid())
    {
        User = user ?? throw new ArgumentNullValueException(nameof(user));
        UserId = user.Id;
        Amount = amount ?? throw new ArgumentNullValueException(nameof(amount));
        Category = category ?? throw new ArgumentNullValueException(nameof(category));
        Description = description;
        CreationDate = DateTime.UtcNow;
        ModificationDate = null;
    }

    public bool ChangeAmount(Amount newAmount)
    {
        if (newAmount == null) throw new ArgumentNullValueException(nameof(newAmount));
        if (Amount.Equals(newAmount)) return false;

        Amount = newAmount;
        return true;
    }

    public bool ChangeCategory(Category newCategory)
    {
        if (newCategory == null) throw new ArgumentNullValueException(nameof(newCategory));
        if (Category.Equals(newCategory)) return false;

        Category = newCategory;
        return true;
    }

    public bool ChangeDescription(Description? newDescription)
    {
        if (Equals(Description, newDescription)) return false;

        Description = newDescription;
        return true;
    }

    public void SetModificationDate(DateTime modificationDate)
    {
        if (modificationDate < CreationDate)
            throw new InvalidOperationException("Дата модификации не может быть раньше даты создания.");
        ModificationDate = modificationDate;
    }
}