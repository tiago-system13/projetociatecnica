
using projetociatecnica.Infraestrutura.Repositorios;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace projetociatecnica.Infraestrutura.Repositorio
{
    /// <summary>
    /// Implementa o tipo de padrão chamado Repositório
    /// </summary>
    /// <typeparam name="T">Tipo da entidade</typeparam>
    /// <typeparam name="TKey">Chave Primária</typeparam>
    public class ServicoRepositorio<T, TKey> : IServicoRepositorio<T, TKey>
        where T : class, IEntidade<TKey>
    {
        /// <summary>
        /// Contexto do banco de Dados
        /// </summary>
        private readonly DbContext _entitiesContext;

        /// <summary>
        /// Contrói um repositório a partir de um contexto de Banco de dados
        /// </summary>
        /// <param name="entitiesContext">Contexto de Banco de Dados</param>
        public ServicoRepositorio(DbContext entitiesContext)
        {
            if (entitiesContext == null)
            {
                throw new ArgumentNullException(nameof(entitiesContext));
            }

            _entitiesContext = entitiesContext;
        }

        /// <summary>
        /// Lista todas as entidades
        /// </summary>
        /// <returns>Uma pesquisa para aquela entidade</returns>
        public virtual IQueryable<T> Listar() => _entitiesContext.Set<T>();

        /// <summary>
        /// Todos os itens daquela entidade
        /// </summary>
        public virtual IQueryable<T> Todos => Listar();

        /// <summary>
        /// Deve ser utilizado quando o LazyLoading for desativado para trazer os objetos dependentes
        /// </summary>
        /// <param name="includeProperties">Propriedades a serem incluidas</param>
        /// <returns></returns>
        public virtual IQueryable<T> TodosIncluindo(
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _entitiesContext.Set<T>();
            return includeProperties
                .Aggregate(query, (q, y) => q.Include(y));
        }

        /// <summary>
        /// Busca uma entidade pela sua chave primária
        /// </summary>
        /// <param name="key">Chave primária</param>
        /// <returns></returns>
        public T PorId(object key) => Listar().FirstOrDefault(x => key == (object)x.Id);

        /// <summary>
        /// busca entidade pela sua chave primária
        /// </summary>
        /// <param name="key">Chave Primária</param>
        /// <returns></returns>
        public T PorId(int key) => Listar().FirstOrDefault(x => key == Convert.ToInt32(x.Id));

        /// <summary>
        /// Lista entidades pro algum critério
        /// </summary>
        /// <param name="predicate">Critério</param>
        /// <returns>Uma pesquisa filtradas por algum critério</returns>
        public virtual IQueryable<T> ListarPor(Expression<Func<T, bool>> predicate) => _entitiesContext.Set<T>().Where(predicate);

        /// <summary>
        /// Lista através de páginas
        /// </summary>
        /// <param name="pageIndex">Índice de Página</param>
        /// <param name="pageSize">Tamanho da Página</param>
        /// <param name="keySelector">Chaves para ordenação</param>
        /// <param name="orderBy">Tipo de Ordenação</param>
        /// <returns>Uma pesquisa páginada</returns>
        public virtual ListaPaginavel<T> Listar(
            int pageIndex,
            int pageSize,
            Expression<Func<T, TKey>> keySelector,
            TipoDeOrdenacao orderBy) => Listar(pageIndex, pageSize, keySelector, null, orderBy);

        /// <summary>
        /// Lista através de páginas
        /// </summary>
        /// <param name="pageIndex">Índice de página</param>
        /// <param name="pageSize">Tamanho da página</param>
        /// <param name="keySelector">Chaves para ordenação</param>
        /// <param name="predicate">Critério para filtrar as entidades</param>
        /// <param name="orderBy">Tipo de Ordenação</param>
        /// <param name="includeProperties">Propriedades para serem carregadas junto com a entidade</param>
        /// <returns>Uma lista paginável</returns>
        public virtual ListaPaginavel<T> Listar(
            int pageIndex, int pageSize,
            Expression<Func<T, TKey>> keySelector,
            Expression<Func<T, bool>> predicate, TipoDeOrdenacao orderBy,
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = TodosIncluindo(includeProperties);

            query = (predicate == null) ? query : query.Where(predicate);

            return query.ParaListaPaginavel(pageIndex, pageSize, orderBy, keySelector);
        }

        private void SetEntryStateOf(T entity, EntityState to)
        {
            DbEntityEntry dbEntityEntry = _entitiesContext.Entry<T>(entity);
            dbEntityEntry.State = to;
        }

        /// <summary>
        /// Insere uma entidade
        /// </summary>
        /// <param name="entity">Entidade</param>
        public virtual void Inserir(T entity)
        {
            _entitiesContext.Set<T>().Add(entity);
            SetEntryStateOf(entity, to: EntityState.Added);
        }

        /// <summary>
        /// Atualiza uma entidade
        /// </summary>
        /// <param name="entity">Entidade</param>
        public virtual void Editar(T entity)
        {
            SetEntryStateOf(entity, to: EntityState.Modified);
        }

        /// <summary>
        /// Exclui uma entidade
        /// </summary>
        /// <param name="entity">Entidade</param>
        public virtual void Excluir(T entity)
        {
            SetEntryStateOf(entity, to: EntityState.Deleted);
        }

        /// <summary>
        /// Exclui entidade por pelo seu código
        /// </summary>
        /// <param name="entityId">Chave de entidade</param>
        public virtual void Excluir(TKey entityId)
        {
            var entity = _entitiesContext.Set<T>().Find(entityId);
            SetEntryStateOf(entity, to: EntityState.Deleted);
        }

        /// <summary>
        /// Executa commit da transação
        /// </summary>
        public virtual void Salvar()
        {
            _entitiesContext.SaveChanges();
        }
    }
}
