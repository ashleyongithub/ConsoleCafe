using ConsoleCafe.Data;

namespace ConsoleCafe.Service;

public interface IOrderService
{
    List<OrderLine> GetOrderLines();
    void CreateOrder(MenuItem item, int quantity);
    decimal GetOrderTotal();
    decimal GetOrderSubTotal();
    decimal GetOrderDiscount();
}

public class OrderService : IOrderService
{
    public List<OrderLine> GetOrderLines()
    {
        throw new NotImplementedException();
    }

    public void CreateOrder(MenuItem item, int quantity)
    {
        throw new NotImplementedException();
    }

    public decimal GetOrderTotal()
    {
        throw new NotImplementedException();
    }

    public decimal GetOrderSubTotal()
    {
        throw new NotImplementedException();
    }

    public decimal GetOrderDiscount()
    {
        throw new NotImplementedException();
    }
}