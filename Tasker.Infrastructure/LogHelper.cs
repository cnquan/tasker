using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Tasker.Infrastructure
{
    public class LogHelper
    {
        private static ILog _Log;

        public static void Write(string msg, LogLevel lv = LogLevel.INFO)
        {
            log4net.GlobalContext.Properties["LogName"] = string.Format("{0}.{1}.log", DateTime.Now.ToString("yyyy-MM-dd"), lv.ToString().ToLower());
            _Log = LogManager.GetLogger(typeof(LogHelper));
            switch (lv)
            {
                case LogLevel.ALL: _Log.Info(msg); break;
                case LogLevel.DEBUG: _Log.Debug(msg); break;
                case LogLevel.ERROR: _Log.Error(msg); break;
                case LogLevel.FATAL: _Log.Fatal(msg); break;
                case LogLevel.INFO: _Log.Info(msg); break;
                case LogLevel.WARN: _Log.Warn(msg); break;
                default:
                    _Log.Info(msg); break;
            }
        }
    }

    /// <summary>
    /// 日志等级
    /// </summary>
    public enum LogLevel
    {
        OFF,
        FATAL,
        ERROR,
        WARN,
        INFO,
        DEBUG,
        ALL,
    }
}
