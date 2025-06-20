using ConsoleCafe.Data;
using ConsoleCafe.Data.Enums;
using ConsoleCafe.Service;

namespace ConsoleCafe.Tests;

public class DiscountRulesServiceTests
{
    private readonly DiscountRulesService _discountRulesService = new();

    [Fact]
    public void MaxDiscount_Returns6()
    {
        Assert.Equal(6.00m, _discountRulesService.MaximumDiscount());
    }
    
    [Fact]
    public void EligibleForTenPercentDiscount_WhenFoodAndDrinkOrdered_ReturnsTrue()
    {
        var order = new Order();
        var food = new MenuItem { Name = "Ham Toastie", Price = 5m, Type = ItemType.Food };
        var drink = new MenuItem { Name = "Coffee", Price = 2m, Type = ItemType.Drink };

        order.OrderLines.Add(new OrderLine { Item = food, Quantity = 1 });
        order.OrderLines.Add(new OrderLine { Item = drink, Quantity = 1 });

        Assert.True(_discountRulesService.EligibleForTenPercentDiscount(order));
    }
    
    [Fact]
    public void EligibleForTenPercentDiscount_WhenFoodAndWaterOrdered_ReturnsFalse()
    {
        var order = new Order();
        var food = new MenuItem { Name = "Ham Toastie", Price = 5m, Type = ItemType.Food };
        var drink = new MenuItem { Name = "Water", Price = 0m, Type = ItemType.Drink };

        order.OrderLines.Add(new OrderLine { Item = food, Quantity = 1 });
        order.OrderLines.Add(new OrderLine { Item = drink, Quantity = 1 });

        Assert.False(_discountRulesService.EligibleForTenPercentDiscount(order));
    }
    
    [Fact]
    public void EligibleForTwentyPercentDiscount_WhenSubtotalGreaterThan20_ReturnsTrue()
    {
        var order = new Order();
        var expensiveFood = new MenuItem { Name = "Lobster", Price = 25.00m, Type = ItemType.Food };
        order.OrderLines.Add(new OrderLine { Item = expensiveFood, Quantity = 1 });

        Assert.True(_discountRulesService.EligibleForTwentyPercentDiscount(order));
    }

    [Fact]
    public void EligibleForTwentyPercentDiscount_WhenSubtotalLessThan20_ReturnsFalse()
    {
        var order = new Order();
        var cheapFood = new MenuItem { Name = "Small Toastie", Price = 2.00m, Type = ItemType.Food };
        order.OrderLines.Add(new OrderLine { Item = cheapFood, Quantity = 1 });

        Assert.False(_discountRulesService.EligibleForTwentyPercentDiscount(order));
    }
}