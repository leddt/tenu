using Tenu.Core.Models;

namespace SampleSite.Models
{
    public class HomeModel
    {
        private readonly Content _content;
        private readonly MyService _myService;

        public HomeModel(Content content, MyService myService)
        {
            _content = content;
            _myService = myService;
        }

        public string Title => _content.Properties["title"].RawValue;
        public string MyMessage => _myService.GetMessage();
    }
}