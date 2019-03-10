using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;

namespace Handy.App.Middlewares
{
    public class FormatQueryStringMiddleware
    {
        private readonly RequestDelegate _next;

        public FormatQueryStringMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var qs = context.Request.QueryString.Value;

            var newQs = qs?.Replace("%5B", string.Empty)
                           .Replace("%5D", string.Empty)
                           .Replace("[", string.Empty)
                           .Replace("]", string.Empty);

            context.Request.QueryString = new QueryString(newQs);
            await _next(context);
        }
    }
}