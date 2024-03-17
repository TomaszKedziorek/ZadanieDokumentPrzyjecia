using Microsoft.AspNetCore.Mvc;
using Api.DataAccess.IRepository;
using Api.Dtos;
using Api.Errors;
using Api.Model.Entities;
using AutoMapper;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdmissionDocumentController : ControllerBase
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;
  public AdmissionDocumentController(IUnitOfWork unitOfWork, IMapper mapper)
  {
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }
  [HttpGet("{id}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
  public async Task<ActionResult<AdmissionDocumentDto>> GetById(int id)
  {
    AdmissionDocument? document = await _unitOfWork.AdmissionDocuments
        .GetFirstOrDefaultAsync(x => x.Id == id, includeProperties: "Supplier,CommodityList,TargetWarehouse,Labels");

    if (document == null)
      return NotFound(new ApiResponse(404));

    var documentDto = _mapper.Map<AdmissionDocumentDto>(document);
    return Ok(documentDto);
  }

  [HttpGet]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public async Task<ActionResult<IReadOnlyList<AdmissionDocumentDto>>> GetAll()
  {
    IEnumerable<AdmissionDocument> documents = await _unitOfWork.AdmissionDocuments
        .GetAllAsync(includeProperties: "Supplier,CommodityList,TargetWarehouse,Labels");
    var documentDtos = _mapper.Map<IReadOnlyList<AdmissionDocumentDto>>(documents);
    return Ok(documentDtos);
  }


  [HttpPost]
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult> Create([FromBody] AdmissionDocumentDto dto)
  {
    AdmissionDocument document = _mapper.Map<AdmissionDocument>(dto);
    await _unitOfWork.AdmissionDocuments.AddAsync(document);
    var result = await _unitOfWork.SaveAsymc();
    return Created($"api/AdmissionDocuments/{document.Id}", document);
  }
  
  [HttpPut("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
  public async Task<ActionResult> Update([FromRoute] int id, [FromBody] AdmissionDocumentDto dto)
  {
    AdmissionDocument? documentFromDb = await _unitOfWork.AdmissionDocuments
        .GetFirstOrDefaultAsync(x => x.Id == id, tracked: false);

    if (documentFromDb == null)
      return NotFound(new ApiResponse(404));

    AdmissionDocument document = _mapper.Map<AdmissionDocument>(dto);
    await _unitOfWork.AdmissionDocuments.Update(document);
    await _unitOfWork.SaveAsymc();
    return NoContent();
  }
}

