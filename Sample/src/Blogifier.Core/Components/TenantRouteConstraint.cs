using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Blogifier.Core.Components
{
    public class TenantRouteConstraint : IRouteConstraint
    {
        public bool Match(
            HttpContext httpContext, 
            IRouter route, 
            string routeKey, 
            RouteValueDictionary values, 
            RouteDirection routeDirection)
        {
            if(httpContext.Request.ContentType == "text/html")
            {
                
            }

            var tenants = new List<string>();
            tenants.Add("foo");
            tenants.Add("bar");

            var tenant = values["tenant"];
            if (tenants.Contains(tenant))
            {
                // set current tenant (blog) here

                var a = httpContext.Request.Headers;

                var x = httpContext.Request.Path;
                System.Diagnostics.Debug.WriteLine(x);
                return true;
            }

            return false;
        }
    }

    //public class SubdomainRoute : RouteBase
    //{
    //    public SubdomainRoute(string template, 
    //        string name, 
    //        IInlineConstraintResolver constraintResolver, 
    //        RouteValueDictionary defaults, 
    //        IDictionary<string, object> constraints, 
    //        RouteValueDictionary dataTokens) : base(template, name, constraintResolver, defaults, constraints, dataTokens)
    //    {

    //    }

    //    protected override Task OnRouteMatched(RouteContext context)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    protected override VirtualPathData OnVirtualPathGenerated(VirtualPathContext context)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

}
