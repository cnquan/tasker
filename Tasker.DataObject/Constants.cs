using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.DataObject
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

        /// <summary>
        /// 命令执行状态
        /// </summary>
        public enum CommandState
        {
            /// <summary>
            /// 未执行
            /// </summary>
            None = 0,

            /// <summary>
            /// 执行错误
            /// </summary>
            Error = 1,

            /// <summary>
            /// 成功执行
            /// </summary>
            Success = 2
        }

        /// <summary>
        /// 任务通用命令
        /// </summary>
        public enum CommandName
        {
            /// <summary>
            /// 关闭任务
            /// </summary>
            StopTask = 0,

            /// <summary>
            /// 启动任务
            /// </summary>
            StartTask = 1,

            /// <summary>
            /// 重启任务
            /// </summary>
            ReStartTask = 2,

            /// <summary>
            /// 卸载任务
            /// </summary>
            UninstallTask = 3,
        }
    }
}
