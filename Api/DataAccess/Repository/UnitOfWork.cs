using Api.DataAccess.IRepository;

namespace Api.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{

  private readonly AppDbContext _dbContext;
  public IWarehouseRepository Warehouses { get; private set; }
  public ICommodityRepository Commodities { get; private set; }
  public ILabelRepository Lables { get; private set; }
  public ISupplierRepository Suppliers { get; private set; }
  public IAdmissionDocumentRepository AdmissionDocuments { get; private set; }

  public UnitOfWork(AppDbContext dbContext)
  {
    _dbContext = dbContext;
    Warehouses = new WarehouseRepository(dbContext);
    Commodities = new CommodityRepository(dbContext);
    Lables = new LabelsRepository(dbContext);
    Suppliers = new SupplierRepository(dbContext);
    AdmissionDocuments = new AdmissionDocumentRepository(dbContext);
  }

  public async Task<bool> SaveAsymc()
  {
    var saved = await _dbContext.SaveChangesAsync();
    return saved > 0;
  }
}
