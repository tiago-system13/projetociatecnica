using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projetociatecnica.Infraestrutura.Repositorios;
using projetociatecnica.Infraestrutura.Repositorio;
using dominio.dominio;

namespace aplicacao.Servicos.ServicoDePessoa
{
    public class ServicoPessoa : IServicoPessoa
    {
        private readonly IServicoRepositorio<PessoaFisica, int> _pessoaFisica;
        private readonly IServicoRepositorio<PessoaJuridica, int> _pessoaJuridica;

        public ServicoPessoa(IServicoRepositorio<PessoaFisica, int> pessoaFisica, IServicoRepositorio<PessoaJuridica, int> pessoaJuridica)
        {
            _pessoaFisica = pessoaFisica;
            _pessoaJuridica = pessoaJuridica;
        }

        private bool IdadeMaiorDe19(FormularioPessoa formularioPessoa)
        {
            return DateTime.Now.Year - formularioPessoa.DataNascimento.Value.Year >= 19;

        }
        private PessoaFisica PreencherPessoaFisica(FormularioPessoa formularioPessoa)
        {
            return new PessoaFisica()
            {
                Nome = formularioPessoa.Nome,
                SobreNome = formularioPessoa.SobreNome,
                DataNascimento = formularioPessoa.DataNascimento.Value,
                Logradouro = formularioPessoa.Logradouro,
                Cep = formularioPessoa.Cep,
                Bairro = formularioPessoa.Bairro,
                Cidade = formularioPessoa.Cidade,
                Complemento = formularioPessoa.Complemento,
                Cpf = formularioPessoa.NumeroDocumento,
                Numero = formularioPessoa.Numero,
                Uf = formularioPessoa.Uf
            };
        }
        private PessoaJuridica PreencherPessoaJuridica(FormularioPessoa formularioPessoa)
        {
            return new PessoaJuridica()
            {
                NomeFantisia = formularioPessoa.Nome,
                RazaoSocial = formularioPessoa.SobreNome,
                Logradouro = formularioPessoa.Logradouro,
                Cep = formularioPessoa.Cep,
                Bairro = formularioPessoa.Bairro,
                Cidade = formularioPessoa.Cidade,
                Complemento = formularioPessoa.Complemento,
                Cnpj = formularioPessoa.NumeroDocumento,
                Numero = formularioPessoa.Numero,
                Uf = formularioPessoa.Uf
            };
        }

        private void CadastrarPessoaFisica(FormularioPessoa formularioPessoa)
        {
            _pessoaFisica.Inserir(PreencherPessoaFisica(formularioPessoa));

        }
        private void CadastrarPessoaJuridica(FormularioPessoa formularioPessoa)
        {
            _pessoaJuridica.Inserir(PreencherPessoaJuridica(formularioPessoa));
        }
        public void CadastrarPessoa(FormularioPessoa formularioPessoa)
        {
            if (formularioPessoa.TipoPessoa.Equals("PF"))
            {
                if (!IdadeMaiorDe19(formularioPessoa))
                    throw new ArgumentException("Somente poderá ser cadastrado pessoa física com idade superior ou igual a 19 anos");

                CadastrarPessoaFisica(formularioPessoa);

            }
            else
            {
                CadastrarPessoaJuridica(formularioPessoa);
            }

            _pessoaJuridica.Salvar();
        }
        private FormularioPessoa CarregarPessoaFisica(int id)
        {
            return _pessoaFisica.Todos.Where(x => x.Id == id).Select(p => new FormularioPessoa()
            {
                NumeroDocumento = p.Cpf,
                Nome = p.Nome,
                SobreNome = p.SobreNome,
                Cep = p.Cep,
                Bairro = p.Bairro,
                Cidade = p.Cidade,
                Logradouro = p.Logradouro,
                Complemento = p.Complemento,
                DataNascimento = p.DataNascimento,
                Uf = p.Uf,
                Numero = p.Numero,
                TipoPessoa = "PF"
            }).FirstOrDefault();
        }

