using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.ServiceContracts;

namespace Tasker.Domain.Services
{
    public class LogServiceImpl : ApplicationService, ILogService
    {
        private Repositories.INodeLogRepository _NodeLogRepository;
        private Repositories.ITaskLogRepository _TaskLogRepository;
        private Repositories.INodeRepository _NodeRepository;
        private Repositories.ITaskRepository _TaskRepository;
        public LogServiceImpl(Repositories.IRepositoryContext context,
            Repositories.INodeLogRepository nodeLog,
            Repositories.ITaskLogRepository taskLog,
            Repositories.INodeRepository node,
            Repositories.ITaskRepository task)
            : base(context)
        {
            _NodeLogRepository = nodeLog;
            _TaskLogRepository = taskLog;
            _NodeRepository = node;
            _TaskRepository = task;
        }

        public bool AddNodeLog(int nodeId, string msg)
        {
            Model.NodeLog log = new Model.NodeLog()
            {
                LogType = Storage.Constants.LogType.SystemLog,
                Node = _NodeRepository.GetNode(nodeId),
                CreateTime = DateTime.Now,
                Content = msg
            };
            return _NodeLogRepository.AddNodeLog(log);
        }

        public bool AddNodeError(int nodeId, string msg, Exception ex)
        {
            Model.NodeLog log = new Model.NodeLog()
            {
                LogType = Storage.Constants.LogType.SystemError,
                Node = _NodeRepository.GetNode(nodeId),
                CreateTime = DateTime.Now,
                Content = string.Format(@"{0},错误信息:{1},堆栈信息:{2}", msg, ex.Message, ex.StackTrace)
            };
            return _NodeLogRepository.AddNodeLog(log);
        }

        public bool AddTaskLog(int taskId, string msg)
        {
            Model.Task task = _TaskRepository.GetTask(taskId);
            Model.TaskLog log = new Model.TaskLog()
            {
                LogType = Storage.Constants.LogType.SystemLog,
                Node = task.Node,
                Task = task,
                CreateTime = DateTime.Now,
                Content = msg
            };
            return _TaskLogRepository.AddTaskLog(log);
        }

        public bool AddTaskError(int taskId, string msg, Exception ex)
        {
            Model.Task task = _TaskRepository.GetTask(taskId);
            Model.TaskLog log = new Model.TaskLog()
            {
                LogType = Storage.Constants.LogType.SystemError,
                Node = task.Node,
                Task = task,
                CreateTime = DateTime.Now,
                Content = string.Format(@"{0},错误信息:{1},堆栈信息:{2}", msg, ex.Message, ex.StackTrace)
            };
            return _TaskLogRepository.AddTaskLog(log);
        }
    }
}
