using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Tasker.Infrastructure.Unity
{
    /// <summary>
    /// IOC注入器
    /// </summary>
    public sealed class ServiceLocator : IServiceProvider
    {
        /// <summary>
        /// IOC容器
        /// </summary>
        private readonly IUnityContainer _Container;

        /// <summary>
        /// IOC上下文
        /// </summary>
        private static readonly ServiceLocator instance = new ServiceLocator();

        /// <summary>
        /// 构造函数
        /// </summary>
        private ServiceLocator()
        {
            _Container = new UnityContainer();
            ConfigureUnity(_Container, "Config\\unity.config");
        }

        /// <summary>
        /// IOC实例
        /// </summary>
        public static ServiceLocator Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// 映射实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public T GetService<T>()
        {
            return _Container.Resolve<T>();
        }

        /// <summary>
        /// 映射所以实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public IEnumerable<T> ResolveAll<T>()
        {
            return _Container.ResolveAll<T>();
        }

        /// <summary>
        /// 映射实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="overridedArguments">动态插入属性</param>
        /// <returns></returns>
        public T GetService<T>(object overridedArguments)
        {
            var overrides = GetParameterOverrides(overridedArguments);
            return _Container.Resolve<T>(overrides.ToArray());
        }

        /// <summary>
        /// 映射实例
        /// </summary>
        /// <param name="serviceType">实例类型</param>
        /// <param name="overridedArguments">动态插入属性</param>
        /// <returns></returns>
        public object GetService(Type serviceType, object overridedArguments)
        {
            var overrides = GetParameterOverrides(overridedArguments);
            return _Container.Resolve(serviceType, overrides.ToArray());
        }

        /// <summary>
        /// 映射实例
        /// </summary>
        /// <param name="serviceType">实例类型</param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            return _Container.Resolve(serviceType);
        }

        /// <summary>
        /// 动态属性映射
        /// </summary>
        /// <param name="overridedArguments">动态属性</param>
        /// <returns></returns>
        private IEnumerable<ParameterOverride> GetParameterOverrides(object overridedArguments)
        {
            var overrides = new List<ParameterOverride>();
            var argumentsType = overridedArguments.GetType();
            argumentsType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToList()
                .ForEach(property =>
                {
                    var propertyValue = property.GetValue(overridedArguments, null);
                    var propertyName = property.Name;
                    overrides.Add(new ParameterOverride(propertyName, propertyValue));
                });
            return overrides;
        }

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="container">IOC容器</param>
        /// <param name="configFile">配置文件路径</param>
        private void ConfigureUnity(IUnityContainer container, string configFile)
        {
            ExeConfigurationFileMap infraFileMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = configFile
            };
            UnityConfigurationSection infraConfig = (UnityConfigurationSection)ConfigurationManager
                .OpenMappedExeConfiguration(infraFileMap, ConfigurationUserLevel.None)
                .GetSection("unity");
            try
            {
                if (infraConfig.Containers.Default != null)
                {
                    infraConfig.Configure(container);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
