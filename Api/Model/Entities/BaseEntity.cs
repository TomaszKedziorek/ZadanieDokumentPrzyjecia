using System.ComponentModel.DataAnnotations;

namespace Api.Model.Entities;

public class BaseEntity
{
  [Key]
  public int Id { get; set; }
}
