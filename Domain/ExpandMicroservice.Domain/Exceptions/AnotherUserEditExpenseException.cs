using ExpandMicroservice.Domain.Entities;

namespace ExpandMicroservice.Domain.Exceptions;

public class AnotherUserEditExpenseException(Expense expense, User user)
    : InvalidOperationException($"Пользователь {user.Username.Value} не может редактировать расход с Id {expense.Id}, так как он принадлежит другому пользователю.")
{
    public Expense Expense => expense;
    public User User => user;
}