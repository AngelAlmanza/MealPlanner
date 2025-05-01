using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealPlannerApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ingredients_unitMeasures_UnitMeasureId",
                table: "ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_mealPlanEntries_mealPlanWeeks_MealPlanWeekId",
                table: "mealPlanEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_mealPlanEntries_recipeInstances_RecipeInstanceId",
                table: "mealPlanEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_recipeIngredients_ingredients_IngredientId",
                table: "recipeIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_recipeIngredients_recipes_RecipeId",
                table: "recipeIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_recipeInstances_recipes_RecipeId",
                table: "recipeInstances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_unitMeasures",
                table: "unitMeasures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_recipes",
                table: "recipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_recipeInstances",
                table: "recipeInstances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_recipeIngredients",
                table: "recipeIngredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_mealPlanWeeks",
                table: "mealPlanWeeks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_mealPlanEntries",
                table: "mealPlanEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ingredients",
                table: "ingredients");

            migrationBuilder.RenameTable(
                name: "unitMeasures",
                newName: "UnitMeasures");

            migrationBuilder.RenameTable(
                name: "recipes",
                newName: "Recipes");

            migrationBuilder.RenameTable(
                name: "recipeInstances",
                newName: "RecipeInstances");

            migrationBuilder.RenameTable(
                name: "recipeIngredients",
                newName: "RecipeIngredients");

            migrationBuilder.RenameTable(
                name: "mealPlanWeeks",
                newName: "MealPlanWeeks");

            migrationBuilder.RenameTable(
                name: "mealPlanEntries",
                newName: "MealPlanEntries");

            migrationBuilder.RenameTable(
                name: "ingredients",
                newName: "Ingredients");

            migrationBuilder.RenameIndex(
                name: "IX_recipeInstances_RecipeId",
                table: "RecipeInstances",
                newName: "IX_RecipeInstances_RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_recipeIngredients_RecipeId",
                table: "RecipeIngredients",
                newName: "IX_RecipeIngredients_RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_recipeIngredients_IngredientId",
                table: "RecipeIngredients",
                newName: "IX_RecipeIngredients_IngredientId");

            migrationBuilder.RenameIndex(
                name: "IX_mealPlanEntries_RecipeInstanceId",
                table: "MealPlanEntries",
                newName: "IX_MealPlanEntries_RecipeInstanceId");

            migrationBuilder.RenameIndex(
                name: "IX_mealPlanEntries_MealPlanWeekId",
                table: "MealPlanEntries",
                newName: "IX_MealPlanEntries_MealPlanWeekId");

            migrationBuilder.RenameIndex(
                name: "IX_ingredients_UnitMeasureId",
                table: "Ingredients",
                newName: "IX_Ingredients_UnitMeasureId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnitMeasures",
                table: "UnitMeasures",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipes",
                table: "Recipes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeInstances",
                table: "RecipeInstances",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeIngredients",
                table: "RecipeIngredients",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealPlanWeeks",
                table: "MealPlanWeeks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealPlanEntries",
                table: "MealPlanEntries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_UnitMeasures_UnitMeasureId",
                table: "Ingredients",
                column: "UnitMeasureId",
                principalTable: "UnitMeasures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MealPlanEntries_MealPlanWeeks_MealPlanWeekId",
                table: "MealPlanEntries",
                column: "MealPlanWeekId",
                principalTable: "MealPlanWeeks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MealPlanEntries_RecipeInstances_RecipeInstanceId",
                table: "MealPlanEntries",
                column: "RecipeInstanceId",
                principalTable: "RecipeInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Ingredients_IngredientId",
                table: "RecipeIngredients",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Recipes_RecipeId",
                table: "RecipeIngredients",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeInstances_Recipes_RecipeId",
                table: "RecipeInstances",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_UnitMeasures_UnitMeasureId",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_MealPlanEntries_MealPlanWeeks_MealPlanWeekId",
                table: "MealPlanEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_MealPlanEntries_RecipeInstances_RecipeInstanceId",
                table: "MealPlanEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Ingredients_IngredientId",
                table: "RecipeIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Recipes_RecipeId",
                table: "RecipeIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeInstances_Recipes_RecipeId",
                table: "RecipeInstances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnitMeasures",
                table: "UnitMeasures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipes",
                table: "Recipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeInstances",
                table: "RecipeInstances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeIngredients",
                table: "RecipeIngredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealPlanWeeks",
                table: "MealPlanWeeks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealPlanEntries",
                table: "MealPlanEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients");

            migrationBuilder.RenameTable(
                name: "UnitMeasures",
                newName: "unitMeasures");

            migrationBuilder.RenameTable(
                name: "Recipes",
                newName: "recipes");

            migrationBuilder.RenameTable(
                name: "RecipeInstances",
                newName: "recipeInstances");

            migrationBuilder.RenameTable(
                name: "RecipeIngredients",
                newName: "recipeIngredients");

            migrationBuilder.RenameTable(
                name: "MealPlanWeeks",
                newName: "mealPlanWeeks");

            migrationBuilder.RenameTable(
                name: "MealPlanEntries",
                newName: "mealPlanEntries");

            migrationBuilder.RenameTable(
                name: "Ingredients",
                newName: "ingredients");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeInstances_RecipeId",
                table: "recipeInstances",
                newName: "IX_recipeInstances_RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeIngredients_RecipeId",
                table: "recipeIngredients",
                newName: "IX_recipeIngredients_RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeIngredients_IngredientId",
                table: "recipeIngredients",
                newName: "IX_recipeIngredients_IngredientId");

            migrationBuilder.RenameIndex(
                name: "IX_MealPlanEntries_RecipeInstanceId",
                table: "mealPlanEntries",
                newName: "IX_mealPlanEntries_RecipeInstanceId");

            migrationBuilder.RenameIndex(
                name: "IX_MealPlanEntries_MealPlanWeekId",
                table: "mealPlanEntries",
                newName: "IX_mealPlanEntries_MealPlanWeekId");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredients_UnitMeasureId",
                table: "ingredients",
                newName: "IX_ingredients_UnitMeasureId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_unitMeasures",
                table: "unitMeasures",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_recipes",
                table: "recipes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_recipeInstances",
                table: "recipeInstances",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_recipeIngredients",
                table: "recipeIngredients",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_mealPlanWeeks",
                table: "mealPlanWeeks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_mealPlanEntries",
                table: "mealPlanEntries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ingredients",
                table: "ingredients",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ingredients_unitMeasures_UnitMeasureId",
                table: "ingredients",
                column: "UnitMeasureId",
                principalTable: "unitMeasures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_mealPlanEntries_mealPlanWeeks_MealPlanWeekId",
                table: "mealPlanEntries",
                column: "MealPlanWeekId",
                principalTable: "mealPlanWeeks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_mealPlanEntries_recipeInstances_RecipeInstanceId",
                table: "mealPlanEntries",
                column: "RecipeInstanceId",
                principalTable: "recipeInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_recipeIngredients_ingredients_IngredientId",
                table: "recipeIngredients",
                column: "IngredientId",
                principalTable: "ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_recipeIngredients_recipes_RecipeId",
                table: "recipeIngredients",
                column: "RecipeId",
                principalTable: "recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_recipeInstances_recipes_RecipeId",
                table: "recipeInstances",
                column: "RecipeId",
                principalTable: "recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
