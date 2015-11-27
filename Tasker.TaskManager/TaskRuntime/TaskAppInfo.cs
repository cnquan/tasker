using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.TaskManager.TaskRuntime
{
    /// <summary>
    /// 任务配置信息
    /// </summary>
    [Serializable]
    public class TaskAppInfo : Dictionary<string, object>
    {
        public TaskAppInfo()
            : base()
        { }

        protected TaskAppInfo(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
