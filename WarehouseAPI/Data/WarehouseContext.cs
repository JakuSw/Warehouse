using Microsoft.EntityFrameworkCore;
namespace Warehouse.Data;

public class WarehouseContext : DbContext
{
    public WarehouseContext(DbContextOptions<WarehouseContext> options) : base(options) { }
    
    public DbSet<Warehouse> Warehouses { get; set; }
}