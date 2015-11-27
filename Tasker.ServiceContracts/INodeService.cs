using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.DataObject;

namespace Tasker.ServiceContracts
{
    public interface INodeService
    {
        /// <summary>
        /// 获取节点心跳间隔
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        int GetNodeHeartBeat(int Id);

        /// <summary>
        /// 更新节点状态
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool RefreshNode(NodeDTO obj);

        /// <summary>
        /// 根据任务状态，获取节点任务
        /// </summary>
        /// <param name="nodeId">节点ID</param>
        /// <param name="state">任务状态</param>
        /// <returns></returns>
        List<int> GetNodeTasks(int nodeId, Constants.TaskState taskState = null);
    }
}
