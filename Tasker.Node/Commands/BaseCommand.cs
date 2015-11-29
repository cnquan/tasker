using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.DataObject;

namespace Tasker.Node.Commands
{
    public class BaseCommand
    {
        /// <summary>
        /// 命令信息
        /// </summary>
        public CommandDTO CommandInfo { get; set; }

        /// <summary>
        /// 命令执行方法约定
        /// </summary>
        public virtual void Execute() { }
    }
}
