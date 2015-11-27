using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace Tasker.Node.NodeRuntime
{
    /// <summary>
    /// 全局任务池管理
    /// </summary>
    public class TaskPoolManager : IDisposable
    {
        /// <summary>
        /// 任务运行池
        /// </summary>
        private static Dictionary<string, NodeTaskRuntimeInfo> TaskRuntimePool = new Dictionary<string, NodeTaskRuntimeInfo>();

        /// <summary>
        /// 任务池管理
        /// </summary>
        private static TaskPoolManager _TaskPoolManager;

        /// <summary>
        /// 任务池操作锁
        /// </summary>
        private static object _Lock = new object();

        /// <summary>
        /// 任务执行计划
        /// </summary>
        private static IScheduler _Sched;

        /// <summary>
        /// 静态初始化
        /// </summary>
        static TaskPoolManager()
        {
            _TaskPoolManager = new TaskPoolManager();
            ISchedulerFactory sf = new StdSchedulerFactory();
            _Sched = sf.GetScheduler();
            _Sched.Start();
        }

        /// <summary>
        /// 获取任务池实例
        /// </summary>
        /// <returns></returns>
        public static TaskPoolManager GetInstance()
        {
            return _TaskPoolManager;
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool Add(string taskId, NodeTaskRuntimeInfo info)
        {
            lock (_Lock)
            {
                if (!TaskRuntimePool.ContainsKey(taskId))
                {
                    JobDetailImpl jobDetail = new JobDetailImpl(info.Task.Id.ToString(), info.Task.Category.Id.ToString(), typeof(TaskJob));
                    var trigger = Tools.CronFactory.CreateTrigger(info);
                    _Sched.ScheduleJob(jobDetail, trigger);
                    TaskRuntimePool.Add(taskId, info);
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 移除任务
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public bool Remove(string taskId)
        {
            lock (_Lock)
            {
                if (TaskRuntimePool.ContainsKey(taskId))
                {
                    NodeTaskRuntimeInfo info = TaskRuntimePool[taskId];
                    TriggerKey t = new TriggerKey(info.Task.Id.ToString(), info.Task.Category.Id.ToString());
                    _Sched.PauseTrigger(t);
                    _Sched.UnscheduleJob(t);
                    _Sched.DeleteJob(new JobKey(info.Task.Id.ToString(), info.Task.Category.Id.ToString()));
                    TaskRuntimePool.Remove(taskId);
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 获取任务运行时信息
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public NodeTaskRuntimeInfo Get(string taskId)
        {
            if (!TaskRuntimePool.ContainsKey(taskId))
                return null;
            lock (_Lock)
            {
                if (TaskRuntimePool.ContainsKey(taskId))
                    return TaskRuntimePool[taskId];
                else
                    return null;
            }
        }

        /// <summary>
        /// 获取节点任务列表
        /// </summary>
        /// <returns></returns>
        public List<NodeTaskRuntimeInfo> GetList()
        {
            return TaskRuntimePool.Values.ToList();
        }

        /// <summary>
        /// 资源释放
        /// </summary>
        public void Dispose()
        {
            if (_Sched != null && !_Sched.IsShutdown)
                _Sched.Shutdown();
        }
    }
}
