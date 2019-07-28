using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tenu.Core.Interfaces;
using Tenu.Core.Models;

namespace Tenu.BackOffice.Api
{
    [Route("api/[controller]")]
    public class ContentController : ControllerBase
    {
        private readonly IContentRepository _contentRepository;

        public ContentController(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        [HttpGet]
        public async Task<Content[]> GetAtRoot()
        {
            return (await _contentRepository.GetAtRoot()).OrderBy(x => x.Name).ToArray();
        }

        [HttpGet("{id}")]
        public async Task<Content> GetById(Guid id)
        {
            return await _contentRepository.GetById(id);
        }

        [HttpGet("{id}/children")]
        public async Task<Content[]> GetChildren(Guid id)
        {
            return (await _contentRepository.GetChildren(id)).OrderBy(x => x.Name).ToArray();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            await _contentRepository.Delete(id);
            return NoContent();
        }

        [HttpPost]
        public async Task<Content> Create([FromBody] Content content)
        {
            content.Id = Guid.NewGuid();
            await _contentRepository.Save(content);
            return content;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Content content)
        {
            if (content.Id != id) return BadRequest("Id mismatch");
            await _contentRepository.Save(content);
            return Ok(content);
        }
    }
}