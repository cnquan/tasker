using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Xml;
using System.IO;

namespace Tasker.Domain.Repositories
{
    public class TaskerDbConnection : IDisposable
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 释放标志
        /// </summary>
        private bool _Disposed = false;

        /// <summary>
        /// 初始化
        /// </summary>
        public TaskerDbConnection()
        {
            InitConnection();
        }

        /// <summary>
        /// 初始化连接
        /// </summary>
        private void InitConnection()
        {
            string root = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string xmlfile = root.Remove(root.LastIndexOf('\\') + 1) + "Config\\efconnect.config";
            if (File.Exists(xmlfile))
            {
                ConnectionString = GetConfigValue(xmlfile, "ConnectionString");
            }
            else
            {
                throw new FileNotFoundException("未能找到服务名称配置文件 efconnect.config！路径:" + xmlfile);
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
                    ConnectionString = null;
                }
            }
            _Disposed = true;
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~TaskerDbConnection()
        {
            Dispose(false);
        }
    }
}
