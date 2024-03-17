using Api.Model.Entities;

namespace Api.Dtos;

public class CommodityDto : BaseDto
{
  public string? Name { get; set; }
  public string? Code { get; set; }
  public double Price { get; set; }
  public int Quantity { get; set; }
  public int? AdmissionDocumentId { get; set; }
}
