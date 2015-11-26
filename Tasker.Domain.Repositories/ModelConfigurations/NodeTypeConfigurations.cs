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
    public class NodeTypeConfigurations : EntityTypeConfiguration<Node>
    {
        public NodeTypeConfigurations()
        {
            ToTable("tb_node");
            HasKey(x => x.Id);
            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.NodeName)
                .IsUnicode()
                .HasMaxLength(200);
            Property(x => x.NodeHost)
                .IsUnicode()
                .HasMaxLength(100);
            Property(x => x.Remark)
                .IsUnicode()
                .HasMaxLength(2000);
        }
    }
}
