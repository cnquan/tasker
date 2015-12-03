using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.ServiceContracts;
using Tasker.Infrastructure;

namespace Tasker.Domain.Services
{
    public class NodeServiceImpl : ApplicationService, INodeService
    {
        private Repositories.INodeRepository _NodeRepository;
        private Repositories.ICommandRepository _CommandRepository;
        public NodeServiceImpl(Repositories.IRepositoryContext context,
            Repositories.INodeRepository node,
            Repositories.ICommandRepository command)
            : base(context)
        {
            _NodeRepository = node;
            _CommandRepository = command;
        }

        public int GetNodeHeartBeat(int Id)
        {
            Model.Node node = _NodeRepository.GetNode(Id);
            if (node == null)
                throw new Exception("节点: " + Id + "已被移除");
            if (node.NodeHeartbeat.IsNull() || node.NodeHeartbeat <= 0)
                return 5000;
            else
                return node.NodeHeartbeat.To<int>();
        }

        public bool RefreshNode(DataObject.NodeDTO obj)
        {
            Model.Node node = _NodeRepository.GetNode(obj.Id);
            try
            {
                if (node == null)
                {
                    _NodeRepository.Add(AutoMapper.Mapper.Map<DataObject.NodeDTO, Model.Node>(obj));
                    _NodeRepository.Context.Commit();
                }
                else
                {
                    node.NodeHost = obj.NodeHost;
                    node.UpdateTime = obj.UpdateTime;
                    _NodeRepository.Modify(node);
                    _NodeRepository.Context.Commit();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<int> GetNodeTasks(int nodeId, DataObject.Constants.TaskState taskState)
        {
            List<Model.Command> commands = _CommandRepository.GetNodeCommands(nodeId, (int)taskState);
            if (commands.Count <= 0)
                return new List<int>();
            else
                return commands.Select(x => x.Id).ToList();
        }

        public List<DataObject.CommandDTO> GetNodeCommands(int nodeId, int lastCommandId = -1)
        {
            List<Model.Command> commands = _CommandRepository.GetNodeLastCommands(nodeId, lastCommandId);
            if (commands.Count <= 0)
                return new List<DataObject.CommandDTO>();
            else
                return AutoMapper.Mapper.Map<List<Model.Command>, List<DataObject.CommandDTO>>(commands);
        }

        public int GetNodeLastCommandId(int nodeId)
        {
            Model.Command command = _CommandRepository.GetNodeLastCommand(nodeId);
            if (command == null)
                return -1;
            else
                return command.Id;
        }

        public bool UpdateNodeCommand(DataObject.CommandDTO obj)
        {
            try
            {
                _CommandRepository.Modify(AutoMapper.Mapper.Map<DataObject.CommandDTO, Model.Command>(obj));
                _CommandRepository.Context.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
