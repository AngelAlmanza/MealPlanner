using MealPlannerApi.Data.Entities;
using MealPlannerApi.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MealPlannerApi.Data.Repository
{
    public class RecipeRepository : IRepository<Recipe>
    {
        private readonly MealPlannerDbContext _context;
    
        public RecipeRepository(MealPlannerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Recipe>> Get()
            => await _context.Recipes
                .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .ThenInclude(i => i.UnitMeasure)
                .ToListAsync();
    
        public async Task<Recipe> GetById(int id)
            => await _context.Recipes
                .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .ThenInclude(i => i.UnitMeasure)
                .FirstOrDefaultAsync(r => r.Id == id);
    
        public async Task Add(Recipe entity)
            => await _context.Recipes.AddAsync(entity);

        public void Update(Recipe entity)
        {
            _context.Recipes.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    
        public void Delete(Recipe entity)
            => _context.Recipes.Remove(entity);
    
        public IEnumerable<Recipe> Search(Func<Recipe, bool> filter)
            => _context.Recipes
                .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .ThenInclude(i => i.UnitMeasure)
                .AsEnumerable()
                .Where(filter)
                .ToList();
        
    }
}