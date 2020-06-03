using ServiceStack.Api.OpenApi.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api
{
    public class RouteTags
    {
        public static List<OpenApiTag> TopLevelRoutes()
        {
            return new List<OpenApiTag>
            {
                new OpenApiTag { Name = "prices", Description = "No desc" }
            };
        }
    }
}
