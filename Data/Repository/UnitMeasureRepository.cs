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
            => await _context.unitMeasures.ToListAsync();

        public async Task<UnitMeasure> GetById(int id)
            => await _context.unitMeasures.FindAsync(id);

        public async Task Add(UnitMeasure entity)
            => await _context.unitMeasures.AddAsync(entity);

        public void Update(UnitMeasure entity)
        {
            _context.unitMeasures.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(UnitMeasure entity)
            => _context.unitMeasures.Remove(entity);

        public IEnumerable<UnitMeasure> Search(Func<UnitMeasure, bool> filter)
            => _context.unitMeasures.Where(filter).ToList();
    }
}
