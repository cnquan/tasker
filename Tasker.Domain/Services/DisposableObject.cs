using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.ConstrainedExecution;

namespace Tasker.Domain.Repositories
{
    /// <summary>
    /// 资源回收类
    /// </summary>
    public abstract class DisposableObject : CriticalFinalizerObject, IDisposable
    {
        /// <summary>
        /// 析构回收
        /// </summary>
        ~DisposableObject()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// 回收状态
        /// </summary>
        /// <param name="disposing"></param>
        protected abstract void Dispose(bool disposing);

        /// <summary>
        /// 资源回收
        /// </summary>
        protected void ExplicitDispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 资源回收
        /// </summary>
        public void Dispose()
        {
            this.ExplicitDispose();
        }
    }
}
