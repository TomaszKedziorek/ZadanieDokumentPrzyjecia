using Api.DataAccess.IRepository;
using Api.Model.Entities;

namespace Api.DataAccess.Repository;

public class WarehouseRepository : GenericRepository<Warehouse>, IWarehouseRepository
{
  private readonly AppDbContext _dbContext;

  public WarehouseRepository(AppDbContext dbContext) : base(dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task Update(Warehouse item)
  {
    _dbContext.Warehouses.Update(item);
  }
}

