using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace MDataIm20
{
    public class LogHelper
    {
        public static ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

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
