using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.TaskManager.Model
{
    /// <summary>
    /// 任务日志
    /// </summary>
    public class TaskLog
    {
        /// <summary>
        /// 标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 任务
        /// </summary>
        public int Task_Id { get; set; }

        /// <summary>
        /// 节点
        /// </summary>
        public int Node_Id { get; set; }

        /// <summary>
        /// 日志内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 日志类型
        /// </summary>
        public LogType LogType { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }

    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType
    {
        [Description("常用日志")]
        CommonLog = 1,
        [Description("系统日志")]
        SystemLog = 2,
        [Description("系统错误日志")]
        SystemError = 3,
        [Description("常用错误日志")]
        CommonError = 4,
    }
}
