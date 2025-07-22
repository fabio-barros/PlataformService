using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data.Interfaces;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController(IPlatformRepository repository, IMapper mapper) : ControllerBase
    {
        private readonly IPlatformRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        // GET: api/platforms
        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetAllPlatforms()
        {
            var platforms = _repository.GetAllPlatforms();
            var platformDtos = _mapper.Map<IEnumerable<PlatformReadDto>>(platforms);
            return Ok(platformDtos);
        }

        // GET: api/platforms/{id}
        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var platform = _repository.GetPlatformById(id);
            if (platform == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PlatformReadDto>(platform));
        }

        // POST: api/platforms
        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            if (platformCreateDto == null)
            {
                return BadRequest();
            }

            var platformModel = _mapper.Map<Platform>(platformCreateDto);

            _repository.CreatePlatform(platformModel);
            _repository.SaveChanges();

            var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);
            return CreatedAtRoute(nameof(GetPlatformById), new { id = platformReadDto.Id }, platformReadDto);
        }

        // PUT: api/platforms/{id}
        [HttpPut("{id}")]
        public ActionResult UpdatePlatform(int id, PlatformUpdateDto platformUpdateDto)
        {
            if (platformUpdateDto == null)
            {
                return BadRequest();
            }

            var existingPlatform = _repository.GetPlatformById(id);
            if (existingPlatform == null)
            {
                return NotFound();
            }

            _mapper.Map(platformUpdateDto, existingPlatform);

            _repository.UpdatePlatform(existingPlatform);
            _repository.SaveChanges();

            return NoContent();
        }

        // DELETE: api/platforms/{id}
        [HttpDelete("{id}")]
        public ActionResult DeletePlatform(int id)
        {
            var existingPlatform = _repository.GetPlatformById(id);
            if (existingPlatform == null)
            {
                return NotFound();
            }

            _repository.DeletePlatform(id);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}