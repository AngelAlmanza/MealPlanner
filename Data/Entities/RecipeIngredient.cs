using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MealPlannerApi.Data.Entities
{
    public class RecipeIngredient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int RecipeId { get; set; }

        [ForeignKey("RecipeId")]
        public virtual Recipe Recipe { get; set; }

        [Required]
        public int IngredientId { get; set; }

        [ForeignKey("IngredientId")]
        public virtual Ingredient Ingredient { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Quantity { get; set; }
    }
}
