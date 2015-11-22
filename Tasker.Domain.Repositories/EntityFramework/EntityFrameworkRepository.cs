using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;
using Tasker.Domain.Repositories;
using Tasker.Domain.Aggregate;
using Tasker.Domain.Storage;

namespace Tasker.Domain.Repositories.EntityFramework
{
    /// <summary>
    /// EF仓储实例
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TAggregateRoot"></typeparam>
    public class EntityFrameworkRepository<TKey, TAggregateRoot> : IRepository<TKey, TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot<TKey>
    {
        /// <summary>
        /// EF上下文
        /// </summary>
        private readonly IEntityFrameworkRepositoryContext efContext;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context"></param>
        public EntityFrameworkRepository(IRepositoryContext context)
        {
            if (context is IEntityFrameworkRepositoryContext)
                this.efContext = context as IEntityFrameworkRepositoryContext;
        }

        public IRepositoryContext Context
        {
            get { return this.efContext; }
        }

        public void Add(TAggregateRoot obj)
        {
            efContext.Context.Entry(obj).State = System.Data.EntityState.Added;
        }

        public void Modify(TAggregateRoot obj)
        {
            efContext.Context.Entry(obj).State = System.Data.EntityState.Modified;
        }

        public void Delete(TAggregateRoot obj)
        {
            efContext.Context.Entry(obj).State = System.Data.EntityState.Deleted;
        }

        public IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> where = null,
                                                  Expression<Func<TAggregateRoot, dynamic>> sort = null,
                                                  SortOrder sortby = SortOrder.Unspecified,
                                                  params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            var query = efContext.Context.Set<TAggregateRoot>().AsQueryable();
            if (eagerLoadingProperties != null &&
               eagerLoadingProperties.Length > 0)
            {
                for (int i = 1; i < eagerLoadingProperties.Length; i++)
                {
                    var eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperties[i]);
                    query = query.Include(eagerLoadingPath);
                }

            }
            if (where != null)
            {
                query = query.Where(where);
            }
            if (sort != null)
            {
                switch (sortby)
                {
                    case SortOrder.Ascending:
                        query = query.OrderBy(sort); break;
                    case SortOrder.Descending:
                        query = query.OrderByDescending(sort); break;
                    default:
                        break;
                }
            }
            return query;
        }

        public PagedResult<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> where = null,
                                                  Expression<Func<TAggregateRoot, dynamic>> sort = null,
                                                  SortOrder sortby = SortOrder.Unspecified,
                                                  int pageNumber = 1,
                                                  int pageSize = 10,
                                                  params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            var query = this.FindAll(where, sort, sortby, eagerLoadingProperties);
            int skip = (pageNumber - 1) * pageSize;
            int take = pageSize;
            var list = query.Skip(skip).Take(take).GroupBy(p => new { Total = query.Count() }).FirstOrDefault();
            if (list == null)
                return null;
            else
                return new PagedResult<TAggregateRoot>(list.Key.Total, (list.Key.Total + pageSize - 1) / pageSize, pageSize, pageNumber, list.Select(p => p).ToList());
        }

        public TAggregateRoot Find(Expression<Func<TAggregateRoot, bool>> where,
                                   params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            var query = efContext.Context.Set<TAggregateRoot>().AsQueryable();
            if (eagerLoadingProperties != null &&
                eagerLoadingProperties.Length > 0)
            {
                for (int i = 1; i < eagerLoadingProperties.Length; i++)
                {
                    var eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperties[i]);
                    query = query.Include(eagerLoadingPath);
                }
            }
            return query.Where(where).FirstOrDefault();
        }

        private string GetEagerLoadingPath(Expression<Func<TAggregateRoot, dynamic>> eagerLoadingProperty)
        {
            MemberExpression memberExpression = this.GetMemberInfo(eagerLoadingProperty);
            var parameterName = eagerLoadingProperty.Parameters.First().Name;
            var memberExpressionStr = memberExpression.ToString();
            var path = memberExpressionStr.Replace(parameterName + ".", "");
            return path;
        }

        private MemberExpression GetMemberInfo(LambdaExpression lambda)
        {
            if (lambda == null)
                throw new ArgumentNullException("method");

            MemberExpression memberExpr = null;

            if (lambda.Body.NodeType == ExpressionType.Convert)
            {
                memberExpr = ((UnaryExpression)lambda.Body).Operand as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = lambda.Body as MemberExpression;
            }

            if (memberExpr == null)
                throw new ArgumentException("method");

            return memberExpr;
        }
    }

    /// <summary>
    /// 默认EF仓储实例
    /// </summary>
    /// <typeparam name="TAggregateRoot"></typeparam>
    public class EntityFrameworkRepository<TAggregateRoot> : EntityFrameworkRepository<int, TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {
        public EntityFrameworkRepository(IRepositoryContext context)
            : base(context)
        {

        }
    }

}
