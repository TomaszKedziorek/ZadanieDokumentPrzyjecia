namespace Api.Dtos;

public class AddressDto : BaseDto
{
  public required string Country { get; set; }
  public required string Street { get; set; }
  public required string City { get; set; }
  public required string State { get; set; }
  public required string ZipCode { get; set; }
}