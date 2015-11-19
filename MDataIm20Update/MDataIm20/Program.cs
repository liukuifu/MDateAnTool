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
                Console.WriteLine("请输入数据类型名和日期");
                LogHelper.writeWarnLog("请输入数据类型名和日期");
                return;
            }

            if (args.Length < 2)
            {
                Console.WriteLine("请输入数据类型名和日期");
                LogHelper.writeWarnLog("请输入数据类型名和日期");
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
                Console.WriteLine("日期不能为空");
                LogHelper.writeWarnLog("日期不能为空");
                return;
            }

            Int64 itemCount = 0;
            int intSourceDataCount = 0;
            int intInsertDU = 0;
            int intInsertUI = 0;
            int intCount = 0;
            string[] s;
            //List<MData> list = new List<MData>();
            MData md = null;
            TaskData td = null;
            DBConnect db = new DBConnect();
            DateTime dt = DateTime.Now;

            string strJson = string.Empty;
            string strDBType = args[0];
            //string strDBType = "task";
            string strInputDate = args[1];
            //string strFileName = @"E:\导入数据\temp.eggdata.log.2015-10-29.001";
            string strTableName = string.Empty;
            string strDUTableName = string.Empty;
            string strUITableName = string.Empty;
            try
            {
                if ("go2.0".Equals(strDBType))
                {
                    strTableName = "Go20SourceData";
                    strDUTableName = "Go20DailyUser";
                    strUITableName = "Go20UserInfo";
                }
                else if ("C#2.0".Equals(strDBType))
                {
                    strTableName = "Cs20SourceData";
                    strDUTableName = "Cs20DailyUser";
                    strUITableName = "Cs20UserInfo";
                }
                else if ("killer2.0".Equals(strDBType))
                {
                    strTableName = "Killer20SourceData";
                    strDUTableName = "Killer20DailyUser";
                    strUITableName = "Killer20UserInfo";
                }
                //else if ("killer".Equals(strDBType))
                //{
                //    strTableName = "Killer20SourceData";
                //}
                else if ("task".Equals(strDBType))
                {
                    strTableName = "Go20TaskSD";
                    strDUTableName = "Go20TaskInfo";
                    strUITableName = string.Empty;
                }

                // 取得(SourceData)单日件数
                Console.WriteLine("GetSourceDataCount Start.");
                intSourceDataCount = db.GetSourceDataCount(strInputDate, strTableName);
                Console.WriteLine("GetSourceDataCount Count = " + intSourceDataCount);
                Console.WriteLine("GetSourceDataCount Insert End.");

                if ("task".Equals(strDBType))
                {
                    // 向表(DailyUser)中插入数据
                    Console.WriteLine("TaskInfo Insert Start.");
                    Console.WriteLine("strDUTableName = " + strDUTableName);
                    intInsertDU = db.InsertTaskInfo(strInputDate, strTableName, strDUTableName);
                    Console.WriteLine("TaskInfo Insert Count = " + intInsertDU);
                    Console.WriteLine("TaskInfo Insert End.");

                    int intdaycount = 0;
                    int inttaskcount = 0;
                    int intreturncount = 0;

                    intdaycount = db.GetTaskDayCount(strInputDate, strTableName);
                    inttaskcount = db.GetTaskResultCount(strInputDate, strTableName);
                    intreturncount = db.GetTaskResultReturnCount(strInputDate, strTableName);

                    Console.WriteLine("InsertDailyVisitUserStatistics For Task Start.");
                    intCount = db.InsertDailyVisitUserStatistics(strDBType, strInputDate, intSourceDataCount, intdaycount, inttaskcount, intreturncount);
                    Console.WriteLine("InsertDailyVisitUserStatistics For Task Count = " + intCount);
                    Console.WriteLine("InsertDailyVisitUserStatistics For Task End.");
                } 
                else
                {
                    // 向表(DailyUser)中插入数据
                    Console.WriteLine("DailyUser Insert Start.");
                    intInsertDU = db.InsertDailyUser(strInputDate, strTableName, strDUTableName);
                    Console.WriteLine("DailyUser Insert Count = " + intInsertDU);
                    Console.WriteLine("DailyUser Insert End.");

                    if (intInsertDU > 0)
                    {
                        // 更新表(DailyUser)中数据
                        Console.WriteLine("DailyUser Update Start.");
                        int intUpdateDU = db.UpdateDailyUser(strInputDate, strTableName, strDUTableName);
                        Console.WriteLine("DailyUser Update End.");

                        Console.WriteLine("UserInfo Insert Start.");
                        // 向表(UserInfo)中插入数据
                        intInsertUI = db.InsertUserInfo(strInputDate, strDUTableName, strUITableName);
                        Console.WriteLine("UserInfo Insert Count = " + intInsertUI);
                        Console.WriteLine("UserInfo Insert End.");
                        //if(intInsertUI>0 && "Go20UserInfo".Equals(strUITableName))
                        //{
                        //    Console.WriteLine("UpdateGo20UserInfo Start.");
                        //    db.UpdateGo20UserInfo(strInputDate, strTableName, strUITableName);
                        //    Console.WriteLine("UpdateGo20UserInfo End.");
                        //}
                    }
                    Console.WriteLine("InsertDailyVisitUserStatistics For 2.0 Start.");
                    intCount = db.InsertDailyVisitUserStatistics(strDBType, strInputDate, intSourceDataCount, intInsertDU, intInsertUI);
                    Console.WriteLine("InsertDailyVisitUserStatistics For 2.0 Count = " + intCount);
                    Console.WriteLine("InsertDailyVisitUserStatistics For 2.0 End.");

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
