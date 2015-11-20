using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Tasker.Domain.Model;

namespace Tasker.Domain.Repositories
{
    public class TaskerDbContext : DbContext
    {
        public TaskerDbContext()
            : base("Tasker")
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.AutoDetectChangesEnabled = true;
        }

        /// <summary>
        /// User
        /// </summary>
        public DbSet<Model.User.User> User
        {
            get { return Set<Model.User.User>(); }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations
                        .Add(new ModelConfigurations.User.UserTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
