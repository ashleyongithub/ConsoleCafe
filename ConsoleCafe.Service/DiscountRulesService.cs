using ConsoleCafe.Data;
using ConsoleCafe.Data.Enums;

namespace ConsoleCafe.Service;

public interface IDiscountRulesService
{
    decimal MaximumDiscount();
    bool EligibleForTenPercentDiscount(Order order);
    bool EligibleForTwentyPercentDiscount(Order order);
}

public class DiscountRulesService : IDiscountRulesService
{
    public decimal MaximumDiscount() => 6.00m;

    public bool EligibleForTenPercentDiscount(Order order)
    {
        var hasFood = order.OrderLines.Any(i => i.Item.Type == ItemType.Food);
        var hasDrink = order.OrderLines.Any(i => i.Item.Type == ItemType.Drink && i.Item.Name != "Water");
        return hasFood && hasDrink;
    }

    public bool EligibleForTwentyPercentDiscount(Order order)
    {
        return order.Subtotal >= 20m;
    }
}