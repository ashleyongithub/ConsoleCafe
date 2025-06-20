using ConsoleCafe.Service;

namespace ConsoleCafe.Tests;

public class MenuServiceTests
{
    [Fact]
    public void GetMenuItems_ReturnsMenuItems()
    {
        var menuService = new MenuService();

        var items = menuService.GetMenuItems();

        Assert.NotNull(items);
        Assert.Contains(items, item => item.Name == "Coffee");
        Assert.Contains(items, item => item.Name == "BBQ Chicken Toastie");
    }
}