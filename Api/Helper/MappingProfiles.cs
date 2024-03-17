using Api.Dtos;
using Api.Model.Entities;
using AutoMapper;

namespace Api.Helper;

public class MappingProfiles : Profile
{
  public MappingProfiles()
  {
    CreateMap<Warehouse, WarehouseDto>();
    CreateMap<Label, LabelDto>();
    CreateMap<LabelDto, Label>();

    CreateMap<Address, AddressDto>();
    CreateMap<Supplier, SupplierDto>()
      .ForMember(a => a.AddressId, s => s.MapFrom(d => d.Address.Id))
      .ForMember(a => a.Country, s => s.MapFrom(d => d.Address.Country))
      .ForMember(a => a.State, s => s.MapFrom(d => d.Address.State))
      .ForMember(a => a.City, s => s.MapFrom(d => d.Address.City))
      .ForMember(a => a.Street, s => s.MapFrom(d => d.Address.Street))
      .ForMember(a => a.ZipCode, s => s.MapFrom(d => d.Address.ZipCode));

    CreateMap<SupplierDto, Supplier>()
      .ForMember(a => a.Address, s => s.MapFrom(d => new Address()
      { Id = d.AddressId, Country = d.Country, Street = d.Street, City = d.City, State = d.State, ZipCode = d.ZipCode }));

    CreateMap<Commodity, CommodityDto>();
    CreateMap<CommodityDto, Commodity>();

    CreateMap<AdmissionDocument, AdmissionDocumentDto>();
    CreateMap<AdmissionDocumentDto, AdmissionDocument>();
  }
}
