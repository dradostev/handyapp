using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App.Filters
{
    public class ValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.HttpContext.Response.StatusCode = 422;

                var errors = new Dictionary<string, IEnumerable<string>>();
                foreach (var kvp in context.ModelState)
                {
                    errors.Add(kvp.Key, kvp.Value.Errors.Select(x => x.ErrorMessage));
                }
                
                context.Result = new JsonResult(errors);
            }
        }
    }
}