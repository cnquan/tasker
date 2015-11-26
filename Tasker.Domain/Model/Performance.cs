using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain.Aggregate;

namespace Tasker.Domain.Model
{
    /// <summary>
    /// 任务性能
    /// </summary>
    public class Performance : IAggregateRoot
    {
        /// <summary>
        /// 标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 宿主节点
        /// </summary>
        public Node Node { get; set; }

        /// <summary>
        /// 任务
        /// </summary>
        public Task Task { get; set; }

        /// <summary>
        /// CPU占用率
        /// </summary>
        public double? Cpu { get; set; }

        /// <summary>
        /// 内测占用率
        /// </summary>
        public double? Memory { get; set; }

        /// <summary>
        /// 应用大小
        /// </summary>
        public double? InstallSize { get; set; }

        /// <summary>
        /// 刷新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
    }
}
