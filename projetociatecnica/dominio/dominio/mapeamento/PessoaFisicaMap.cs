using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio.dominio.mapeamento
{
    public class PessoaFisicaMap : EntityTypeConfiguration<PessoaFisica>
    {
        public PessoaFisicaMap()
        {
            ToTable("ciatecnica.tb_pessoa_fisica");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);            
            Property(x => x.Cpf).HasColumnName("cpf").HasMaxLength(11).IsRequired();
            Property(x => x.DataNascimento).HasColumnName("data_nascimento");
            Property(x => x.Nome).HasColumnName("nome").HasMaxLength(100);
            Property(x => x.SobreNome).HasColumnName("sobrenome").HasMaxLength(100);
            Property(x => x.Logradouro).HasColumnName("logradouro").HasMaxLength(80).IsRequired();
            Property(x => x.Numero).HasColumnName("numero").HasMaxLength(10).IsRequired();
            Property(x => x.Complemento).HasColumnName("complemento").HasMaxLength(40).IsOptional();
            Property(x => x.Cep).HasColumnName("cep").HasMaxLength(8).IsRequired();
            Property(x => x.Uf).HasColumnName("uf").HasMaxLength(8).IsRequired();
            Property(x => x.Cidade).HasColumnName("cidade").HasMaxLength(80).IsRequired();
            Property(x => x.Bairro).HasColumnName("bairro").HasMaxLength(70).IsRequired();
           
        }
    }
}
