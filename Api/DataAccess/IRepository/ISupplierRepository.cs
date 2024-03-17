using Api.Model.Entities;

namespace Api.DataAccess.IRepository;

public interface ISupplierRepository:IGenericRepository<Supplier>
{
  Task Update(Supplier item);
}

