using System.Collections.Generic;

namespace Tenu.Core.Interfaces
{
    public interface ITenuConfigProvider
    {
        IEnumerable<string> ListChildren(string path);
        string ReadConfig(string path);
    }
}