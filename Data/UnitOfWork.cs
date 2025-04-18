namespace MealPlannerApi.Data
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Save();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly MealPlannerDbContext _context;

        public UnitOfWork(MealPlannerDbContext context)
        {
            _context = context;
        }

        public async Task<int> Save() => await _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
