using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderAgentLib;

namespace MakeBat
{
    class Program
    {
        private static string DlFullPath = ConfigurationManager.AppSettings["DownLoadPath"];
        private static string DLType = ConfigurationManager.AppSettings["DLType"];

        static void Main(string[] args)
        {
            LogHelper.writeInfoLog("Main Start");
            DateTime dt = DateTime.Now.AddDays(-1);
            Dictionary<string, string> hm = new Dictionary<string, string>();
            //Hashtable ht = new Hashtable();
            try
            {
                string dlURL = string.Empty;
                string dlName = string.Empty;
                string dlDLSavePath = DlFullPath + "\\" + dt.ToString("yyyy-MM-dd");
                
                Agent taat1 = new Agent();
                switch (DLType)
                {
                    case "go2.0-1":
                    //sw.WriteLine("THUNDERDL http://browserbased.info:8080/golog/egg" + dt.ToString("yyyyMMdd") + ".zip 
                    //egg" + dt.ToString("yyyyMMdd") + @".zip 
                    //E:\MD\MDZipFile\" + dt.ToString("yyyy-MM-dd"));
                        dlURL = "http://browserbased.info:8080/golog/egg" + dt.ToString("yyyyMMdd") + ".zip";
                        dlName = "egg" + dt.ToString("yyyyMMdd") + ".zip";
                        dlDLSavePath = DlFullPath + "\\" + dt.ToString("yyyy-MM-dd") + "-1";
                        break;
                    case "C#2.0-1":
                        //sw.WriteLine("THUNDERDL http://52.19.132.192:8080/log/cegg" + dt.ToString("yyyyMMdd") + ".zip 
                        //cegg" + dt.ToString("yyyyMMdd") + @".zip 
                        //E:\MD\MDZipFile\" + dt.ToString("yyyy-MM-dd"));
                        dlURL = "http://52.19.132.192:8080/log/cegg" + dt.ToString("yyyyMMdd") + ".zip";
                        dlName = "cegg" + dt.ToString("yyyyMMdd") + ".zip";
                        dlDLSavePath = DlFullPath + "\\" + dt.ToString("yyyy-MM-dd") + "-1";
                        break;
                    case "killer2.0-1":
                        //sw.WriteLine("THUNDERDL http://52.18.118.209:8080/log/kegg" + dt.ToString("yyyyMMdd") + ".zip 
                        //kegg" + dt.ToString("yyyyMMdd") + @".zip 
                        //E:\MD\MDZipFile\" + dt.ToString("yyyy-MM-dd"));
                        dlURL = "http://52.18.118.209:8080/log/kegg" + dt.ToString("yyyyMMdd") + ".zip";
                        dlName = "kegg" + dt.ToString("yyyyMMdd") + ".zip ";
                        dlDLSavePath = DlFullPath + "\\" + dt.ToString("yyyy-MM-dd") + "-1";
                        break;
                    case "go2.0":
                        //sw.WriteLine("THUNDERDL http://browserbased.info:8080/golog/egg" + dt.ToString("yyyyMMdd") + ".zip 
                        //egg" + dt.ToString("yyyyMMdd") + @".zip 
                        //E:\MD\MDZipFile\" + dt.ToString("yyyy-MM-dd"));
                        dlURL = "http://browserbased.info:8080/golog/egg" + dt.ToString("yyyyMMdd") + ".zip";
                        dlName = "egg" + dt.ToString("yyyyMMdd") + ".zip";
                        break;
                    case "C#2.0":
                        //sw.WriteLine("THUNDERDL http://52.19.132.192:8080/log/cegg" + dt.ToString("yyyyMMdd") + ".zip 
                        //cegg" + dt.ToString("yyyyMMdd") + @".zip 
                        //E:\MD\MDZipFile\" + dt.ToString("yyyy-MM-dd"));
                        dlURL = "http://52.19.132.192:8080/log/cegg" + dt.ToString("yyyyMMdd") + ".zip";
                        dlName = "cegg" + dt.ToString("yyyyMMdd") + ".zip";
                        break;
                    case "killer2.0":
                        //sw.WriteLine("THUNDERDL http://52.18.118.209:8080/log/kegg" + dt.ToString("yyyyMMdd") + ".zip 
                        //kegg" + dt.ToString("yyyyMMdd") + @".zip 
                        //E:\MD\MDZipFile\" + dt.ToString("yyyy-MM-dd"));
                        dlURL = "http://52.18.118.209:8080/log/kegg" + dt.ToString("yyyyMMdd") + ".zip";
                        dlName = "kegg" + dt.ToString("yyyyMMdd") + ".zip ";
                        break;
                    default:
                        return;
                }
                taat1.AddTask(dlURL, dlName, dlDLSavePath);

                taat1.CommitTasks();                
                
                LogHelper.writeInfoLog("Main End");
            }
            catch (Exception ex)
            {
                LogHelper.writeErrorLog(ex);
            }
        }
    }
}
