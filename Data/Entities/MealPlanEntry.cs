using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MealPlannerApi.Data.Entities
{
    public enum MealType
    {
        Breakfast,
        Lunch,
        Dinner,
        Snack
    }

    public class MealPlanEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int RecipeInstanceId { get; set; }

        [ForeignKey("RecipeInstanceId")]
        public virtual RecipeInstance RecipeInstance { get; set; }

        [Required]
        public int MealPlanWeekId { get; set; }

        [ForeignKey("MealPlanWeekId")]
        public virtual MealPlanWeek MealPlanWeek { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [EnumDataType(typeof(MealType))]
        public MealType MealType { get; set; }

        [Required]
        public int ServingsUsed { get; set; }
    }
}
