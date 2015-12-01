﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Node.NodeRuntime;

namespace Tasker.Node.Commands
{
    public class StartTaskCommand : BaseCommand
    {
        public override void Execute()
        {
            TaskProvider p = new TaskProvider();
            p.Start(this.CommandInfo.TaskId);
        }
    }
}
