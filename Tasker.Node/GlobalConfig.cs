using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Node
{
    /// <summary>
    /// 节点全集配置
    /// </summary>
    public static class GlobalConfig
    {
        /// <summary>
        /// 节点标识
        /// </summary>
        public static int NodeId { get; set; }

        /// <summary>
        /// 任务DLL目录
        /// </summary>
        public static string TaskDLLDir = "TaskDLL";

        /// <summary>
        /// 任务DLL缓存目录
        /// </summary>
        public static string TaskDLLCacheDir = "TaskDLLCache";

        /// <summary>
        /// 任务共享DLL目录
        /// </summary>
        public static string TaskDLLShareDir = "TaskDLLShare";

        /// <summary>
        /// 节点监控
        /// </summary>
        public static List<NodeMonitor.BaseMonitor> Monitors = new List<NodeMonitor.BaseMonitor>();
    }
}
