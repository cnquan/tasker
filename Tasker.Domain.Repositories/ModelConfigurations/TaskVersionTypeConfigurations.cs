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
    public class TaskVersionTypeConfigurations : EntityTypeConfiguration<TaskVersion>
    {
        public TaskVersionTypeConfigurations()
        {
            ToTable("tb_task_version");
            HasKey(x => x.Id);
            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Version)
                .HasMaxLength(100);
            Property(x => x.FileName)
                .HasMaxLength(200);
        }
    }
}
