using ExpandMicroservice.Domain.Entities;
using ExpandMicroservice.ValueObjects;

namespace ExpandMicroservice.DomainApp;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Трекер расходов ===\n");

        // Создаём пользователя
        var username = new Username("Антон");
        var user = new User(username);
        Console.WriteLine($"Пользователь: {user.Username.Value}");

        // Создаём категории
        var foodCategory = user.CreateCategory(new CategoryName("Еда"));
        var transportCategory = user.CreateCategory(new CategoryName("Транспорт"));
        Console.WriteLine($"Созданы категории: {foodCategory.Name.Value}, {transportCategory.Name.Value}");

        // Добавляем расходы
        var expense1 = user.CreateExpense(new Amount(350), foodCategory, new Description("Обед в столовой"));
        var expense2 = user.CreateExpense(new Amount(120), transportCategory, new Description("Метро"));
        var expense3 = user.CreateExpense(new Amount(500), foodCategory, new Description("Ужин"));

        Console.WriteLine($"\nДобавлены расходы:");
        Console.WriteLine($"- {expense1.Description?.Value}: {expense1.Amount.Value} руб. ({expense1.Category.Name.Value})");
        Console.WriteLine($"- {expense2.Description?.Value}: {expense2.Amount.Value} руб. ({expense2.Category.Name.Value})");
        Console.WriteLine($"- {expense3.Description?.Value}: {expense3.Amount.Value} руб. ({expense3.Category.Name.Value})");

        // Считаем общую сумму расходов
        var total = user.Expenses.Sum(e => e.Amount.Value);
        Console.WriteLine($"\nОбщая сумма расходов: {total} руб.");

        // Считаем сумму по категории "Еда"
        var foodTotal = user.Expenses
            .Where(e => e.Category.Name.Value == "Еда")
            .Sum(e => e.Amount.Value);
        Console.WriteLine($"Расходы на еду: {foodTotal} руб.");

        // Редактируем расход
        var newAmount = new Amount(400);
        user.EditExpense(expense1, newAmount: newAmount);
        Console.WriteLine($"\nПосле редактирования: {expense1.Description?.Value} теперь {expense1.Amount.Value} руб.");

        // Удаляем расход
        user.DeleteExpense(expense2);
        Console.WriteLine($"\nПосле удаления: осталось расходов {user.Expenses.Count}");

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}