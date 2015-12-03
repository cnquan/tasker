using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Domain.Model
{
    /// <summary>
    /// 任务日志
    /// </summary>
    public class TaskLog : Aggregate.IAggregateRoot
    {
        /// <summary>
        /// 标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 任务
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// 任务
        /// </summary>
        public virtual Task Task { get; set; }

        /// <summary>
        /// 节点
        /// </summary>
        public int NodeId { get; set; }

        /// <summary>
        /// 节点
        /// </summary>
        public virtual Node Node { get; set; }

        /// <summary>
        /// 日志内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 日志类型
        /// </summary>
        public Storage.Constants.LogType? LogType { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}
