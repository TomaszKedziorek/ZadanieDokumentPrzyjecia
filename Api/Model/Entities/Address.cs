using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Model.Entities;

public class Address : BaseEntity
{
  [Required]
  public required string Country { get; set; }
  [Required]
  public required string Street { get; set; }
  [Required]
  public required string City { get; set; }
  [Required]
  public required string State { get; set; }
  [Required]
  public required string ZipCode { get; set; }
  [ForeignKey("Supplier")]
  public int SupplierId { get; set; }
  public Supplier Supplier { get; set; }
}