using ExpandMicroservice.Domain.Entities;
using ExpandMicroservice.ValueObjects;

namespace ExpandMicroservice.Domain.Exceptions;

public class DuplicateCategoryException(CategoryName name, User user)
    : InvalidOperationException($"Категория с именем \"{name.Value}\" уже существует у пользователя {user.Username.Value}.")
{
    public CategoryName Name => name;
    public User User => user;
}