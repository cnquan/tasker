using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain.Model.User;

namespace Tasker.Domain.Repositories.UserRepository
{
    public interface IUserRepository : IRepository<int, User>
    {
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userNo"></param>
        /// <returns></returns>
        User GetUserByNo(string userNo);
    }
}
