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

var marcas = new List<Marca>();

//crear
app.MapPost("/marcas", (Marca marca) =>
{
marcas.Add(marca);
return Results.Ok();
});

//modificar
app.MapPut("/marcas/{id}", (int id, Marca marca) =>
{
var existingMarca = marcas.FirstOrDefault(m => m.Id == id);
if (existingMarca != null)
{
existingMarca.Name = marca.Name;
existingMarca.Descripcion = marca.Descripcion;
return Results.Ok();
}
else
{
return Results.NotFound();
}
});

//obtener por id
app.MapGet("/marcas/{id}", (int id) =>
{
var marca = marcas.FirstOrDefault(m => m.Id == id);
return marca;
});

app.Run();

internal class Marca
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Descripcion { get; set; } 
}