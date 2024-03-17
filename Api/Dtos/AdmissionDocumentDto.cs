using Api.Model.Entities;

namespace Api.Dtos;

public class AdmissionDocumentDto : BaseDto
{
  public int? TargetWarehouseId { get; set; }
  public WarehouseDto? TargetWarehouse { get; set; }
  public int? SupplierId { get; set; }
  public SupplierDto? Supplier { get; set; }
  public ICollection<LabelDto> Labels { get; set; } = new List<LabelDto>();
  public ICollection<CommodityDto> CommodityList { get; set; } = new List<CommodityDto>();
  public bool Canceled { get; set; }
  public bool Approved { get; set; }
}
