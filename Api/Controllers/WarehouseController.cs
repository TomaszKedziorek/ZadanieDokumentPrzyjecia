using Api.DataAccess.IRepository;
using Api.Dtos;
using Api.Errors;
using Api.Model.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WarehouseController : ControllerBase
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;

  public WarehouseController(IUnitOfWork unitOfWork, IMapper mapper)
  {
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  [HttpGet("{id}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
  public async Task<ActionResult<WarehouseDto>> GetWarehouse(int id)
  {
    Warehouse? warehouse = await _unitOfWork.Warehouses.GetFirstOrDefaultAsync(x => x.Id == id);
    if (warehouse == null)
      return NotFound(new ApiResponse(404));
    var warehouseDto = _mapper.Map<WarehouseDto>(warehouse);
    return Ok(warehouseDto);
  }

  [HttpGet]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public async Task<ActionResult<IReadOnlyList<WarehouseDto>>> GetWarehouses()
  {
    IEnumerable<Warehouse> warehouses = await _unitOfWork.Warehouses.GetAllAsync();
    var warehouseDtos = _mapper.Map<IReadOnlyList<WarehouseDto>>(warehouses);
    return Ok(warehouseDtos);
  }
}

