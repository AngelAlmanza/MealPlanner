using MealPlannerApi.Data.Entities;

namespace MealPlannerApi.DTOs
{
    public class MealPlanItemUpdateDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public MealType MealType { get; set; }
        public int RecipeId { get; set; }
    }
}