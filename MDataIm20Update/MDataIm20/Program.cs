using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;
using System.Threading.Tasks;

namespace MDataIm20Update
{
    class Program
    {
        static void Main(string[] args)
        {
            LogHelper.writeInfoLog("Main Start");
            if (args == null)
            {
                Console.WriteLine("请输入数据类型名和数据文件");
                LogHelper.writeWarnLog("请输入数据类型名和数据文件");
                return;
            }

            if (args.Length < 2)
            {
                Console.WriteLine("请输入数据类型名和数据文件");
                LogHelper.writeWarnLog("请输入数据类型名和数据文件");
                return;
            }
            if (string.IsNullOrEmpty(args[0]))
            {
                Console.WriteLine("数据类型名不能为空");
                LogHelper.writeWarnLog("数据类型名不能为空");
                return;
            }

            if (string.IsNullOrEmpty(args[1]))
            {
                Console.WriteLine("数据文件不能为空");
                LogHelper.writeWarnLog("数据文件不能为空");
                return;
            }

            Int64 itemCount = 0;
            string[] s;
            //List<MData> list = new List<MData>();
            MData md = null;
            TaskData td = null;
            DBConnect db = new DBConnect();
            DateTime dt = DateTime.Now;

            string strJson = string.Empty;
            string strDBType = args[0];
            //string strDBType = "task";
            string strFileName = args[1];
            //string strFileName = @"E:\导入数据\temp.eggdata.log.2015-10-29.001";
            string strTableName = string.Empty;
            string strDUTableName = string.Empty;
            string strUITableName = string.Empty;
            try
            {
                if ("go".Equals(strDBType))
                {
                    strTableName = "Go20SourceData";
                    strDUTableName = "Go20DailyUser";
                    strUITableName = "Go20UserInfo";
                }
                //else if ("C#".Equals(strDBType))
                //{
                //    strTableName = "CSharp20SourceData";
                //}
                //else if ("killer".Equals(strDBType))
                //{
                //    strTableName = "Killer20SourceData";
                //}
                else if ("task".Equals(strDBType))
                {
                    strTableName = "Go20TaskSD";
                    strDUTableName = string.Empty;
                    strUITableName = string.Empty;
                }
                
                if ("go".Equals(strDBType))
                {
                    string strInputDateTemp = strFileName.Substring(strFileName.Length - 10);
                    string strInputDate = strInputDateTemp.Substring(strInputDateTemp.Length - 4) +
                        "-" + strInputDateTemp.Substring(strInputDateTemp.Length - 7, 2) +
                        "-" + strInputDateTemp.Substring(strInputDateTemp.Length - 10, 2);

                    // 向表(DailyUser)中插入数据
                    Console.WriteLine("DailyUser Insert Start.");
                    int intInsertDU = db.InsertDailyUser(strInputDate, strTableName, strDUTableName);
                    Console.WriteLine("DailyUser Insert Count = " + intInsertDU);
                    Console.WriteLine("DailyUser Insert End.");

                    if (intInsertDU > 0)
                    {
                        // 更新表(DailyUser)中数据
                        Console.WriteLine("DailyUser Update Start.");
                        int intUpdateDU = db.UpdateDailyUser(strInputDate, strTableName, strDUTableName);
                        Console.WriteLine("DailyUser Update Count = " + intInsertDU);
                        Console.WriteLine("DailyUser Update End.");

                        Console.WriteLine("UserInfo Insert Start.");
                        // 向表(UserInfo)中插入数据
                        int intInsertUI = db.InsertUserInfo(strInputDate, strDUTableName, strUITableName);
                        Console.WriteLine("UserInfo Insert Count = " + intInsertUI);
                        Console.WriteLine("UserInfo Insert End.");
                    }
                }

            }
            catch (Exception ex)
            {
                LogHelper.writeErrorLog(ex);
            }

            //db.InsertList(list);

            LogHelper.writeInfoLog("Main End");
        }
    }
}
