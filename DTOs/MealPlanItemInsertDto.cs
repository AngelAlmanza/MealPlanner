using MealPlannerApi.Data.Entities;

namespace MealPlannerApi.DTOs
{
    public class MealPlanItemInsertDto
    {
        public DateTime Date { get; set; }
        public MealType MealType { get; set; }
        public int RecipeId { get; set; }
    }
}