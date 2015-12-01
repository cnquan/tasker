using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.ServiceContracts;

namespace Tasker.Domain.Services
{
    public class TaskServiceImpl : ApplicationService, ITaskService
    {
        private Repositories.ITaskRepository _TaskRepository;
        public TaskServiceImpl(Repositories.IRepositoryContext context,
            Repositories.ITaskRepository tp)
            : base(context)
        {
            _TaskRepository = tp;
        }

        public DataObject.TaskDTO GetTask(int Id)
        {
            Model.Task t = _TaskRepository.GetTask(Id);
            return AutoMapper.Mapper.Map<Model.Task, DataObject.TaskDTO>(t);
        }

        public bool UpdateTaskState(int Id, DataObject.Constants.TaskState state)
        {
            Model.Task t = _TaskRepository.GetTask(Id);
            if (t == null)
                throw new Exception("任务:" + Id + ",已被删除");
            t.TaskState = (Storage.Constants.TaskState)state;
            return _TaskRepository.UpdateTask(t);
        }
    }
}
