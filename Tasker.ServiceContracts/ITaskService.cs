using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.DataObject;

namespace Tasker.ServiceContracts
{
    public interface ITaskService
    {
        /// <summary>
        /// 获取任务信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        TaskDTO GetTask(int Id);

        /// <summary>
        /// 更新任务状态
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        bool UpdateTaskState(int Id, Constants.TaskState state);
    }
}
