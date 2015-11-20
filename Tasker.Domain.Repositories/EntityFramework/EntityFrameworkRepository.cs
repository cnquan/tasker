using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Tasker.Domain.Repositories;
using Tasker.Domain.Aggregate;

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
        /// EF上下文实例
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

        /// <summary>
        /// EF上下文实例
        /// </summary>
        protected IEntityFrameworkRepositoryContext EFContext
        {
            get { return this.efContext; }
        }

        public IRepositoryContext Context
        {
            get { return this.efContext; }
        }

        public void Add(TAggregateRoot obj)
        {
            throw new NotImplementedException();
        }

        public void Modify(TAggregateRoot obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(TAggregateRoot obj)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TAggregateRoot> FindAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> where, Expression<Func<TAggregateRoot, dynamic>> sort)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> where, Expression<Func<TAggregateRoot, dynamic>> sort, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TAggregateRoot> FindAll(params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sort, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            throw new NotImplementedException();
        }

        public TAggregateRoot Find(params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            throw new NotImplementedException();
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
