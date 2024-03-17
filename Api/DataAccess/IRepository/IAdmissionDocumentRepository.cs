using Api.Model.Entities;

namespace Api.DataAccess.IRepository;

public interface IAdmissionDocumentRepository:IGenericRepository<AdmissionDocument>
{
  Task Update(AdmissionDocument item);
}

