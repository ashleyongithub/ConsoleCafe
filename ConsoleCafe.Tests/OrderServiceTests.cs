using ConsoleCafe.Data;
using ConsoleCafe.Data.Enums;
using ConsoleCafe.Service;
using Moq;

namespace ConsoleCafe.Tests;

public class OrderServiceTests
{
    private readonly Mock<IDiscountService> _discountServiceMock;
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        _discountServiceMock = new Mock<IDiscountService>();
        _orderService = new OrderService(_discountServiceMock.Object);
    }

    [Fact]
    public void CreateOrderLine_AddsNewLine()
    {
        var item = new MenuItem { Name = "Coffee", Price = 2.00m, Type = ItemType.Drink };

        _orderService.CreateOrderLine(item, 2);

        var lines = _orderService.GetOrderLines().ToList();

        Assert.Single(lines);
        Assert.Equal(item, lines[0].Item);
        Assert.Equal(2, lines[0].Quantity);
    }

    [Fact]
    public void CreateOrderLine_MultipleCalls_AddsMultipleLines()
    {
        var item1 = new MenuItem { Name = "Coffee", Price = 2.00m, Type = ItemType.Drink };
        var item2 = new MenuItem { Name = "Toastie", Price = 5.00m, Type = ItemType.Food };

        _orderService.CreateOrderLine(item1, 1);
        _orderService.CreateOrderLine(item2, 3);

        var lines = _orderService.GetOrderLines().ToList();

        Assert.Equal(2, lines.Count);
        Assert.Contains(lines, l => l.Item == item1 && l.Quantity == 1);
        Assert.Contains(lines, l => l.Item == item2 && l.Quantity == 3);
    }

    [Fact]
    public void GetSubtotal_ReturnsSumOfAllLines()
    {
        var item1 = new MenuItem { Name = "Coffee", Price = 2.00m, Type = ItemType.Drink };
        var item2 = new MenuItem { Name = "Toastie", Price = 5.00m, Type = ItemType.Food };

        _orderService.CreateOrderLine(item1, 2); // £4.00
        _orderService.CreateOrderLine(item2, 1); // £5.00

        var subtotal = _orderService.GetSubtotal();

        Assert.Equal(9.00m, subtotal);
    }

    [Fact]
    public void GetTotal_ReturnsSubtotalMinusDiscount()
    {
        var item = new MenuItem { Name = "Coffee", Price = 2.00m, Type = ItemType.Drink };
        _orderService.CreateOrderLine(item, 3); // subtotal 6.00

        _discountServiceMock
            .Setup(ds => ds.CalculateDiscount(It.IsAny<Order>()))
            .Returns(1.50m);

        var total = _orderService.GetTotal();

        Assert.Equal(4.50m, total);
    }
}