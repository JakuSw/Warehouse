namespace WarehouseAPI.Models;

public class Product
{
    public int Id { get; set; }
    public int Count { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }

    public int WarehouseId { get; set; }
    public Warehouse Warehouse { get; set;  }
}