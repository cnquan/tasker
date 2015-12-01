using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.ServiceContracts;
using Tasker.Infrastructure.Unity;

namespace Tasker.Node.NodeMonitor
{
    public class TaskRecoverMonitor : BaseMonitor
    {
        private ILogService _LogService;
        private INodeService _NodeService;

        public TaskRecoverMonitor()
        {
            _LogService = ServiceLocator.Instance.GetService<ILogService>();
            _NodeService = ServiceLocator.Instance.GetService<INodeService>();
        }

        public override int Interval
        {
            get
            {
                return 1000 * 60;//间隔一分钟
            }
        }

        public override void Run()
        {
            try
            {
                List<int> tasks = _NodeService.GetNodeTasks(GlobalConfig.NodeId, DataObject.Constants.TaskState.Stop);
                List<int> runTasks = new List<int>();
                foreach (var item in tasks)
                {
                    try
                    {
                        var info = Tasker.Node.NodeRuntime.TaskPoolManager.GetInstance().Get(item.ToString());
                        if (info != null)
                            runTasks.Add(item);
                    }
                    catch (Exception e)
                    {
                        _LogService.AddNodeError(GlobalConfig.NodeId, "节点任务异常运行检测出现错误", e);
                    }
                }
                runTasks.ForEach((c) =>
                {
                    _LogService.AddTaskError(c, "任务【" + c + "】资源运行异常，可能需要手动卸载。", new Exception("任务处于停止状态，但是相应集群节点中，发现任务存在在运行池中未释放"));
                });

            }
            catch (Exception ex)
            {
                _LogService.AddNodeError(GlobalConfig.NodeId, "任务资源回收异常", ex);
            }
        }
    }
}
