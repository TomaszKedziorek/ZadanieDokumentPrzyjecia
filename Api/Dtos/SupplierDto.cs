namespace Api.Dtos;

public class SupplierDto : BaseDto
{
  public required string CompanyName { get; set; }
  public int AddressId { get; set; }
  public required string Country { get; set; }
  public required string Street { get; set; }
  public required string City { get; set; }
  public required string State { get; set; }
  public required string ZipCode { get; set; }
}