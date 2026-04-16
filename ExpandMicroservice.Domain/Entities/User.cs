using ExpandMicroservice.Domain.Base;
using ExpandMicroservice.ValueObjects;
using ExpandMicroservice.Domain.Exceptions;

namespace ExpandMicroservice.Domain.Entities;

public class User : Entity<Guid>
{
    private readonly List<Expense> _expenses = new();
    private readonly List<Category> _categories = new();

    public Username Username { get; private set; }
    public IReadOnlyCollection<Expense> Expenses => _expenses.AsReadOnly();
    public IReadOnlyCollection<Category> Categories => _categories.AsReadOnly();

    protected User() : base(Guid.NewGuid()) { }

    public User(Username username) : base(Guid.NewGuid())
    {
        Username = username ?? throw new ArgumentNullValueException(nameof(username));
    }

    public bool ChangeUsername(Username newUsername)
    {
        if (newUsername == null) throw new ArgumentNullValueException(nameof(newUsername));
        if (Username.Equals(newUsername)) return false;

        Username = newUsername;
        return true;
    }

    public Category CreateCategory(CategoryName name)
    {
        var category = new Category(name);
        _categories.Add(category);
        return category;
    }

    public bool DeleteCategory(Category category)
    {
        if (!_categories.Contains(category)) return false;
        return _categories.Remove(category);
    }

    public Expense CreateExpense(Amount amount, Category category, Description? description = null)
    {
        if (category == null) throw new ArgumentNullValueException(nameof(category));
        if (!_categories.Contains(category))
            throw new InvalidOperationException("Категория не принадлежит этому пользователю.");

        var expense = new Expense(this, amount, category, description);
        _expenses.Add(expense);
        return expense;
    }

    public bool EditExpense(Expense expense, Amount? newAmount = null, Category? newCategory = null, Description? newDescription = null)
    {
        if (expense.User != this) throw new AnotherUserEditExpenseException(expense, this);
        if (!_expenses.Contains(expense)) throw new ExpenseNotBelongUserException(expense, this);

        var isEdited = false;

        if (newAmount != null && expense.ChangeAmount(newAmount))
            isEdited = true;

        if (newCategory != null && expense.ChangeCategory(newCategory))
            isEdited = true;

        if (newDescription != null && expense.ChangeDescription(newDescription))
            isEdited = true;

        if (isEdited)
            expense.SetModificationDate(DateTime.UtcNow);

        return isEdited;
    }

    public bool DeleteExpense(Expense expense)
    {
        if (expense.User != this) throw new AnotherUserDeleteExpenseException(expense, this);
        if (!_expenses.Contains(expense)) throw new ExpenseNotBelongUserException(expense, this);

        return _expenses.Remove(expense);
    }
}