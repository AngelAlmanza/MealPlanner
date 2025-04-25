using MealPlannerApi.Data.Entities;
using MealPlannerApi.Data.Repository;
using MealPlannerApi.Data.Repository.IRepository;

namespace MealPlannerApi.Data
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Save();
        IRepository<UnitMeasure> UnitMeasureRepository { get; }
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly MealPlannerDbContext _context;
        public IRepository<UnitMeasure> UnitMeasureRepository { get; }

        public UnitOfWork(
                MealPlannerDbContext context
            )
        {
            _context = context;
            UnitMeasureRepository = new UnitMeasureRepository(context);
        }

        public async Task<int> Save() => await _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
