using projetociatecnica.Infraestrutura.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aplicacao.Servicos.ServicoDePessoa
{
    public class PesquisaPessoa:PesquisaPorPagina
    {
        public string TipoPessoa { get; set; }
        public string NumeroDocumento { get; set; }        
        public string Nome { get; set; }
    }
}
