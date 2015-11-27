using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.TaskManager.TaskRuntime
{
    /// <summary>
    /// 任务锁
    /// </summary>
    public class TaskLock
    {
        /// <summary>
        /// 任务运行标识
        /// </summary>
        private bool _Running = false;

        /// <summary>
        /// 第一道锁
        /// </summary>
        private object _FirstLock = new object();

        /// <summary>
        /// 第二道锁
        /// </summary>
        private object _SecondLock = new object();

        /// <summary>
        /// 内部加锁
        /// </summary>
        /// <returns></returns>
        private bool TryFirstLock()
        {
            if (_Running)
                return false;
            lock (_FirstLock)
            {
                if (_Running)
                    return false;
                else
                {
                    _Running = true;
                    return true;
                }
            }
        }

        /// <summary>
        /// 锁释放
        /// </summary>
        private void TryEndLock()
        {
            _Running = false;
        }

        /// <summary>
        /// 任务执行加锁
        /// </summary>
        /// <param name="action"></param>
        public void Invoke(Action action)
        {
            if (this.TryFirstLock())
            {
                try
                {
                    lock (_SecondLock)
                    {
                        action();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    this.TryEndLock();
                }
            }
        }
    }
}
