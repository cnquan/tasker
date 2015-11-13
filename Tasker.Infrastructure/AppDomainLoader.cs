using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Infrastructure
{
    /// <summary>
    /// 程序集加载
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AppDomainLoader<T> where T: class
    {
        /// <summary>
        /// 加载应用程序域，获取相应实例
        /// </summary>
        /// <param name="dllpath"></param>
        /// <param name="classpath"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public T Load(string dllPath, string classPath, out AppDomain domain)
        {
            AppDomainSetup setup = new AppDomainSetup();
            if (System.IO.File.Exists(dllPath + ".config"))
                setup.ConfigurationFile = classPath + ".config";
            setup.ShadowCopyFiles = "true";
            setup.ApplicationBase = System.IO.Path.GetDirectoryName(dllPath);
            domain = AppDomain.CreateDomain(System.IO.Path.GetFileName(dllPath), null, setup);
            AppDomain.MonitoringIsEnabled = true;
            T obj = (T)domain.CreateInstanceFromAndUnwrap(dllPath, classPath);
            return obj;
        }

        /// <summary>
        /// 卸载应用程序域
        /// </summary>
        /// <param name="domain"></param>
        public void UnLoad(AppDomain domain)
        {
            AppDomain.Unload(domain);
            domain = null;
        }
    }
}
