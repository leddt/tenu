using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiteDB;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Tenu.Core.Interfaces;
using Tenu.Core.Models;

namespace Tenu.ContentStorage.LiteDB
{
    public class LiteDbContentOptions
    {
        public string DataFile { get; set; } = "TenuContent.db";
    }

    public class LiteDbContentRepository : IContentRepository, IDisposable
    {
        private readonly LiteDatabase _db;
        private readonly LiteCollection<Content> _contentCollection;

        public LiteDbContentRepository(IOptions<LiteDbContentOptions> options, IHostingEnvironment env)
        {
            var file = $"{env.ContentRootPath}/{options.Value.DataFile}";
            _db = new LiteDatabase(file);
            _contentCollection = _db.GetCollection<Content>();
            _contentCollection.EnsureIndex(x => x.ParentId);
        }

        public Task<Content> GetById(Guid contentId)
        {
            return Task.FromResult(_contentCollection.FindById(contentId));
        }

        public Task<IEnumerable<Content>> GetChildren(Guid parentContentId)
        {
            return Task.FromResult(_contentCollection.Find(x => x.ParentId == parentContentId));
        }

        public Task<IEnumerable<Content>> GetAtRoot()
        {
            return Task.FromResult(_contentCollection.Find(x => x.ParentId == null));
        }

        public Task Save(Content content)
        {
            _contentCollection.Upsert(content);
            return Task.CompletedTask;
        }

        public Task Delete(Guid contentId)
        {
            _contentCollection.Delete(contentId);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}
