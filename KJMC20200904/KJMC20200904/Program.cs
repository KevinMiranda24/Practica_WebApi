var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var products = new List<Product>();

app.MapGet("products", () =>
{
    return products;
});


app.MapGet("/products/{id}", (int id) =>
{
    var product = products.FirstOrDefault(x => x.Id == id);
    return product;
});

app.MapPost("/products", (Product product) =>
{
    products.Add(product);
    return Results.Ok();
});

app.MapPut("/products/{id}", (int id, Product product) =>
{
    var existproduct = products.FirstOrDefault(x => x.Id == id);
    if(existproduct != null)
    {
        existproduct.Name = product.Name; 
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }
 
});

app.MapDelete("/products/{id}", (int id) =>
{
    var existproduct = products.FirstOrDefault(x => x.Id == id);
    if (existproduct != null)
    {
        products.Remove(existproduct);
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }

});

app.Run();

internal class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
}




