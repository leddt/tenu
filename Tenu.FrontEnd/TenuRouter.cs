using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tenu.Core.Interfaces;
using Tenu.Core.Models;

namespace Tenu.FrontEnd
{
    public class TenuRouter
    {
        private readonly IContentRepository _repo;

        public TenuRouter(IContentRepository repo)
        {
            _repo = repo;
        }

        public async Task<Content> FindContentForUrl(string url)
        {
            foreach (var (root, path) in await FindMatchingRoots(url))
            {
                var result = root;

                var segments = path.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries);
                foreach (var segment in segments)
                {
                    result = await _repo.GetChildByUrl(result.Id, segment);
                }

                if (result != null)
                    return result;
            }

            return null;
        }

        private async Task<IEnumerable<(Content root, string path)>> FindMatchingRoots(string url)
        {
            var results = new List<(Content, string)>();
            var uri = new Uri(url);

            foreach (var candidate in EnumerateRootCandidates(uri))
            {
                var result = await _repo.GetByUrl(candidate);
                if (result != null)
                    results.Add((result, GetRestOfPath(uri, candidate)));
            }

            return results;
        }

        private string GetRestOfPath(Uri uri, string candidate)
        {
            if (!candidate.StartsWith("/"))
                candidate = new Uri($"http://{candidate}").AbsolutePath;

            return uri.AbsolutePath.Remove(0, candidate.Length);
        }

        IEnumerable<string> EnumerateRootCandidates(Uri uri)
        {
            while (true)
            {
                yield return $"{uri.Host}{(uri.IsDefaultPort ? "" : $":{uri.Port}")}{uri.AbsolutePath}";
                yield return uri.AbsolutePath;

                if (uri.AbsolutePath != "/")
                    uri = new Uri(uri, "..");
                else
                    break;
            }
        }
    }
}