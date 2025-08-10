using MealPlannerApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealPlannerApi.Data.Seeds
{
    public class IngredientSeed : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.HasData(
                new Ingredient() { Id = 1, Name = "Avena", UnitMeasureId = 1 },
                new Ingredient() { Id = 2, Name = "Cebolla", UnitMeasureId = 1 },
                new Ingredient() { Id = 3, Name = "Tomate", UnitMeasureId = 1 },
                new Ingredient() { Id = 4, Name = "Chile Serrano", UnitMeasureId = 1 },
                new Ingredient() { Id = 5, Name = "Brocoli", UnitMeasureId = 3 }
            );
        }
    }
}