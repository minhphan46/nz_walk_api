using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UdemyProject1.CustomActionFilters;
using UdemyProject1.Models.Domain;
using UdemyProject1.Models.DTO.Difficulty;
using UdemyProject1.Repositories.Interfaces;

namespace UdemyProject1.Controllers
{
    [Route("api/difficulties")]
    [ApiController]
    public class DifficultyController : ControllerBase
    {
        private readonly IDifficultyRepository difficultyRepository;
        private readonly IMapper mapper;

        public DifficultyController(IDifficultyRepository difficultyRepository, IMapper mapper)
        {
            this.difficultyRepository = difficultyRepository;
            this.mapper = mapper;
        }

        // GET: https://localhost:portnumber/api/difficulties
        [HttpGet]
        public async Task<IActionResult> GetAllDifficulties()
        {
            var difficultyDomain = await difficultyRepository.GetAllAsync();
            return Ok(mapper.Map<List<DifficultyDto>>(difficultyDomain));
        }

        // GET: https://localhost:portnumber/api/difficulties/{id}
        [HttpGet]
        [Route("{difficultyId:Guid}")]
        public async Task<IActionResult> GetDifficultyById([FromRoute] Guid difficultyId)
        {
            var difficultyDomain = await difficultyRepository.GetByIdAsync(difficultyId);

            if (difficultyDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<DifficultyDto>(difficultyDomain));
        }

        // POST: https://localhost:portnumber/api/difficulties
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateDifficulty([FromBody] AddDifficultyDto addDifficultyRequestDto)
        {
            var difficultyDomainModel = mapper.Map<Difficulty>(addDifficultyRequestDto);

            difficultyDomainModel = await difficultyRepository.CreateAsync(difficultyDomainModel);

            var difficultyDto = mapper.Map<DifficultyDto>(difficultyDomainModel);

            return CreatedAtAction(nameof(GetDifficultyById), new { difficultyId = difficultyDto.Id }, difficultyDto);
        }

        // PUT: https://localhost:portnumber/api/difficulties/{id}
        [HttpPut]
        [Route("{difficultyId:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateDifficulty([FromRoute] Guid difficultyId, [FromBody] AddDifficultyDto updateDifficultyRequestDto)
        {
            var difficultyDomainModel = mapper.Map<Difficulty>(updateDifficultyRequestDto);
            // check if the difficulty exists
            difficultyDomainModel = await difficultyRepository.UpdateAsync(difficultyId, difficultyDomainModel);

            if (difficultyDomainModel == null)
            {
                return NotFound();
            }

            var difficultyDto = mapper.Map<DifficultyDto>(difficultyDomainModel);

            return Ok(difficultyDto);
        }

        // DELETE: https://localhost:portnumber/api/difficulties/{id}
        [HttpDelete]
        [Route("{difficultyId:Guid}")]
        public async Task<IActionResult> DeleteDifficulty([FromRoute] Guid difficultyId)
        {
            var difficultyDomainModel = await difficultyRepository.DeleteAsync(difficultyId);

            if (difficultyDomainModel == null)
            {
                return NotFound();
            }

            var difficultyDto = mapper.Map<DifficultyDto>(difficultyDomainModel);

            return Ok(difficultyDto);
        }
    }
}
