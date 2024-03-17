using System.ComponentModel.DataAnnotations;

namespace Api.Model.Entities;

public class Label : BaseEntity
{
  [Required]
  public required string Name { get; set; }
  public ICollection<AdmissionDocument> AdmissionDocuments { get; set; } = new List<AdmissionDocument>();

  public override bool Equals(object other)
  {
    return Equals(other as Label);
  }

  public virtual bool Equals(Label other)
  {
    if (other == null) { return false; }
    if (object.ReferenceEquals(this, other)) { return true; }
    return this.Id == other.Id;
  }

  public override int GetHashCode()
  {
    return this.Id;
  }
}
