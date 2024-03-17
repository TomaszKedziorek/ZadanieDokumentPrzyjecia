using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Model.Entities;

public class Commodity : BaseEntity
{
  [Required]
  public required string Name { get; set; }
  [Required]
  public required string Code { get; set; }
  [Required]
  public double Price { get; set; }
  [Required]
  public int Quantity { get; set; }
  [ForeignKey("AdmissionDocument")]
  public int? AdmissionDocumentId { get; set; }
  public AdmissionDocument? AdmissionDocument { get; set; }
}
