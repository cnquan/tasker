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
    public class UserTypeConfiguration : EntityTypeConfiguration<Model.User>
    {
        public UserTypeConfiguration()
        {
            ToTable("tb_user");
            HasKey(x => x.Id);
            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.UserEmail)
                .IsUnicode()
                .HasMaxLength(200);
            Property(x => x.UserName)
                .IsUnicode()
                .HasMaxLength(200);
            Property(x => x.UserNo)
                .IsUnicode()
                .HasMaxLength(64);
            Property(x => x.UserPwd)
                .IsUnicode()
                .HasMaxLength(64);
            Property(x => x.UserTel)
                .IsUnicode()
                .HasMaxLength(100);
            Property(x => x.Remark)
                .IsUnicode()
                .HasMaxLength(2000);
        }
    }
}
