using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Domain.Storage
{
    /// <summary>
    /// 枚举字典
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// 任务执行状态
        /// </summary>
        public enum TaskState
        {
            /// <summary>
            /// 停止
            /// </summary>
            Stop,

            /// <summary>
            /// 运行中
            /// </summary>
            Running
        }

        /// <summary>
        /// 日志类型
        /// </summary>
        public enum LogType
        {
            /// <summary>
            /// 常用日志
            /// </summary>
            CommonLog = 1,

            /// <summary>
            /// 系统日志
            /// </summary>
            SystemLog = 2,

            /// <summary>
            /// 系统错误日志
            /// </summary>
            SystemError = 3,

            /// <summary>
            /// 常用错误日志
            /// </summary>
            CommonError = 4,
        }
    }
}
