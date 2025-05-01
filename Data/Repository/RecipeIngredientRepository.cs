using MealPlannerApi.Data.Entities;
using MealPlannerApi.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MealPlannerApi.Data.Repository
{
    public class RecipeIngredientRepository : IRepository<RecipeIngredient>, IBulkInsertRepository<RecipeIngredient>
    {
        private readonly MealPlannerDbContext _context;

        public RecipeIngredientRepository(MealPlannerDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<RecipeIngredient>> Get()
            => await _context.RecipeIngredients.Include(record => record.Ingredient).ToListAsync();
        
        public async Task<RecipeIngredient> GetById(int id)
            => await _context.RecipeIngredients
                .Include(record => record.Ingredient)
                .FirstOrDefaultAsync(record => record.Id == id);
        
        public async Task Add(RecipeIngredient entity)
            => await _context.RecipeIngredients.AddAsync(entity);
        
        public void Update(RecipeIngredient entity)
        {
            _context.RecipeIngredients.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        
        public void Delete(RecipeIngredient entity)
            => _context.RecipeIngredients.Remove(entity);
        
        public IEnumerable<RecipeIngredient> Search(Func<RecipeIngredient, bool> filter)
            => _context.RecipeIngredients.Include(i => i.Ingredient).Where(filter).ToList();
        
        public async Task AddRange(IEnumerable<RecipeIngredient> entities)
            => _context.RecipeIngredients.AddRange(entities);

        public async Task DeleteRange(int id)
        {
            var recipeIngredients = await _context.RecipeIngredients
                .Where(ri => ri.RecipeId == id)
                .ToListAsync();
            _context.RecipeIngredients.RemoveRange(recipeIngredients);
        }
    }
}