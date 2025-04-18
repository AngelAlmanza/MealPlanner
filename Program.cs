using MealPlannerApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddDbContext<MealPlannerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MealPlannerConnection"));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    MealPlannerDbContext dbContext = scope.ServiceProvider.GetRequiredService<MealPlannerDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
