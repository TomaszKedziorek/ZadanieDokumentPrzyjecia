using System.Collections.ObjectModel;
using Api.DataAccess.IRepository;
using Api.Model.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api.DataAccess.Repository;

public class AdmissionDocumentRepository : GenericRepository<AdmissionDocument>, IAdmissionDocumentRepository
{
  private readonly AppDbContext _dbContext;

  public AdmissionDocumentRepository(AppDbContext dbContext) : base(dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task Update(AdmissionDocument item)
  {
    var adDb = await _dbContext.AdmissionDocuments.Where(x => x.Id == item.Id)
                    .Include(x => x.Labels)
                    .Include(x => x.CommodityList)
                    .FirstOrDefaultAsync();
    if (adDb != null)
    {
      adDb.Approved = item.Approved;
      adDb.Canceled = item.Canceled;
      adDb.SupplierId = item.SupplierId;
      adDb.TargetWarehouseId = item.TargetWarehouseId;
      HandleLabels(adDb.Labels, item.Labels);
      if (item.Id == 0)
        adDb.CommodityList = item.CommodityList;
    }
  }

  private void HandleLabels(ICollection<Label> labelsFromDb, ICollection<Label> labelsFromClient)
  {
    List<Label> labelsToRemove = labelsFromDb.Except(labelsFromClient).ToList();
    foreach (var label in labelsToRemove)
    {
      labelsFromDb.Remove(label);
    }
    foreach (var label in labelsFromClient)
    {
      if (!labelsFromDb.Any(x => x.Id == label.Id))
        labelsFromDb.Add(label);
    }
  }

  public override async Task AddAsync(AdmissionDocument entity)
  {
    //First Way of Adding entity with collection with many-to-many relation
    // entity.Labels = _dbContext.Labels.Where(labels => entity.Labels.Contains(labels)).ToList();
    //Second Way of Adding entity with collection with many-to-many relation
    _dbContext.AdmissionDocuments.Attach(entity);
    await _dbContext.AdmissionDocuments.AddAsync(entity);
  }
}

