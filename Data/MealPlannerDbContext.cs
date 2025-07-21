using MealPlannerApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MealPlannerApi.Data
{
    public class MealPlannerDbContext : DbContext
    {
        public MealPlannerDbContext(DbContextOptions<MealPlannerDbContext> options)
            : base(options)
        {
        }

        public DbSet<UnitMeasure> UnitMeasures { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<MealPlanItem> MealPlanItems { get; set; }
    }
}
