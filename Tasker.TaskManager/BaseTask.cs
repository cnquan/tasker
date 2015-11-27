using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.TaskManager.TaskRuntime;

namespace Tasker.TaskManager
{
    /// <summary>
    /// 任务基类
    /// </summary>
    public abstract class BaseTask : MarshalByRefObject, IDisposable
    {
        /// <summary>
        /// 任务配置信息
        /// </summary>
        public TaskAppInfo AppInfo = new TaskAppInfo();

        /// <summary>
        /// 任务运行时信息
        /// </summary>
        public TaskRuntimeInfo TaskRuntimeInfo = new TaskRuntimeInfo();

        /// <summary>
        /// 任务运行时操作
        /// </summary>
        public TaskRuntimeOperator TaskRuntimeOperator;

        /// <summary>
        /// 任务资源释放类
        /// </summary>
        public TaskDisposeOperator TaskDisposeOperator;

        /// <summary>
        /// 任务基类初始化
        /// </summary>
        public BaseTask()
        {
            TaskRuntimeOperator = new TaskRuntimeOperator(this);
        }

        /// <summary>
        /// 忽略默认对象租用行为
        /// </summary>
        /// <returns></returns>
        public override object InitializeLifetimeService()
        {
            return null;
        }

        /// <summary>
        /// 任务运行入口
        /// </summary>
        public void TryRun()
        {
            try
            {
                TaskRuntimeOperator.UpdateLastStartTime(DateTime.Now);
                Run();
                TaskRuntimeOperator.UpdateLastEndTime(DateTime.Now);
                TaskRuntimeOperator.UpdateTaskSuccess();
                TaskRuntimeOperator.AddTaskLog(new Model.TaskLog()
                {
                    Content = "任务【" + TaskRuntimeInfo.Task.TaskName + "】执行完毕",
                    LogType = Model.LogType.SystemLog,
                    Node_Id = TaskRuntimeInfo.Task.Node_Id,
                    Task_Id = TaskRuntimeInfo.Task.Id,
                    CreateTime = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                TaskRuntimeOperator.UpdateTaskError(DateTime.Now);
                TaskRuntimeOperator.AddTaskLog(new Model.TaskLog()
                {
                    Content = ("错误:" + ex.Message + " 堆栈:" + ex.StackTrace),
                    LogType = Model.LogType.SystemError,
                    Node_Id = TaskRuntimeInfo.Task.Node_Id,
                    Task_Id = TaskRuntimeInfo.Task.Id,
                    CreateTime = DateTime.Now
                });
            }
        }

        /// <summary>
        /// 任务运行契约接口
        /// </summary>
        public virtual void Run() { }

        /// <summary>
        /// 资源释放
        /// </summary>
        public void Dispose()
        {
            if (TaskDisposeOperator != null)
            {
                TaskDisposeOperator.State = TaskDisposeState.Disposing;
                TaskRuntimeOperator.AddTaskLog(new Model.TaskLog()
                {
                    Content = "任务【" + TaskRuntimeInfo.Task.TaskName + "】开始资源释放,并等待任务安全终止信号",
                    LogType = Model.LogType.SystemLog,
                    Node_Id = TaskRuntimeInfo.Task.Node_Id,
                    Task_Id = TaskRuntimeInfo.Task.Id,
                    CreateTime = DateTime.Now
                });
                TaskDisposeOperator.WaitDisposeFinished();
                TaskRuntimeOperator.AddTaskLog(new Model.TaskLog()
                {
                    Content = "任务【" + TaskRuntimeInfo.Task.TaskName + "】已安全终止结束",
                    LogType = Model.LogType.SystemLog,
                    Node_Id = TaskRuntimeInfo.Task.Node_Id,
                    Task_Id = TaskRuntimeInfo.Task.Id,
                    CreateTime = DateTime.Now
                });
            }
        }
    }
}
