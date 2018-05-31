using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using MultiSearch.API.Infrastructure;
using MultiSearch.API.Infrastructure.Repositories;
using MultiSearch.API.Infrastructure.SearchEngines;
using MultiSearch.API.Infrastructure.Services;

namespace MultiSearch.API.App_Start
{
    public class IoCConfig
    {
        public static IContainer Build()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<MultiSearchContext>()
                .AsSelf()
                .InstancePerRequest();

            builder.RegisterType<SearchRequestRepository>()
                .As<ISearchRequestRepository>()
                .InstancePerRequest();

            builder.RegisterType<SearchEnginesFactory>()
                .As<ISearchEnginesFactory>()
                .InstancePerRequest();

            builder.RegisterType<FastestResultSearchService>()
                .As<ISearchService>()
                .InstancePerRequest();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            return container;
        }
    }
}