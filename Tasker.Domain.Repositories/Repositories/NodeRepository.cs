using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain.Repositories;

namespace Tasker.Domain.Repositories.Repositories
{
    public class NodeRepository : EntityFramework.EntityFrameworkRepository<Model.Node>,
                                  INodeRepository
    {

        public NodeRepository(IRepositoryContext context)
            : base(context)
        { }

        public Model.Node GetNode(int Id)
        {
            return base.Find(x => x.Id == Id);
        }
    }
}
