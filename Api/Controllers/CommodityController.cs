using Microsoft.AspNetCore.Mvc;
using Api.DataAccess.IRepository;
using Api.Dtos;
using Api.Errors;
using Api.Model.Entities;
using AutoMapper;
using Bogus;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommodityController : ControllerBase
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;
  public CommodityController(IUnitOfWork unitOfWork, IMapper mapper)
  {
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  [HttpGet("{id}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
  public async Task<ActionResult<CommodityDto>> GetById(int id)
  {
    Commodity? commodity = await _unitOfWork.Commodities
              .GetFirstOrDefaultAsync(x => x.Id == id);

    if (commodity == null)
      return NotFound(new ApiResponse(404));

    var commodityDto = _mapper.Map<CommodityDto>(commodity);
    return Ok(commodityDto);
  }

  [HttpGet]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public async Task<ActionResult<IReadOnlyList<CommodityDto>>> GetAll()
  {
    IEnumerable<Commodity> commodities = await _unitOfWork.Commodities.GetAllAsync();
    var commodityDtos = _mapper.Map<IReadOnlyList<CommodityDto>>(commodities);
    return Ok(commodityDtos);
  }

  [HttpGet()]
  [Route("admissionDocument")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public async Task<ActionResult<IReadOnlyList<CommodityDto>>> GetAll([FromQuery] int? addmisionDocId)
  {
    IEnumerable<Commodity> commodities = await _unitOfWork.Commodities
    .GetAllAsync(x => x.AdmissionDocumentId == addmisionDocId,includeProperties:"AdmissionDocument");
    var commodityDtos = _mapper.Map<IReadOnlyList<CommodityDto>>(commodities);
    return Ok(commodityDtos);
  }

  [HttpPost]
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult> Create([FromBody] CommodityDto dto)
  {
    Commodity commodity = _mapper.Map<Commodity>(dto);
    await _unitOfWork.Commodities.AddAsync(commodity);
    var result = await _unitOfWork.SaveAsymc();
    return Created($"api/Commodity/{commodity.Id}", commodity);
  }

  [HttpPut("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
  public async Task<ActionResult> Update([FromRoute] int id, [FromBody] CommodityDto dto)
  {
    Commodity? commodityFromDb = await _unitOfWork.Commodities
        .GetFirstOrDefaultAsync(x => x.Id == id, tracked: false);

    if (commodityFromDb == null)
      return NotFound(new ApiResponse(404));

    Commodity commodity = _mapper.Map<Commodity>(dto);
    await _unitOfWork.Commodities.Update(commodity);
    await _unitOfWork.SaveAsymc();
    return NoContent();
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
  public async Task<ActionResult> Delete([FromRoute] int id)
  {
    Commodity? commodityFromDb = await _unitOfWork.Commodities
        .GetFirstOrDefaultAsync(x => x.Id == id, tracked: false);

    if (commodityFromDb == null)
      return NotFound(new ApiResponse(404));

    _unitOfWork.Commodities.Remove(commodityFromDb);
    await _unitOfWork.SaveAsymc();
    return NoContent();
  }

}

