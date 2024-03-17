using Api.Model.Entities;

namespace Api.DataAccess.IRepository;

public interface ICommodityRepository : IGenericRepository<Commodity>
{
  Task Update(Commodity item);
}

