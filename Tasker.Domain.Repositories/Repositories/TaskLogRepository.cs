using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Domain.Repositories.Repositories
{
    public class TaskLogRepository : EntityFramework.EntityFrameworkRepository<Model.TaskLog>,
                                     ITaskLogRepository
    {
        public TaskLogRepository(IRepositoryContext context)
            : base(context)
        { }
    }
}
