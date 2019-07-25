using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tenu.Core.Interfaces;
using Tenu.Core.Models;

namespace Tenu.ContentStorage.FileSystem
{
    public class FileSystemContentOptions
    {
        public string ContentRoot { get; set; } = "TenuContent";
    }

    public class FileSystemContentRepository : IContentRepository
    {
        private readonly PhysicalFileProvider _fileProvider;

        public FileSystemContentRepository(IOptions<FileSystemContentOptions> options, IHostingEnvironment env)
        {
            var root = $"{env.ContentRootPath}/{options.Value.ContentRoot}";
            if (!Directory.Exists(root))
                Directory.CreateDirectory(root);

            _fileProvider = new PhysicalFileProvider(root);
        }

        public Task<Content> GetById(Guid contentId)
        {
            var file = GetFileInfo(contentId);
            return file.Exists 
                ? Task.FromResult(ReadFileAs<Content>(file)) 
                : Task.FromResult<Content>(null);
        }

        public async Task<IEnumerable<Content>> GetChildren(Guid parentContentId)
        {
            var parent = await GetById(parentContentId);

            return await Task.WhenAll(GetChildrenIds(parent).Select(GetById));
        }

        public async Task<IEnumerable<Content>> GetAtRoot()
        {
            return await Task.WhenAll(GetRootIds().Select(GetById));
        }

        public async Task Save(Content content)
        {
            var existingContent = await GetById(content.Id);
            if (existingContent != null)
            {
                if (existingContent.ParentId != content.ParentId)
                {
                    await RemoveIdFromParent(existingContent);
                    await AddIdToParent(content);
                }
            }
            else
            {
                await AddIdToParent(content);
            }

            SaveFile(GetFileInfo(content.Id), content);
        }

        public async Task Delete(Guid contentId)
        {
            var file = GetFileInfo(contentId);
            if (!file.Exists) return;

            var content = await GetById(contentId);
            await RemoveIdFromParent(content);

            File.Delete(file.PhysicalPath);
        }


        private static string GetFileName(Guid contentId) => contentId.ToString("N") + ".json";
        private IFileInfo GetFileInfo(Guid contentId) => _fileProvider.GetFileInfo(GetFileName(contentId));
        private IFileInfo GetRootFile() => GetFileInfo(Guid.Empty);

        private static T ReadFileAs<T>(IFileInfo file)
        {
            using (var stream = file.CreateReadStream())
            using (var sr = new StreamReader(stream))
            using (var reader = new JsonTextReader(sr))
            {
                var serializer = new JsonSerializer();
                return serializer.Deserialize<T>(reader);
            }
        }

        private IEnumerable<Guid> GetRootIds()
        {
            var rootFile = GetRootFile();
            return rootFile.Exists 
                ? ReadFileAs<Guid[]>(rootFile) 
                : Enumerable.Empty<Guid>();
        }

        private void SetRootIds(IEnumerable<Guid> ids)
        {
            var rootFile = GetRootFile();
            SaveFile(rootFile, ids.ToArray());
        }

        private static void SaveFile(IFileInfo rootFile, object data)
        {
            var serializer = new JsonSerializer();

            using (var stream = File.Create(rootFile.PhysicalPath))
            using (var writer = new StreamWriter(stream))
            {
                serializer.Serialize(writer, data);
            }
        }

        private IEnumerable<Guid> GetChildrenIds(Content content)
        {
            var children = content.GetMetadata<JArray>("__children")?.Values<string>().Select(Guid.Parse);
            return children ?? Enumerable.Empty<Guid>();
        }

        private void SetChildrenIds(Content content, IEnumerable<Guid> ids)
        {
            content.SetMetadata("__children", ids.ToArray());
        }

        private async Task RemoveIdFromParent(Content content)
        {
            if (content.ParentId == null)
            {
                SetRootIds(GetRootIds().Where(x => x != content.Id));
            }
            else
            {
                var previousParent = await GetById(content.ParentId.Value);
                SetChildrenIds(previousParent, GetChildrenIds(previousParent).Where(x => x != content.Id));
                await Save(previousParent);
            }
        }

        private async Task AddIdToParent(Content content)
        {
            if (content.ParentId == null)
            {
                SetRootIds(GetRootIds().Concat(new[] {content.Id}));
            }
            else
            {
                var previousParent = await GetById(content.ParentId.Value);
                SetChildrenIds(previousParent, GetChildrenIds(previousParent).Concat(new[] {content.Id}));
                await Save(previousParent);
            }
        }
    }
}
