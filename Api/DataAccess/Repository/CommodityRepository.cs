using Api.DataAccess.IRepository;
using Api.Model.Entities;

namespace Api.DataAccess.Repository;

public class CommodityRepository : GenericRepository<Commodity>, ICommodityRepository
{
  private readonly AppDbContext _dbContext;

  public CommodityRepository(AppDbContext dbContext) : base(dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task Update(Commodity item)
  {
    _dbContext.Commodities.Update(item);
  }
}