        private FormularioPessoa CarregarPessoaJuridica(int id)
        {
            return _pessoaJuridica.Todos.Where(x => x.Id == id).Select(p => new FormularioPessoa()
            {
                NumeroDocumento = p.Cnpj,
                Nome = p.NomeFantisia,
                SobreNome = p.RazaoSocial,
                Cep = p.Cep,
                Bairro = p.Bairro,
                Cidade = p.Cidade,
                Logradouro = p.Logradouro,
                Complemento = p.Complemento,
                Uf = p.Uf,
                Numero = p.Numero,
                TipoPessoa = "PJ"
            }).FirstOrDefault();
        }

        public FormularioPessoa CarregarPessoa(int id, string tipoPessoa)
        {
            if (tipoPessoa.Equals("PF"))
            {
                return CarregarPessoaFisica(id);
            }

            return CarregarPessoaJuridica(id);
        }

        private bool ExisteCPF(FormularioPessoa formularioPessoa)
        {
            return _pessoaFisica.Todos.Any(x => x.Cpf == formularioPessoa.NumeroDocumento);
        }

        private void EditarPessoFisica(FormularioPessoa formularioPessoa)
        {

            var pessoaFisica = _pessoaFisica.Todos.FirstOrDefault(pf => pf.Id == formularioPessoa.Id);

            if (formularioPessoa.NumeroDocumento != pessoaFisica.Cpf && ExisteCPF(formularioPessoa))
            {
                throw new ArgumentException("Já existe cadastro do CPF para uma outra pessoa física");
            }

            pessoaFisica.Logradouro = formularioPessoa.Logradouro;
            pessoaFisica.Nome = formularioPessoa.Nome;
            pessoaFisica.SobreNome = formularioPessoa.SobreNome;
            pessoaFisica.Uf = formularioPessoa.Uf;
            pessoaFisica.Numero = formularioPessoa.Numero;
            pessoaFisica.Bairro = formularioPessoa.Bairro;
            pessoaFisica.Cep = formularioPessoa.Cep;
            pessoaFisica.Cidade = formularioPessoa.Cidade;
            pessoaFisica.Complemento = formularioPessoa.Complemento;
            pessoaFisica.Cpf = formularioPessoa.NumeroDocumento;
            pessoaFisica.DataNascimento = formularioPessoa.DataNascimento.Value;
            _pessoaFisica.Editar(pessoaFisica);
        }
        private bool ExisteCnpJ(FormularioPessoa formularioPessoa)
        {
            return _pessoaJuridica.Todos.Any(x => x.Cnpj == formularioPessoa.NumeroDocumento);
        }

        private void EditarPessoJuridica(FormularioPessoa formularioPessoa)
        {
            var pessoaJuridica = _pessoaJuridica.Todos.FirstOrDefault(pf => pf.Id == formularioPessoa.Id);

            if (formularioPessoa.NumeroDocumento != pessoaJuridica.Cnpj && ExisteCnpJ(formularioPessoa))
            {
                throw new ArgumentException("Já existe cadastro do CNPJ para outra pessoa juridica");
            }

            pessoaJuridica.Logradouro = formularioPessoa.Logradouro;
            pessoaJuridica.NomeFantisia = formularioPessoa.Nome;
            pessoaJuridica.RazaoSocial = formularioPessoa.SobreNome;
            pessoaJuridica.Uf = formularioPessoa.Uf;
            pessoaJuridica.Numero = formularioPessoa.Numero;
            pessoaJuridica.Bairro = formularioPessoa.Bairro;
            pessoaJuridica.Cep = formularioPessoa.Cep;
            pessoaJuridica.Cidade = formularioPessoa.Cidade;
            pessoaJuridica.Complemento = formularioPessoa.Complemento;
            pessoaJuridica.Cnpj = formularioPessoa.NumeroDocumento;
            _pessoaJuridica.Editar(pessoaJuridica);
        }
        public void EditarPessoa(FormularioPessoa formularioPessoa)
        {
            if (formularioPessoa.TipoPessoa.Equals("PF"))
            {
                if (!IdadeMaiorDe19(formularioPessoa))
                    throw new ArgumentException("Somente poderá ser cadastrado pessoa física com idade superior ou igual a 19 anos");

                EditarPessoFisica(formularioPessoa);
            }
            else
            {
                EditarPessoJuridica(formularioPessoa);
            }

            _pessoaJuridica.Salvar();
        }

