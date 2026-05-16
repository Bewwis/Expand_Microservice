using Microsoft.EntityFrameworkCore;
using ExpandMicroservice.Domain.Base;
using ExpandMicroservice.Domain.Repositories.Abstractions.Base;
using ExpandMicroservice.Infrastructure.EntityFramework;

namespace ExpandMicroservice.Infrastructure.EntityFramework.RepositoriesEF;

public class EfRepository<TEntity, TId> : IRepository<TEntity, TId>
    where TEntity : Entity<TId>
    where TId : struct, IEquatable<TId>
{
    protected readonly ApplicationDbContext _context;

    public EfRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
    {
        var query = _context.Set<TEntity>();
        return await (asNoTracking ? query.AsNoTracking() : query).ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<TEntity?> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(entity);
        await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken) > 0 ? entity : null;
    }

    public async Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(entity);
        _context.Set<TEntity>().Update(entity);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(entity);
        _context.Set<TEntity>().Remove(entity);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(TId id, CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        return entity != null && await DeleteAsync(entity, cancellationToken);
    }
}