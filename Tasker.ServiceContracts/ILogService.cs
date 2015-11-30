using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.DataObject;

namespace Tasker.ServiceContracts
{
    public interface ILogService
    {
        /// <summary>
        /// 添加节点日志
        /// </summary>
        /// <param name="nodeId">节点ID</param>
        /// <param name="msg">日志信息</param>
        /// <returns></returns>
        bool AddNodeLog(int nodeId, string msg);

        /// <summary>
        /// 添加节点错误日志
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        bool AddNodeError(int nodeId, string msg, Exception ex);

        /// <summary>
        /// 添加任务日志
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <param name="msg">日志信息</param>
        /// <returns></returns>
        bool AddTaskLog(int taskId, string msg);

        /// <summary>
        /// 添加任务错误日志
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        bool AddTaskError(int taskId, string msg, Exception ex);
    }
}
