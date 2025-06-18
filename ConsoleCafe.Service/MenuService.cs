using ConsoleCafe.Data;
using ConsoleCafe.Data.Enums;

namespace ConsoleCafe.Service;

public interface IMenuService
{
    List<MenuItem> GetMenuItems();
}

public class MenuService : IMenuService
{
    public List<MenuItem> GetMenuItems() =>
    [
        new() { Name = "BBQ Chicken Toastie", Type = ItemType.Food, Price = 6.23m },
        new() { Name = "Ham and Cheese Toastie", Type = ItemType.Food, Price = 5.78m },
        new() { Name = "Chocolate Brownie", Type = ItemType.Food, Price = 3.50m },
        new() { Name = "Tea", Type = ItemType.Drink, Price = 3.65m },
        new() { Name = "Coffee", Type = ItemType.Drink, Price = 4.64m },
        new() { Name = "Water", Type = ItemType.Drink, Price = 0.00m }
    ];
}