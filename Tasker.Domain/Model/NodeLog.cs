using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Tasker.Domain.Aggregate;

namespace Tasker.Domain.Model
{
    /// <summary>
    /// 节点日志
    /// </summary>
    public class NodeLog : IAggregateRoot
    {
        /// <summary>
        /// 标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 节点
        /// </summary>
        public Node Node { get; set; }

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
