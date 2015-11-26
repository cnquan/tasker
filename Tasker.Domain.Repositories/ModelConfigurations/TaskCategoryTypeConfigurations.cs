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
    public class TaskCategoryTypeConfigurations : EntityTypeConfiguration<TaskCategory>
    {
        public TaskCategoryTypeConfigurations()
        {
            ToTable("tb_task_category");
            HasKey(x => x.Id);
            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CategoryName)
                .HasMaxLength(200);
            Property(x => x.Remark)
                .HasMaxLength(2000);
        }
    }
}
