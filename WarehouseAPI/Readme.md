## Warehouse API

1. Add required packages from NuGet

`Microsoft.EntityFrameworkCore.Sqlite version 7.0.7`
`Microsoft.EntityFrameworkCore.Design version 7.0.7`

2. Add `Warehouse.cs` model
3. Add `WarehouseContext.cs` in Data folder
4. Add database provider in `Program.cs`

`builder.Services.AddDbContext<WarehouseContext>(opt => opt.UseSqlite("Data Source = Warehouse.db"));`
5. Install `dotnet ef` via terminal

`dotnet tool install --global dotnet-ef`

6. In terminal go to WarehouseAPI folder and add migration

`dotnet ef migrations add Init`

7. Add Swagger 

Under builder

`builder.Services.AddEndpointsApiExplorer();`

`builder.Services.AddSwaggerGen();`

Under app

`app.UseSwagger();`

`app.UseSwaggerUI();`

8. Add GET endpoint for Warehouse
9. Add GET by id endpoint for Warehouse
10. Add POST endpoint for Warehouse

Add `WarehouseDto.cs` 
11. Add PUT endpoint for Warehouse
12. Add DELETE endpoint for Warehouse
13. Add `Product.cs` model
14. Add Product DbSet to context and field rules if needed

`public DbSet<Product> Products { get; set; }`

15. Add new migration via terminal 

`dotnet ef migrations add AddingProducts`

16. Add POST endpoint for Product
17. Add GET by id endpoint for Product
18. Add GET endpoint for Product
19. Add JSON configuration to avoid issues with circular dependency

`
builder.Services.Configure<JsonOptions>(options =>
{
options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
`

20. Add GET endpoint to download Warehouse with products


