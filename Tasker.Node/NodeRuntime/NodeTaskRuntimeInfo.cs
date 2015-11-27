using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.TaskManager;
using Tasker.TaskManager.TaskRuntime;

namespace Tasker.Node.NodeRuntime
{
    /// <summary>
    /// 节点任务运行时信息
    /// </summary>
    public class NodeTaskRuntimeInfo
    {
        /// <summary>
        /// 应用程序域
        /// </summary>
        public AppDomain Domain;

        /// <summary>
        /// 任务信息
        /// </summary>
        public DataObject.TaskDTO Task;

        /// <summary>
        /// 任务实例
        /// </summary>
        public BaseTask TaskDLL;

        /// <summary>
        /// 任务锁
        /// </summary>
        public TaskLock TaskLock;
    }
}
