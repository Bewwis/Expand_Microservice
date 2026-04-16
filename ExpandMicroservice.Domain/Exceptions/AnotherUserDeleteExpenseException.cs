using ExpandMicroservice.Domain.Entities;

namespace ExpandMicroservice.Domain.Exceptions;

public class AnotherUserDeleteExpenseException(Expense expense, User user)
    : InvalidOperationException($"Пользователь {user.Username.Value} не может удалить расход с Id {expense.Id}, так как он принадлежит другому пользователю.")
{
    public Expense Expense => expense;
    public User User => user;
}