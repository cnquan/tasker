using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.TaskManager.DAL;

namespace Tasker.TaskManager.TaskRuntime
{
    /// <summary>
    /// 任务底层操作类
    /// </summary>
    public class TaskRuntimeOperator
    {
        /// <summary>
        /// 任务引用
        /// </summary>
        protected BaseTask Task = null;

        /// <summary>
        /// 初始化操作
        /// </summary>
        /// <param name="task"></param>
        public TaskRuntimeOperator(BaseTask task)
        {
            Task = task;
        }

        /// <summary>
        /// 最近一次运行开始时间
        /// </summary>
        /// <param name="time"></param>
        public void UpdateLastStartTime(DateTime time)
        {
            TaskDAL dal = new TaskDAL(Task.TaskRuntimeInfo.TaskConnectString);
            dal.UpdateTaskLastStartTime(Task.TaskRuntimeInfo.Task.Id, time);
        }

        /// <summary>
        /// 最近一次运行结束时间
        /// </summary>
        /// <param name="time"></param>
        public void UpdateLastEndTime(DateTime time)
        {
            TaskDAL dal = new TaskDAL(Task.TaskRuntimeInfo.TaskConnectString);
            dal.UpdateTaskLastEndTime(Task.TaskRuntimeInfo.Task.Id, time);
        }

        /// <summary>
        /// 任务运行失败
        /// </summary>
        /// <param name="time"></param>
        public void UpdateTaskError(DateTime time)
        {
            TaskDAL dal = new TaskDAL(Task.TaskRuntimeInfo.TaskConnectString);
            dal.UpdateTaskError(Task.TaskRuntimeInfo.Task.Id, time);
        }

        /// <summary>
        /// 任务运行成功
        /// </summary>
        /// <param name="time"></param>
        public void UpdateTaskSuccess()
        {
            TaskDAL dal = new TaskDAL(Task.TaskRuntimeInfo.TaskConnectString);
            dal.UpdateTaskSuccess(Task.TaskRuntimeInfo.Task.Id);
        }

        /// <summary>
        /// 任务运行日志
        /// </summary>
        /// <param name="obj"></param>
        public void AddTaskLog(Model.TaskLog obj)
        {
            TaskLogDAL dal = new TaskLogDAL(Task.TaskRuntimeInfo.TaskConnectString);
            dal.Add(obj);
        }
    }
}
