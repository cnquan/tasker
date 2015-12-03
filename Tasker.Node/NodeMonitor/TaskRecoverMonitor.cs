using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.ServiceContracts;
using Tasker.Infrastructure.Unity;
using Tasker.Infrastructure;

namespace Tasker.Node.NodeMonitor
{
    public class TaskRecoverMonitor : BaseMonitor
    {
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
                Tools.ServiceSupport service = new Tools.ServiceSupport();
                List<int> tasks = service.NodeService.GetNodeTasks(GlobalConfig.NodeId, DataObject.Constants.TaskState.Stop);
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
                        service.LogService.AddNodeError(GlobalConfig.NodeId, "节点任务异常运行检测出现错误", e);
                    }
                }
                runTasks.ForEach((c) =>
                {
                    service.LogService.AddTaskError(c, "任务[" + c + "]资源运行异常，可能需要手动卸载。", new Exception("任务处于停止状态，但是相应集群节点中，发现任务存在在运行池中未释放"));
                });

            }
            catch (Exception ex)
            {
                LogHelper.Write("节点[" + GlobalConfig.NodeId + "],任务资源回收异常,错误信息：" + ex.Message);
            }
        }
    }
}
