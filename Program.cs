using Inlämning_Backend.Endpoints;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();            
app.MapScalarApiReference();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();


ProductEndpoints.MapProductEndpoints(app);
CategoryEndpoints.MapCategoryEndpoints(app);

app.Run();
