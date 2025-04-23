using MealPlannerApi.Data.Entities;
using MealPlannerApi.Data.Repository.IRepository;

namespace MealPlannerApi.Data
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Save();
        IRepository<UnitMeasure> UnitMeasureRepository { get; }
        IRepository<Ingredient> IngredientRepository { get; }
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly MealPlannerDbContext _context;
        public IRepository<UnitMeasure> UnitMeasureRepository { get; private set; }
        public IRepository<Ingredient> IngredientRepository { get; private set; }

        public UnitOfWork(
                MealPlannerDbContext context,
                IRepository<Ingredient> ingredientRepository,
                IRepository<UnitMeasure> unitMeasureRepository
            )
        {
            _context = context;
            UnitMeasureRepository = unitMeasureRepository;
            IngredientRepository = ingredientRepository;
        }

        public async Task<int> Save() => await _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
