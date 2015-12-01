using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Domain.Repositories.Repositories
{
    public class NodeLogRepository : EntityFramework.EntityFrameworkRepository<Model.NodeLog>,
                                     INodeLogRepository
    {
        public NodeLogRepository(IRepositoryContext context)
            : base(context)
        { }

        public bool AddNodeLog(Model.NodeLog obj)
        {
            try
            {
                base.Add(obj);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
