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
    public class NodeLogTypeConfigurations:EntityTypeConfiguration<Model.NodeLog>
    {
        public NodeLogTypeConfigurations()
        {
            ToTable("tb_node_log");
            HasKey(x => x.Id);
            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
