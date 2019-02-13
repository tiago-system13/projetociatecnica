using aplicacao;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace projetociatecnicaweb.App_Start
{
    public static class AutofacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterSource(new ViewRegistrationSource());
            builder.RegisterFilterProvider();
            builder.Register(c => HttpContext.Current.User).InstancePerRequest();
            builder.Register(c => HttpContext.Current.Session).InstancePerRequest();
            /// <summary>
            /// Registre aqui seu módulo
            /// </summary>
            builder.RegisterModule<AplicacaoModulo>();                    
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}