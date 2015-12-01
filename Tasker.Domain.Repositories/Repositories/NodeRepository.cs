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

        public bool AddNode(Model.Node obj)
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

        public bool UpdateNode(Model.Node obj)
        {
            try
            {
                base.Modify(obj);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
