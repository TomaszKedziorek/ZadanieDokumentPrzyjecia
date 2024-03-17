using System.ComponentModel.DataAnnotations;

namespace Api.Model.Entities;

public class Warehouse : BaseEntity
{
  [Required]
  public required string Name { get; set; }
  [Required]
  public required string Symbol { get; set; }
  public ICollection<AdmissionDocument> AdmissionDocuments { get; set; } = new List<AdmissionDocument>();
}
