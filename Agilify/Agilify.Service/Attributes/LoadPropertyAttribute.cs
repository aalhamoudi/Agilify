using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace AgilifyService.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class LoadPropertyAttribute : ActionFilterAttribute
    {
        private string propertyName;

        public LoadPropertyAttribute(string propertyName)
        {
            this.propertyName = propertyName;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);

            var uriBuilder = new UriBuilder(actionContext.Request.RequestUri);
            var parameters = uriBuilder.Query.TrimStart('?').Split(new[] {'&'}, StringSplitOptions.RemoveEmptyEntries).ToList();

            int index = -1;

            for (int i = 0; i < parameters.Count; i++)
            {
                if (parameters[i].StartsWith("$expand", StringComparison.Ordinal))
                {
                    index = i;
                    break;
                }
            }

            if (index < 1)
                parameters.Add("$expand=" + propertyName);
            else
                parameters[index] = parameters[index] + "," + propertyName;

            uriBuilder.Query = string.Join("&", parameters);
            actionContext.Request.RequestUri = uriBuilder.Uri;
        }
    }
}