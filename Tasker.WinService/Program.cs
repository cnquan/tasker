using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;

namespace Tasker.WinService
{
    public class Program
    {
        static void Main(string[] args)
        {
            ServiceBase[] sc = new ServiceBase[]
            {
                new NodeService()
            };
            ServiceBase.Run(sc);
        }
    }
}
