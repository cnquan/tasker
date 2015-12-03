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
            get { return efContext; }
        }

        public void BeginTransaction()
        {
            
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Rollback()
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}
