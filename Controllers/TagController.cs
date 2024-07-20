using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UdemyProject1.CustomActionFilters;
using UdemyProject1.Helpers;
using UdemyProject1.Models.Domain;
using UdemyProject1.Models.DTO.Tag;
using UdemyProject1.Repositories.Interfaces;

namespace UdemyProject1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository tagRepository;
        private readonly IMapper mapper;

        public TagController(ITagRepository tagRepository, IMapper mapper)
        {
            this.tagRepository = tagRepository;
            this.mapper = mapper;
        }

        // GET: https://localhost:portnumber/api/tags
        [HttpGet]
        public async Task<IActionResult> GetAllDifficulties()
        {
            var tagDomain = await tagRepository.GetAllAsync();
            var apiResponse = new APISucessResponse(
                statusCode: HttpStatusCode.OK,
                message: "Successfully get all tags",
                data: mapper.Map<List<TagDto>>(tagDomain)
            );
            return Ok(apiResponse);
        }

        // GET: https://localhost:portnumber/api/tags/{id}
        [HttpGet]
        [Route("{tagId:Guid}")]
        public async Task<IActionResult> GetTagById([FromRoute] Guid tagId)
        {
            var tagDomain = await tagRepository.GetByIdAsync(tagId);

            if (tagDomain == null)
            {
                return NotFound();
            }

            // return Ok(mapper.Map<TagDto>(tagDomain));
            var apiResponse = new APISucessResponse(
                statusCode: HttpStatusCode.OK,
                message: "Successfully get tag by id",
                data: mapper.Map<TagDto>(tagDomain)
            );
            return Ok(apiResponse);
        }

        // POST: https://localhost:portnumber/api/tags
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateTag([FromBody] AddTagDto addTagRequestDto)
        {
            var tagDomainModel = mapper.Map<Tag>(addTagRequestDto);

            tagDomainModel = await tagRepository.CreateAsync(tagDomainModel);

            var tagDto = mapper.Map<TagDto>(tagDomainModel);

            // return CreatedAtAction(nameof(GetTagById), new { tagId = tagDto.Id }, tagDto);
            var apiResponse = new APISucessResponse(
                statusCode: HttpStatusCode.Created,
                message: "Successfully created tag",
                data: tagDto
            );
            return CreatedAtAction(nameof(GetTagById), new { tagId = tagDto.Id }, apiResponse);
        }

        // PUT: https://localhost:portnumber/api/tags/{id}
        [HttpPut]
        [Route("{tagId:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateTag([FromRoute] Guid tagId, [FromBody] AddTagDto updateTagRequestDto)
        {
            var tagDomainModel = mapper.Map<Tag>(updateTagRequestDto);
            // check if the tag exists
            tagDomainModel = await tagRepository.UpdateAsync(tagId, tagDomainModel);

            if (tagDomainModel == null)
            {
                return NotFound();
            }

            var tagDto = mapper.Map<TagDto>(tagDomainModel);

            var apiResponse = new APISucessResponse(
                statusCode: HttpStatusCode.OK,
                message: "Successfully updated tag",
                data: tagDto
            );

            return Ok(apiResponse);
        }

        // DELETE: https://localhost:portnumber/api/tags/{id}
        [HttpDelete]
        [Route("{tagId:Guid}")]
        public async Task<IActionResult> DeleteTag([FromRoute] Guid tagId)
        {
            var tagDomainModel = await tagRepository.DeleteAsync(tagId);

            if (tagDomainModel == null)
            {
                return NotFound();
            }

            var tagDto = mapper.Map<TagDto>(tagDomainModel);

            var apiResponse = new APISucessResponse(
                statusCode: HttpStatusCode.OK,
                message: "Successfully deleted tag",
                data: tagDto
            );

            return Ok(apiResponse);
        }
    }
}
