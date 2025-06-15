using MealPlannerApi.Data.Entities;
using MealPlannerApi.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MealPlannerApi.Data.Repository
{
    public class MealPlanEntryRepository : IMealEntryRepository
    {
        private readonly MealPlannerDbContext _context;

        public MealPlanEntryRepository(MealPlannerDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<MealPlanEntry>> Get()
            => await _context.MealPlanEntries
                .Include(mpe => mpe.MealPlanWeek)
                .Include(mpe => mpe.RecipeInstance)
                .ThenInclude(r => r.Recipe)
                .ThenInclude(r => r.RecipeIngredients)
                .ToListAsync();

        public async Task<MealPlanEntry> GetById(int id)
            => await _context.MealPlanEntries
                .Include(mpe => mpe.MealPlanWeek)
                .Include(mpe => mpe.RecipeInstance)
                .ThenInclude(r => r.Recipe)
                .ThenInclude(r => r.RecipeIngredients)
                .FirstOrDefaultAsync(mpe => mpe.Id == id);       

        public async Task Add(MealPlanEntry entity)
            => await _context.MealPlanEntries.AddAsync(entity);

        public void Update(MealPlanEntry entity)
        {
            _context.MealPlanEntries.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(MealPlanEntry entity)
            => _context.MealPlanEntries.Remove(entity);

        public IEnumerable<MealPlanEntry> Search(Func<MealPlanEntry, bool> filter)
            => _context.MealPlanEntries
                .Include(mpe => mpe.MealPlanWeek)
                .Include(mpe => mpe.RecipeInstance)
                .ThenInclude(r => r.Recipe)
                .ThenInclude(r => r.RecipeIngredients)
                .AsEnumerable()
                .Where(filter)
                .ToList();
        
        public async Task<IEnumerable<MealPlanEntry>> GetByRecipeInstanceId(int recipeInstanceId)
            => await _context.MealPlanEntries
                .Where(mpe => mpe.RecipeInstanceId == recipeInstanceId)
                .Include(mpe => mpe.MealPlanWeek)
                .Include(mpe => mpe.RecipeInstance)
                .ThenInclude(r => r.Recipe)
                .ThenInclude(r => r.RecipeIngredients)
                .ToListAsync();
    }
}