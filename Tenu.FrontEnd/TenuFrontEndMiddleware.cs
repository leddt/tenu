using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using Tenu.Core.Models;

namespace Tenu.FrontEnd
{
    public class TenuFrontEndMiddleware
    {
        private readonly RequestDelegate _next;

        public TenuFrontEndMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, TenuRouter router)
        {
            if (!context.Request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
            {
                await _next(context);
                return;
            }

            var content = await router.FindContentForUrl(context.Request.GetEncodedUrl());
            if (content == null)
            {
                await _next(context);
                return;
            }

            await RenderContent(context, content);
        }

        private static Task RenderContent(HttpContext context, Content content)
        {
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(JsonConvert.SerializeObject(content));
        }
    }
}