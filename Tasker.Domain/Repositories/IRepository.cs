using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain.Aggregate;

namespace Tasker.Domain.Repositories
{
    /// <summary>
    /// 仓储规范
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TAggregateRoot"></typeparam>
    public interface IRepository<TKey, TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot<TKey>
    {
        /// <summary>
        /// 仓储上下文
        /// </summary>
        IRepositoryContext Context { get; }

        /// <summary>
        /// 新增聚合
        /// </summary>
        /// <param name="obj"></param>
        void Add(TAggregateRoot obj);

        /// <summary>
        /// 更新聚合
        /// </summary>
        /// <param name="obj"></param>
        void Modify(TAggregateRoot obj);

        /// <summary>
        /// 移除聚合
        /// </summary>
        /// <param name="obj"></param>
        void Delete(TAggregateRoot obj);

        /// <summary>
        /// 查询全部聚合
        /// </summary>
        /// <returns></returns>
        IQueryable<TAggregateRoot> FindAll();

        /// <summary>
        /// 查询聚合
        /// </summary>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> where,
                                           Expression<Func<TAggregateRoot, dynamic>> sort);

        /// <summary>
        /// 分页查询聚合
        /// </summary>
        /// <param name="sortPredicate"></param>
        /// <param name="sort"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> where,
                                           Expression<Func<TAggregateRoot, dynamic>> sort, int pageNumber, int pageSize);

        /// <summary>
        /// 查询全部聚合(加载聚合项)
        /// </summary>
        /// <param name="eagerLoadingProperties"></param>
        /// <returns></returns>
        IQueryable<TAggregateRoot> FindAll(params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);

        /// <summary>
        /// 查询全部聚合(带排序)
        /// </summary>
        /// <param name="sort"></param>
        /// <param name="eagerLoadingProperties"></param>
        /// <returns></returns>
        IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sort,
                                           params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);

        /// <summary>
        /// 查询单个聚合
        /// </summary>
        /// <param name="eagerLoadingProperties"></param>
        /// <returns></returns>
        TAggregateRoot Find(params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);
    }

    /// <summary>
    /// 默认仓储规范
    /// </summary>
    /// <typeparam name="TAggregateRoot"></typeparam>
    public interface IRepository<TAggregateRoot> : IRepository<int, TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {

    }
}
