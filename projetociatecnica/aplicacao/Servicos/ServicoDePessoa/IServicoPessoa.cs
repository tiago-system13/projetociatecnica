using projetociatecnica.Infraestrutura.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aplicacao.Servicos.ServicoDePessoa
{
    public interface IServicoPessoa
    {
        void CadastrarPessoa(FormularioPessoa formularioPessoa);        
        void EditarPessoa(FormularioPessoa formularioPessoa);
        FormularioPessoa CarregarPessoa(int id, string tipoPessoa);
        void ExcluirPessoa(int id, string tipoPessoa);
        ListaPaginavel<ProjecaoDadosPessoa> ListarPessoas(PesquisaPessoa dadosPesquisa);
        IEnumerable<ProjecaoDadosPessoa> ListarTodasPessoas();
    }
}
