using Api.DataAccess.IRepository;
using Api.Dtos;
using Api.Model.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LabelController : ControllerBase
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;
  public LabelController(IUnitOfWork unitOfWork, IMapper mapper)
  {
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  [HttpGet]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public async Task<ActionResult<IReadOnlyList<LabelDto>>> GetAll()
  {
    IEnumerable<Label> labels = await _unitOfWork.Lables.GetAllAsync();
    var labelDtos = _mapper.Map<IReadOnlyList<LabelDto>>(labels);
    return Ok(labelDtos);
  }

}

