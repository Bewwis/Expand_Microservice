using Microsoft.EntityFrameworkCore;
using ExpandMicroservice.Domain.Entities;
using ExpandMicroservice.Domain.Repositories.Abstractions;

namespace ExpandMicroservice.Infrastructure.EntityFramework.RepositoriesEF;

public class EfCategoryRepository : EfRepository<Category, Guid>, ICategoryRepository
{
    public EfCategoryRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Category?> GetCategoryByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _context.Categories
            .FirstOrDefaultAsync(c => c.Name.Value == name, cancellationToken);
    }

    public async Task<IEnumerable<Category>> GetUserCategoriesAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.Categories)
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

        return user?.Categories ?? new List<Category>();
    }
}