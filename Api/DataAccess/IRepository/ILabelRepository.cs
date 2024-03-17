using Api.Model.Entities;

namespace Api.DataAccess.IRepository;

public interface ILabelRepository : IGenericRepository<Label>
{
  Task Update(Label item);
}

