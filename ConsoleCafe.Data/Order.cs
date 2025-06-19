namespace ConsoleCafe.Data;

public class Order
{
    public List<OrderLine> OrderLines { get; set; } = new();

    public decimal Subtotal => OrderLines.Sum(i => i.Item.Price * i.Quantity);
}