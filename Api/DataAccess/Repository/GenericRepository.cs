using System.Linq.Expressions;
using Api.DataAccess.IRepository;
using Api.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.DataAccess.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
  private readonly AppDbContext _dbContext;
  internal DbSet<T> dbSet;

  public GenericRepository(AppDbContext dbContext)
  {
    _dbContext = dbContext;
    dbSet = _dbContext.Set<T>();
  }

  public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true)
  {
    IQueryable<T> query;
    Track(out query, tracked);
    query = query.Where(filter);
    IncludeProperties(ref query, includeProperties);
    return await query.FirstOrDefaultAsync();
  }

  public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, bool tracked = true)
  {
    IQueryable<T> query;
    Track(out query, tracked);
    if (filter != null)
      query = query.Where(filter);
    IncludeProperties(ref query, includeProperties);
    return await query.ToListAsync();
  }

  virtual public async Task AddAsync(T entity)
  {
    await dbSet.AddAsync(entity);
  }

  public void Remove(T entity)
  {
    dbSet.Remove(entity);
  }

  public void RemoveRange(IEnumerable<T> entities)
  {
    dbSet.RemoveRange(entities);
  }

  private void Track(out IQueryable<T> query, bool tracked)
  {
    if (tracked)
    { query = dbSet; }
    else
    { query = dbSet.AsNoTracking(); }
  }

  private void IncludeProperties(ref IQueryable<T> query, string? includeProperties = null)
  {
    if (includeProperties != null)
    {
      foreach (var includProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
      {
        query = query.Include(includProp);
      }
    }
  }
}
