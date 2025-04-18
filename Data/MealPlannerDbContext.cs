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

        public DbSet<UnitMeasure> unitMeasures { get; set; }
        public DbSet<Ingredient> ingredients { get; set; }
        public DbSet<Recipe> recipes { get; set; }
        public DbSet<RecipeIngredient> recipeIngredients { get; set; }
        public DbSet<MealPlanWeek> mealPlanWeeks { get; set; }
        public DbSet<RecipeInstance> recipeInstances { get; set; }
        public DbSet<MealPlanEntry> mealPlanEntries { get; set; }
    }
}
