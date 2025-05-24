namespace MealPlannerApi.DTOs
{
    public class RecipeInstanceDto
    {
        public int Id { get; set; }
        public RecipeDto Recipe { get; set; }
        public int TotalServings { get; set; }
        public string? Notes { get; set; }
    }
}