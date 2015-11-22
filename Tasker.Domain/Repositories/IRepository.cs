using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain.Aggregate;
using Tasker.Domain.Storage;

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
        /// 查询聚合
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <param name="sortby">排序方式</param>
        /// <param name="eagerLoadingProperties">动态加载</param>
        /// <returns></returns>
        IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> where = null,
                                           Expression<Func<TAggregateRoot, dynamic>> sort = null,
                                           SortOrder sortby = SortOrder.Unspecified,
                                           params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);

        /// <summary>
        /// 分页查询聚合
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <param name="sortby">排序方式</param>
        /// <param name="pageNumber">页数</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="eagerLoadingProperties">动态加载</param>
        /// <returns></returns>
        PagedResult<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> where = null,
                                           Expression<Func<TAggregateRoot, dynamic>> sort = null,
                                           SortOrder sortby = SortOrder.Unspecified,
                                           int pageNumber = 1,
                                           int pageSize = 10,
                                           params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);

        /// <summary>
        /// 查询单个聚合
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="eagerLoadingProperties">动态加载</param>
        /// <returns></returns>
        TAggregateRoot Find(Expression<Func<TAggregateRoot, bool>> where,
                            params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);
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
