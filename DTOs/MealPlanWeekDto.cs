namespace MealPlannerApi.DTOs
{
    public class MealPlanWeekDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Name { get; set; }
    }
}