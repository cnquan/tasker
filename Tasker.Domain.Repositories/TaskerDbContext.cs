using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Tasker.Domain.Model;
using System.Data.Entity.ModelConfiguration.Conventions;

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

        /// <summary>
        /// Node
        /// </summary>
        public DbSet<Model.Node> Node
        {
            get { return Set<Model.Node>(); }
        }

        /// <summary>
        /// NodeLog
        /// </summary>
        public DbSet<Model.NodeLog> NodeLog
        {
            get { return Set<Model.NodeLog>(); }
        }

        /// <summary>
        /// Task
        /// </summary>
        public DbSet<Model.Task> Task
        {
            get { return Set<Model.Task>(); }
        }

        /// <summary>
        /// TaskCategory
        /// </summary>
        public DbSet<Model.TaskCategory> TaskCategory
        {
            get { return Set<Model.TaskCategory>(); }
        }

        /// <summary>
        /// TaskVersion
        /// </summary>
        public DbSet<Model.TaskVersion> TaskVersion
        {
            get { return Set<Model.TaskVersion>(); }
        }

        /// <summary>
        /// TaskLog
        /// </summary>
        public DbSet<Model.TaskLog> TaskLog
        {
            get { return Set<Model.TaskLog>(); }
        }

        /// <summary>
        /// Performance
        /// </summary>
        public DbSet<Model.Performance> Performance
        {
            get { return Set<Model.Performance>(); }
        }

        /// <summary>
        /// Commands
        /// </summary>
        public DbSet<Model.Command> Command
        {
            get { return Set<Model.Command>(); }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();//关闭级联删除
            modelBuilder.Configurations
                        .Add(new ModelConfigurations.UserTypeConfiguration())
                        .Add(new ModelConfigurations.NodeTypeConfigurations())
                        .Add(new ModelConfigurations.PerformanceTypeConfigurations())
                        .Add(new ModelConfigurations.TaskCategoryTypeConfigurations())
                        .Add(new ModelConfigurations.TaskLogTypeConfigurations())
                        .Add(new ModelConfigurations.TaskTypeConfigurations())
                        .Add(new ModelConfigurations.TaskVersionTypeConfigurations())
                        .Add(new ModelConfigurations.NodeLogTypeConfigurations())
                        .Add(new ModelConfigurations.CommandTypeConfigurations());
            base.OnModelCreating(modelBuilder);
        }
    }
}
