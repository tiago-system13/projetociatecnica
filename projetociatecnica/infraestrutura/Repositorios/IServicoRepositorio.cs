using projetociatecnica.Infraestrutura.Repositorios;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace projetociatecnica.Infraestrutura.Repositorio
{
    /// <summary>
    /// interface que determina tipos de classes de Repositório
    /// </summary>
    /// <typeparam name="T">Classe de entidade</typeparam>
    /// <typeparam name="TKey">Tipo de chave primária da entidade</typeparam>
    public interface IServicoRepositorio<T, TKey> where T : class, IEntidade<TKey>
    {
        /// <summary>
        /// Inclui ao carregar a entidade, dados de objetos dependentes. Usando quando se desativa o carregamento automático de objetos dependentes
        /// Trabalha semelhante a um "join" em SQLs.
        /// </summary>
        /// <param name="includeProperties">Conjunto de propriedades a ser incluidos para </param>
        /// <returns>Ojbeto com suas dependencias carregadas</returns>
        IQueryable<T> TodosIncluindo(params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Lista todas as entidades no Repositório.
        /// </summary>
        IQueryable<T> Todos { get; }

        /// <summary>
        /// Lista toas as entidades no Repositório;
        /// </summary>
        /// <returns>Um Objeto IQueriable</returns>
        IQueryable<T> Listar();

        /// <summary>
        /// Carrega uma entidade pelo seu Identificador.
        /// </summary>
        /// <param name="id">Valor de seu Identificador</param>
        /// <returns></returns>
        T PorId(object id);

        /// <summary>
        /// Lista entidades no Repositório de acordo com os filtros passados.
        /// </summary>
        /// <param name="predicado">Filtros</param>
        /// <returns></returns>
        IQueryable<T> ListarPor(Expression<Func<T, bool>> predicado);

        /// <summary>
        /// Lista entidades por páginas.
        /// </summary>
        /// <param name="indiceDaPagina">Número da página</param>
        /// <param name="tamanhoDaPagina">Quantidade de itens por página</param>
        /// <param name="chaveSeletora">Chaves(Propriedades) de ordenação</param>
        /// <param name="ordenarPor">Tipo de ordenação: Acendente(ASC) ou Descrescente(DESC)</param>
        /// <returns>Uma lista paginável</returns>
        ListaPaginavel<T> Listar(int indiceDaPagina, int tamanhoDaPagina, Expression<Func<T, TKey>> chaveSeletora, TipoDeOrdenacao ordenarPor);

        /// <summary>
        /// Lista entidades por páginas.
        /// </summary>
        /// <param name="indiceDaPagina">Número da página</param>
        /// <param name="tamanhoDaPagina">Quantidade de itens por página</param>
        /// <param name="chaveSeletora">Chaves(Propriedades) de ordenação</param>
        /// <param name="ordenarPor">Tipo de ordenação: Acendente(ASC) ou Descrescente(DESC)</param>
        /// <param name="incluindoPropriedades">Conjunto de Propriedade para carregar junto com a Entidade. Assemelha com a clausula "join" em SQLs</param>
        /// <returns>Uma lista paginável</returns>
        ListaPaginavel<T> Listar(int indiceDaPagina, int tamanhoDaPagina, Expression<Func<T, TKey>> chaveSeletora,
            Expression<Func<T, bool>> predicado, TipoDeOrdenacao ordenarPor, params Expression<Func<T, object>>[] incluindoPropriedades);

        /// <summary>
        /// Insere uma entidade.
        /// </summary>
        /// <param name="entidade">Dados da entidade</param>
        void Inserir(T entidade);

        /// <summary>
        /// Exclui entidade.
        /// </summary>
        /// <param name="entidade">Exclui uma entidade</param>
        void Excluir(T entidade);

        /// <summary>
        /// Exclui uma entidade pelo seu identificador.
        /// </summary>
        /// <param name="chaveDaEntidade">Identificador da entidade</param>
        void Excluir(TKey chaveDaEntidade);

        /// <summary>
        /// Atualiza uma entidade.
        /// </summary>
        /// <param name="entidade">Dados da entidade</param>
        void Editar(T entidade);

        /// <summary>
        /// Salva as alterações feitas no Repositório. 
        /// Multiplas operações podem ser feitas, 
        /// porém somente chamando este método é que realmente estas operações acontecem.
        /// Assemelha ao "Commit" de um Banco de dados
        /// </summary>
        void Salvar();
    }
}
