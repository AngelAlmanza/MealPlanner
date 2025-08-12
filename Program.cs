using System.Text;
using FluentValidation;
using MealPlannerApi.AutoMappers;
using MealPlannerApi.Data;
using MealPlannerApi.DTOs;
using MealPlannerApi.Services;
using MealPlannerApi.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuestPDF.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

QuestPDF.Settings.License = LicenseType.Community;

var builder = WebApplication.CreateBuilder(args);

// JWT
var jwtKey = builder.Configuration["Jwt:Key"];
var key = Encoding.ASCII.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization();

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
builder.Services
    .AddScoped<ICommonService<MealPlanItemDto, MealPlanItemInsertDto, MealPlanItemUpdateDto>, MealPlanItemService>();
builder.Services.AddScoped<ICommonService<RecipeDto, RecipeInsertDto, RecipeUpdateDto>, RecipeService>();
builder.Services.AddScoped<ICommonService<UnitMeasureDto, UnitMeasureInsertDto, UnitMeasureUpdateDto>, UnitMeasureService>();

builder.Services.AddDbContext<MealPlannerDbContext>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("MealPlannerConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MealPlannerConnection"))
    );
});

// Validators
builder.Services.AddScoped<IValidator<IngredientInsertDto>, IngredientInsertValidator>();
builder.Services.AddScoped<IValidator<IngredientUpdateDto>, IngredientUpdateValidator>();
builder.Services.AddScoped<IValidator<LoginDto>, LoginValidator>();
builder.Services.AddScoped<IValidator<MealPlanItemInsertDto>, MealPlanItemInsertValidator>();
builder.Services.AddScoped<IValidator<MealPlanItemUpdateDto>, MealPlanItemUpdateValidator>();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
