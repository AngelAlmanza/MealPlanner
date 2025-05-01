namespace MealPlannerApi.Data.Repository.IRepository
{
    public interface IBulkInsertRepository <T> where T : class
    {
        Task AddRange(IEnumerable<T> entities);
        Task DeleteRange(int id);
    }
}