using MealPlannerApi.Data.Entities;
using MealPlannerApi.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MealPlannerApi.Data.Repository
{
    public class MealPlanWeekRepository : IRepository<MealPlanWeek>
    {
        private readonly MealPlannerDbContext _context;
        
        public MealPlanWeekRepository(MealPlannerDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<MealPlanWeek>> Get()
            => await _context.MealPlanWeeks.ToListAsync();
        
        public async Task<MealPlanWeek> GetById(int id)
            => await _context.MealPlanWeeks.FirstOrDefaultAsync(m => m.Id == id);
        
        public async Task Add(MealPlanWeek entity)
            => await _context.MealPlanWeeks.AddAsync(entity);
        
        public void Update(MealPlanWeek entity)
        {
            _context.MealPlanWeeks.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        
        public void Delete(MealPlanWeek entity)
            => _context.MealPlanWeeks.Remove(entity);
        
        public IEnumerable<MealPlanWeek> Search(Func<MealPlanWeek, bool> filter)
            => _context.MealPlanWeeks
                .AsEnumerable()
                .Where(filter)
                .ToList();
    }    
}
