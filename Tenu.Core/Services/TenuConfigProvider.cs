using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Tenu.Core.Interfaces;

namespace Tenu.Core.Services
{
    public class TenuConfigProvider : ITenuConfigProvider
    {
        private readonly IFileProvider _fileProvider;

        public TenuConfigProvider(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public IEnumerable<string> ListChildren(string path)
        {
            var contents = _fileProvider.GetDirectoryContents(path);
            if (!contents.Exists) yield break;

            foreach (var file in contents)
                yield return file.Name;
        }

        public string ReadConfig(string path)
        {
            var configFile = _fileProvider.GetFileInfo(path);
            if (!configFile.Exists)
                return null;

            using (var stream = configFile.CreateReadStream())
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}