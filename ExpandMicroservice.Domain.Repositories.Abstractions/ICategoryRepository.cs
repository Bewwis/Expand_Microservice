using ExpandMicroservice.Domain.Entities;
using ExpandMicroservice.Domain.Repositories.Abstractions.Base;

namespace ExpandMicroservice.Domain.Repositories.Abstractions;

public interface ICategoryRepository : IRepository<Category, Guid>
{
    Task<Category?> GetCategoryByNameAsync(string name, CancellationToken cancellationToken);
    Task<IEnumerable<Category>> GetUserCategoriesAsync(Guid userId, CancellationToken cancellationToken);
}