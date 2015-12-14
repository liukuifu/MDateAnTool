using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInfoUpdate
{
    class Program
    {
        static void Main(string[] args)
        {
            LogHelper.writeInfoLog("Main Start");
            //if (args == null)
            //{
            //    Console.WriteLine("请输入数据类型名和日期");
            //    LogHelper.writeWarnLog("请输入数据类型名和日期");
            //    return;
            //}

            //if (args.Length < 2)
            //{
            //    Console.WriteLine("请输入数据类型名和日期");
            //    LogHelper.writeWarnLog("请输入数据类型名和日期");
            //    return;
            //}
            //if (string.IsNullOrEmpty(args[0]))
            //{
            //    Console.WriteLine("数据类型名不能为空");
            //    LogHelper.writeWarnLog("数据类型名不能为空");
            //    return;
            //}

            //if (string.IsNullOrEmpty(args[1]))
            //{
            //    Console.WriteLine("日期不能为空");
            //    LogHelper.writeWarnLog("日期不能为空");
            //    return;
            //}

            DBConnect db = new DBConnect();
            DateTime dt = DateTime.Now;

            string strJson = string.Empty;
            //string strDBType = args[0];
            string strDBType = "go3.0";
            //string strInputDate = args[1];
            string strInputDate = "2015-12-11";
            //string strFileName = @"E:\导入数据\temp.eggdata.log.2015-10-29.001";
            string strTableName = string.Empty;
            string strDUTableName = string.Empty;
            string strUITableName = string.Empty;
            try
            {
                if ("go2.0".Equals(strDBType))
                {
                    strTableName = "Go20SourceData";
                    //strTableName = "[Go20SourceData-bak]";
                    strDUTableName = "Go20DailyUser";
                    strUITableName = "Go20UserInfo";
                }
                else if ("killer2.0".Equals(strDBType))
                {
                    strTableName = "Killer20SourceData";
                    strDUTableName = "Killer20DailyUser";
                    strUITableName = "Killer20UserInfo";
                }
                else if("C#2.0".Equals(strDBType))
                {
                    strTableName = "Cs20SourceData";
                    strDUTableName = "Cs20DailyUser";
                    strUITableName = "Cs20UserInfo";
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
                else if ("go3.0".Equals(strDBType))
                {
                    strTableName = "Go30SD";
                    //strTableName = "[Go20SourceData-bak]";
                    strDUTableName = "Go30DailyUser";
                    strUITableName = "Go30UserInfo";
                }

                if ("Go20UserInfo".Equals(strUITableName)
                    || "Killer20UserInfo".Equals(strUITableName))
                {
                    Console.WriteLine("UpdateGo20UserInfo Start.");
                    db.UpdateGo20UserInfo(strInputDate, strTableName, strUITableName);
                    Console.WriteLine("UpdateGo20UserInfo End.");
                }
                else if ("Go30UserInfo".Equals(strUITableName))
                {
                    Console.WriteLine("UpdateGo30UserInfo Start.");
                    db.UpdateGo30UserInfo(strInputDate, strTableName, strUITableName);
                    //db.UpdateGo30UserInfoByCondition(strInputDate, strTableName, strUITableName);
                    Console.WriteLine("UpdateGo30UserInfo End.");
                }
                //db.GetGo20UserInfo();

            }
            catch (Exception ex)
            {
                LogHelper.writeErrorLog(ex);
            }

        }
    }
}
