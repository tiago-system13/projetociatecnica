using dominio.dominio.mapeamento;
using System.Data.Entity;

namespace aplicacao
{
    public class AplicacaoContex : DbContext
    {
        public AplicacaoContex(string connectionString) : base(connectionString)
        {
            Database.SetInitializer<AplicacaoContex>(null);
            Configuration.LazyLoadingEnabled = true;
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public AplicacaoContex() : base("Base")
        {
            Database.SetInitializer<AplicacaoContex>(null);
            this.Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {            
            modelBuilder.Configurations.Add(new PessoaFisicaMap());
            modelBuilder.Configurations.Add(new PessoaJuridicaMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
