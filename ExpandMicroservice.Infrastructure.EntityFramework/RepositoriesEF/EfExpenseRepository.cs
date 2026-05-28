using Microsoft.EntityFrameworkCore;
using ExpandMicroservice.Domain.Entities;
using ExpandMicroservice.Domain.Repositories.Abstractions;

namespace ExpandMicroservice.Infrastructure.EntityFramework.RepositoriesEF;

public class EfExpenseRepository : EfRepository<Expense, Guid>, IExpenseRepository
{
    public EfExpenseRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Expense>> GetUserExpensesAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Expenses
            .Where(e => e.User.Id == userId)
            .Include(e => e.Category)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Expense>> GetExpensesByCategoryAsync(Guid userId, Guid categoryId, CancellationToken cancellationToken)
    {
        return await _context.Expenses
            .Where(e => e.User.Id == userId && e.Category.Id == categoryId)
            .Include(e => e.Category)
            .ToListAsync(cancellationToken);
    }

    public async Task<decimal> GetTotalExpensesByDateRangeAsync(Guid userId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
        return await _context.Expenses
            .Where(e => e.User.Id == userId && e.CreationDate >= startDate && e.CreationDate <= endDate)
            .SumAsync(e => e.Amount.Value, cancellationToken);
    }
}