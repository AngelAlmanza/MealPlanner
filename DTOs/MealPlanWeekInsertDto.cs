namespace MealPlannerApi.DTOs
{
    public class MealPlanWeekInsertDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Name { get; set; }
    }
}