using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.TaskManager.TaskRuntime
{
    /// <summary>
    /// 任务运行信息
    /// </summary>
    [Serializable]
    public class TaskRuntimeInfo
    {
        /// <summary>
        /// 任务数据库连接
        /// </summary>
        public string TaskConnectString { get; set; }

        /// <summary>
        /// 任务信息
        /// </summary>
        public Model.Task Task { get; set; }
    }
}
