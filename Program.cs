
using Microsoft.Extensions.DependencyInjection;
using MinimalCatalogApi.Contracts;
using MinimalCatalogApi.Repositories;

var builder = WebApplication.CreateBuilder(args);
// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// var productsRepository = new ProductRepository(connectionString!);

app.MapGet("/", () => "Hello World!");

//  PRODUCT ENDPOINTS

app.MapGet("/products", async (IProductRepository productsRepository) =>
{
    try
    {
        var products = await productsRepository.GetProducts();
        return Results.Ok(products);
    }
    catch (Exception ex)
    {
        throw new Exception(ex.Message);
    }
});

app.MapGet("/products/{id}", async (int id, IProductRepository productsRepository) =>
{   
    try
    {
        var product = await productsRepository.GetProductById(id);
        return Results.Ok(product);    
    }
    catch (Exception ex)
    {
        throw new Exception(ex.Message);
    }
});

app.MapPost("/products", async (UpsertProductDto productToCreate, IProductRepository productsRepository) => {
    var productCreated = await productsRepository.CreateProduct(productToCreate);
    if (productCreated) {
        return Results.Ok("Product has been created successfully");
    } else {
        return Results.Problem("Error at creating Product", statusCode:500);
    }
});

app.MapPut("/products/{id}", async (int productId, UpsertProductDto productToCreate, IProductRepository productsRepository) => {
    var productCreated = await productsRepository.UpdateProductById(productId, productToCreate);
    if (productCreated) {
        return Results.Ok("Product has been updated successfully");
    } else {
        return Results.Problem("Error at updating Product", statusCode: 500);
    }
});

app.MapDelete("/products/{id}", async (int productId, IProductRepository productsRepository) => {
    var productCreated = await productsRepository.DeactivateProductById(productId);
    if (productCreated) {
        return Results.Ok("Product has been deactivated successfully");
    } else {
        return Results.Problem("Error at deacting Product", statusCode:500);
    }
});

app.Run();
