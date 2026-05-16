using ExpandMicroservice.Domain.Entities;
using ExpandMicroservice.Domain.Repositories.Abstractions.Base;

namespace ExpandMicroservice.Domain.Repositories.Abstractions;

public interface IUserRepository : IRepository<User, Guid>
{
    Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken);
}