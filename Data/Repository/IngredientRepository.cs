using MealPlannerApi.Data.Entities;
using MealPlannerApi.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MealPlannerApi.Data.Repository
{
    public class IngredientRepository : IRepository<Ingredient>
    {
        private readonly MealPlannerDbContext _context;

        public IngredientRepository(MealPlannerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ingredient>> Get()
            => await _context.Ingredients.Include(record => record.UnitMeasure).ToListAsync();

        public async Task<Ingredient> GetById(int id)
            => await _context.Ingredients.Include(record => record.UnitMeasure).FirstOrDefaultAsync(record => record.Id == id);

        public async Task Add(Ingredient entity)
            => await _context.Ingredients.AddAsync(entity);

        public void Update(Ingredient entity)
        {
            _context.Ingredients.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Ingredient entity)
            => _context.Ingredients.Remove(entity);

        public IEnumerable<Ingredient> Search(Func<Ingredient, bool> filter)
            => _context.Ingredients.Include(i => i.UnitMeasure).Where(filter).ToList();
    }
}
