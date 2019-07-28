using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.SpaServices.StaticFiles;
using Microsoft.Extensions.FileProviders;

namespace Tenu.BackOffice.AspNetServices
{
#if DEBUG
    internal class BackOfficeFileProvider : PhysicalFileProvider
    {
        public BackOfficeFileProvider() : base(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/../../../../Tenu.BackOffice/app/dist")
        {
        }
    }
#else
    internal class BackOfficeFileProvider : ManifestEmbeddedFileProvider
    {
        public BackOfficeFileProvider() : base(Assembly.GetExecutingAssembly(), "app/dist")
        {
        }
    }
#endif

    internal class BackOfficeSpaStaticFileProvider : ISpaStaticFileProvider
    {
        public BackOfficeSpaStaticFileProvider()
        {
            FileProvider = new BackOfficeFileProvider();
        }

        public IFileProvider FileProvider { get; }
    }
}