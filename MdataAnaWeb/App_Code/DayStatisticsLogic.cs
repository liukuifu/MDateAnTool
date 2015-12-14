using Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MdataAn
{
    /// <summary>
    /// Summary description for DayStatisticsLogic
    /// </summary>
    public class DayStatisticsLogic
    {
        public DayStatisticsLogic()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static DataTable Get20DataToTable(DateTime startDt, 
            string strTableName, 
            string strUITableName, 
            string strDUTableName, 
            string strDBType,
            int dayCount)
        {

            LogHelper.writeInfoLog("Get20DataToTable Start");
            bool updateFlg = false;
            bool ThreeDayNumberOfNewUsersUpdateFlg = false;

            //Int64 intCount = 0;
            Int64 intdaycount = 0;
            Int64 intUIDCount = 0;
            Int64 intLossCount = 0;
            
            Int64 intnewcount = 0;
            Int64 intsecondnewcount = 0;
            Int64 intthirdnewcount = 0;
            Int64 intthreenewcount = 0;
            Int64 integg1usercount = 0;
            Int64 intkillusercount = 0;
            Int64 intAfterWeekcount = 0;
            
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
            table.Columns.Add("DAU");
            table.Columns.Add("日新增用户");
            table.Columns.Add("新用户次日访问数");
            table.Columns.Add("新用户第三日访问数");
            table.Columns.Add("新用户三日内访问数");
            table.Columns.Add("新用户次日访问比例");
            table.Columns.Add("新用户第三日访问比例");
            table.Columns.Add("新用户三日内访问比例");
            if ("go2.0".Equals(strDBType))
            {
                table.Columns.Add("总UID数");
                table.Columns.Add("流失数");
            }
            table.Columns.Add("DAU在一周后的存活数");
            table.Columns.Add("DAU在一周后的存活比例");
            table.Columns.Add("egg1中存在用户数");
            table.Columns.Add("kill安装用户数");
            table.Columns.Add(CommonResource.HighVersion);
            table.Columns.Add(CommonResource.LowVersion);
            table.Columns.Add("其它版本");

            //table.Columns.Add("date");
            //table.Columns.Add("daycount");
            //table.Columns.Add("new");
            //table.Columns.Add("secondnew");
            //table.Columns.Add("secondnewp");
            //table.Columns.Add("thirdnew");
            //table.Columns.Add("thirdnewp");
            //table.Columns.Add("threenew");
            //table.Columns.Add("threenewp");
            //table.Columns.Add("uidcount");
            //table.Columns.Add("lossuidcount");
            //table.Columns.Add("weekACount");
            //table.Columns.Add("weekACountp");
            //table.Columns.Add("egg1user");
            //table.Columns.Add("killuser");
            //table.Columns.Add("HighVersion");
            //table.Columns.Add("LowVersion");
            //table.Columns.Add("vother");

            DataRow dr = null;

            for (int i = 0; i < dayCount; i++)
            {
                updateFlg = false;
                strInput = startDt.AddDays(i).ToString("yyyy-MM-dd");

                strSecondDay = string.Format("{0:yyyy-MM-dd}", startDt.AddDays(i + 1));
                strThirdDay = string.Format("{0:yyyy-MM-dd}", startDt.AddDays(i + 2));

                LogHelper.writeDebugLog("strInput = " + strInput);
                LogHelper.writeDebugLog("strDBType = " + strDBType);

                dvusd = dbc.GetDailyVisitUserStatistics(strInput, strDBType);

                LogHelper.writeDebugLog("dvusd = " + dvusd.ToString());

                dr = table.NewRow();

                //if (string.IsNullOrEmpty(dvusd.TotalNumberOfDays))
                //{
                //    intCount = dbc.GetTCount(strInput, strTableName);
                //    dvusd.TotalNumberOfDays = Convert.ToString(intCount);
                //    updateFlg = true;
                //}
                //else
                //{
                //    intCount = Convert.ToInt64(dvusd.TotalNumberOfDays);
                //}


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
                    DateTime dtT1 = DateTime.Parse(strSecondDay);
                    DateTime dtT2 = DateTime.Parse(strInput).AddDays(1);
                    if (dtT1 >= dtT2)
                    {
                        intsecondnewcount = 0;
                    }
                    else
                    {
                        intsecondnewcount = dbc.GetSecondNewCount(strInput, strSecondDay, strDUTableName, strUITableName);
                        dvusd.NextDayNumberOfNewUsers = Convert.ToString(intsecondnewcount);
                        updateFlg = true;
                    }
                }
                else
                {
                    intsecondnewcount = Convert.ToInt64(dvusd.NextDayNumberOfNewUsers);
                }

                if (string.IsNullOrEmpty(dvusd.ThirdDayNumberOfNewUsers)
                    || "0".Equals(dvusd.ThirdDayNumberOfNewUsers))
                {
                    DateTime dtT1 = DateTime.Parse(strThirdDay);
                    DateTime dtT2 = DateTime.Parse(strInput).AddDays(1);
                    if (dtT1 >= dtT2)
                    { 
                        intthirdnewcount = 0;
                    }
                    else
                    {
                        intthirdnewcount = dbc.GetThirdNewCount(strInput, strThirdDay, strDUTableName, strUITableName);
                        dvusd.ThirdDayNumberOfNewUsers = Convert.ToString(intthirdnewcount);
                        updateFlg = true;
                        ThreeDayNumberOfNewUsersUpdateFlg = true;
                    }
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

                if ("go2.0".Equals(strDBType))
                {
                    // 总用户数
                    if (string.IsNullOrEmpty(dvusd.Extension2))
                    {
                        intUIDCount = dbc.GetGo20UserInfoCount(strSecondDay,strUITableName);
                        dvusd.Extension2 = Convert.ToString(intUIDCount);
                        updateFlg = true;
                    }
                    else
                    {
                        intUIDCount = Convert.ToInt64(dvusd.Extension2);
                    }

                    // 流失用户数
                    if (string.IsNullOrEmpty(dvusd.Extension1))
                    {
                        intLossCount = dbc.GetLossCount(strUITableName);
                        dvusd.Extension1 = Convert.ToString(intLossCount);
                        updateFlg = true;
                    }
                    else
                    {
                        intLossCount = Convert.ToInt64(dvusd.Extension1);
                    }
                }

                dr["统计日期"] = strInput;
                //dr["日总访问量"] = intCount;
                dr["DAU"] = intdaycount;
                dr["日新增用户"] = intnewcount;
                dr["新用户次日访问数"] = intsecondnewcount;
                dr["新用户次日访问比例"] = ((double)intsecondnewcount / (double)intnewcount).ToString("P");
                dr["新用户第三日访问数"] = intthirdnewcount;
                dr["新用户第三日访问比例"] = ((double)intthirdnewcount / (double)intnewcount).ToString("P");
                dr["新用户三日内访问数"] = intthreenewcount;
                dr["新用户三日内访问比例"] = ((double)intthreenewcount / (double)intnewcount).ToString("P");
                if ("go2.0".Equals(strDBType))
                {
                    dr["总UID数"] = intUIDCount;
                    dr["流失数"] = intLossCount;
                }
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

            return table;
        }


        public static DataTable GetTask20DataToTable(DateTime startDt, 
            string strTableName, 
            string strUITableName, 
            string strDUTableName, 
            string strDBType,
            int dayCount,
            string strTaskId)
        {
            LogHelper.writeInfoLog("GetTask20DataToTable Start");

            bool updateFlg = false;
            bool ThreeDayNumberOfNewUsersUpdateFlg = false;

            //Int64 intCount = 0;
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
            string[] strIdArr = new string[] { };

            DailyVisitUserStatisticsData dvusd = new DailyVisitUserStatisticsData();

            DBConnect dbc = new DBConnect();
            if (!string.IsNullOrEmpty(strTaskId))
            {
                if (strTaskId.IndexOf(";") > 0)
                {
                    strIdArr = strTaskId.Split(';');
                }
                else
                {
                    strIdArr = new string[] { strTaskId };
                }
            }
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("统计日期");
            //table.Columns.Add("日总访问量");
            table.Columns.Add("DAU");
            table.Columns.Add("task result");
            table.Columns.Add("task result比例");
            table.Columns.Add("task result return == 0");
            table.Columns.Add("task result return == 0比例");
            if (!string.IsNullOrEmpty(strTaskId))
            {
                foreach (string strId in strIdArr)
                {
                    table.Columns.Add("taskid : " + strId);
                    table.Columns.Add("taskid : " + strId + " return == 0");
                    table.Columns.Add("taskid : " + strId + " return == 0 比例");
                }
            }
            DataRow dr = null;
            //string inputTaskId = strTaskId;

            for (int i = 0; i < dayCount; i++)
            {
                updateFlg = false;
                strInput = startDt.AddDays(i).ToString("yyyy-MM-dd");

                strSecondDay = string.Format("{0:yyyy-MM-dd}", startDt.AddDays(i + 1));
                strThirdDay = string.Format("{0:yyyy-MM-dd}", startDt.AddDays(i + 2));

                LogHelper.writeDebugLog("strInput = " + strInput);
                LogHelper.writeDebugLog("strDBType = " + strDBType);

                dvusd = dbc.GetDailyVisitUserStatistics(strInput, strDBType);

                LogHelper.writeDebugLog("dvusd = " + dvusd.ToString());

                dr = table.NewRow();

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
                //dr["日总访问量"] = intCount;
                dr["DAU"] = intdaycount;
                dr["task result"] = inttaskcount;
                dr["task result比例"] = ((double)inttaskcount / (double)intdaycount).ToString("P");
                dr["task result return == 0"] = intreturncount;
                dr["task result return == 0比例"] = ((double)intreturncount / (double)intdaycount).ToString("P");

                if (!string.IsNullOrEmpty(strTaskId))
                {
                    foreach (string strId in strIdArr)
                    {
                        //table.Columns.Add("taskid : " + strId);
                        //table.Columns.Add("taskid : " + strId + " return == 0");
                        //table.Columns.Add("taskid : " + strId + " return == 0 比例");

                        if (!string.IsNullOrEmpty(strId))
                        {
                            intTaskIdcount = dbc.GetTaskIdCount(strInput, strId, strDUTableName);
                            intTaskIdReturncount = dbc.GetTaskIdReturnCount(strInput, strId, strDUTableName);
                        }
                        else
                        {
                            intTaskIdcount = 0;
                            intTaskIdReturncount = 0;
                        }
                        dr["taskid : " + strId] = intTaskIdcount;
                        dr["taskid : " + strId + " return == 0"] = intTaskIdReturncount;
                        dr["taskid : " + strId + " return == 0 比例"] = ((double)intTaskIdReturncount / (double)intTaskIdcount).ToString("P");
                    }
                }
                table.Rows.Add(dr);

                if (string.IsNullOrEmpty(dvusd.UType) &&
                    string.IsNullOrEmpty(dvusd.UDate))
                {
                    dvusd.UType = strDBType;
                    dvusd.UDate = strInput;
                    //dvusd.TotalNumberOfDays = Convert.ToString(intCount);
                    dvusd.DayNumberOfUsers = Convert.ToString(intdaycount);
                    dvusd.TaskNumber = Convert.ToString(inttaskcount);
                    dvusd.TaskNumberOfSuccess = Convert.ToString(intreturncount);
                    dbc.InsertDailyVisitUserStatisticsForTask(dvusd);
                }
                else
                {
                    if (updateFlg)
                    {
                        //dvusd.TotalNumberOfDays = Convert.ToString(intCount);
                        dvusd.DayNumberOfUsers = Convert.ToString(intdaycount);
                        dvusd.TaskNumber = Convert.ToString(inttaskcount);
                        dvusd.TaskNumberOfSuccess = Convert.ToString(intreturncount);
                        dbc.UpdateDailyVisitUserStatistics(dvusd);
                    }
                }
            }

            LogHelper.writeInfoLog("GetTask20DataToTable End");
            return table;
        }

        public static DataTable Get20ChannelDataToTable(DateTime startDt, 
            string strTableName, 
            string strUITableName, 
            string strDUTableName, 
            string strDBType,
            string strChannelValue,
            int dayCount)
        {

            LogHelper.writeInfoLog("Get20ChannelDataToTable Start");
            bool updateFlg = false;
            bool ThreeDayNumberOfNewUsersUpdateFlg = false;

            //Int64 intCount = 0;
            Int64 intdaycount = 0;
            Int64 intdauChannelcount = 0;
            Int64 intdnuChannelcount = 0;
            Int64 intsecondnewcount = 0;
            Int64 intthirdnewcount = 0;
            Int64 intthreenewcount = 0;

            string strInput = string.Empty;
            string strSecondDay = string.Empty;
            string strThirdDay = string.Empty;

            DailyVisitUserStatisticsData dvusd = new DailyVisitUserStatisticsData();

            DBConnect dbc = new DBConnect();

            System.Data.DataTable table = new System.Data.DataTable();

            table.Columns.Add("统计日期");
            //table.Columns.Add("日总访问量");
            table.Columns.Add("DAU数");
            table.Columns.Add("DAU channel : " + strChannelValue + " 用户数");
            table.Columns.Add("DAU中比例");
            table.Columns.Add("DNU channel : " + strChannelValue + " 用户数");
            table.Columns.Add("次日访问数");
            table.Columns.Add("第三日访问数");
            table.Columns.Add("三日内访问数");
            table.Columns.Add("次日访问比例");
            table.Columns.Add("第三日访问比例");
            table.Columns.Add("三日内访问比例");

            DataRow dr = null;

            for (int i = 0; i < dayCount; i++)
            {
                intdaycount = 0;
                intdauChannelcount = 0;
                intdnuChannelcount = 0;
                intsecondnewcount = 0;
                intthirdnewcount = 0;
                intthreenewcount = 0;

                strInput = startDt.AddDays(i).ToString("yyyy-MM-dd");

                strSecondDay = string.Format("{0:yyyy-MM-dd}", startDt.AddDays(i + 1));
                strThirdDay = string.Format("{0:yyyy-MM-dd}", startDt.AddDays(i + 2));

                LogHelper.writeDebugLog("strInput = " + strInput);
                LogHelper.writeDebugLog("strDBType = " + strDBType);

                dvusd = dbc.GetDailyVisitUserStatistics(strInput, strDBType);

                LogHelper.writeDebugLog("dvusd = " + dvusd.ToString());

                dr = table.NewRow();
                
                if (string.IsNullOrEmpty(dvusd.DayNumberOfUsers))
                {
                    intdaycount = dbc.GetDayCount(strInput, strDUTableName);
                    dvusd.DayNumberOfUsers = Convert.ToString(intdaycount);
                    //updateFlg = true;
                }
                else
                {
                    intdaycount = Convert.ToInt64(dvusd.DayNumberOfUsers);
                }

                intdauChannelcount = dbc.GetDauChannelCount(strInput, strDUTableName, strChannelValue);

                intdnuChannelcount = dbc.GetDnuChannelCount(strInput, strDUTableName, strUITableName, strChannelValue);

                intsecondnewcount = dbc.GetDnuSecondCount(strInput, strSecondDay, strDUTableName, strUITableName, strChannelValue);

                intthirdnewcount = dbc.GetDnuThirdCount(strInput, strThirdDay, strDUTableName, strUITableName, strChannelValue);

                intthreenewcount = dbc.GetDnuThreeCount(strInput, strSecondDay, strThirdDay, strDUTableName, strUITableName, strChannelValue);


                dr["统计日期"] = strInput;
                dr["DAU数"] = intdaycount;
                dr["DAU channel : " + strChannelValue + " 用户数"] = intdauChannelcount;
                dr["DAU中比例"] = ((double)intdauChannelcount / (double)intdaycount).ToString("P");
                dr["DNU channel : " + strChannelValue + " 用户数"] = intdnuChannelcount;
                dr["次日访问数"] = intsecondnewcount;
                dr["次日访问比例"] = ((double)intsecondnewcount / (double)intdnuChannelcount).ToString("P");
                dr["第三日访问数"] = intthirdnewcount;
                dr["第三日访问比例"] = ((double)intthirdnewcount / (double)intdnuChannelcount).ToString("P");
                dr["三日内访问数"] = intthreenewcount;
                dr["三日内访问比例"] = ((double)intthreenewcount / (double)intdnuChannelcount).ToString("P");

                table.Rows.Add(dr);
            }

            LogHelper.writeInfoLog("Get20ChannelDataToTable End");


            return table;
        }

    }
}