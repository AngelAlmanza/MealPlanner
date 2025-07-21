namespace MealPlannerApi.DTOs
{
    public class RecipeInsertDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Servings { get; set; }
        public string? Url { get; set; }
        public List<RecipeIngredientInsertDto> Ingredients { get; set; }
    }
}