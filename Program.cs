using Inlämning_Backend.Data;
using Inlämning_Backend.Endpoints;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.MapOpenApi();            
app.MapScalarApiReference();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();


ProductEndpoints.MapProductEndpoints(app);
CategoryEndpoints.MapCategoryEndpoints(app);

app.Run();
