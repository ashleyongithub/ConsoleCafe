using ConsoleCafe.Data;
using ConsoleCafe.Data.Enums;
using ConsoleCafe.Service;
using Moq;

namespace ConsoleCafe.Tests;

public class DiscountServiceTests
{
    private readonly Mock<IDiscountRulesService> _rulesMock;
    private readonly DiscountService _discountService;

    public DiscountServiceTests()
    {
        _rulesMock = new Mock<IDiscountRulesService>();
        _discountService = new DiscountService(_rulesMock.Object);
    }

    [Fact]
    public void CalculateDiscount_CapsDiscountAt6()
    {
        var order = TestFactory.CreateOrder(
            ("Food", 20m, ItemType.Food, 1),
            ("Drinks", 20m, ItemType.Drink, 1)
        );

        _rulesMock.Setup(r => r.EligibleForTwentyPercentDiscount(order)).Returns(true);
        _rulesMock.Setup(r => r.EligibleForTenPercentDiscount(order)).Returns(false);

        var discount = _discountService.CalculateDiscount(order);

        Assert.Equal(6.00m, discount);
    }
    
    [Fact]
    public void CalculateDiscount_Applies20Percent_WhenEligible()
    {
        var order = TestFactory.CreateOrder(
            ("Food", 20m, ItemType.Food, 1)
        );

        _rulesMock.Setup(r => r.EligibleForTwentyPercentDiscount(order)).Returns(true);
        _rulesMock.Setup(r => r.EligibleForTenPercentDiscount(order)).Returns(true);

        var discount = _discountService.CalculateDiscount(order);

        Assert.Equal(4m, discount);
    }

    [Fact]
    public void CalculateDiscount_Applies10Percent_WhenEligible()
    {
        var order = TestFactory.CreateOrder(
            ("Food", 5m, ItemType.Food, 1),
            ("Drinks", 5m, ItemType.Drink, 1)
        );

        _rulesMock.Setup(r => r.EligibleForTwentyPercentDiscount(order)).Returns(false);
        _rulesMock.Setup(r => r.EligibleForTenPercentDiscount(order)).Returns(true);

        var discount = _discountService.CalculateDiscount(order);

        Assert.Equal(1m, discount);
    }

    [Fact]
    public void CalculateDiscount_Applies20Percent_WhenEligibleAndFoodAndDrinkOrdered()
    {
        var order = TestFactory.CreateOrder(
            ("Food", 10m, ItemType.Food, 1),
            ("Drinks", 10m, ItemType.Food, 1)
            
        );

        _rulesMock.Setup(r => r.EligibleForTwentyPercentDiscount(order)).Returns(false);
        _rulesMock.Setup(r => r.EligibleForTenPercentDiscount(order)).Returns(true);

        var discount = _discountService.CalculateDiscount(order);

        Assert.Equal(2m, discount);
    }

    [Fact]
    public void CalculateDiscount_DoesNotApplyDiscount_WhenNotEligible()
    {
        var order = TestFactory.CreateOrder(
            ("Food", 10m, ItemType.Food, 1)
        );
        
        _rulesMock.Setup(r => r.EligibleForTwentyPercentDiscount(order)).Returns(false);
        _rulesMock.Setup(r => r.EligibleForTenPercentDiscount(order)).Returns(false);

        var discount = _discountService.CalculateDiscount(order);

        Assert.Equal(0m, discount);
    }
}