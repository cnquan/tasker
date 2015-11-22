using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Tasker.Domain.Model;

namespace Tasker.Domain.Repositories
{
    /// <summary>
    /// EF DBContext 初始化
    /// </summary>
    public sealed class TaskerDbContextInitailizer : DropCreateDatabaseIfModelChanges<TaskerDbContext>
    {
        protected override void Seed(TaskerDbContext context)
        {
            /* 默认数据创建 */
            User user = new User()
            {
                UserName = "admin",
                UserPwd = "123",
                UserEmail = "starworking@126.com",
                UserNo = "001",
                UserTel = "13559134655",
                Remark = "管理员"
            };
            context.User.Add(user);

            base.Seed(context);
        }
    }
}
