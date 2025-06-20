using System.Globalization;
using ConsoleCafe.Service;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleCafe.App;

public static class Program
{
    public static void Main(string[] args)
    {
        CultureInfo.CurrentCulture = new CultureInfo("en-GB");

        var services = new ServiceCollection();
        services.AddSingleton<IMenuService, MenuService>();
        services.AddSingleton<IOrderService, OrderService>();
        services.AddSingleton<IDiscountService, DiscountService>();
        services.AddSingleton<IDiscountRulesService, DiscountRulesService>();
        services.AddTransient<App>();

        var serviceProvider = services.BuildServiceProvider();

        var app = serviceProvider.GetService<App>();
        app?.Run();
    }
}