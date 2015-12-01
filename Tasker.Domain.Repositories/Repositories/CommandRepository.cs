using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain.Repositories;

namespace Tasker.Domain.Repositories.Repositories
{
    public class CommandRepository : EntityFramework.EntityFrameworkRepository<Model.Command>,
                                     ICommandRepository
    {
        public CommandRepository(IRepositoryContext context)
            : base(context)
        { }

        public List<Model.Command> GetNodeCommands(int nodeId)
        {
            return base.FindAll(x => x.Node.Id == nodeId, null, Storage.SortOrder.Unspecified, x => x.Task).ToList();
        }

        public List<Model.Command> GetNodeCommands(int nodeId, int state)
        {
            Storage.Constants.CommandState s = (Storage.Constants.CommandState)state;
            return base.FindAll(x => x.Node.Id == nodeId && x.State == s,
                                null,
                                Storage.SortOrder.Unspecified,
                                x => x.Task).ToList();
        }

        public List<Model.Command> GetNodeLastCommands(int nodeId, int lastCommandId)
        {
            return base.FindAll(x => x.Node.Id == nodeId && x.Id > lastCommandId,
                                null,
                                Storage.SortOrder.Unspecified,
                                x => x.Task).ToList();
        }

        public Model.Command GetNodeLastCommand(int nodeId)
        {
            return base.FindAll(null, null, Storage.SortOrder.Descending, x => x.Task).FirstOrDefault();
        }

        public bool AddCommand(Model.Command obj)
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

        public bool UpdateCommand(Model.Command obj)
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
