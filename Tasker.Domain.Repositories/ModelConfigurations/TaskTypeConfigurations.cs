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
    public class TaskTypeConfigurations : EntityTypeConfiguration<Model.Task>
    {
        public TaskTypeConfigurations()
        {
            ToTable("tb_task");
            HasKey(x => x.Id);
            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TaskName)
                .HasMaxLength(200);
            Property(x => x.TaskCron)
                .HasMaxLength(200);
            Property(x => x.TaskMainClassDLLName)
                .HasMaxLength(200);
            Property(x => x.TaskMainClassNameSpace)
                .HasMaxLength(200);
            Property(x => x.Remark)
                .HasMaxLength(2000);
            HasRequired(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId);
            HasRequired(x => x.TaskVersion)
                .WithMany()
                .HasForeignKey(x => x.VersionId);
            HasRequired(x => x.Creator)
                .WithMany()
                .HasForeignKey(x => x.CreatorId);
            HasRequired(x => x.Node)
                .WithMany()
                .HasForeignKey(x => x.NodeId);
        }
    }
}
