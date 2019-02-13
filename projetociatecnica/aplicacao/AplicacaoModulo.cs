using Autofac;
using projetociatecnica.Infraestrutura.Repositorio;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace aplicacao
{
    public class AplicacaoModulo: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ctx => new AplicacaoContex("Base"))
             .As<DbContext>()
             .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(ServicoRepositorio<,>))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(AplicacaoModulo)))
               .Where(x =>
                           x.Name
                            .StartsWith("Servico"))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
