using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.DataObject;

namespace Tasker.ServiceContracts
{
    public interface IUserService
    {
        /// <summary>
        /// 根据编号获取用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        UserDTO GetUser(string userNo);
    }
}
