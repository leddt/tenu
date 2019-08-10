using System.Dynamic;
using Tenu.Core.Models;

namespace Tenu.FrontEnd
{
    internal class DynamicContentModel : DynamicObject
    {
        private readonly Content _content;
        private readonly PropertyConverterService _converterService;

        public DynamicContentModel(Content content, PropertyConverterService converterService)
        {
            _content = content;
            _converterService = converterService;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var property = _content.Properties[binder.Name];

            result = property == null
                ? null
                : _converterService.ConvertDefault(property);

            return true;
        }
    }
}