        public ListaPaginavel<ProjecaoDadosPessoa> ListarPessoas(PesquisaPessoa dadosPesquisa)
        {
            var pessoasFisicas = from pf in _pessoaFisica.Todos
                                 select new ProjecaoDadosPessoa()
                                 {
                                     Id = pf.Id,
                                     TipoPessoa = "PF",
                                     Nome = pf.Nome +" " + pf.SobreNome,
                                     Bairro = pf.Bairro,
                                     Cep = pf.Cep,
                                     Cidade = pf.Cidade,
                                     Logradouro = pf.Logradouro + ", " + pf.Numero + (pf.Complemento == null ? "" : "," + pf.Complemento),
                                     NumeroDocumento = pf.Cpf,
                                     UF = pf.Uf
                                     
                                 };

            var pessoasJuridicas = from pj in _pessoaJuridica.Todos
                                   select new ProjecaoDadosPessoa()
                                   {
                                       Id = pj.Id,
                                       TipoPessoa = "PJ",
                                       Nome = pj.NomeFantisia,
                                       Bairro = pj.Bairro,
                                       Cep = pj.Cep,
                                       Cidade = pj.Cidade,
                                       Logradouro = pj.Logradouro + ", " + pj.Numero + (pj.Complemento == null ? "" : "," + pj.Complemento),
                                       NumeroDocumento = pj.Cnpj,
                                       UF = pj.Uf
                                   };

            var query = pessoasJuridicas.Union(pessoasFisicas);

            if (!string.IsNullOrEmpty(dadosPesquisa.TipoPessoa))
                query = query.Where(x => x.TipoPessoa.Equals(dadosPesquisa.TipoPessoa));

            if (!string.IsNullOrEmpty(dadosPesquisa.Nome))
                query = query.Where(x => x.Nome.Contains(dadosPesquisa.Nome));


            if (!string.IsNullOrEmpty(dadosPesquisa.NumeroDocumento))
                query = query.Where(x => x.NumeroDocumento.Equals(dadosPesquisa.NumeroDocumento));

            return query.ParaListaPaginavel(dadosPesquisa.IndiceDePagina, dadosPesquisa.RegistrosPorPagina, dadosPesquisa.Ordenacao, x => x.Nome);
        }

        private void ExcluirPessoaFisica(PessoaFisica pessoa)
        {
            _pessoaFisica.Excluir(pessoa);

        }

        private void ExcluirPessoaJuridica(PessoaJuridica pessoa)
        {
            _pessoaJuridica.Excluir(pessoa);

        }

        public void ExcluirPessoa(int id, string tipoPessoa)
        {
            if (tipoPessoa.Equals("PF"))
                ExcluirPessoaFisica(_pessoaFisica.Todos.FirstOrDefault(pf => pf.Id == id));
            else
                ExcluirPessoaJuridica(_pessoaJuridica.Todos.FirstOrDefault(pj => pj.Id == id));

            _pessoaFisica.Salvar();
        }

        public IEnumerable<ProjecaoDadosPessoa> ListarTodasPessoas()
        {
            var pessoasFisicas = from pf in _pessoaFisica.Todos
                                 select new ProjecaoDadosPessoa()
                                 {
                                     Id = pf.Id,
                                     TipoPessoa = "PF",
                                     Nome = pf.Nome + " " + pf.SobreNome,
                                     Bairro = pf.Bairro,
                                     Cep = pf.Cep,
                                     Cidade = pf.Cidade,
                                     Logradouro = pf.Logradouro + ", " + pf.Numero + (pf.Complemento == null ? "" : "," + pf.Complemento),
                                     NumeroDocumento = pf.Cpf,
                                     UF = pf.Uf

                                 };

            var pessoasJuridicas = from pj in _pessoaJuridica.Todos
                                   select new ProjecaoDadosPessoa()
                                   {
                                       Id = pj.Id,
                                       TipoPessoa = "PJ",
                                       Nome = pj.NomeFantisia,
                                       Bairro = pj.Bairro,
                                       Cep = pj.Cep,
                                       Cidade = pj.Cidade,
                                       Logradouro = pj.Logradouro + ", " + pj.Numero + (pj.Complemento == null ? "" : "," + pj.Complemento),
                                       NumeroDocumento = pj.Cnpj,
                                       UF = pj.Uf
                                   };

            var query = pessoasJuridicas.Union(pessoasFisicas);
            return query;

        }
    }
}
