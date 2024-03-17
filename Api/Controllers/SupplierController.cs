using Api.DataAccess.IRepository;
using Api.Dtos;
using Api.Errors;
using Api.Model.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SupplierController : ControllerBase
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;
  public SupplierController(IUnitOfWork unitOfWork, IMapper mapper)
  {
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  [HttpGet("{id}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
  public async Task<ActionResult<SupplierDto>> GetById(int id)
  {
    Supplier? supplier = await _unitOfWork.Suppliers
              .GetFirstOrDefaultAsync(x => x.Id == id, includeProperties: "Address");

    if (supplier == null)
      return NotFound(new ApiResponse(404));

    var supplierDto = _mapper.Map<SupplierDto>(supplier);
    return Ok(supplierDto);
  }

  [HttpGet]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public async Task<ActionResult<IReadOnlyList<SupplierDto>>> GetAll()
  {
    IEnumerable<Supplier> suppliers = await _unitOfWork.Suppliers.GetAllAsync(includeProperties: "Address");
    var supplierDtos = _mapper.Map<IReadOnlyList<SupplierDto>>(suppliers);
    return Ok(supplierDtos);
  }

  [HttpPost]
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult> Create([FromBody] SupplierDto dto)
  {
    Supplier supplier = _mapper.Map<Supplier>(dto);
    await _unitOfWork.Suppliers.AddAsync(supplier);
    var result = await _unitOfWork.SaveAsymc();
    return Created($"api/Supplier/{supplier.Id}", supplier);
  }

  [HttpPut("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
  public async Task<ActionResult> Update([FromRoute] int id, [FromBody] SupplierDto dto)
  {
    Supplier? supplierFromDb = await _unitOfWork.Suppliers
        .GetFirstOrDefaultAsync(x => x.Id == id, includeProperties: "Address", tracked: false);

    if (supplierFromDb == null)
      return NotFound(new ApiResponse(404));

    Supplier supplier = _mapper.Map<Supplier>(dto);
    await _unitOfWork.Suppliers.Update(supplier);
    await _unitOfWork.SaveAsymc();
    return NoContent();
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
  public async Task<ActionResult> Delete([FromRoute] int id)
  {
    Supplier? supplierFromDb = await _unitOfWork.Suppliers
        .GetFirstOrDefaultAsync(x => x.Id == id, includeProperties: "Address");

    if (supplierFromDb == null)
      return NotFound(new ApiResponse(404));

    _unitOfWork.Suppliers.Remove(supplierFromDb);
    await _unitOfWork.SaveAsymc();
    return NoContent();
  }

}

