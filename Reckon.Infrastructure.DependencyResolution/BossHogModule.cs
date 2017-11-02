using Autofac;
using Reckon.Domain;
using Reckon.DomainService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reckon.Infrastructure.DependencyResolution
{
    public class BossHogModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Services
            builder.RegisterType<PrintService>().As<IPrintService>().InstancePerLifetimeScope();
            builder.RegisterType<SearchService>().As<ISearchService>().InstancePerLifetimeScope();
        }
    }
}
