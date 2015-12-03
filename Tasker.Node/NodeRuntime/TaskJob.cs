using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Infrastructure.Unity;
using Tasker.Infrastructure;
using Tasker.ServiceContracts;
using Quartz;

namespace Tasker.Node.NodeRuntime
{
    public class TaskJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                Tools.ServiceSupport service = new Tools.ServiceSupport();
                int taskId = Convert.ToInt32(context.JobDetail.Key.Name);
                NodeTaskRuntimeInfo info = TaskPoolManager.GetInstance().Get(taskId.ToString());
                if (info == null || info.TaskDLL == null)
                {
                    service.LogService.AddTaskError(taskId, "", new System.Exception("当前任务信息为空引用，任务编号[" + taskId + "]"));
                    return;
                }
                info.TaskLock.Invoke(() =>
                {
                    try
                    {
                        info.TaskDLL.TryRun();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                });
            }
            catch (Exception ex)
            {
                LogHelper.Write("任务回调系统异常，错误信息：" + ex.Message);
            }
        }
    }
}
