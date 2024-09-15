using BlogAPI.DTOs.TagDTOs;
using BlogAPI.Models;
using BlogAPI.Repository.TagRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [Route("api/tag")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository _tagRepo;
        public TagController(ITagRepository tagRepo)
        {
            _tagRepo = tagRepo;
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAllAsync()
        {
            var tags = await _tagRepo.GetAllAsync();

            var tagDTOs = tags.Select(t => new TagDTO
            {
                Id = t.Id,
                Name = t.Name
            }).ToList();

            return Ok(tagDTOs);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var tag = await _tagRepo.GetByIdAsync(id);

            if (tag == null)
                return NotFound($"No Tag With {id} Was Found!");

            var tagDTO = new TagDTO
            {
                Id = tag.Id,
                Name = tag.Name
            };

            return Ok(tagDTO);
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTagDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tag = new Tag
            {
                Name = dto.Name
            };

            await _tagRepo.CreateAsync(tag);

            var tagDTO = new TagDTO
            {
                Id = tag.Id,
                Name = tag.Name
            };

            return Ok(tagDTO);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTagDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tag = await _tagRepo.GetByIdAsync(id);

            if (tag == null)
                return NotFound($"No Tag With {id} Was Found!");

            tag.Name = dto.Name;

            _tagRepo.Update(tag);

            var tagDTO = new TagDTO
            {
                Id = tag.Id,
                Name = tag.Name
            };

            return Ok(tagDTO);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tag = await _tagRepo.GetByIdAsync(id);

            if (tag == null)
                return NotFound($"No Tag With {id} Was Found!");

            _tagRepo.Delete(tag);

            var tagDTO = new TagDTO
            {
                Id = tag.Id,
                Name = tag.Name
            };

            return Ok(tagDTO);
        }
    }
}
