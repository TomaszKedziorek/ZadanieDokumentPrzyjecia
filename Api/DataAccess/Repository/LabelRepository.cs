using Api.DataAccess.IRepository;
using Api.Model.Entities;

namespace Api.DataAccess.Repository;

public class LabelsRepository : GenericRepository<Label>, ILabelRepository
{
  private readonly AppDbContext _dbContext;

  public LabelsRepository(AppDbContext dbContext) : base(dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task Update(Label item)
  {
    _dbContext.Labels.Update(item);
  }
}

