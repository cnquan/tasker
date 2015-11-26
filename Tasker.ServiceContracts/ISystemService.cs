using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.ServiceContracts
{
    public interface ISystemService
    {
        /// <summary>
        /// 获取系统时间
        /// </summary>
        /// <returns></returns>
        DateTime GetSysTime();
    }
}
