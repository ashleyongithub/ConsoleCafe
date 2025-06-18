namespace ConsoleCafe.Data;

public class OrderLine
{
    public MenuItem Item { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice => Item.Price * Quantity;
}