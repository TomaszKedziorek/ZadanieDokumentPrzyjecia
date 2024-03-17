using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Model.Entities;

public class Supplier : BaseEntity
{
  [Required]
  public required string CompanyName { get; set; }
  public int AddressId { get; set; }
  public required Address Address { get; set; }
  public ICollection<AdmissionDocument> AdmissionDocuments { get; set; } = new List<AdmissionDocument>();
}
