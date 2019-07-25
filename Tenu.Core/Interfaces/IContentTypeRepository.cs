using Tenu.Core.Models;

namespace Tenu.Core.Interfaces
{
    public interface IContentTypeRepository
    {
        ContentType[] GetAll();
        ContentType GetByAlias(string contentTypeAlias);
    }
}