using MealPlannerApi.Data.Entities;

namespace MealPlannerApi.Data.Repository.IRepository
{
    public interface IMealEntryRepository : IRepository<MealPlanEntry>
    {
        Task<IEnumerable<MealPlanEntry>> GetByRecipeInstanceId(int recipeInstanceId);
    }
}