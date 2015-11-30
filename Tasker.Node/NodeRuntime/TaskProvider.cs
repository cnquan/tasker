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
using Tasker.Infrastructure.Serialization;

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
                    Task = new TaskManager.Model.Task()
                    {
                        Id = taskRuntime.Task.Id,
                        TaskName = taskRuntime.Task.TaskName,
                        Node_Id = taskRuntime.Task.Node.Id,
                        TaskCreateTime = taskRuntime.Task.TaskCreateTime,
                        TaskUpdateTime = taskRuntime.Task.TaskUpdateTime,
                        TaskLastStartTime = taskRuntime.Task.TaskLastStartTime,
                        TaskLastEndTime = taskRuntime.Task.TaskLastEndTime,
                        TaskLastErrorTime = taskRuntime.Task.TaskLastErrorTime,
                        TaskRunCount = taskRuntime.Task.TaskRunCount,
                        TaskErrorCount = taskRuntime.Task.TaskErrorCount,
                        TaskState = (int)taskRuntime.Task.TaskState,
                        TaskVersion_Id = taskRuntime.Task.TaskVersion.Id,
                        TaskConfiguration = taskRuntime.Task.TaskConfiguration,
                        TaskCron = taskRuntime.Task.TaskCron,
                        TaskMainClassDLLName = taskRuntime.Task.TaskMainClassDLLName,
                        TaskMainClassNameSpace = taskRuntime.Task.TaskMainClassNameSpace,
                        Creator_Id = taskRuntime.Task.Creator.Id,
                        CreateDate = taskRuntime.Task.CreateDate,
                        Remark = taskRuntime.Task.Remark
                    }
                };
                taskDLL.AppInfo = new TaskAppInfo();
                if (taskRuntime.Task.TaskConfiguration.IsNotNull())
                {
                    taskDLL.AppInfo = new JsonHelper().Deserialize<TaskAppInfo>(taskRuntime.Task.TaskConfiguration);
                }
                taskRuntime.TaskDLL = taskDLL;

                bool result = TaskPoolManager.GetInstance().Add(taskId.ToString(), taskRuntime);
                if (result)
                {
                    _TaskService.UpdateTaskState(taskId, DataObject.Constants.TaskState.Running);
                }
                _LogService.AddNodeLog(taskRuntime.Task.Node.Id,
                    "节点【" + taskRuntime.Task.Node.NodeName + "】开启任务【" + taskRuntime.Task.TaskName + "】成功！");
                return result;
            }
            catch (Exception ex)
            {
                Dispose(taskId, taskRuntime, true);
                throw ex;
            }
        }

        /// <summary>
        /// 停止任务
        /// </summary>
        /// <param name="taskId"></param>
        public bool Stop(int taskId)
        {
            var task = TaskPoolManager.GetInstance().Get(taskId.ToString());
            bool result;
            if (task == null)
            {
                result = _TaskService.UpdateTaskState(taskId, DataObject.Constants.TaskState.Stop);
            }
            else
            {
                result = Dispose(taskId, task, true);
                if (result)
                {
                    _TaskService.UpdateTaskState(taskId, DataObject.Constants.TaskState.Stop);
                }
            }
            _LogService.AddTaskLog(taskId, "节点【" + GlobalConfig.NodeId + "】停止任务成功");
            return result;
        }

        /// <summary>
        /// 卸载任务
        /// </summary>
        /// <param name="taskId"></param>
        public bool Uninstall(int taskId)
        {
            var task = TaskPoolManager.GetInstance().Get(taskId.ToString());
            bool result = true;
            if (task == null)
            {
                result = _TaskService.UpdateTaskState(taskId, DataObject.Constants.TaskState.Stop);
            }
            else
            {
                result = Dispose(taskId, task, true);
                if (result)
                {
                    _TaskService.UpdateTaskState(taskId, DataObject.Constants.TaskState.Stop);
                }
            }
            _LogService.AddTaskLog(taskId, "节点【" + GlobalConfig.NodeId + "】卸载任务成功");
            return result;
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
            if (info != null && info.TaskDLL != null)
            {
                try
                {
                    info.TaskDLL.Dispose();
                    info.TaskDLL = null;
                }
                catch (TaskDisposeTimeOutException e)
                {
                    _LogService.AddNodeError(GlobalConfig.NodeId, "强制释放任务资源", e);
                    if (!isForce) throw e;
                }
                catch (Exception ex)
                {
                    _LogService.AddNodeError(GlobalConfig.NodeId, "强制释放任务资源", ex);
                }
            }
            if (info != null && info.Domain != null)
            {
                try
                {
                    new AppDomainLoader<BaseTask>().UnLoad(info.Domain);
                    info.Domain = null;
                }
                catch (Exception e)
                {
                    _LogService.AddNodeError(GlobalConfig.NodeId, "强制释放应用程序域", e);
                }
            }
            if (TaskPoolManager.GetInstance().Get(taskId.ToString()) != null)
            {
                try
                {
                    TaskPoolManager.GetInstance().Remove(taskId.ToString());
                }
                catch (Exception e)
                {
                    _LogService.AddNodeError(GlobalConfig.NodeId, "强制是否任务池", e);
                }
            }
            _LogService.AddTaskLog(taskId, "节点【" + GlobalConfig.NodeId + "】已对任务【" + taskId + "】进行资源释放");
            return true;
        }
    }
}
