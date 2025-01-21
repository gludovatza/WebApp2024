using Microsoft.EntityFrameworkCore;
using WebApp2024.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(opts => {
    opts.UseSqlServer(builder.Configuration[
    "ConnectionStrings:ProductConnection"]);
    opts.EnableSensitiveDataLogging(true);
});
var app = builder.Build();
app.UseMiddleware<WebApp2024.TestMiddleware>();
app.MapGet("/", () => "Hello World!");

var context = app.Services.CreateScope().ServiceProvider
 .GetRequiredService<DataContext>();
SeedData.SeedDatabase(context);

app.Run();
