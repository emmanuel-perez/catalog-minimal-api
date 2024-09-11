var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

//  PRODUCT ENDPOINTS

// app.MapGet("/products" => );


app.Run();
