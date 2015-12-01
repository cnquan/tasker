using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Tasker.Node;
using Tasker.Node.Tools;
using Tasker.Node.Commands;
using Tasker.Node.NodeMonitor;
using Tasker.Infrastructure;

namespace Tasker.WinService
{
    partial class NodeService : ServiceBase
    {
        public NodeService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                using (SettingHelper se = new SettingHelper())
                {
                    GlobalConfig.NodeId = se.serviceId.To<int>();
                    GlobalConfig.TaskConnectString = se.connect;
                }
                if (GlobalConfig.NodeId <= 0 || GlobalConfig.TaskConnectString.IsNull())
                {
                    LogHelper.Write("节点服务配置错误，请查看 /Config 下配置项！", LogLevel.ERROR);
                }
                IOHelper.DirMake(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\" + GlobalConfig.TaskDLLShareDir + @"\");
                CommandQueue.Start();
                GlobalConfig.Monitors.Add(new NodeHeartBeatMonitor());
                GlobalConfig.Monitors.Add(new TaskRecoverMonitor());
                GlobalConfig.Monitors.Add(new TaskStopedMonitor());
                LogHelper.Write("节点服务启动成功！", LogLevel.INFO);
            }
            catch (Exception ex)
            {
                LogHelper.Write("节点服务启动失败！", LogLevel.ERROR);
            }
        }

        protected override void OnStop()
        {
            LogHelper.Write("节点服务已经停止！", LogLevel.INFO);
        }
    }
}
