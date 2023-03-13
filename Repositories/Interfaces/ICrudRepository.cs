using System.Linq.Expressions;

namespace IndustryIncident.Repositories.Interfaces
{
    public interface ICrudRepository<T> where T : class
    {

        void Add(T entity);
        void AddRange(IEnumerable<T> entities);

        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

        int Count();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        T GetSingleOrDefault(Expression<Func<T, bool>> predicate);
        T Get(int id);
        IEnumerable<T> GetAll();
    }
}
