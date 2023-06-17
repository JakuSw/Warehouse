using Microsoft.EntityFrameworkCore;
using WarehouseAPI.Models;

namespace WarehouseAPI.Data;

public class WarehouseContext : DbContext
{
    public WarehouseContext(DbContextOptions<WarehouseContext> options) : base(options) { }
    
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(p =>
        {
            p.Property(e => e.Type)
                .HasMaxLength(15);
            p.Property(e => e.Count)
                .HasDefaultValue(0);
        });
    }
}