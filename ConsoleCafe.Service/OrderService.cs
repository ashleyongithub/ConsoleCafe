using ConsoleCafe.Data;
using ConsoleCafe.Service;

public interface IOrderService
{
    void CreateOrderLine(Order order, MenuItem item, int quantity);
    List<OrderLine> GetOrderLines(Order order);
    decimal GetSubtotal(Order order);
    decimal GetDiscount(Order order);
    decimal GetTotal(Order order);
}

public class OrderService : IOrderService
{
    private readonly IDiscountService _discountService;
    public OrderService(IDiscountService discountService)
    {
        _discountService = discountService;
    }
    
    public void CreateOrderLine(Order order, MenuItem item, int quantity)
    {
        var existingLine = order.OrderLines.FirstOrDefault(l => l.Item.Name == item.Name);
        if (existingLine != null)
            existingLine.Quantity += quantity;
        else
            order.OrderLines.Add(new OrderLine { Item = item, Quantity = quantity });
    }

    public List<OrderLine> GetOrderLines(Order order) => order.OrderLines;

    public decimal GetSubtotal(Order order) => order.Subtotal;

    public decimal GetDiscount(Order order) => _discountService.CalculateDiscount(order);

    public decimal GetTotal(Order order) => GetSubtotal(order) - GetDiscount(order);
    
}