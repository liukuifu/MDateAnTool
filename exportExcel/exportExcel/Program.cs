using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace exportExcel
{
    class Program
    {
        static void Main(string[] args)
        {
            //Dictionary<string, string> hm = new Dictionary<string, string>();
            //Hashtable ht = new Hashtable();
            //ht.Add("go", "go");
            System.Data.DataTable dtb = new System.Data.DataTable();
            DateTime startDt = DateTime.Now.AddDays(-7);
            DateTime nowDt = DateTime.Now;
            string strTableName = string.Empty;
            string strUITableName = string.Empty;
            string strDUTableName = string.Empty;
            string strSheetName = string.Empty;
            string strExcelName = "数据分析-" + nowDt.ToString("yyyyMMdd");
            string[] strArry = new string[] { "go", "C#", "killer", "go2.0", "C#2.0", "killer2.0", "task20" };
            int i = 1;
            foreach (string strT in strArry)//i = 0; i < hm.Count(); i++)
            {
                switch (strT)
                {
                    case "go":
                        strTableName = "GoSourceData";
                        strUITableName = "UserInfo";
                        strDUTableName = "GoDailyUser";
                        strSheetName = "go";
                        dtb = Get10DataToTable(startDt, strTableName, strUITableName, strDUTableName, strSheetName);
                        break;
                    case "C#":
                        strTableName = "CSharpSourceData";
                        strUITableName = "CsUserInfo";
                        strDUTableName = "CsDailyUser";
                        strSheetName = "C#";
                        dtb = Get10DataToTable(startDt, strTableName, strUITableName, strDUTableName, strSheetName);
                        break;
                    case "killer":
                        strTableName = "KillerSourceData";
                        strUITableName = "KillerUserInfo";
                        strDUTableName = "KillerDailyUser";
                        strSheetName = "Killer";
                        dtb = Get10DataToTable(startDt, strTableName, strUITableName, strDUTableName, strSheetName);
                        break;
                    case "go2.0":
                        strTableName = "Go20SourceData";
                        strUITableName = "Go20UserInfo";
                        strDUTableName = "Go20DailyUser";
                        strSheetName = "go2.0";
                        dtb = Get20DataToTable(startDt, strTableName, strUITableName, strDUTableName, strSheetName);
                        break;
                    case "C#2.0":
                        strTableName = "Cs20SourceData";
                        strUITableName = "Cs20UserInfo";
                        strDUTableName = "Cs20DailyUser";
                        strSheetName = "C#2.0";
                        dtb = Get20DataToTable(startDt, strTableName, strUITableName, strDUTableName, strSheetName);
                        break;
                    case "killer2.0":
                        strTableName = "Killer20SourceData";
                        strDUTableName = "Killer20DailyUser";
                        strUITableName = "Killer20UserInfo";
                        strSheetName = "killer2.0";
                        dtb = Get20DataToTable(startDt, strTableName, strUITableName, strDUTableName, strSheetName);
                        break;
                    case "task20":
                        strTableName = "Go20TaskSD";
                        strDUTableName = "Go20TaskInfo";
                        strSheetName = "task20";
                        dtb = GetTask20DataToTable(startDt, strTableName, strUITableName, strDUTableName, strSheetName);
                        break;
                    default:
                        return;
                }
                exportToExcel(i,strExcelName, strSheetName, dtb);
                i = i + 1;
            }
        }

        private static void exportToExcel(int index,string strExcelName, string strSheetName, System.Data.DataTable dtb)
        {
            LogHelper.writeInfoLog("exportToExcel Start");
            try
            {
                DateTime dt = DateTime.Now.AddDays(-1);
                string UnFullPath = CommonResource.UnZipPath.ToString() + "\\" + dt.ToString("yyyy-MM-dd");
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.DisplayAlerts = false;
                //excel.visible = true;
                Microsoft.Office.Interop.Excel.Workbook book;
                if (File.Exists(UnFullPath + "\\" + strExcelName + ".xlsx"))
                {
                    //存在
                    book = excel.Workbooks.Open(UnFullPath + "\\" + strExcelName + ".xlsx",
                    0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

                }
                else
                {
                    // 不存在
                    book = excel.Workbooks.Add(Type.Missing);
                    book.Sheets.Add(Missing.Value, Missing.Value, 7, Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
                }

                //if(book.Sheets.Count < index)
                //{
                //    book.Sheets.Add(Missing.Value, Missing.Value, 1, Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
                //}

                Microsoft.Office.Interop.Excel.Worksheet sheet = (Worksheet)book.Sheets[index];

                //Microsoft.Office.Interop.Excel.Worksheet sheet = book.ActiveSheet as Microsoft.Office.Interop.Excel.Worksheet;                
                sheet.Name = strSheetName;

                for (int i = 0; i < dtb.Columns.Count; i++)
                {
                    sheet.Cells[1, i + 1] = dtb.Columns[i].ColumnName;
                }
                for (int m = 0; m < dtb.Rows.Count; m++)
                {
                    for (int n = 0; n < dtb.Rows[m].ItemArray.Count(); n++)
                    {
                        sheet.Cells[m + 2, n + 1] = dtb.Rows[m].ItemArray[n];
                    }
                }

                //book.Save();


                book.SaveAs(UnFullPath + "\\" + strExcelName + ".xlsx",
                     //Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, 
                     Missing.Value,
                    Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                    //Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, 
                    Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);


                book.Close(true, UnFullPath + "\\" + strExcelName + ".xlsx", Missing.Value);


                //退出Excel.Application


                excel.Quit();
            }
            catch(Exception ex)
            {
                LogHelper.writeErrorLog(ex);
            }
            LogHelper.writeInfoLog("exportToExcel End");
        }

        private static System.Data.DataTable Get10DataToTable(DateTime startDt, string strTableName, string strUITableName, string strDUTableName, string strDBType)
        {
            //LogHelper.writeInfoLog("search_Click Start");
            bool updateFlg = false;
            bool ThreeDayNumberOfNewUsersUpdateFlg = false;

            Int64 intCount = 0;
            Int64 intdaycount = 0;

            Int64 inttaskcount = 0;
            Int64 intreturncount = 0;

            Int64 intnewcount = 0;
            Int64 intsecondnewcount = 0;
            Int64 intthirdnewcount = 0;
            Int64 intthreenewcount = 0;
            Int64 integg1usercount = 0;
            Int64 intkillusercount = 0;
            Int64 intAfterWeekcount = 0;

            Int64 intTaskIdcount = 0;
            Int64 intTaskIdReturncount = 0;

            Int64 intv112 = 0;
            Int64 v107 = 0;
            Int64 vother = 0;

            //this.search.Enabled = false;
            //string rtn = string.Empty;
            string strInput = string.Empty;
            string strSecondDay = string.Empty;
            string strThirdDay = string.Empty;
            DailyVisitUserStatisticsData dvusd = new DailyVisitUserStatisticsData();


            DBConnect dbc = new DBConnect();

            LogHelper.writeDebugLog("strInput = " + strInput);
            LogHelper.writeDebugLog("strDBType = " + strDBType);

            System.Data.DataTable table = new System.Data.DataTable();
            DataRow dr = null;

            table.Columns.Add("统计日期");
            table.Columns.Add("日总访问量");
            table.Columns.Add("DAU");
            table.Columns.Add("日新增用户");
            table.Columns.Add("新用户次日访问数");
            table.Columns.Add("新用户第三日访问数");
            table.Columns.Add("新用户三日内访问数");
            table.Columns.Add("新用户次日访问比例");
            table.Columns.Add("新用户第三日访问比例");
            table.Columns.Add("新用户三日内访问比例");

            for (int i = 0; i < 7; i++)
            {
                dr = table.NewRow();

                strInput = startDt.AddDays(i).ToString("yyyy-MM-dd");
                strSecondDay = string.Format("{0:yyyy-MM-dd}", startDt.AddDays(i + 1));
                strThirdDay = string.Format("{0:yyyy-MM-dd}", startDt.AddDays(i + 2));

                dvusd = dbc.GetDailyVisitUserStatistics(strInput, strDBType);

                if (string.IsNullOrEmpty(dvusd.TotalNumberOfDays))
                {
                    //LogHelper.writeDebugLog("1111111");

                    intCount = dbc.GetTCount(strInput, strTableName);
                    dvusd.TotalNumberOfDays = Convert.ToString(intCount);
                    updateFlg = true;
                }
                else
                {
                    intCount = Convert.ToInt64(dvusd.TotalNumberOfDays);
                }

                if (string.IsNullOrEmpty(dvusd.DayNumberOfUsers))
                {
                    intdaycount = dbc.GetDayCount(strInput, strDUTableName);
                    dvusd.DayNumberOfUsers = Convert.ToString(intdaycount);
                    updateFlg = true;
                }
                else
                {
                    intdaycount = Convert.ToInt64(dvusd.DayNumberOfUsers);
                }

                if (string.IsNullOrEmpty(dvusd.NumberOfDaysNewUsers))
                {
                    intnewcount = dbc.GetNewCount(strInput, strTableName, strUITableName);
                    dvusd.NumberOfDaysNewUsers = Convert.ToString(intnewcount);
                    updateFlg = true;
                }
                else
                {
                    intnewcount = Convert.ToInt64(dvusd.NumberOfDaysNewUsers);
                }

                if (string.IsNullOrEmpty(dvusd.NextDayNumberOfNewUsers)
                    || "0".Equals(dvusd.NextDayNumberOfNewUsers))
                {
                    //LogHelper.writeDebugLog("44444");

                    intsecondnewcount = dbc.GetSecondNewCount(strInput, strSecondDay, strDUTableName, strUITableName);
                    dvusd.NextDayNumberOfNewUsers = Convert.ToString(intsecondnewcount);
                    updateFlg = true;
                }
                else
                {
                    intsecondnewcount = Convert.ToInt64(dvusd.NextDayNumberOfNewUsers);
                }


                if (string.IsNullOrEmpty(dvusd.ThirdDayNumberOfNewUsers)
                    || "0".Equals(dvusd.ThirdDayNumberOfNewUsers))
                {
                    //LogHelper.writeDebugLog("55555");

                    intthirdnewcount = dbc.GetThirdNewCount(strInput, strThirdDay, strDUTableName, strUITableName);
                    dvusd.ThirdDayNumberOfNewUsers = Convert.ToString(intthirdnewcount);
                    updateFlg = true;
                    ThreeDayNumberOfNewUsersUpdateFlg = true;
                }
                else
                {
                    intthirdnewcount = Convert.ToInt64(dvusd.ThirdDayNumberOfNewUsers);
                }

                if (string.IsNullOrEmpty(dvusd.ThreeDayNumberOfNewUsers)
                    || "0".Equals(dvusd.ThreeDayNumberOfNewUsers)
                    || ThreeDayNumberOfNewUsersUpdateFlg)
                {
                    //LogHelper.writeDebugLog("666666");

                    intthreenewcount = dbc.GetThreeNewCount(strInput, strSecondDay, strThirdDay, strDUTableName, strUITableName);
                    dvusd.ThreeDayNumberOfNewUsers = Convert.ToString(intthreenewcount);
                    updateFlg = true;
                }
                else
                {
                    intthreenewcount = Convert.ToInt64(dvusd.ThreeDayNumberOfNewUsers);
                }

                dr["统计日期"] = strInput;
                dr["日总访问量"] = intCount;
                dr["DAU"] = intdaycount;
                dr["日新增用户"] = intnewcount;
                dr["新用户次日访问数"] = intsecondnewcount;
                dr["新用户次日访问比例"] = ((double)intsecondnewcount / (double)intnewcount).ToString("P");
                dr["新用户第三日访问数"] = intthirdnewcount;
                dr["新用户第三日访问比例"] = ((double)intthirdnewcount / (double)intnewcount).ToString("P");
                dr["新用户三日内访问数"] = intthreenewcount;
                dr["新用户三日内访问比例"] = ((double)intthreenewcount / (double)intnewcount).ToString("P");

                table.Rows.Add(dr);

                if (string.IsNullOrEmpty(dvusd.UType) &&
                    string.IsNullOrEmpty(dvusd.UDate))
                {
                    //LogHelper.writeDebugLog("77777");

                    dvusd.UType = strDBType;
                    dvusd.UDate = strInput;
                    dbc.InsertDailyVisitUserStatistics(dvusd);
                }
                else
                {
                    //LogHelper.writeDebugLog("888888");

                    if (updateFlg)
                    {
                        //LogHelper.writeDebugLog("9999999");

                        dbc.UpdateDailyVisitUserStatistics(dvusd);
                    }
                }
            }

            return table;
        }

        private static System.Data.DataTable Get20DataToTable(DateTime startDt, string strTableName, string strUITableName, string strDUTableName, string strDBType)
        {

            LogHelper.writeInfoLog("Get20DataToTable Start");
            bool updateFlg = false;
            bool ThreeDayNumberOfNewUsersUpdateFlg = false;

            Int64 intCount = 0;
            Int64 intdaycount = 0;

            Int64 inttaskcount = 0;
            Int64 intreturncount = 0;

            Int64 intnewcount = 0;
            Int64 intsecondnewcount = 0;
            Int64 intthirdnewcount = 0;
            Int64 intthreenewcount = 0;
            Int64 integg1usercount = 0;
            Int64 intkillusercount = 0;
            Int64 intAfterWeekcount = 0;

            Int64 intTaskIdcount = 0;
            Int64 intTaskIdReturncount = 0;

            Int64 intv112 = 0;
            Int64 v107 = 0;
            Int64 vother = 0;
            string strInput = string.Empty;
            string strSecondDay = string.Empty;
            string strThirdDay = string.Empty;

            DailyVisitUserStatisticsData dvusd = new DailyVisitUserStatisticsData();

            DBConnect dbc = new DBConnect();

            System.Data.DataTable table = new System.Data.DataTable();

            table.Columns.Add("统计日期");
            table.Columns.Add("日总访问量");
            table.Columns.Add("DAU");
            table.Columns.Add("日新增用户");
            table.Columns.Add("新用户次日访问数");
            table.Columns.Add("新用户第三日访问数");
            table.Columns.Add("新用户三日内访问数");
            table.Columns.Add("新用户次日访问比例");
            table.Columns.Add("新用户第三日访问比例");
            table.Columns.Add("新用户三日内访问比例");
            table.Columns.Add("DAU在一周后的存活数");
            table.Columns.Add("DAU在一周后的存活比例");
            table.Columns.Add("egg1中存在用户数");
            table.Columns.Add("kill安装用户数");
            table.Columns.Add(CommonResource.HighVersion);
            table.Columns.Add(CommonResource.LowVersion);
            table.Columns.Add("其它版本");

            DataRow dr = null;

            for (int i = 0; i < 7; i++)
            {
                strInput = startDt.AddDays(i).ToString("yyyy-MM-dd");

                strSecondDay = string.Format("{0:yyyy-MM-dd}", startDt.AddDays(i + 1));
                strThirdDay = string.Format("{0:yyyy-MM-dd}", startDt.AddDays(i + 2));

                LogHelper.writeDebugLog("strInput = " + strInput);
                LogHelper.writeDebugLog("strDBType = " + strDBType);

                dvusd = dbc.GetDailyVisitUserStatistics(strInput, strDBType);

                LogHelper.writeDebugLog("dvusd = " + dvusd.ToString());

                dr = table.NewRow();

                if (string.IsNullOrEmpty(dvusd.TotalNumberOfDays))
                {
                    intCount = dbc.GetTCount(strInput, strTableName);
                    dvusd.TotalNumberOfDays = Convert.ToString(intCount);
                    updateFlg = true;
                }
                else
                {
                    intCount = Convert.ToInt64(dvusd.TotalNumberOfDays);
                }


                if (string.IsNullOrEmpty(dvusd.DayNumberOfUsers))
                {
                    intdaycount = dbc.GetDayCount(strInput, strDUTableName);
                    dvusd.DayNumberOfUsers = Convert.ToString(intdaycount);
                    updateFlg = true;
                }
                else
                {
                    intdaycount = Convert.ToInt64(dvusd.DayNumberOfUsers);
                }

                if (string.IsNullOrEmpty(dvusd.NumberOfDaysNewUsers))
                {
                    intnewcount = dbc.GetNewCount(strInput, strTableName, strUITableName);
                    dvusd.NumberOfDaysNewUsers = Convert.ToString(intnewcount);
                    updateFlg = true;
                }
                else
                {
                    intnewcount = Convert.ToInt64(dvusd.NumberOfDaysNewUsers);
                }

                if (string.IsNullOrEmpty(dvusd.NextDayNumberOfNewUsers)
                    || "0".Equals(dvusd.NextDayNumberOfNewUsers))
                {
                    intsecondnewcount = dbc.GetSecondNewCount(strInput, strSecondDay, strDUTableName, strUITableName);
                    dvusd.NextDayNumberOfNewUsers = Convert.ToString(intsecondnewcount);
                    updateFlg = true;
                }
                else
                {
                    intsecondnewcount = Convert.ToInt64(dvusd.NextDayNumberOfNewUsers);
                }

                if (string.IsNullOrEmpty(dvusd.ThirdDayNumberOfNewUsers)
                    || "0".Equals(dvusd.ThirdDayNumberOfNewUsers))
                {
                    intthirdnewcount = dbc.GetThirdNewCount(strInput, strThirdDay, strDUTableName, strUITableName);
                    dvusd.ThirdDayNumberOfNewUsers = Convert.ToString(intthirdnewcount);
                    updateFlg = true;
                    ThreeDayNumberOfNewUsersUpdateFlg = true;
                }
                else
                {
                    intthirdnewcount = Convert.ToInt64(dvusd.ThirdDayNumberOfNewUsers);
                }

                if (string.IsNullOrEmpty(dvusd.ThreeDayNumberOfNewUsers)
                    || "0".Equals(dvusd.ThreeDayNumberOfNewUsers)
                    || ThreeDayNumberOfNewUsersUpdateFlg)
                {
                    intthreenewcount = dbc.GetThreeNewCount(strInput, strSecondDay, strThirdDay, strDUTableName, strUITableName);
                    dvusd.ThreeDayNumberOfNewUsers = Convert.ToString(intthreenewcount);
                    updateFlg = true;
                }
                else
                {
                    intthreenewcount = Convert.ToInt64(dvusd.ThreeDayNumberOfNewUsers);
                }

                if (string.IsNullOrEmpty(dvusd.NumberOfNewUsersEgg1))
                {
                    integg1usercount = dbc.GetEgg1UserCount(strInput, strUITableName);
                    dvusd.NumberOfNewUsersEgg1 = Convert.ToString(integg1usercount);
                    updateFlg = true;
                }
                else
                {
                    integg1usercount = Convert.ToInt64(dvusd.NumberOfNewUsersEgg1);
                }

                if (string.IsNullOrEmpty(dvusd.DayNumberOfUsersKillInstallation))
                {
                    intkillusercount = dbc.GetKillUserCount(strInput, strDUTableName);
                    dvusd.DayNumberOfUsersKillInstallation = Convert.ToString(intkillusercount);
                    updateFlg = true;
                }
                else
                {
                    intkillusercount = Convert.ToInt64(dvusd.DayNumberOfUsersKillInstallation);
                }

                intAfterWeekcount = dbc.GetAfterWeekcount(strInput, strDUTableName);

                dr["统计日期"] = strInput;
                dr["日总访问量"] = intCount;
                dr["DAU"] = intdaycount;
                dr["日新增用户"] = intnewcount;
                dr["新用户次日访问数"] = intsecondnewcount;
                dr["新用户次日访问比例"] = ((double)intsecondnewcount / (double)intnewcount).ToString("P");
                dr["新用户第三日访问数"] = intthirdnewcount;
                dr["新用户第三日访问比例"] = ((double)intthirdnewcount / (double)intnewcount).ToString("P");
                dr["新用户三日内访问数"] = intthreenewcount;
                dr["新用户三日内访问比例"] = ((double)intthreenewcount / (double)intnewcount).ToString("P");
                dr["egg1中存在用户数"] = integg1usercount;
                dr["kill安装用户数"] = intkillusercount;
                dr["DAU在一周后的存活数"] = intAfterWeekcount;
                dr["DAU在一周后的存活比例"] = ((double)intAfterWeekcount / (double)intdaycount).ToString("P"); 

                intv112 = dbc.GetVCount(strInput, strDUTableName, CommonResource.HighVersion);
                v107 = dbc.GetVCount(strInput, strDUTableName, CommonResource.LowVersion);
                vother = dbc.GetNotVCount(strInput, strDUTableName, CommonResource.LowVersion, CommonResource.HighVersion); 
                dr[CommonResource.HighVersion] = intv112;
                dr[CommonResource.LowVersion] = v107;
                dr["其它版本"] = vother;

                table.Rows.Add(dr);

                if (string.IsNullOrEmpty(dvusd.UType) &&
                    string.IsNullOrEmpty(dvusd.UDate))
                {
                    dvusd.UType = strDBType;
                    dvusd.UDate = strInput;
                    dbc.InsertDailyVisitUserStatisticsForGo20(dvusd);
                }
                else
                {
                    if (updateFlg)
                    {
                        dbc.UpdateDailyVisitUserStatistics(dvusd);
                    }
                }

            }
            if ("go2.0".Equals(strDBType))
            {
                dr = table.NewRow();              
                dr["统计日期"] = "总UID数";
                dr["日总访问量"] = "=sum(C2:C8)";
                table.Rows.Add(dr);

                dr = table.NewRow();
                DateTime nowDt = DateTime.Now.AddDays(-1);

                strInput = nowDt.ToString("yyyy-MM-dd");
                dvusd = dbc.GetDailyVisitUserStatistics(strInput, strDBType);
                dr["统计日期"] = "流失数";
                dr["日总访问量"] = dvusd.Extension1;
                table.Rows.Add(dr);

            }


            return table;
        }

        private static System.Data.DataTable GetTask20DataToTable(DateTime startDt, string strTableName, string strUITableName, string strDUTableName, string strDBType)
        {
            LogHelper.writeInfoLog("search_Click Start");
            bool updateFlg = false;
            bool ThreeDayNumberOfNewUsersUpdateFlg = false;

            Int64 intCount = 0;
            Int64 intdaycount = 0;

            Int64 inttaskcount = 0;
            Int64 intreturncount = 0;

            Int64 intnewcount = 0;
            Int64 intsecondnewcount = 0;
            Int64 intthirdnewcount = 0;
            Int64 intthreenewcount = 0;
            Int64 integg1usercount = 0;
            Int64 intkillusercount = 0;
            Int64 intAfterWeekcount = 0;

            Int64 intTaskIdcount = 0;
            Int64 intTaskIdReturncount = 0;

            Int64 intv112 = 0;
            Int64 v107 = 0;
            Int64 vother = 0;

            //this.search.Enabled = false;
            //string rtn = string.Empty;
            string strInput = string.Empty;

            string strSecondDay = string.Empty;
            string strThirdDay = string.Empty;

            DailyVisitUserStatisticsData dvusd = new DailyVisitUserStatisticsData();

            DBConnect dbc = new DBConnect();

            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("统计日期");
            table.Columns.Add("日总访问量");
            table.Columns.Add("DAU");
            table.Columns.Add("task");
            table.Columns.Add("taskp");
            table.Columns.Add("return");
            table.Columns.Add("returnp");
            table.Columns.Add("taskid");
            table.Columns.Add("taskidreturn");
            table.Columns.Add("taskidreturnp");

            DataRow dr = null;
            string inputTaskId = "8";

            for (int i = 0; i < 7; i++)
            {
                strInput = startDt.AddDays(i).ToString("yyyy-MM-dd");

                strSecondDay = string.Format("{0:yyyy-MM-dd}", startDt.AddDays(i + 1));
                strThirdDay = string.Format("{0:yyyy-MM-dd}", startDt.AddDays(i + 2));

                LogHelper.writeDebugLog("strInput = " + strInput);
                LogHelper.writeDebugLog("strDBType = " + strDBType);

                dvusd = dbc.GetDailyVisitUserStatistics(strInput, strDBType);

                LogHelper.writeDebugLog("dvusd = " + dvusd.ToString());

                dr = table.NewRow();
                if (string.IsNullOrEmpty(dvusd.TotalNumberOfDays))
                {
                    intCount = dbc.GetTaskCount(strInput, strTableName);
                    updateFlg = true;
                }
                else
                {
                    intCount = Convert.ToInt64(dvusd.TotalNumberOfDays);
                }

                if (string.IsNullOrEmpty(dvusd.DayNumberOfUsers))
                {
                    intdaycount = dbc.GetTaskDayCount(strInput, strTableName);
                    updateFlg = true;
                }
                else
                {
                    intdaycount = Convert.ToInt64(dvusd.DayNumberOfUsers);
                }

                if (string.IsNullOrEmpty(dvusd.TaskNumber))
                {
                    inttaskcount = dbc.GetTaskResultCount(strInput, strTableName);
                    updateFlg = true;
                }
                else
                {
                    inttaskcount = Convert.ToInt64(dvusd.TaskNumber);
                }

                if (string.IsNullOrEmpty(dvusd.TaskNumberOfSuccess))
                {
                    intreturncount = dbc.GetTaskResultReturnCount(strInput, strTableName);
                    updateFlg = true;
                }
                else
                {
                    intreturncount = Convert.ToInt64(dvusd.TaskNumberOfSuccess);
                }

                dr["统计日期"] = strInput;
                dr["日总访问量"] = intCount;
                dr["DAU"] = intdaycount;
                dr["task"] = inttaskcount;
                dr["taskp"] = ((double)inttaskcount / (double)intdaycount).ToString("P");
                dr["return"] = intreturncount;
                dr["returnp"] = ((double)intreturncount / (double)intdaycount).ToString("P");

                if (!string.IsNullOrEmpty(inputTaskId))
                {
                    intTaskIdcount = dbc.GetTaskIdCount(strInput, inputTaskId, strDUTableName);
                    intTaskIdReturncount = dbc.GetTaskIdReturnCount(strInput, inputTaskId, strDUTableName);
                }
                else
                {
                    intTaskIdcount = 0;
                    intTaskIdReturncount = 0;
                }
                dr["taskid"] = intTaskIdcount;
                dr["taskidreturn"] = intTaskIdReturncount;
                dr["taskidreturnp"] = ((double)intTaskIdReturncount / (double)intTaskIdcount).ToString("P");
                table.Rows.Add(dr);

                if (string.IsNullOrEmpty(dvusd.UType) &&
                    string.IsNullOrEmpty(dvusd.UDate))
                {
                    dvusd.UType = strDBType;
                    dvusd.UDate = strInput;
                    dvusd.TotalNumberOfDays = Convert.ToString(intCount);
                    dvusd.DayNumberOfUsers = Convert.ToString(intdaycount);
                    dvusd.TaskNumber = Convert.ToString(inttaskcount);
                    dvusd.TaskNumberOfSuccess = Convert.ToString(intreturncount);
                    dbc.InsertDailyVisitUserStatisticsForTask(dvusd);
                }
                else
                {
                    if (updateFlg)
                    {
                        dvusd.TotalNumberOfDays = Convert.ToString(intCount);
                        dvusd.DayNumberOfUsers = Convert.ToString(intdaycount);
                        dvusd.TaskNumber = Convert.ToString(inttaskcount);
                        dvusd.TaskNumberOfSuccess = Convert.ToString(intreturncount);
                        dbc.UpdateDailyVisitUserStatistics(dvusd);
                    }
                }
            }

            return table;
        }
    }
}
