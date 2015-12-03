using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain.Aggregate;
using Tasker.Domain.Storage;

namespace Tasker.Domain.Model
{
    /// <summary>
    /// 命令
    /// </summary>
    public class Command:IAggregateRoot
    {
        /// <summary>
        /// 标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 命令名称
        /// </summary>
        public string CommandName { get; set; }

        /// <summary>
        /// 命令执行状态
        /// </summary>
        public Constants.CommandState? State { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        public int NodeId { get; set; }

        /// <summary>
        /// 节点
        /// </summary>
        public virtual Node Node { get; set; }

        /// <summary>
        /// 任务ID
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// 任务
        /// </summary>
        public virtual Task Task { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}
