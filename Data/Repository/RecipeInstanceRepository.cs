using MealPlannerApi.Data.Entities;
using MealPlannerApi.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MealPlannerApi.Data.Repository
{
    public class RecipeInstanceRepository : IRepository<RecipeInstance>
    {
        private readonly MealPlannerDbContext _context;

        public RecipeInstanceRepository(MealPlannerDbContext context)
        {
            _context = context;    
        }

        public async Task<IEnumerable<RecipeInstance>> Get()
            => await _context.RecipeInstances
                .Include(record => record.Recipe)
                .ToListAsync();

        public async Task<RecipeInstance> GetById(int id)
            => await _context.RecipeInstances
                .Include(record => record.Recipe)
                .FirstOrDefaultAsync(record => record.Id == id);

        public async Task Add(RecipeInstance entity)
            => await _context.RecipeInstances.AddAsync(entity);

        public void Update(RecipeInstance entity)
        {
            _context.RecipeInstances.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(RecipeInstance entity)
            => _context.RecipeInstances.Remove(entity);

        public IEnumerable<RecipeInstance> Search(Func<RecipeInstance, bool> filter)
            => _context.RecipeInstances
                .Include(i => i.Recipe)
                .Where(filter)
                .ToList();  
    }
}