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
        var order = TestFactory.CreateOrder(
            ("Toastie", 5m, ItemType.Food, 1),
            ("Coffee", 2m, ItemType.Drink, 1)
        );

        Assert.True(_discountRulesService.EligibleForTenPercentDiscount(order));
    }
    
    [Fact]
    public void EligibleForTenPercentDiscount_WhenFoodAndWaterOrdered_ReturnsFalse()
    {
        var order = TestFactory.CreateOrder(
            ("Toastie", 5m, ItemType.Food, 1),
            ("Water", 2m, ItemType.Drink, 1)
        );

        Assert.False(_discountRulesService.EligibleForTenPercentDiscount(order));
    }
    
    [Fact]
    public void EligibleForTwentyPercentDiscount_WhenSubtotalGreaterThan20_ReturnsTrue()
    {
        var order = TestFactory.CreateOrder(
            ("Lobster", 25m, ItemType.Food, 1)
        );

        Assert.True(_discountRulesService.EligibleForTwentyPercentDiscount(order));
    }

    [Fact]
    public void EligibleForTwentyPercentDiscount_WhenSubtotalLessThan20_ReturnsFalse()
    {
        var order = TestFactory.CreateOrder(
            ("Cheap Toastie", 2m, ItemType.Food, 1)
        );
        
        Assert.False(_discountRulesService.EligibleForTwentyPercentDiscount(order));
    }
}