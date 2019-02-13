using aplicacao;
using aplicacao.Servicos.ServicoDePessoa;
using Autofac;
using Autofac.Integration.WebApi;
using projetociatecnica.Infraestrutura.Repositorio;
using System.Data.Entity;
using System.Reflection;
using System.Web.Http;

namespace projetociatecnica.webapi
{
    public static class AutofacConfigApi
    {
        internal static void ConfigureContainer(HttpConfiguration config)
        {
            var apiBuilder = new ContainerBuilder();

            apiBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            apiBuilder.RegisterWebApiFilterProvider(config);

            apiBuilder
                .RegisterType<AplicacaoContex>().As<DbContext>()
                .InstancePerLifetimeScope();

            apiBuilder
               .RegisterGeneric(typeof(ServicoRepositorio<,>))
                  .As(typeof(IServicoRepositorio<,>)).InstancePerRequest();

            apiBuilder.RegisterType<ServicoPessoa>()
               .As<IServicoPessoa>().InstancePerLifetimeScope();           

            var container = apiBuilder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}