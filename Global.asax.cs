using Api.Repository;
using Api.Repository.Interface;
using Api.Repository.Wrapper;
using Api.Services;
using Funq;
using ServiceStack;
using ServiceStack.Api.OpenApi;
using ServiceStack.Caching;
using ServiceStack.Logging;
using ServiceStack.Logging.NLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Api
{
    public class Global : System.Web.HttpApplication
    {
        public class AppHost : AppHostBase
        {
            public AppHost() : base("Api", typeof(PricingService).Assembly)
            {
                LogManager.LogFactory = new NLogFactory();
            }

            public override void Configure(Container container)
            {
                Plugins.Add(new OpenApiFeature
                {
                    Tags = RouteTags.TopLevelRoutes()
                });

                container.Register<ICacheClient>(new MemoryCacheClient());

                container.RegisterAs<PricingClientWrapper, IPricingClient>();
                container.RegisterAs<PricingRepository, IPricingRepository>();
            }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            new AppHost().Init();
        }
    }
}
