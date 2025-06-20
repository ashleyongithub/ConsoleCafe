using ConsoleCafe.Data;

namespace ConsoleCafe.Service;

public interface IDiscountService
{
    decimal CalculateDiscount(Order order);
}

public class DiscountService : IDiscountService
{
    private readonly IDiscountRulesService _discountRulesService;
    
    public DiscountService(IDiscountRulesService discountRulesService)
    {
        _discountRulesService = discountRulesService;
    }
    public decimal CalculateDiscount(Order order)
    {
        var subtotal = order.Subtotal;

        var discount = 0m;

        if (_discountRulesService.EligibleForTwentyPercentDiscount(order))
        {
            discount = subtotal * 0.20m;
        }
        else if (_discountRulesService.EligibleForTenPercentDiscount(order))
        {
            discount = subtotal * 0.10m;
        }

        return Math.Round(Math.Min(discount, 6.00m), 2);
    }
}