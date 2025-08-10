using MealPlannerApi.Data.Entities;
using MealPlannerApi.Data.Seeds;
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
        public DbSet<User> Users { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UnitMeasureSeed());
            builder.ApplyConfiguration(new IngredientSeed());
            builder.ApplyConfiguration(new UserSeed());
        }
    }
}
