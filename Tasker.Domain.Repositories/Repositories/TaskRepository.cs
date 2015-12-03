using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain.Repositories;

namespace Tasker.Domain.Repositories.Repositories
{
    public class TaskRepository : EntityFramework.EntityFrameworkRepository<Model.Task>,
                                  ITaskRepository
    {
        public TaskRepository(IRepositoryContext context)
            : base(context)
        { }

        public Model.Task GetTask(int Id)
        {
            return base.Find(x => x.Id == Id,
                             x => x.Node,
                             x => x.TaskVersion,
                             x => x.Category);
        }
    }
}
