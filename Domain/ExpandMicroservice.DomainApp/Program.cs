using Microsoft.EntityFrameworkCore;
using ExpandMicroservice.Domain.Entities;
using ExpandMicroservice.ValueObjects;
using ExpandMicroservice.Infrastructure.EntityFramework;

namespace ExpandMicroservice.DomainApp;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Трекер расходов ===\n");

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=ExpandMicroserviceDB;Username=postgres;Password=123");

        using (var context = new ApplicationDbContext(optionsBuilder.Options))
        {
            var username = new Username("Антон");
            var user = new User(username);
            context.Users.Add(user);
            context.SaveChanges();
            Console.WriteLine($"Пользователь: {user.Username.Value}");

            var foodCategory = new Category(new CategoryName("Еда"));
            var transportCategory = new Category(new CategoryName("Транспорт"));
            context.Categories.AddRange(foodCategory, transportCategory);
            context.SaveChanges();
            Console.WriteLine($"Созданы категории: {foodCategory.Name.Value}, {transportCategory.Name.Value}");

            var expense1 = new Expense(user, new Amount(350), foodCategory, new Description("Обед в столовой"));
            var expense2 = new Expense(user, new Amount(120), transportCategory, new Description("Метро"));
            var expense3 = new Expense(user, new Amount(500), foodCategory, new Description("Ужин"));

            context.Expenses.AddRange(expense1, expense2, expense3);
            context.SaveChanges();

            Console.WriteLine($"\nДобавлены расходы:");
            Console.WriteLine($"- {expense1.Description?.Value}: {expense1.Amount.Value} руб. ({expense1.Category.Name.Value})");
            Console.WriteLine($"- {expense2.Description?.Value}: {expense2.Amount.Value} руб. ({expense2.Category.Name.Value})");
            Console.WriteLine($"- {expense3.Description?.Value}: {expense3.Amount.Value} руб. ({expense3.Category.Name.Value})");

            var expenses = context.Expenses.Include(e => e.Category).ToList();
            var total = expenses.Sum(e => e.Amount.Value);
            var foodTotal = expenses
                .Where(e => e.Category.Name.Value == "Еда")
                .Sum(e => e.Amount.Value);

            Console.WriteLine($"\nОбщая сумма расходов: {total} руб.");
            Console.WriteLine($"Расходы на еду: {foodTotal} руб.");
        }

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}