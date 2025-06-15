using MealPlannerApi.Data.Entities;

namespace MealPlannerApi.DTOs
{
    public class MealPlanEntryDto
    {
        public int Id { get; set; }
        public virtual MealPlanWeekDto MealPlanWeek { get; set; }
        public virtual RecipeInstanceDto RecipeInstance { get; set; }
        public DateTime Date { get; set; }
        public MealType MealType { get; set; }
        public int ServingsUsed { get; set; }
    }
}