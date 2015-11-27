using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Infrastructure.Unity;
using Tasker.ServiceContracts;
using Tasker.Node.NodeRuntime;

namespace Tasker.Node.NodeMonitor
{
    /// <summary>
    /// 任务异常停止监控
    /// </summary>
    public class TaskStopedMonitor : BaseMonitor
    {
        private ILogService _LogService;
        private INodeService _NodeService;

        public TaskStopedMonitor(ILogService logService,
            INodeService nodeService)
        {
            _LogService = logService;
            _NodeService = nodeService;
        }

        public override int Interval
        {
            get
            {
                return 1000 * 60;//一分钟间隔
            }
        }

        public override void Run()
        {
            try
            {
                List<int> tasks = _NodeService.GetNodeTasks(GlobalConfig.NodeId, DataObject.Constants.TaskState.Running);
                List<int> stopTasks = new List<int>();
                foreach (var item in tasks)
                {
                    try
                    {
                        var info = TaskPoolManager.GetInstance().Get(item.ToString());
                        if (info == null)
                            stopTasks.Add(item);
                    }
                    catch (Exception ex)
                    {
                        _LogService.AddNodeError(GlobalConfig.NodeId, ex);
                    }
                }
                stopTasks.ForEach((c) =>
                {
                    _LogService.AddTaskError(c, new Exception("任务【" + c + "】处于运行状态，但是相应集群节点中，未发现任务在运行"));
                });
            }
            catch (Exception ex)
            {
                _LogService.AddNodeError(GlobalConfig.NodeId, ex);
            }
        }
    }
}
