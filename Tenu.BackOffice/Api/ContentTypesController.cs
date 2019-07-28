using Microsoft.AspNetCore.Mvc;
using Tenu.Core.Interfaces;
using ContentType = Tenu.Core.Models.ContentType;

namespace Tenu.BackOffice.Api
{
    [Route("api/[controller]")]
    public class ContentTypesController : ControllerBase
    {
        private readonly IContentTypeRepository _contentTypeRepository;

        public ContentTypesController(IContentTypeRepository contentTypeRepository)
        {
            _contentTypeRepository = contentTypeRepository;
        }

        [HttpGet]
        public ContentType[] GetAll()
        {
            return _contentTypeRepository.GetAll();
        }

        [HttpGet("{alias}")]
        public ContentType GetByAlias(string alias)
        {
            return _contentTypeRepository.GetByAlias(alias);
        }
    }
}