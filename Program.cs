using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
//using System.Text.Json.Serialization;
using WebApp2024.Models;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(opts => {
    opts.UseSqlServer(builder.Configuration[
    "ConnectionStrings:ProductConnection"]);
    opts.EnableSensitiveDataLogging(true);
});
builder.Services.AddControllers();

builder.Services.AddCors();

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.Configure<MvcNewtonsoftJsonOptions>(opts => {
    opts.SerializerSettings.NullValueHandling
    = Newtonsoft.Json.NullValueHandling.Ignore;
});

//builder.Services.Configure<JsonOptions>(opts => {
//    opts.JsonSerializerOptions.DefaultIgnoreCondition
//    = JsonIgnoreCondition.WhenWritingNull;
//});

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApp", Version = "v1" });
});

var app = builder.Build();
app.MapControllers();
app.UseMiddleware<WebApp2024.TestMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(options => {
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApp");
});

app.MapGet("/", () => "Hello World!");

var context = app.Services.CreateScope().ServiceProvider
 .GetRequiredService<DataContext>();
SeedData.SeedDatabase(context);

app.Run();
