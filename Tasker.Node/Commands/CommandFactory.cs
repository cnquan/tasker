using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Tasker.DataObject;

namespace Tasker.Node.Commands
{
    public class CommandFactory
    {
        public static void Execute(CommandDTO commandInfo)
        {
            string namespc = typeof(BaseCommand).Namespace;
            var commandInstance = Assembly.GetAssembly(typeof(BaseCommand)).CreateInstance(namespc + "." + commandInfo.CommandName + "Command", true);
            if (commandInstance != null && commandInstance is BaseCommand)
            {
                var command = commandInstance as BaseCommand;
                command.CommandInfo = commandInfo;
                command.Execute();
            }
        }
    }
}
