using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.DataObject
{
    public class NodeDTO
    {
        /// <summary>
        /// 标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// 节点宿主
        /// </summary>
        public string NodeHost { get; set; }

        /// <summary>
        /// 节点心跳周期（毫秒）
        /// </summary>
        public int? NodeHeartbeat { get; set; }

        /// <summary>
        /// 节点创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 节点刷新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
