using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Domain.Repositories
{
    public interface INodeRepository
    {
        Model.Node GetNode(int Id);

        bool AddNode(Model.Node obj);

        bool UpdateNode(Model.Node obj);
    }
}
