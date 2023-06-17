namespace WarehouseAPI.Dtos;

public class ProductDto
{
    public int Count { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }

    public int WarehouseId { get; set; }
}