using Api.Model.Entities;

namespace Api.DataAccess.IRepository;

public interface IWarehouseRepository : IGenericRepository<Warehouse>
{
  Task Update(Warehouse item);
}

