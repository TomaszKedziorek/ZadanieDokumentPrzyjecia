namespace Api.DataAccess.IRepository;
public interface IUnitOfWork
{
  IWarehouseRepository Warehouses { get; }
  ICommodityRepository Commodities { get; }
  ILabelRepository Lables { get; }
  ISupplierRepository Suppliers { get; }
  IAdmissionDocumentRepository AdmissionDocuments { get; }

  Task<bool> SaveAsymc();
}
