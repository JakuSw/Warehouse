using WarehouseAPI.Dtos;

namespace WarehouseAPI.Models;

public class Warehouse
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Product> Products { get; set; }
    public void Update(WarehouseDto updatedWarehouse)
    {
        Name = updatedWarehouse.Name;
    }
}