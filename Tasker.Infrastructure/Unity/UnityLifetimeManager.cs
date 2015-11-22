using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Tasker.Infrastructure.Cache;

namespace Tasker.Infrastructure.Unity
{
    /// <summary>
    /// IOC容器生命周期
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UnityLifetimeManager : LifetimeManager
    {
        /// <summary>
        /// 实例标识
        /// </summary>
        private Guid key = Guid.NewGuid();

        /// <summary>
        /// 构造函数
        /// </summary>
        public UnityLifetimeManager()
            : base()
        {
        }

        /// <summary>
        /// 获取当前IOC容器
        /// </summary>
        /// <returns></returns>
        public override object GetValue()
        {
            return RAMCacheProvider.Get("unity", key.ToString());
        }

        /// <summary>
        /// 设置当前IOC容器
        /// </summary>
        /// <param name="newValue"></param>
        public override void SetValue(object newValue)
        {
            RAMCacheProvider.Put("unity", key.ToString(), newValue);
        }

        /// <summary>
        /// 移除当前IOC容器
        /// </summary>
        public override void RemoveValue()
        {
            RAMCacheProvider.Remove("unity", key.ToString());
        }
    }
}
