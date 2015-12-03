using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeBat
{
    class Program
    {
        static void Main(string[] args)
        {
            LogHelper.writeInfoLog("Main Start");
            DateTime dt = DateTime.Now.AddDays(-1);
            Dictionary<string, string> hm = new Dictionary<string, string>();
            //Hashtable ht = new Hashtable();
            try
            {
                string DlFullPath = CommonResource.DownLoadPath.ToString();
                //判断文件路径是否存在，不存在则创建文件夹
                if (!Directory.Exists(DlFullPath))
                {
                    Directory.CreateDirectory(DlFullPath);//不存在就创建目录
                }
                //// 如果文件不存在，创建文件； 如果存在，覆盖文件
                string fullName = string.Empty;
                fullName = DlFullPath + @"\ceggimport.bat";
                //StreamWriter sw1 = new StreamWriter(fullName, false, Encoding.UTF8);
                //// 也可以指定编码方式 
                //// true 是 append text, false 为覆盖原文件 
                ////StreamWriter sw2 = new StreamWriter(@"c:\temp\utf-8.txt", true, Encoding.UTF8);

                //// FileMode.CreateNew: 如果文件不存在，创建文件；如果文件已经存在，抛出异常 
                //FileStream fs = new FileStream(fullName, FileMode.CreateNew, FileAccess.Write, FileShare.Read);

                // 如果文件不存在，创建文件； 如果存在，覆盖文件 
                FileInfo myFile = new FileInfo(fullName);
                StreamWriter sw = myFile.CreateText();

                // 写一个字符            
                sw.WriteLine("e:");
                sw.WriteLine("cd "+ DlFullPath);
                sw.WriteLine("THUNDERDL http://52.19.132.192:8080/log/cegg" + dt.ToString("yyyyMMdd") + ".zip cegg" + dt.ToString("yyyyMMdd") + @".zip E:\MD\MDZipFile\" + dt.ToString("yyyy-MM-dd"));

                sw.Close();
                
                fullName = string.Empty;
                fullName = DlFullPath + @"\keggimport.bat";
                myFile = new FileInfo(fullName);
                sw = myFile.CreateText();

                // 写一个字符            
                sw.WriteLine("e:");
                sw.WriteLine("cd " + DlFullPath);
                sw.WriteLine("THUNDERDL http://52.18.118.209:8080/log/kegg" + dt.ToString("yyyyMMdd") + ".zip kegg" + dt.ToString("yyyyMMdd") + @".zip E:\MD\MDZipFile\" + dt.ToString("yyyy-MM-dd"));

                sw.Close();

                fullName = string.Empty;
                fullName = DlFullPath + @"\eggimport.bat";
                myFile = new FileInfo(fullName);
                sw = myFile.CreateText();

                // 写一个字符            
                sw.WriteLine("e:");
                sw.WriteLine("cd " + DlFullPath);
                sw.WriteLine("THUNDERDL http://browserbased.info:8080/golog/egg" + dt.ToString("yyyyMMdd") + ".zip egg" + dt.ToString("yyyyMMdd") + @".zip egg20151202.zip E:\MD\MDZipFile\" + dt.ToString("yyyy-MM-dd"));

                sw.Close();

                LogHelper.writeInfoLog("Main End");
            }
            catch (Exception ex)
            {
                LogHelper.writeErrorLog(ex);
            }
        }
    }
}
