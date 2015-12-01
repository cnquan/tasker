using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tasker.ServiceContracts;
using Tasker.DataObject;
using Tasker.Infrastructure.Unity;

namespace Tasker.Node.Commands
{
    public class CommandQueue
    {
        /// <summary>
        /// 节点命令监听线程
        /// </summary>
        private static Thread _Thread;

        /// <summary>
        /// 节点最新命令ID
        /// </summary>
        private static int _LastCommandId = -1;

        /// <summary>
        /// 静态初始化
        /// </summary>
        static CommandQueue()
        {
            _Thread = new Thread(Run);
            _Thread.IsBackground = true;
            _Thread.Start();
        }

        /// <summary>
        /// 开启监听
        /// </summary>
        public static void Start() { }

        /// <summary>
        /// 运行处理
        /// </summary>
        static void Run()
        {
            RecoveryTask();
            OnCommand();
        }

        /// <summary>
        /// 恢复已启动任务
        /// </summary>
        static void RecoveryTask()
        {
            INodeService nodeService = ServiceLocator.Instance.GetService<INodeService>();
            ILogService logService = ServiceLocator.Instance.GetService<ILogService>();
            try
            {
                logService.AddNodeLog(GlobalConfig.NodeId,
                    "节点【" + GlobalConfig.NodeId + "】启动成功，准备开始恢复开启任务");
                List<int> tasks = nodeService.GetNodeTasks(GlobalConfig.NodeId, Constants.TaskState.Running);
                foreach (int t in tasks)
                {
                    try
                    {
                        CommandFactory.Execute(new CommandDTO()
                        {
                            Id = -1,
                            NodeId = GlobalConfig.NodeId,
                            TaskId = t,
                            State = Constants.CommandState.None,
                            CommandName = Constants.CommandName.StartTask.ToString(),
                            CreateTime = DateTime.Now
                        });
                    }
                    catch (Exception ex)
                    {
                        logService.AddTaskError(GlobalConfig.NodeId,
                            "恢复已经开启的任务【" + t + "】失败", ex);
                    }
                }
                logService.AddNodeLog(GlobalConfig.NodeId,
                    string.Format("恢复已经开启的任务完毕，共{0}条任务重启", tasks.Count));
            }
            catch (Exception e)
            {
                logService.AddNodeError(GlobalConfig.NodeId, "恢复开启任务失败", e);
            }
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        static void OnCommand()
        {
            ILogService logService = ServiceLocator.Instance.GetService<ILogService>();
            INodeService nodeService = ServiceLocator.Instance.GetService<INodeService>();
            while (true)
            {
                try
                {
                    if (_LastCommandId < 0)
                        _LastCommandId = nodeService.GetNodeLastCommandId(GlobalConfig.NodeId);
                    List<CommandDTO> commands = nodeService.GetNodeCommands(GlobalConfig.NodeId, _LastCommandId);
                    foreach (CommandDTO item in commands)
                    {
                        try
                        {
                            CommandFactory.Execute(item);
                            item.State = Constants.CommandState.Success;
                            nodeService.UpdateNodeCommand(item);
                            logService.AddNodeLog(GlobalConfig.NodeId,
                                string.Format("当前节点执行命令成功! id:{0},命令:{1}.}", item.Id, item.CommandName));
                        }
                        catch (Exception ex)
                        {
                            item.State = Constants.CommandState.Error;
                            nodeService.UpdateNodeCommand(item);
                            logService.AddNodeError(GlobalConfig.NodeId,
                                string.Format("当前节点执行命令失败！id:{0},命令:{1}", item.Id, item.CommandName),
                                ex);
                        }
                        _LastCommandId = Math.Max(_LastCommandId, item.Id);
                    }
                }
                catch (Exception e)
                {
                    logService.AddNodeError(GlobalConfig.NodeId,
                        "节点【" + GlobalConfig.NodeId + "】命令监听异常", e);
                }
                Thread.Sleep(3000);
            }
        }
    }
}
