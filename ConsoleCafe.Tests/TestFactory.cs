using ConsoleCafe.Data;
using ConsoleCafe.Data.Enums;

namespace ConsoleCafe.Tests;

public static class TestFactory
{
    public static Order CreateOrder(params (string name, decimal price, ItemType type, int qty)[] items)
    {
        var order = new Order();
        foreach (var (name, price, type, qty) in items)
        {
            var menuItem = CreateMenuItem(name, price, type);
            order.OrderLines.Add(new OrderLine
            {
                Item = menuItem,
                Quantity = qty
            });
        }
        return order;
    }
    
    public static MenuItem CreateMenuItem(string name, decimal price, ItemType type)
    {
        return new MenuItem
        {
            Name = name,
            Price = price,
            Type = type
        };
    }
}