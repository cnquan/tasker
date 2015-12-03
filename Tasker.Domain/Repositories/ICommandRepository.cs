using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Domain.Repositories
{
    public interface ICommandRepository : IRepository<Model.Command>
    {
        List<Model.Command> GetNodeCommands(int nodeId);

        List<Model.Command> GetNodeCommands(int nodeId, int state);

        List<Model.Command> GetNodeLastCommands(int nodeId, int lastCommandId);

        Model.Command GetNodeLastCommand(int nodeId);
    }
}
