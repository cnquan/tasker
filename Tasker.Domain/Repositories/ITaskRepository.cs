using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Domain.Repositories
{
    public interface ITaskRepository : IRepository<Model.Task>
    {
        Model.Task GetTask(int Id);
    }
}
