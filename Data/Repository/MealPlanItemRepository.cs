using MealPlannerApi.Data.Entities;
using MealPlannerApi.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MealPlannerApi.Data.Repository
{
    public class MealPlanItemRepository : IRepository<MealPlanItem>
    {
        private readonly MealPlannerDbContext _context;
        
        public MealPlanItemRepository(MealPlannerDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<MealPlanItem>> Get()
            => await _context.MealPlanItems
                .Include(mpi => mpi.Recipe)
                .ThenInclude(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .ThenInclude(i => i.UnitMeasure)
                .ToListAsync();
        
        public async Task<MealPlanItem> GetById(int id)
            => await _context.MealPlanItems
                .Include(mpi => mpi.Recipe)
                .ThenInclude(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .ThenInclude(i => i.UnitMeasure)
                .FirstOrDefaultAsync(mpi => mpi.Id == id);

        public async Task Add(MealPlanItem entity)
            => await _context.MealPlanItems.AddAsync(entity);

        public void Update(MealPlanItem entity)
        {
            _context.MealPlanItems.Update(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        
        public void Delete(MealPlanItem entity)
            => _context.MealPlanItems.Remove(entity);
        
        public IEnumerable<MealPlanItem> Search(Func<MealPlanItem, bool> filter)
            => _context.MealPlanItems
                .Include(mpi => mpi.Recipe)
                .ThenInclude(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .ThenInclude(i => i.UnitMeasure)
                .AsEnumerable()
                .Where(filter)
                .ToList();
    }
}