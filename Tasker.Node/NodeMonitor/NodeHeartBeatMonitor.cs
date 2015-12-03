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
        public override int Interval
        {
            get
            {
                return new Tools.ServiceSupport().NodeService.GetNodeHeartBeat(GlobalConfig.NodeId);
            }
        }

        public override void Run()
        {
            Tools.ServiceSupport service = new Tools.ServiceSupport();
            DateTime sysTime = service.SystemService.GetSysTime();
            service.NodeService.RefreshNode(new NodeDTO()
            {
                Id = GlobalConfig.NodeId,
                NodeHost = System.Net.Dns.GetHostName(),
                NodeName = "新增节点_" + GlobalConfig.NodeId,
                CreateTime = sysTime,
                UpdateTime = sysTime,
                NodeHeartbeat = 10000
            });
        }
    }
}
