using Microsoft.EntityFrameworkCore;
using ExpandMicroservice.Domain.Entities;
using ExpandMicroservice.Domain.Repositories.Abstractions;
using ExpandMicroservice.ValueObjects;

namespace ExpandMicroservice.Infrastructure.EntityFramework.RepositoriesEF;

public class EfUserRepository : EfRepository<User, Guid>, IUserRepository
{
    public EfUserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include("_expenses")
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include("_expenses")
            .FirstOrDefaultAsync(u => u.Username == new Username(username), cancellationToken);
    }
}