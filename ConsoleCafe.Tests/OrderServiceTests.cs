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
        var order = new Order();
        var coffee = TestFactory.CreateMenuItem("Coffee", 2m, ItemType.Drink);

        _orderService.CreateOrderLine(order, coffee, 2);

        var lines = _orderService.GetOrderLines(order).ToList();

        Assert.Single(lines);
        Assert.Equal(coffee, lines[0].Item);
        Assert.Equal(2, lines[0].Quantity);
    }

    [Fact]
    public void CreateOrderLine_MultipleCalls_AddsMultipleLines()
    {
        var order = new Order();
        var coffee = TestFactory.CreateMenuItem("Coffee", 2m, ItemType.Drink);
        var toastie = TestFactory.CreateMenuItem("Toastie", 5m, ItemType.Drink);

        _orderService.CreateOrderLine(order, coffee, 1);
        _orderService.CreateOrderLine(order, toastie, 3);

        var lines = _orderService.GetOrderLines(order).ToList();

        Assert.Equal(2, lines.Count);
        Assert.Contains(lines, l => l.Item == coffee && l.Quantity == 1);
        Assert.Contains(lines, l => l.Item == toastie && l.Quantity == 3);
    }

    [Fact]
    public void GetSubtotal_ReturnsSumOfAllLines()
    {
        var order = TestFactory.CreateOrder(
            ("Coffee", 2m, ItemType.Drink, 2),
            ("Toastie", 5m, ItemType.Food, 1)
        );

        var subtotal = _orderService.GetSubtotal(order);

        Assert.Equal(9.00m, subtotal);
    }

    [Fact]
    public void GetTotal_ReturnsSubtotalMinusDiscount()
    {
        var order = TestFactory.CreateOrder(
            ("Coffee", 2m, ItemType.Drink, 3)
        );

        _discountServiceMock
            .Setup(ds => ds.CalculateDiscount(It.IsAny<Order>()))
            .Returns(1.50m);

        var total = _orderService.GetTotal(order);

        Assert.Equal(4.50m, total);
    }
}