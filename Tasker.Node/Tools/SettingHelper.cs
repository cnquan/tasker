using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace Tasker.Node.Tools
{
    public class SettingHelper : IDisposable
    {
        /// <summary>
        /// 服务编号
        /// </summary>
        public string serviceId { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string serviceName { get; set; }

        /// <summary>
        /// 服务简称
        /// </summary>
        public string displayName { get; set; }

        /// <summary>
        /// 服务说明
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 数据连接
        /// </summary>
        public string connect { get; set; }

        /// <summary>
        /// 释放标志
        /// </summary>
        private bool _Disposed = false;

        /// <summary>
        /// 构造函数
        /// </summary>
        public SettingHelper()
        {
            InitSettings();
        }

        /// <summary> 
        /// 初始化服务配置信息 
        /// </summary> 
        private void InitSettings()
        {
            string root = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string xmlfile = root.Remove(root.LastIndexOf('\\') + 1) + "Config\\node.config";
            if (File.Exists(xmlfile))
            {
                serviceId = GetConfigValue(xmlfile, "NodeId");
                serviceName = "Tasker.NodeService_" + serviceId;
                displayName = GetConfigValue(xmlfile, "NodeName");
                description = GetConfigValue(xmlfile, "NodeDescription");
            }
            else
            {
                throw new FileNotFoundException("未能找到配置文件 node.config！ 路径:" + xmlfile);
            }
            string conFile = root.Remove(root.LastIndexOf('\\') + 1) + "Config\\connect.config";
            if (File.Exists(conFile))
            {
                connect = GetConfigValue(conFile, "ConnectionString");
            }
            else
            {
                throw new FileNotFoundException("未能找到配置文件 connect.config! 路径：" + conFile);
            }
        }

        /// <summary>
        /// 读取 XML中指定节点值
        /// </summary>
        /// <param name="configpath">配置文件路径</param>
        /// <param name="strKeyName">键值</param>        
        /// <returns></returns>
        protected static string GetConfigValue(string configpath, string strKeyName)
        {
            using (XmlTextReader tr = new XmlTextReader(configpath))
            {
                while (tr.Read())
                {
                    if (tr.NodeType == XmlNodeType.Element)
                    {
                        if (tr.Name == "add" && tr.GetAttribute("key") == strKeyName)
                        {
                            return tr.GetAttribute("value");
                        }
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// 资源回收
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 自动回收
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._Disposed)
            {
                if (disposing)
                {
                    serviceName = null;
                    displayName = null;
                    description = null;
                }
            }
            _Disposed = true;
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~SettingHelper()
        {
            Dispose(false);
        }
    }
}
