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

                //Agent taat2 = new Agent();
                //taat2.AddTask("http://54.154.68.193:8080/mdata.csharp.06.zip", "mdata.csharp.06.zip", @"E:\dltest");
                //taat2.CommitTasks();

                //string DlFullPath = CommonResource.DownLoadPath.ToString();
                ////判断文件路径是否存在，不存在则创建文件夹
                //if (!Directory.Exists(DlFullPath))
                //{
                //    Directory.CreateDirectory(DlFullPath);//不存在就创建目录
                //}
                ////// 如果文件不存在，创建文件； 如果存在，覆盖文件
                //string fullName = string.Empty;
                //fullName = DlFullPath + @"\ceggimport.bat";

                //System.Diagnostics.Process p1 = new System.Diagnostics.Process();
                //p1.StartInfo.FileName = @"THUNDERDL";//需要启动的程序名 
                //p1.StartInfo.Arguments = @"http://52.30.13.201:8080/mdata.killer.06.zip mdata.killer.06.zip E:\dltest";//启动参数 
                //p1.Start();//启动 
                ////if (p1.HasExited)//判断是否运行结束 
                ////{
                ////    p1.Kill();
                ////}
                ////p1.

                //p1.StartInfo.Arguments = @"http://52.30.13.201:8080/mdata.killer.06.zip mdata.killer.06.zip E:\dltest";//启动参数 
                //p1.Start();//启动 

                //System.Diagnostics.Process p2 = new System.Diagnostics.Process();
                //p2.StartInfo.FileName = @"THUNDERDL";//需要启动的程序名 
                //p2.StartInfo.Arguments = @"http://54.154.68.193:8080/mdata.csharp.06.zip mdata.csharp.06.zip E:\dltest";//启动参数 
                //p2.Start();//启动 
                //if (p2.HasExited)//判断是否运行结束 
                //{
                //    p2.Kill();
                //}
                //StreamWriter sw1 = new StreamWriter(fullName, false, Encoding.UTF8);
                //// 也可以指定编码方式 
                //// true 是 append text, false 为覆盖原文件 
                ////StreamWriter sw2 = new StreamWriter(@"c:\temp\utf-8.txt", true, Encoding.UTF8);

                //// FileMode.CreateNew: 如果文件不存在，创建文件；如果文件已经存在，抛出异常 
                //FileStream fs = new FileStream(fullName, FileMode.CreateNew, FileAccess.Write, FileShare.Read);

                //// 如果文件不存在，创建文件； 如果存在，覆盖文件 
                //FileInfo myFile = new FileInfo(fullName);
                //StreamWriter sw = myFile.CreateText();

                //// 写一个字符            
                //sw.WriteLine("e:");
                //sw.WriteLine("cd "+ DlFullPath);
                //sw.WriteLine("THUNDERDL http://52.19.132.192:8080/log/cegg" + dt.ToString("yyyyMMdd") + ".zip cegg" + dt.ToString("yyyyMMdd") + @".zip E:\MD\MDZipFile\" + dt.ToString("yyyy-MM-dd"));

                //sw.Close();

                //fullName = string.Empty;
                //fullName = DlFullPath + @"\keggimport.bat";
                //myFile = new FileInfo(fullName);
                //sw = myFile.CreateText();

                //// 写一个字符            
                //sw.WriteLine("e:");
                //sw.WriteLine("cd " + DlFullPath);
                //sw.WriteLine("THUNDERDL http://52.18.118.209:8080/log/kegg" + dt.ToString("yyyyMMdd") + ".zip kegg" + dt.ToString("yyyyMMdd") + @".zip E:\MD\MDZipFile\" + dt.ToString("yyyy-MM-dd"));

                //sw.Close();

                //fullName = string.Empty;
                //fullName = DlFullPath + @"\eggimport.bat";
                //myFile = new FileInfo(fullName);
                //sw = myFile.CreateText();

                //// 写一个字符            
                //sw.WriteLine("e:");
                //sw.WriteLine("cd " + DlFullPath);
                //sw.WriteLine("THUNDERDL http://browserbased.info:8080/golog/egg" + dt.ToString("yyyyMMdd") + ".zip egg" + dt.ToString("yyyyMMdd") + @".zip E:\MD\MDZipFile\" + dt.ToString("yyyy-MM-dd"));

                //sw.Close();

                LogHelper.writeInfoLog("Main End");
            }
            catch (Exception ex)
            {
                LogHelper.writeErrorLog(ex);
            }
        }
    }
}
