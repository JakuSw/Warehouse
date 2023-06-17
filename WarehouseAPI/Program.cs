using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using WarehouseAPI.Data;
using WarehouseAPI.Dtos;
using WarehouseAPI.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WarehouseContext>(opt => opt.UseSqlite("Data Source = Warehouse.db"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//configuration of JSON responses
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

await MigrateDatabase(app.Services, app.Logger);


// Warehouse endpoints
app.MapGet("/", (WarehouseContext context) =>
{
    var result = context.Warehouses.ToList();
    return Results.Ok(result);
});

app.MapGet("/{id:int}", (WarehouseContext context, int id) =>
{
    var result = context.Warehouses.FirstOrDefault(w => w.Id == id);
    if (result is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(result);
});

app.MapPost("/", (WarehouseContext context, WarehouseDto dto) =>
{
    var warehouse = new Warehouse()
    {
        Name = dto.Name
    };

    context.Warehouses.Add(warehouse);
    context.SaveChanges();
    return Results.Created($"/{warehouse.Id}", warehouse);
});

app.MapPut("/{id:int}", (WarehouseContext context, int id, WarehouseDto dto) =>
{
    var existingWarehouse = context.Warehouses.FirstOrDefault(w => w.Id == id);
    if (existingWarehouse is null)
    {
        return Results.NotFound();
    }
    existingWarehouse.Update(dto);
    context.Warehouses.Update(existingWarehouse);
    context.SaveChanges();
    return Results.Ok(existingWarehouse);
});

app.MapDelete("/{id:int}", (WarehouseContext context, int id) =>
{
    var warehouseToDelete = context.Warehouses.FirstOrDefault(w => w.Id == id);
    if (warehouseToDelete is null)
    {
        return Results.NotFound();
    }

    context.Warehouses.Remove(warehouseToDelete);
    context.SaveChanges();
    return Results.NoContent();
});

// Products endpoints

app.MapPost("/products", (WarehouseContext context, ProductDto product) =>
{
    var newProduct = new Product()
    {
        Count = product.Count,
        Name = product.Name,
        Type = product.Type,
        WarehouseId = product.WarehouseId
    };

    context.Products.Add(newProduct);
    context.SaveChanges();

    return Results.Created($"/products/{newProduct.Id}", newProduct);
});

app.MapGet("/products/{id:int}", (WarehouseContext context, int id) =>
{
    var result = context.Products.FirstOrDefault(w => w.Id == id);
    if (result is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(result);
});

app.MapGet("/products", (WarehouseContext context) =>
{
    var result = context.Products.ToList();
    return Results.Ok(result);
});

//Other endpoints
app.MapGet("/warehouse-with-products/{id:int}", (WarehouseContext context, int id) =>
{
    var warehouseWithProducts = context.Warehouses
        .Include(w => w.Products)
        .FirstOrDefault(w => w.Id == id);
    
    if (warehouseWithProducts is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(warehouseWithProducts);
});

app.Run();


async Task MigrateDatabase(IServiceProvider services, ILogger logger)
{
    //automatic database migration 
    await using var db = services.CreateScope().ServiceProvider.GetRequiredService<WarehouseContext>();
    if (db.Database.GetPendingMigrations().Any())
    {
        logger.LogInformation("Migrating database");
        await db.Database.MigrateAsync();
    }
}
