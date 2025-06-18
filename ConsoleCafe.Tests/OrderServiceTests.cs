using ConsoleCafe.Data.Enums;
using ConsoleCafe.Service;

namespace ConsoleCafe.Tests;

public class OrderServiceTests
{
    private MenuService _menuService;

    public OrderServiceTests()
    {
        _menuService = new MenuService();
    }

    [Fact]
    public void Discount10PercentWithFoodAndDrinkOrder()
    {
        var orderService = new OrderService();
        var food = _menuService.GetMenuItems().First(i => i.Type == ItemType.Food);
        var drink = _menuService.GetMenuItems().First(i => i.Type == ItemType.Drink && i.Name != "Water");
        orderService.CreateOrder(food, 1);
        orderService.CreateOrder(drink, 1);
        var discount = orderService.GetOrderDiscount();
        Assert.Equal(orderService.GetOrderSubTotal() * 0.10m, discount);
    }

    [Fact]
    public void Discount20PercentIfOver20()
    {
        var orderService = new OrderService();
        var item = _menuService.GetMenuItems().First(i => i.Name != "Coffee");
        orderService.CreateOrder(item, 5);
        var discount = orderService.GetOrderDiscount();
        Assert.Equal(orderService.GetOrderSubTotal() * 0.20m, discount);
    }
    
    [Fact]
    public void NoDiscountIfOnlyWaterAsDrink()
    {
        var orderService = new OrderService();
        var food = _menuService.GetMenuItems().First(i => i.Type == ItemType.Food);
        var drink = _menuService.GetMenuItems().First(i => i.Name == "Water");
        orderService.CreateOrder(food, 1);
        orderService.CreateOrder(drink, 1);
        var discount = orderService.GetOrderDiscount();
        Assert.Equal(0.0m, discount);
    }
    
    
    
}