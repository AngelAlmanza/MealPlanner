namespace MealPlannerApi.DTOs
{
    public class RecipeIngredientDto
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }

        public IngredientDto Ingredient { get; set; }
    }
}