using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Model.Entities;

public class AdmissionDocument : BaseEntity
{
  [ForeignKey("TargetWarehouse")]
  public int? TargetWarehouseId { get; set; }
  public Warehouse? TargetWarehouse { get; set; }
  [ForeignKey("Supplier")]
  public int? SupplierId { get; set; }
  public Supplier? Supplier { get; set; }
  public ICollection<Label> Labels { get; set; } = new List<Label>();
  public ICollection<Commodity> CommodityList { get; set; } = new List<Commodity>();
  public bool Canceled { get; set; }
  public bool Approved { get; set; }
}
