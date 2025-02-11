using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using WebApp2024.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(opts => {
    opts.UseSqlServer(builder.Configuration[
    "ConnectionStrings:ProductConnection"]);
    opts.EnableSensitiveDataLogging(true);
});
builder.Services.AddControllers();

builder.Services.AddCors();

builder.Services.Configure<JsonOptions>(opts => {
    opts.JsonSerializerOptions.DefaultIgnoreCondition
    = JsonIgnoreCondition.WhenWritingNull;
});

var app = builder.Build();
app.MapControllers();
app.UseMiddleware<WebApp2024.TestMiddleware>();



app.MapGet("/", () => "Hello World!");

var context = app.Services.CreateScope().ServiceProvider
 .GetRequiredService<DataContext>();
SeedData.SeedDatabase(context);

app.Run();
