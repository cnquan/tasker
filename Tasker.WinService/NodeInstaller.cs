using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace Tasker.WinService
{
    [RunInstaller(true)]
    public partial class NodeInstaller : System.Configuration.Install.Installer
    {
        public NodeInstaller()
        {
            InitializeComponent();

            //设置服务名
            using (SettingHelper setting = new SettingHelper())
            {
                serviceInstaller1.ServiceName = setting.serviceName;
                serviceInstaller1.DisplayName = setting.displayName;
                serviceInstaller1.Description = setting.description;
            } 
        }
    }
}
