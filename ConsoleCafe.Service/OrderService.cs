using ConsoleCafe.Data;
using ConsoleCafe.Service;

public interface IOrderService
{
    void CreateOrderLine(MenuItem item, int quantity);
    List<OrderLine> GetOrderLines();
    decimal GetSubtotal();
    decimal GetDiscount();
    decimal GetTotal();
}

public class OrderService : IOrderService
{
    private readonly Order _order = new();
    private readonly IDiscountService _discountService;
    public OrderService(IDiscountService discountService)
    {
        _discountService = discountService;
    }
    
    public void CreateOrderLine(MenuItem item, int quantity) =>
        _order.OrderLines.Add(new OrderLine { Item = item, Quantity = quantity });

    public List<OrderLine> GetOrderLines() => _order.OrderLines;

    public decimal GetSubtotal() => _order.Subtotal;

    public decimal GetDiscount() => _discountService.CalculateDiscount(_order);

    public decimal GetTotal() => GetSubtotal() - GetDiscount();
}