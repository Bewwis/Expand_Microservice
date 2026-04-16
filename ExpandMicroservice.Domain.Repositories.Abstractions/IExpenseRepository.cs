using ExpandMicroservice.Domain.Entities;
using ExpandMicroservice.Domain.Repositories.Abstractions.Base;

namespace ExpandMicroservice.Domain.Repositories.Abstractions;

public interface IExpenseRepository : IRepository<Expense, Guid>
{
    Task<IEnumerable<Expense>> GetUserExpensesAsync(Guid userId, CancellationToken cancellationToken);
    Task<IEnumerable<Expense>> GetExpensesByCategoryAsync(Guid userId, Guid categoryId, CancellationToken cancellationToken);
    Task<decimal> GetTotalExpensesByDateRangeAsync(Guid userId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
}