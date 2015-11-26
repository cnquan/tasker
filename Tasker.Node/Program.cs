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
            IUserService _UserService = ServiceLocator.Instance.GetService<IUserService>();
            UserDTO user = _UserService.GetUser("001");
            Console.Write(user.ToString());
            Console.Read();
        }
    }
}
