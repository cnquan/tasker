using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.DataObject
{
    public class TaskLogDTO
    {
        /// <summary>
        /// 标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 任务
        /// </summary>
        public Task Task { get; set; }

        /// <summary>
        /// 节点
        /// </summary>
        public NodeDTO Node { get; set; }

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
