using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using Tasker.Domain.Model;

namespace Tasker.Domain.Repositories.ModelConfigurations
{
    public class TaskLogTypeConfigurations : EntityTypeConfiguration<TaskLog>
    {
        public TaskLogTypeConfigurations()
        {
            ToTable("tb_task_log");
            HasKey(x => x.Id);
            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(x => x.Node)
                .WithMany()
                .HasForeignKey(x => x.NodeId);
            HasRequired(x => x.Task)
                .WithMany()
                .HasForeignKey(x => x.TaskId);
        }
    }
}
