using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.ServiceContracts;
using Tasker.Infrastructure.Unity;

namespace Tasker.Node.Tools
{
    public class ServiceSupport
    {
        public INodeService NodeService { get; set; }

        public ILogService LogService { get; set; }

        public ISystemService SystemService { get; set; }

        public ITaskService TaskService { get; set; }

        public ServiceSupport()
        {
            NodeService = ServiceLocator.Instance.GetService<INodeService>();
            LogService = ServiceLocator.Instance.GetService<ILogService>();
            SystemService = ServiceLocator.Instance.GetService<ISystemService>();
            TaskService = ServiceLocator.Instance.GetService<ITaskService>();
        }
    }
}
