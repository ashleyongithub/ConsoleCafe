using ConsoleCafe.Data;
using ConsoleCafe.Data.Enums;

namespace ConsoleCafe.Service;

public interface IDiscountService
{
    decimal CalculateDiscount(Order order);
}

public class DiscountService : IDiscountService
{
    public decimal CalculateDiscount(Order order)
    {
        var subtotal = order.Subtotal;
        var hasFood = order.OrderLines.Any(i => i.Item.Type == ItemType.Food);
        var hasNonWaterDrink = order.OrderLines.Any(i => i.Item.Type == ItemType.Drink && i.Item.Name != "Water");

        var discount = 0m;

        if (subtotal >= 20)
        {
            discount = subtotal * 0.20m;
        }
        else if (hasFood && hasNonWaterDrink)
        {
            discount = subtotal * 0.10m;
        }

        return Math.Round(Math.Min(discount, 6.00m), 2);
    }
}