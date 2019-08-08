using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tenu.Core.Models;

namespace Tenu.Core.Interfaces
{
    public interface IContentRepository
    {
        Task<Content> GetById(Guid contentId);
        Task<Content> GetByUrl(string url);
        Task<Content> GetChildByUrl(Guid parentContentId, string urlSegment);
        Task<IEnumerable<Content>> GetChildren(Guid parentContentId);
        Task<IEnumerable<Content>> GetAtRoot();

        Task Save(Content content);
        Task Delete(Guid contentId);
    }
}