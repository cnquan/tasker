using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Domain.Repositories
{
    public interface ITaskRepository
    {
        Model.Task GetTask(int Id);

        bool UpdateTask(Model.Task obj);
    }
}
