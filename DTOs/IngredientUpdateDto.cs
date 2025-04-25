namespace MealPlannerApi.DTOs
{
    public class IngredientUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UnitMeasureId { get; set; }
    }
}
