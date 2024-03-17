using System.Linq.Expressions;
using Api.Model.Entities;
namespace Api.DataAccess.IRepository;

public interface IGenericRepository<T> where T : BaseEntity
{
  Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true);
  Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, bool tracked = true);
  Task AddAsync(T entity);
  void Remove(T entity);
  void RemoveRange(IEnumerable<T> entities);
}
