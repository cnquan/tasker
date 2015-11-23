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
        static TaskerDbContext()
        {
            Database.SetInitializer<TaskerDbContext>(new TaskerDbContextInitailizer());
        }

        public TaskerDbContext()
            : base(new TaskerDbConnection().ConnectionString)
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.AutoDetectChangesEnabled = true;
        }

        /// <summary>
        /// User
        /// </summary>
        public DbSet<Model.User> User
        {
            get { return Set<Model.User>(); }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations
                        .Add(new ModelConfigurations.UserTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
