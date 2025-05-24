namespace MealPlannerApi.DTOs
{
    public class RecipeInstanceUpdateDto
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int TotalServings { get; set; }
        public string? Notes { get; set; }
    }
}