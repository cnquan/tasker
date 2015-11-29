using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.TaskManager;
using Tasker.TaskManager.TaskRuntime;
using Tasker.ServiceContracts;
using Tasker.Infrastructure;
using Tasker.Infrastructure.Unity;

namespace Tasker.Node.NodeRuntime
{
    /// <summary>
    /// 任务操作
    /// </summary>
    public class TaskProvider
    {
        ITaskService _TaskService;
        ILogService _LogService;

        public TaskProvider(ITaskService taskService,
            ILogService logService)
        {
            _TaskService = taskService;
            _LogService = logService;
        }

        public TaskProvider()
        {
            _TaskService = ServiceLocator.Instance.GetService<ITaskService>();
            _LogService = ServiceLocator.Instance.GetService<ILogService>();
        }

        /// <summary>
        /// 开启任务
        /// </summary>
        /// <param name="taskId"></param>
        public bool Start(int taskId)
        {
            var taskRuntime = TaskPoolManager.GetInstance().Get(taskId.ToString());
            if (taskRuntime != null)
            {
                throw new Exception("任务【" + taskRuntime.Task.TaskName + "】已在运行中");
            }

            taskRuntime = new NodeTaskRuntimeInfo();
            taskRuntime.TaskLock = new TaskLock();
            taskRuntime.Task = _TaskService.GetTask(taskId);

            string localPath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\')
                                + "\\" + GlobalConfig.TaskDLLCacheDir
                                + "\\" + taskRuntime.Task.Id
                                + "\\" + taskRuntime.Task.TaskVersion.Id
                                + "\\" + taskRuntime.Task.TaskVersion.FileName;
            string installPath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\')
                                + "\\" + GlobalConfig.TaskDLLDir
                                + "\\" + taskRuntime.Task.Id;
            string sharePath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\')
                                + "\\" + GlobalConfig.TaskDLLShareDir;
            string mainClassPath = installPath + "\\" + taskRuntime.Task.TaskMainClassDLLName;

            IOHelper.DirMake(localPath);
            IOHelper.DirMake(installPath);
            File.WriteAllBytes(localPath, taskRuntime.Task.TaskVersion.ZipFile);
            CompressHelper.UnCompress(localPath, installPath);
            IOHelper.DirCopy(sharePath, installPath);

            try
            {
                var taskDLL = new AppDomainLoader<BaseTask>().Load(mainClassPath,
                            taskRuntime.Task.TaskMainClassNameSpace,
                            out taskRuntime.Domain);
                taskDLL.TaskRuntimeInfo = new TaskRuntimeInfo()
                {
                    TaskConnectString = GlobalConfig.TaskConnectString,
                    Task = null//TODO: Copy Task Object
                };
                taskDLL.AppInfo = new TaskAppInfo();
                if (taskRuntime.Task.TaskConfiguration.IsNotNull())
                {
                    taskDLL.AppInfo = null; //TODO: Serialization Json 
                }
                taskRuntime.TaskDLL = taskDLL;

                bool result = TaskPoolManager.GetInstance().Add(taskId.ToString(), taskRuntime);
                if (result)
                {
                    _TaskService.UpdateTaskState(taskId, DataObject.Constants.TaskState.Running);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        /// <summary>
        /// 停止任务
        /// </summary>
        /// <param name="taskId"></param>
        public bool Stop(int taskId)
        {
            return true;
        }

        /// <summary>
        /// 卸载任务
        /// </summary>
        /// <param name="taskId"></param>
        public bool Uninstall(int taskId)
        {
            return true;
        }

        /// <summary>
        /// 资源释放
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="info">任务运行时信息</param>
        /// <param name="isForce">强制释放标识</param>
        /// <returns></returns>
        protected bool Dispose(int taskId, NodeTaskRuntimeInfo info, bool isForce)
        {
            return true;
        }
    }
}
