using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UdemyProject1.Data;
using UdemyProject1.Models.Domain;
using UdemyProject1.Models.DTO;
using UdemyProject1.Repositories.Implements;

namespace UdemyProject1.Controllers
{
    // https://localhost:portnumber/api/regions
    [Route("api/regions")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        // GET: https://localhost:portnumber/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regionsDomain = await regionRepository.GetAllAsync();

            return Ok(mapper.Map<List<RegionDto>>(regionsDomain));
        }

        // GET: https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{regionId:Guid}")]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid regionId)
        {
            var regionDomain = await regionRepository.GetByIdAsync(regionId);

            if (regionDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        // POST: https://localhost:portnumber/api/regions
        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetRegionById), new { regionId = regionDto.Id }, regionDto);
        }

        // PUT: https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{regionId:Guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid regionId, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);
            // check if the region exists
            regionDomainModel = await regionRepository.UpdateAsync(regionId, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);
        }

        // DELETE: https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{regionId:Guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid regionId)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(regionId);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);
        }
    }
}
