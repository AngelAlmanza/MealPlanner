namespace MealPlannerApi.DTOs
{
    public class IngredientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UnitMeasureId { get; set; }
        public UnitMeasureDto UnitMeasure { get; set; }
    }
}
