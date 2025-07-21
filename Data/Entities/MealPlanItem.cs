using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MealPlannerApi.Data.Entities
{
    public class MealPlanItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    
        [Required]
        public DateTime Date { get; set; }
    
        [Required]
        public MealType MealType { get; set; }
        
        [Required]
        public int RecipeId { get; set; }
        
        [ForeignKey("RecipeId")]
        public virtual Recipe Recipe { get; set; }
    }

    public enum MealType
    {
        Breakfast,
        Lunch,
        Dinner,
        Snack
    }
}