using MealPlannerApi.Data.Entities;

namespace MealPlannerApi.DTOs
{
    public class MealPlanEntryInsertDto
    {
        public int RecipeInstanceId { get; set; }
        public int MealPlanWeekId { get; set; }
        public DateTime Date { get; set; }
        public MealType MealType { get; set; }
        public int ServingsUsed { get; set; }
    }   
}