using MealPlannerApi.Data.Entities;
using MealPlannerApi.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MealPlannerApi.Data.Repository
{
    public class UnitMeasureRepository : IRepository<UnitMeasure>
    {
        private readonly MealPlannerDbContext _context;

        public UnitMeasureRepository(MealPlannerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UnitMeasure>> Get()
            => await _context.UnitMeasures.ToListAsync();

        public async Task<UnitMeasure> GetById(int id)
            => await _context.UnitMeasures.FindAsync(id);

        public async Task Add(UnitMeasure entity)
            => await _context.UnitMeasures.AddAsync(entity);

        public void Update(UnitMeasure entity)
        {
            _context.UnitMeasures.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(UnitMeasure entity)
            => _context.UnitMeasures.Remove(entity);

        public IEnumerable<UnitMeasure> Search(Func<UnitMeasure, bool> filter)
            => _context.UnitMeasures.Where(filter).ToList();
    }
}
