using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.ServiceContracts;

namespace Tasker.Node.NodeMonitor
{
    public class TaskRecoverMonitor : BaseMonitor
    {
        private ILogService _LogService;
        private INodeService _NodeService;

        public TaskRecoverMonitor(ILogService logService,
            INodeService nodeService)
        {
            _LogService = logService;
            _NodeService = nodeService;
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
                        _LogService.AddNodeError(GlobalConfig.NodeId, e);
                    }
                }
                runTasks.ForEach((c) =>
                {
                    _LogService.AddTaskError(c, new Exception("任务【" + c + "】资源运行异常，可能需要手动卸载。任务处于停止状态，但是相应集群节点中，发现任务存在在运行池中未释放"));
                });

            }
            catch (Exception ex)
            {
                _LogService.AddNodeError(GlobalConfig.NodeId, ex);
            }
        }
    }
}
