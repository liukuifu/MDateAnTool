using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Reflection;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "web.config", Watch = true)]
namespace MdataAn
{
    /// <summary>
    /// Summary description for LogHelper
    /// </summary>
    public class LogHelper
    {
        //public static ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("UserInfoEdit");
        //记录错误日志
        public static void writeErrorLog(Exception ex)
        {
            log.Error("error", ex);
        }
        public static void writeErrorLog(String strLog)
        {
            log.Error("error : " + strLog);
        }

        //记录严重错误
        public static void writeFatalLog(Exception ex)
        {
            log.Fatal("error", ex);
        }
        //记录一般信息
        public static void writeInfoLog(String strLog)
        {
            log.Info(strLog);
        }
        //记录调试信息
        public static void writeDebugLog(String strLog)
        {
            log.Debug(strLog);
        }
        //记录警告信息
        public static void writeWarnLog(String strLog)
        {
            log.Warn(strLog);
        }
    }
}