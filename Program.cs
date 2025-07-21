using FluentValidation;
using MealPlannerApi.AutoMappers;
using MealPlannerApi.Data;
using MealPlannerApi.DTOs;
using MealPlannerApi.Services;
using MealPlannerApi.Validators;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICommonService<IngredientDto, IngredientInsertDto, IngredientUpdateDto>, IngredientService>();
builder.Services.AddScoped<ICommonService<RecipeDto, RecipeInsertDto, RecipeUpdateDto>, RecipeService>();
builder.Services.AddScoped<ICommonService<UnitMeasureDto, UnitMeasureInsertDto, UnitMeasureUpdateDto>, UnitMeasureService>();

builder.Services.AddDbContext<MealPlannerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MealPlannerConnection"));
});

// Validators
builder.Services.AddScoped<IValidator<IngredientInsertDto>, IngredientInsertValidator>();
builder.Services.AddScoped<IValidator<IngredientUpdateDto>, IngredientUpdateValidator>();
builder.Services.AddScoped<IValidator<RecipeInsertDto>, RecipeInsertValidator>();
builder.Services.AddScoped<IValidator<RecipeUpdateDto>, RecipeUpdateValidator>();
builder.Services.AddScoped<IValidator<UnitMeasureInsertDto>, UnitMeasureInsertValidator>();
builder.Services.AddScoped<IValidator<UnitMeasureUpdateDto>, UnitMeasureUpdateValidator>();

// Mapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MealPlannerDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("FrontendPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
