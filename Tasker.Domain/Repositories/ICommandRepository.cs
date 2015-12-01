using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Domain.Repositories
{
    public interface ICommandRepository
    {
        List<Model.Command> GetNodeCommands(int nodeId);

        List<Model.Command> GetNodeCommands(int nodeId, int state);

        List<Model.Command> GetNodeLastCommands(int nodeId, int lastCommandId);

        Model.Command GetNodeLastCommand(int nodeId);

        bool AddCommand(Model.Command obj);

        bool UpdateCommand(Model.Command obj);
    }
}
