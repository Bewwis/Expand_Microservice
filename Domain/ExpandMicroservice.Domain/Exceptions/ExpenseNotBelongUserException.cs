using ExpandMicroservice.Domain.Entities;

namespace ExpandMicroservice.Domain.Exceptions;

public class ExpenseNotBelongUserException(Expense expense, User user)
    : InvalidOperationException($"Расход с Id {expense.Id} не принадлежит пользователю {user.Username.Value}.")
{
    public Expense Expense => expense;
    public User User => user;
}