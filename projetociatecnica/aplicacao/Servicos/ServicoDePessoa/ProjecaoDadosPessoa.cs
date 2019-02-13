using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aplicacao.Servicos.ServicoDePessoa
{
    public class ProjecaoDadosPessoa
    {
        public int Id { get; set; }
        public string TipoPessoa { get; set; }
        public string Nome { get; set; }
        public string NumeroDocumento { get; set; }
        public string Logradouro { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
    }
}
