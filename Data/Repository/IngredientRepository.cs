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
            => await _context.ingredients.Include(record => record.UnitMeasure).ToListAsync();

        public async Task<Ingredient> GetById(int id)
            => await _context.ingredients.Include(record => record.UnitMeasure).FirstOrDefaultAsync(record => record.Id == id);

        public async Task Add(Ingredient entity)
            => await _context.ingredients.AddAsync(entity);

        public void Update(Ingredient entity)
        {
            _context.ingredients.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Ingredient entity)
            => _context.ingredients.Remove(entity);

        public IEnumerable<Ingredient> Search(Func<Ingredient, bool> filter)
            => _context.ingredients.Include(i => i.UnitMeasure).Where(filter).ToList();
    }
}
