using Api.DataAccess.IRepository;
using Api.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.DataAccess.Repository;

public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
{
  private readonly AppDbContext _dbContext;

  public SupplierRepository(AppDbContext dbContext) : base(dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task Update(Supplier item)
  {
    Supplier supplierFromDb = await _dbContext.Suppliers
      .Where(x => x.Id == item.Id).Include(x => x.Address).FirstAsync();

    supplierFromDb.CompanyName = item.CompanyName;
    supplierFromDb.Address.Country = item.Address.Country;
    supplierFromDb.Address.State = item.Address.State;
    supplierFromDb.Address.City = item.Address.City;
    supplierFromDb.Address.Street = item.Address.Street;
    supplierFromDb.Address.ZipCode = item.Address.ZipCode;
  }
}

