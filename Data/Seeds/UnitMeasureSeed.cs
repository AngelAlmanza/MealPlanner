using MealPlannerApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealPlannerApi.Data.Seeds
{
    public class UnitMeasureSeed : IEntityTypeConfiguration<UnitMeasure>
    {
        public void Configure(EntityTypeBuilder<UnitMeasure> builder)
        {
            builder.HasData(
                new UnitMeasure() { Id = 1, Name = "Gramos", Abbreviation = "G" },
                new UnitMeasure() { Id = 2, Name = "Unidades", Abbreviation = "U" },
                new UnitMeasure() { Id = 3, Name = "Piezas", Abbreviation = "PZS" },
                new UnitMeasure() { Id = 4, Name = "Kilogramos", Abbreviation = "KG" }
            );
        }
    }
}