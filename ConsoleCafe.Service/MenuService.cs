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
        new() { Id = 1, Name = "BBQ Chicken Toastie", Type = ItemType.Food, Price = 6.23m },
        new() { Id = 2, Name = "Ham and Cheese Toastie", Type = ItemType.Food, Price = 5.78m },
        new() { Id = 3, Name = "Chocolate Brownie", Type = ItemType.Food, Price = 3.50m },
        new() { Id = 4, Name = "Tea", Type = ItemType.Drink, Price = 3.65m },
        new() { Id = 5, Name = "Coffee", Type = ItemType.Drink, Price = 4.64m },
        new() { Id = 6, Name = "Water", Type = ItemType.Drink, Price = 0.00m }
    ];
}