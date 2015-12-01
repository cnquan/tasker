using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.ServiceContracts;
using Tasker.DataObject;
using Tasker.Infrastructure.Unity;

namespace Tasker.Node.NodeMonitor
{
    /// <summary>
    /// 节点心跳监控
    /// </summary>
    public class NodeHeartBeatMonitor : BaseMonitor
    {
        INodeService _NodeService;
        ISystemService _SysService;

        public NodeHeartBeatMonitor()
        {
            _SysService = ServiceLocator.Instance.GetService<ISystemService>();
            _NodeService = ServiceLocator.Instance.GetService<INodeService>();
        }

        public override int Interval
        {
            get
            {
                return _NodeService.GetNodeHeartBeat(GlobalConfig.NodeId);
            }
        }

        public override void Run()
        {
            _NodeService.RefreshNode(new NodeDTO()
            {
                Id = GlobalConfig.NodeId,
                NodeHost = System.Net.Dns.GetHostName(),
                NodeName = "新增节点_" + GlobalConfig.NodeId,
                UpdateTime = _SysService.GetSysTime(),
                NodeHeartbeat = 10000
            });
        }
    }
}
