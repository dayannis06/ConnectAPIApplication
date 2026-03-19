using System.Text.Json;

using WebApiLab.Console.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
var app = builder.Build();
var jsonFile = System.IO.File.ReadAllText( "./Resources/64KB.json");
var people = JsonSerializer.Deserialize<List<Person>>(jsonFile, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
app.MapGet("/people", () => Results.Ok(people))
   .WithName("GetPeople")
   .Produces<List<Person>>(StatusCodes.Status200OK);
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHttpsRedirection();
app.UseCors();
app.Run();

