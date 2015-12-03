using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Tasker.Domain.Repositories.Repositories
{
    public class NodeLogRepository : EntityFramework.EntityFrameworkRepository<Model.NodeLog>,
                                     INodeLogRepository
    {
        public NodeLogRepository(IRepositoryContext context)
            : base(context)
        { }
    }
}
