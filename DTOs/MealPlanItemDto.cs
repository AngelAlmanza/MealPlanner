using MealPlannerApi.Data.Entities;

namespace MealPlannerApi.DTOs
{
    public class MealPlanItemDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public MealType MealType { get; set; }
        public RecipeDto Recipe { get; set; }
    }
}