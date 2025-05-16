using MealPlannerApi.Data.Entities;
using MealPlannerApi.Data.Repository;
using MealPlannerApi.Data.Repository.IRepository;

namespace MealPlannerApi.Data
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Save();
        IRepository<UnitMeasure> UnitMeasureRepository { get; }
        IRepository<Ingredient> IngredientRepository { get; }
        IRepository<Recipe> RecipeRepository { get; }
        IBulkInsertRepository<RecipeIngredient> RecipeIngredientRepository { get; }
        IRepository<MealPlanWeek> MealPlanWeekRepository { get; }
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly MealPlannerDbContext _context;
        public IRepository<UnitMeasure> UnitMeasureRepository { get; }
        public IRepository<Ingredient> IngredientRepository { get; }
        public IRepository<Recipe> RecipeRepository { get; }
        public IBulkInsertRepository<RecipeIngredient> RecipeIngredientRepository { get; }
        public IRepository<MealPlanWeek> MealPlanWeekRepository { get; }

        public UnitOfWork(
                MealPlannerDbContext context
            )
        {
            _context = context;
            UnitMeasureRepository = new UnitMeasureRepository(context);
            IngredientRepository = new IngredientRepository(context);
            RecipeRepository = new RecipeRepository(context);
            RecipeIngredientRepository = new RecipeIngredientRepository(context);
            MealPlanWeekRepository = new MealPlanWeekRepository(context);
        }

        public async Task<int> Save() => await _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
