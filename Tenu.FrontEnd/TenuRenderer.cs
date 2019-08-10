using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Tenu.Core.Interfaces;
using Tenu.Core.Models;

namespace Tenu.FrontEnd
{
    public class TenuRenderer
    {
        private readonly IContentTypeRepository _contentTypeRepository;
        private readonly IRazorViewEngine _viewEngine;
        private readonly ITempDataProvider _tempDataProvider;

        public TenuRenderer(IContentTypeRepository contentTypeRepository, IRazorViewEngine viewEngine, ITempDataProvider tempDataProvider)
        {
            _contentTypeRepository = contentTypeRepository;
            _viewEngine = viewEngine;
            _tempDataProvider = tempDataProvider;
        }

        public async Task Render(HttpResponse response, Content content)
        {
            if (!await TryRenderContent(response, content))
            {
                await RenderJsonFallback(response, content);
            }
        }

        private static async Task RenderJsonFallback(HttpResponse response, Content content)
        {
            response.ContentType = "application/json";
            await response.WriteAsync(JsonConvert.SerializeObject(content));
        }

        private async Task<bool> TryRenderContent(HttpResponse response, Content content)
        {
            var contentType = _contentTypeRepository.GetByAlias(content.ContentTypeAlias);
            if (contentType == null) return false;

            var result = _viewEngine.GetView(null, $"./TenuConfig/Templates/{contentType.Alias}.cshtml", true);
            if (!result.Success) return false;

            var model = GetModel(content, result.View);
            await RenderView(response, result.View, model);

            return true;
        }

        private object GetModel(Content content, IView view)
        {
            if (!(view is RazorView rv)) return null;

            var modelProperty = rv.RazorPage.GetType().GetProperty("Model");
            var modelType = modelProperty?.PropertyType;

            if (modelType == typeof(Content))
                return content;
            else
                return null;
        }

        private async Task RenderView(HttpResponse response, IView view, object model)
        {
            var actionContext = new ActionContext(response.HttpContext, new RouteData(), new ActionDescriptor());

            var viewData = new ViewDataDictionary(
                new EmptyModelMetadataProvider(),
                new ModelStateDictionary())
            {
                Model = model
            };

            var tempData = new TempDataDictionary(
                response.HttpContext,
                _tempDataProvider);

            using (var writer = new StreamWriter(response.Body))
            {
                var viewContext = new ViewContext(
                    actionContext,
                    view,
                    viewData,
                    tempData,
                    writer,
                    new HtmlHelperOptions());

                await view.RenderAsync(viewContext);
                response.ContentType = "text/html";
            }
        }
    }
}