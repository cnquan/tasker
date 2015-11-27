using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Tasker.TaskManager.TaskRuntime
{
    /// <summary>
    /// 任务卸载
    /// </summary>
    public class TaskDisposeOperator
    {
        /// <summary>
        /// 等待时长
        /// </summary>
        private int MaxWaitTime = 5000;

        /// <summary>
        /// 刷新间隔
        /// </summary>
        private const int CheckTime = 1000;

        /// <summary>
        /// 资源释放状态
        /// </summary>
        public TaskDisposeState State = TaskDisposeState.None;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="maxWaitTime"></param>
        public TaskDisposeOperator(int maxWaitTime = 5000)
        {
            MaxWaitTime = maxWaitTime;
            State = TaskDisposeState.None;
        }

        /// <summary>
        /// 阻塞等待资源释放
        /// </summary>
        public void WaitDisposeFinished()
        {
            int count = 0;
            while (count * CheckTime < MaxWaitTime)
            {
                if (State == TaskDisposeState.Finished)
                    return;
                System.Threading.Thread.Sleep(CheckTime);
                count++;
            }
            throw new TaskDisposeTimeOutException();
        }
    }

    /// <summary>
    /// 资源释放超时异常
    /// </summary>
    public class TaskDisposeTimeOutException : Exception
    {
        public TaskDisposeTimeOutException()
            : base("任务终止时,资源未释放超时。请检查代码是否在检测到任务处于DisposedState=Disposing时,释放任务当前资源,并终止任务继续运行业务代码,并将DisposedState=Finished")
        { }

        protected TaskDisposeTimeOutException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }

    /// <summary>
    /// 资源回收状态
    /// </summary>
    public enum TaskDisposeState
    {
        /// <summary>
        /// 未开始
        /// </summary>
        None,

        /// <summary>
        /// 正在释放
        /// </summary>
        Disposing,

        /// <summary>
        /// 释放完毕
        /// </summary>
        Finished,
    }
}
