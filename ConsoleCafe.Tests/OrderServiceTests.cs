using ConsoleCafe.Data.Enums;
using ConsoleCafe.Service;

namespace ConsoleCafe.Tests;

public class OrderServiceTests
{
    private readonly IDiscountService _discountService = new DiscountService();
    private readonly IMenuService _menuService = new MenuService();

    [Fact]
    public void NoDiscount_WhenOnlyFoodOrdered()
    {
        var orderService = new OrderService(_discountService);
        var food = _menuService.GetMenuItems().First(i => i.Type == ItemType.Food);
        
        orderService.CreateOrderLine(food, 1);
        
        Assert.Equal(6.23m, orderService.GetSubtotal());
        Assert.Equal(0.00m, orderService.GetDiscount());
        Assert.Equal(6.23m, orderService.GetTotal());
    }
    
    [Fact]
    public void TenPercentDiscount_WhenFoodAndDrinkOrdered()
    {
        var orderService = new OrderService(_discountService);

        var food = _menuService.GetMenuItems().First(i => i.Name == "Ham and Cheese Toastie");
        var drink =  _menuService.GetMenuItems().First(i => i.Name == "Coffee");

        orderService.CreateOrderLine(food, 1);
        orderService.CreateOrderLine(drink, 1);

        Assert.Equal(10.42m, orderService.GetSubtotal());
        Assert.Equal(1.04m, orderService.GetDiscount());
        Assert.Equal(9.38m, orderService.GetTotal());
    }

    [Fact]
    public void NoDiscount_WhenFoodOrderedWithOnlyWater()
    {
        var orderService = new OrderService(_discountService);
        var food = _menuService.GetMenuItems().First(i => i.Name == "Chocolate Brownie");
        var drink = _menuService.GetMenuItems().First(i => i.Name == "Water");
        orderService.CreateOrderLine(food, 1);
        orderService.CreateOrderLine(drink, 1);
        
        Assert.Equal(3.50m, orderService.GetSubtotal());
        Assert.Equal(0m, orderService.GetDiscount());
        Assert.Equal(3.50m, orderService.GetTotal());
    }

    [Fact]
    public void TwentyPercentDiscount_WhenSubtotalIsTwentyOrMore()
    {
        var orderService = new OrderService(_discountService);
        var food = _menuService.GetMenuItems().First(i => i.Name == "Chocolate Brownie");
        orderService.CreateOrderLine(food, 6); // £21
        
        Assert.Equal(21m, orderService.GetSubtotal());
        Assert.Equal(4.2m, orderService.GetDiscount());
        Assert.Equal(16.8m, orderService.GetTotal());
    }
    
    [Fact]
    public void TwentyPercentDiscount_WhenBothTenAndTwentyPercentApply()
    {
        var orderService = new OrderService(_discountService);

        var food = _menuService.GetMenuItems().First(i => i.Name == "BBQ Chicken Toastie");
        var drink = _menuService.GetMenuItems().First(i => i.Name == "Tea");

        orderService.CreateOrderLine(food, 3);   // 6.23 
        orderService.CreateOrderLine(drink, 1);  // 3.65 

        Assert.Equal(22.34m, orderService.GetSubtotal());
        Assert.Equal(4.47m, orderService.GetDiscount()); 
        Assert.Equal(17.87m, orderService.GetTotal());
    }
    
    [Fact]
    public void DiscountCappedAt6Pounds()
    {
        var orderService = new OrderService(_discountService);

        var food = _menuService.GetMenuItems().First(i => i.Name == "BBQ Chicken Toastie");
        var drink = _menuService.GetMenuItems().First(i => i.Name == "Coffee");

        orderService.CreateOrderLine(food, 10);
        orderService.CreateOrderLine(drink, 10); 

        Assert.Equal(6.00m, orderService.GetDiscount());
    }
    
    
    
}