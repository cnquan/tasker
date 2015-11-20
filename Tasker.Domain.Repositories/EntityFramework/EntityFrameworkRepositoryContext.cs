using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Tasker.Domain.Repositories;

namespace Tasker.Domain.Repositories.EntityFramework
{
    public class EntityFrameworkRepositoryContext : IEntityFrameworkRepositoryContext
    {
        private readonly DbContext efContext;

        public EntityFrameworkRepositoryContext(DbContext context)
        {
            this.efContext = context;
        }

        public DbContext Context
        {
            get { return this.efContext; }
        }

        public Guid Id
        {
            get { return Guid.NewGuid(); }
        }

        public void BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
