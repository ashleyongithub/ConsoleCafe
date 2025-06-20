using ConsoleCafe.Data.Enums;

namespace ConsoleCafe.Data;

public class MenuItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public ItemType Type { get; set; }

}