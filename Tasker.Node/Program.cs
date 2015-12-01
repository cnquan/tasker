using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.ServiceContracts;
using Tasker.DataObject;
using Tasker.Infrastructure;
using Tasker.Infrastructure.Unity;

namespace Tasker.Node
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (Tools.SettingHelper se = new Tools.SettingHelper())
                {
                    GlobalConfig.NodeId = se.serviceId.To<int>();
                    GlobalConfig.TaskConnectString = se.connect;
                }
                if (GlobalConfig.NodeId <= 0 || GlobalConfig.TaskConnectString.IsNull())
                {
                    LogHelper.Write("节点配置信息错误，请检查 \\Config 下的配置", LogLevel.ERROR);
                }
                Commands.CommandQueue.Start();
                GlobalConfig.Monitors.Add(new NodeMonitor.NodeHeartBeatMonitor());
                GlobalConfig.Monitors.Add(new NodeMonitor.TaskRecoverMonitor());
                GlobalConfig.Monitors.Add(new NodeMonitor.TaskStopedMonitor());
                LogHelper.Write("节点启动成功！");
                Console.Read();
            }
            catch (Exception e)
            {
                LogHelper.Write("节点启动失败，错误信息：" + e.Message, LogLevel.ERROR);
            }
        }
    }
}
