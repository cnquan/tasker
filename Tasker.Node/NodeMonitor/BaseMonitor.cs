using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Tasker.Infrastructure;

namespace Tasker.Node.NodeMonitor
{
    /// <summary>
    /// 节点监控基类
    /// </summary>
    public class BaseMonitor
    {
        /// <summary>
        /// 节点线程
        /// </summary>
        protected Thread _Thread;

        /// <summary>
        /// 节点监控间隔时间
        /// </summary>
        public virtual int Interval { get; set; }

        /// <summary>
        /// 初始化监控
        /// </summary>
        public BaseMonitor()
        {
            _Thread = new Thread(TryRun);
            _Thread.IsBackground = true;
            _Thread.Start();
        }

        /// <summary>
        /// 监控执行入口
        /// </summary>
        private void TryRun()
        {
            while (true)
            {
                try
                {
                    Run();
                    Thread.Sleep(Interval);
                }
                catch (Exception ex)
                {
                    string msg = string.Format("{0}监控严重错误，错误信息:{1}，堆栈:{2}", this.GetType().Name, ex.Message, ex.StackTrace);
                    LogHelper.Write(msg, LogLevel.ERROR);
                }
            }
        }

        /// <summary>
        /// 监控执行方法
        /// </summary>
        public virtual void Run() { }
    }
}
