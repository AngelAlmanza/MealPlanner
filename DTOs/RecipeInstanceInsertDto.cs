namespace MealPlannerApi.DTOs
{
    public class RecipeInstanceInsertDto
    {
        public int RecipeId { get; set; }
        public int TotalServings { get; set; }
        public string? Notes { get; set; }
    }
}