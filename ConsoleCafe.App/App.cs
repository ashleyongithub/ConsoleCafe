using ConsoleCafe.Data;
using ConsoleCafe.Service;

namespace ConsoleCafe.App;

public class App
{
    private readonly IMenuService _menuService;
    private readonly IOrderService _orderService;

    public App(IMenuService menuService, IOrderService orderService)
    {
        _menuService = menuService;
        _orderService = orderService;
    }

    public void Run()
    {
        Console.WriteLine("Welcome to the Console Cafe App!");
        Console.WriteLine("--------------------");
        var continueOrdering = true;
        while (continueOrdering)
        {
            var order = new Order(); 
            DisplayMenu();
            HandleOrdering(order);
            DisplayBill(order);
            
            Console.Write("\nWould you like to place another order? (y/n): ");
            var continueInput = Console.ReadLine()?.Trim().ToLower();

            continueOrdering = continueInput == "y" || continueInput == "yes";
        }

        Console.ReadKey();
    }

    private void DisplayMenu()
    {
        Console.WriteLine("\nHere is the menu:");
        var menuItems = _menuService.GetMenuItems();

        Console.WriteLine("| ID | Item                 | Type  | Price   |");
        Console.WriteLine("|----|----------------------|-------|---------|");

        foreach (var item in menuItems)
        {
            Console.WriteLine($"| {item.Id,-2} | {item.Name,-20} | {item.Type,-5} | {item.Price,7:C2} |");
        }
    }

    private void HandleOrdering(Order order)
    {
        var menuItems = _menuService.GetMenuItems();
        while (true)
        {
            Console.WriteLine("\nEnter the ID of the item you wish to order or type 'done' to get the bill:");
            var input = Console.ReadLine();

            if (string.Equals(input, "done", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            if (!int.TryParse(input, out var itemId))
            {
                Console.WriteLine("Invalid input. Please enter a valid item ID.");
                continue;
            }

            var selectedItem = menuItems.FirstOrDefault(item => item.Id == itemId);
            if (selectedItem == null)
            {
                Console.WriteLine("Item not found. Please enter an ID from the menu.");
                continue;
            }

            Console.WriteLine($"How many '{selectedItem.Name}' would you like?");
            if (!int.TryParse(Console.ReadLine(), out var quantity) || quantity <= 0)
            {
                Console.WriteLine("Invalid quantity. Please enter a positive number.");
                continue;
            }

            _orderService.CreateOrderLine(order, selectedItem, quantity);
            Console.WriteLine($"Added {quantity} x {selectedItem.Name} to your order.");
        }
    }

    private void DisplayBill(Order order)
    {
        var orderDetails = _orderService.GetOrderLines(order);

        Console.WriteLine("\n--- Your Final Bill ---");
        if (orderDetails.Count == 0)
        {
            Console.WriteLine("You did not order any items.");
        }
        else
        {
            Console.WriteLine("| Qty | Item                 | Total   |");
            Console.WriteLine("|-----|----------------------|---------|");
            foreach (var orderItem in orderDetails)
            {
                Console.WriteLine(
                    $"| {orderItem.Quantity,-3} | {orderItem.Item.Name,-20} | {orderItem.TotalPrice,7:C2} |");
            }

            Console.WriteLine("----------------------------------------");
        }

        var totalBill = _orderService.GetTotal(order);
        var subtotal = _orderService.GetSubtotal(order);
        var discount = _orderService.GetDiscount(order);
        Console.WriteLine($"Subtotal: {subtotal:C2}");
        Console.WriteLine($"Discount: {discount:C2}");
        Console.WriteLine($"Total Amount Due: {totalBill:C2}");
        Console.WriteLine("-----------------------");
    }
}