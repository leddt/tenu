using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.SpaServices.StaticFiles;
using Microsoft.Extensions.FileProviders;

namespace Tenu.Backend.AspNetServices
{
#if DEBUG
    internal class BackendFileProvider : PhysicalFileProvider
    {
        public BackendFileProvider() : base(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/../../../../Tenu.Backend/app/dist")
        {
        }
    }
#else
    internal class BackendFileProvider : ManifestEmbeddedFileProvider
    {
        public BackendFileProvider() : base(Assembly.GetExecutingAssembly(), "app/dist")
        {
        }
    }
#endif

    internal class BackendSpaStaticFileProvider : ISpaStaticFileProvider
    {
        public BackendSpaStaticFileProvider()
        {
            FileProvider = new BackendFileProvider();
        }

        public IFileProvider FileProvider { get; }
    }
}