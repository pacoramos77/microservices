using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PlatformController : ControllerBase
  {
    private readonly IPlatformRepo _repository;
    private readonly IMapper _mapper;

    public PlatformController(
      IPlatformRepo repository,
      IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
      Console.WriteLine("--> Getting Platforms...");
      var platforms = _repository.GetAllPlatforms();
      return Ok(platforms.Select((p) => _mapper.Map<PlatformReadDto>(p)));
    }

    [HttpGet("{id}", Name = "GetPlatformById")]
    public ActionResult<PlatformReadDto> GetPlatformById(int id)
    {
      var platformItem = _repository.GetPlatformById(id);
      if (platformItem is null)
      {
        return NotFound();
      }

      return Ok(_mapper.Map<PlatformReadDto>(platformItem));
    }

    [HttpPost]
    public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto)
    {
      var platformModel = _mapper.Map<Platform>(platformCreateDto);
      _repository.CreatePlatform(platformModel);
      _repository.SaveChanges();

      var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

      return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
    }


  }
}
