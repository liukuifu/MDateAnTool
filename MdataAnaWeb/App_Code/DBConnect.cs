using Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace MdataAn
{
    /// <summary>
    /// Summary description for DBConnect
    /// </summary>
    public class DBConnect
    {
        //private static string connectionString =
        //"Server = 10.1.7.126;" +
        //"Database = MDataTemp;" +
        //"User ID = sa;" +
        //"Password = 12345678;";
        private static string connectionString = WebConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;

        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <returns></returns>
        private SqlConnection ConnectionOpen()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }

        public DailyVisitUserStatisticsData GetDailyVisitUserStatistics(string date, string strCType)
        {
            LogHelper.writeDebugLog("GetDailyVisitUserStatistics Start");
            DailyVisitUserStatisticsData rtn = new DailyVisitUserStatisticsData();

            SqlConnection conn = ConnectionOpen();

            string sql = "SELECT keys,UType ,UDate ,TotalNumberOfDays ,DayNumberOfUsers ,NumberOfDaysNewUsers ,NextDayNumberOfNewUsers ,ThirdDayNumberOfNewUsers ,ThreeDayNumberOfNewUsers ,NumberOfNewUsersEgg1 ,DayNumberOfUsersKillInstallation ,TaskNumber ,TaskNumberOfSuccess ,Extension1 ,Extension2 ,Extension3 ,Extension4 ,Extension5 ,createdate ,updatedate "
                                + "FROM DailyVisitUserStatistics where UType = '" + strCType + "' and UDate = '" + date + "'";

            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = Convert.ToInt32(Resources.CommonResource.Command_Timeout);

            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                if (reader != null && reader.FieldCount > 0)
                {
                    rtn.keys = Convert.ToInt32(reader[0]);

                    if (reader[1] != null)
                    {
                        rtn.UType = Convert.ToString(reader[1]);
                    }

                    if (reader[2] != null)
                    {
                        rtn.UDate = Convert.ToString(reader[2]);
                    }

                    if (reader[3] != null)
                    {
                        rtn.TotalNumberOfDays = Convert.ToString(reader[3]);
                    }

                    if (reader[4] != null)
                    {
                        rtn.DayNumberOfUsers = Convert.ToString(reader[4]);
                    }

                    if (reader[5] != null)
                    {
                        rtn.NumberOfDaysNewUsers = Convert.ToString(reader[5]);
                    }

                    if (reader[6] != null)
                    {
                        rtn.NextDayNumberOfNewUsers = Convert.ToString(reader[6]);
                    }

                    if (reader[7] != null)
                    {
                        rtn.ThirdDayNumberOfNewUsers = Convert.ToString(reader[7]);
                    }

                    if (reader[8] != null)
                    {
                        rtn.ThreeDayNumberOfNewUsers = Convert.ToString(reader[8]);
                    }

                    if (reader[9] != null)
                    {
                        rtn.NumberOfNewUsersEgg1 = Convert.ToString(reader[9]);
                    }

                    if (reader[10] != null)
                    {
                        rtn.DayNumberOfUsersKillInstallation = Convert.ToString(reader[10]);
                    }

                    if (reader[11] != null)
                    {
                        rtn.TaskNumber = Convert.ToString(reader[11]);
                    }

                    if (reader[12] != null)
                    {
                        rtn.TaskNumberOfSuccess = Convert.ToString(reader[12]);
                    }

                    if (reader[13] != null)
                    {
                        rtn.Extension1 = Convert.ToString(reader[13]);
                    }

                    if (reader[14] != null)
                    {
                        rtn.Extension2 = Convert.ToString(reader[14]);
                    }

                    if (reader[15] != null)
                    {
                        rtn.Extension3 = Convert.ToString(reader[15]);
                    }

                    if (reader[16] != null)
                    {
                        rtn.Extension4 = Convert.ToString(reader[16]);
                    }

                    if (reader[17] != null)
                    {
                        rtn.Extension5 = Convert.ToString(reader[17]);
                    }

                    if (reader[18] != null)
                    {
                        rtn.createdate = Convert.ToDateTime(reader[18]);
                    }

                    if (reader[19] != null)
                    {
                        rtn.updatedate = Convert.ToDateTime(reader[19]);
                    }
                }
            }
            conn.Close();
            LogHelper.writeDebugLog("GetDailyVisitUserStatistics End");
            return rtn;
        }

        public void GetWeekCount(string strInput, string strDUTableName, ref DataTable table)
        {
            DataRow dr;
            DateTime startDT;
            DateTime endDT;
            DateTime searchDT;
            for (int i = 0; i < 7; i++)
            {
                dr = table.NewRow();
                searchDT = Convert.ToDateTime(strInput).AddDays(i);
                startDT = Convert.ToDateTime(strInput).AddDays(7 + i);
                endDT = Convert.ToDateTime(strInput).AddDays(13 + i);

                SqlConnection conn = ConnectionOpen();
                string sql =
                    "SELECT count(DISTINCT [uid]) FROM "
                    + strDUTableName
                    + " where udate = '"
                    + searchDT.ToString("yyyy-MM-dd")
                    + "'";

                SqlCommand comm = new SqlCommand(sql, conn);
                comm.CommandTimeout = Convert.ToInt32(Resources.CommonResource.Command_Timeout);
                Int64 dayCount = (int)comm.ExecuteScalar();

                sql = "SELECT count(DISTINCT gsd.[uid]) FROM "
                    + strDUTableName
                    + " gsd where gsd.udate between '"
                    + startDT.ToString("yyyy-MM-dd")
                    + "' and '"
                    + endDT.ToString("yyyy-MM-dd")
                    + "' and gsd.uid in (SELECT DISTINCT [uid] FROM "
                    + strDUTableName
                    + " where udate = '"
                    + searchDT.ToString("yyyy-MM-dd")
                    + "')";

                comm = new SqlCommand(sql, conn);
                comm.CommandTimeout = Convert.ToInt32(Resources.CommonResource.Command_Timeout);
                Int64 weekACount = (int)comm.ExecuteScalar();
                conn.Close();

                dr["date"] = searchDT.ToString("yyyy-MM-dd");
                //dr["week"] = searchDT.DayOfWeek;
                dr["week"] = searchDT.ToString("dddd", new System.Globalization.CultureInfo("zh-CN"));
                dr["daycount"] = dayCount;
                dr["weekACount"] = weekACount;
                dr["weekACountp"] = ((double)weekACount / (double)dayCount).ToString("P");
                table.Rows.Add(dr);
            }
            
        }

        public Int64 GetNewCount(string date, string strDataTableName, string strUserTableName)
        {
            LogHelper.writeDebugLog("GetNewCount Start");
            Int64 rtn = 0;

            SqlConnection conn = ConnectionOpen();
            string sql = "SELECT count(1) FROM "
                + strUserTableName 
                + " where Convert(varchar, udate,120) LIKE '" 
                + date 
                + "%'";
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = Convert.ToInt32(Resources.CommonResource.Command_Timeout);
            int intUserInfoCount = (int)comm.ExecuteScalar();
            if (intUserInfoCount > 0)
            {
                rtn = intUserInfoCount;
            }
            else
            {

                //sql = "insert "
                //    + strUserTableName 
                //    + " SELECT DISTINCT uid ,'"
                //    + date
                //    + " 00:00:01.000' FROM "
                //    + strDataTableName 
                //    + " where Convert(varchar, udate,120) like '"
                //    + date
                //    + "%' and uid not in (select uid from "
                //    + strUserTableName
                //    + ") and uid like '{%'";

                //comm = new SqlCommand(sql, conn);
                //comm.CommandTimeout = 240;
                //intUserInfoCount = (int)comm.ExecuteScalar();
                //rtn = intUserInfoCount;
            }
            conn.Close();
            LogHelper.writeDebugLog("GetNewCount End");
            return rtn;
        }

        public Int64 GetTCount(string date, string strDataTableName)
        {
            Int64 rtn = 0;

            SqlConnection conn = ConnectionOpen();
            string sql = "SELECT count(1) FROM "
                + strDataTableName 
                + " where Convert(varchar, udate,120) LIKE '" + date + "%'";
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = Convert.ToInt32(Resources.CommonResource.Command_Timeout);
            rtn = (int)comm.ExecuteScalar();
            conn.Close();
            return rtn;
        }

        public Int64 GetDayCount(string date, string strDailyTableName)
        {
            LogHelper.writeDebugLog("GetDayCount Start");
            Int64 rtn = 0;

            SqlConnection conn = ConnectionOpen();

            string sql = "SELECT count(1) FROM " 
                + strDailyTableName
                + " where udate = '" 
                + date 
                + "'";
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = Convert.ToInt32(Resources.CommonResource.Command_Timeout);
            rtn = (int)comm.ExecuteScalar();

            //SqlDataReader reader = comm.ExecuteReader();
            //while (reader.Read())
            //{
            //    rtn = rtn + 1;
            //}
            conn.Close();
            LogHelper.writeDebugLog("GetDayCount End");
            return rtn;
        }

        public long GetTaskIdReturnCount(string date, string inputTaskId, string strDUTableName)
        {
            Int64 rtn = 0;

            SqlConnection conn = ConnectionOpen();
            string sql = "SELECT count(distinct [uid]) FROM "
                + strDUTableName
                + " where udate = '" + date + "' and data_taskid = '" + inputTaskId + "' and data_return ='0' ";
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = Convert.ToInt32(Resources.CommonResource.Command_Timeout);
            rtn = (int)comm.ExecuteScalar();
            conn.Close();
            return rtn;
        }

        public long GetTaskIdCount(string date, string inputTaskId, string strDUTableName)
        {
            Int64 rtn = 0;

            SqlConnection conn = ConnectionOpen();
            string sql = "SELECT count(distinct [uid]) FROM "
                + strDUTableName
                + " where udate = '" + date + "' and data_taskid = '"+ inputTaskId + "'";
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = Convert.ToInt32(Resources.CommonResource.Command_Timeout);
            rtn = (int)comm.ExecuteScalar();
            conn.Close();
            return rtn;
        }

        public Int64 GetSecondNewCount(string date, string strSecondDay, string strDailyTableName, string strUserTableName)
        {
            LogHelper.writeDebugLog("GetSecondNewCount Start");
            Int64 rtn = 0;

            SqlConnection conn = ConnectionOpen();

            string sql = "SELECT DISTINCT gsd.uid FROM " + strDailyTableName + " gsd where gsd.udate = '"
                + strSecondDay 
                + "' and gsd.uid in (select uif.uid from "+ strUserTableName + " uif where Convert(varchar, uif.udate,120) like '" 
                + date 
                + "%') and uid like '{%'";

            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = Convert.ToInt32(Resources.CommonResource.Command_Timeout);

            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                rtn = rtn + 1;
            }
            conn.Close();
            LogHelper.writeDebugLog("GetSecondNewCount End");
            return rtn;
        }

        public Int64 GetThirdNewCount(string date, string strThirdDay, string strDailyTableName, string strUserTableName)
        {
            LogHelper.writeDebugLog("GetThirdNewCount Start");
            Int64 rtn = 0;

            SqlConnection conn = ConnectionOpen();

            string sql = "SELECT DISTINCT uid FROM "
                + strDailyTableName
                + " gsd where gsd.udate = '"
                + strThirdDay 
                + "' and gsd.uid in (select uif.uid from "
                + strUserTableName 
                + " uif where Convert(varchar, uif.udate,120) like '" 
                + date 
                + "%') and uid like '{%'";

            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = Convert.ToInt32(Resources.CommonResource.Command_Timeout);

            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                rtn = rtn + 1;
            }
            conn.Close();
            LogHelper.writeDebugLog("GetThirdNewCount End");
            return rtn;
        }
        
        public Int64 GetThreeNewCount(string date, string strSecondDay, string strThirdDay, string strDailyTableName, string strUserTableName)
        {
            LogHelper.writeDebugLog("GetThreeNewCount Start");
            Int64 rtn = 0;

            SqlConnection conn = ConnectionOpen();

            string sql = "SELECT DISTINCT gsd.uid FROM "
                + strDailyTableName
                + " gsd where (gsd.udate = '"
                + strSecondDay
                + "' or gsd.udate = '"
                + strThirdDay
                + "')  and gsd.uid in (select uif.uid from "
                + strUserTableName 
                + " uif where Convert(varchar, uif.udate,120) like '"
                + date
                + "%') and uid like '{%'";
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = Convert.ToInt32(Resources.CommonResource.Command_Timeout);

            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                rtn = rtn + 1;
            }
            conn.Close();
            LogHelper.writeDebugLog("GetThreeNewCount End");
            return rtn;
        }
        
        public Int64 GetTaskCount(string date, string strDataTableName)
        {
            Int64 rtn = 0;

            SqlConnection conn = ConnectionOpen();
            string sql = "SELECT count(1) FROM "
                + strDataTableName
                + " where Convert(varchar, udate,120) LIKE '" + date + "%'";
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = Convert.ToInt32(Resources.CommonResource.Command_Timeout);
            rtn = (int)comm.ExecuteScalar();
            conn.Close();
            return rtn;
        }

        public Int64 GetTaskDayCount(string date, string strDataTableName)
        {
            Int64 rtn = 0;

            SqlConnection conn = ConnectionOpen();

            string sql = "SELECT distinct uid FROM "
                + strDataTableName
                + " where Convert(varchar, udate,120) LIKE '" + date + "%'";

            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = Convert.ToInt32(Resources.CommonResource.Command_Timeout);

            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                rtn = rtn + 1;
            }
            conn.Close();
            return rtn;
        }

        public Int64 GetTaskResultCount(string date, string strDataTableName)
        {
            Int64 rtn = 0;

            SqlConnection conn = ConnectionOpen();

            string sql = "SELECT distinct uid FROM "
                + strDataTableName
                + " where Convert(varchar, udate,120) LIKE '" 
                + date 
                + "%' and data_parameter = 'task result'";

            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = Convert.ToInt32(Resources.CommonResource.Command_Timeout);

            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                rtn = rtn + 1;
            }
            conn.Close();
            return rtn;
        }

        
        public Int64 GetTaskResultReturnCount(string date, string strDataTableName)
        {
            Int64 rtn = 0;

            SqlConnection conn = ConnectionOpen();

            string sql = "SELECT distinct uid FROM "
                + strDataTableName
                + " where Convert(varchar, udate,120) LIKE '" 
                + date 
                + "%' and data_parameter = 'task result' and data_return = '0'";

            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = Convert.ToInt32(Resources.CommonResource.Command_Timeout);

            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                rtn = rtn + 1;
            }
            conn.Close();
            return rtn;
        }

        //public Int64 GetOldCount(string date)
        //{
        //    Int64 rtn = 0;

        //    SqlConnection conn = ConnectionOpen();
        //    string sql = "SELECT DISTINCT uid FROM GoSourceData where Convert(varchar, udate,120) LIKE '" + date + "%' and uid not like '{%' and uid <> '' group by uid";
        //    SqlCommand comm = new SqlCommand(sql, conn);
        //    comm.CommandTimeout = Convert.ToInt32(Resources.CommonResource.Command_Timeout);
        //    SqlDataReader reader = comm.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        rtn = rtn + 1;
        //    }
        //    conn.Close();
        //    return rtn;
        //}

        public long GetAfterWeekcount(string strInput, string strDUTableName)
        {
            LogHelper.writeDebugLog("GetAfterWeekcount Start");
            Int64 rtn = 0;
            DateTime startDT = Convert.ToDateTime(strInput).AddDays(7);
            DateTime endDT = Convert.ToDateTime(strInput).AddDays(13);

            SqlConnection conn = ConnectionOpen();
            string sql =
                "SELECT count(DISTINCT gsd.[uid]) FROM "
                + strDUTableName 
                + " gsd where gsd.udate between '" 
                + startDT.ToString("yyyy-MM-dd") 
                + "' and '" 
                + endDT.ToString("yyyy-MM-dd") 
                + "' and gsd.uid in (SELECT DISTINCT [uid] FROM " 
                + strDUTableName 
                + " where udate = '" 
                + strInput 
                + "')";

            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = Convert.ToInt32(Resources.CommonResource.Command_Timeout);
            rtn = (int)comm.ExecuteScalar();
            conn.Close();
            LogHelper.writeDebugLog("GetAfterWeekcount End");
            return rtn;
        }

        public Int64 GetEgg1UserCount(string date, string strUserTableName)
        {
            LogHelper.writeDebugLog("GetEgg1UserCount Start");
            Int64 rtn = 0;

            SqlConnection conn = ConnectionOpen();
            string sql = "SELECT count(DISTINCT uid) FROM "
                + strUserTableName
                + " where uid in (select uif.uid from UserInfo uif) and Convert(varchar, udate,120) LIKE '" + date + "%'";

            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = Convert.ToInt32(Resources.CommonResource.Command_Timeout);
            rtn = (int)comm.ExecuteScalar();
            conn.Close();
            LogHelper.writeDebugLog("GetEgg1UserCount End");
            return rtn;
        }

        public Int64 GetVCount(string strInput, string strDailyTableName, string v)
        {
            LogHelper.writeDebugLog("GetVCount Start");
            Int64 rtn = 0;

            SqlConnection conn = ConnectionOpen();
            string sql = "SELECT count(1) FROM "
                + strDailyTableName
                + " where udate = '" + strInput + "' and version = '" + v + "'";

            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = Convert.ToInt32(Resources.CommonResource.Command_Timeout);
            rtn = (int)comm.ExecuteScalar();

            conn.Close();
            LogHelper.writeDebugLog("GetVCount End");
            return rtn;
        }

        public long GetNotVCount(string strInput, string strDailyTableName, string v1, string v2)
        {
            LogHelper.writeDebugLog("GetNotVCount Start");
            Int64 rtn = 0;

            SqlConnection conn = ConnectionOpen();
            string sql = "SELECT count(1) FROM "
                + strDailyTableName
                + " where udate = '" + strInput + "' and version not in ('" + v1+"','"+ v2 + "')";

            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = Convert.ToInt32(Resources.CommonResource.Command_Timeout);
            rtn = (int)comm.ExecuteScalar();

            conn.Close();
            LogHelper.writeDebugLog("GetNotVCount End");
            return rtn;
        }

        public Int64 GetKillUserCount(string date, string strDailyTableName)
        {
            LogHelper.writeDebugLog("GetKillUserCount Start");
            Int64 rtn = 0;

            SqlConnection conn = ConnectionOpen();
            string sql = "SELECT count(1) FROM "
                + strDailyTableName
                + " where Convert(varchar, udate,120) LIKE '" + date + "' and [kill] <> ''";
            
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = Convert.ToInt32(Resources.CommonResource.Command_Timeout);
            rtn = (int)comm.ExecuteScalar();
            conn.Close();
            LogHelper.writeDebugLog("GetKillUserCount End");
            return rtn;
        }

        public int InsertDailyVisitUserStatisticsForTask(DailyVisitUserStatisticsData dvusd)
        {
            //LogHelper.writeInfoLog("InsertDailyVisitUserStatistics For Task Start");
            int strRtn = 0;
            try
            {
                DateTime dt = DateTime.Now;
                SqlConnection conn = ConnectionOpen();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "insert into DailyVisitUserStatistics(UType, UDate, TotalNumberOfDays, DayNumberOfUsers, TaskNumber, TaskNumberOfSuccess, createdate, updatedate) values (@UType,@UDate,@TotalNumberOfDays,@DayNumberOfUsers,@TaskNumber,@TaskNumberOfSuccess,@createdate,@updatedate)";
                    //清除上一次的参数
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@UType", dvusd.UType));
                    cmd.Parameters.Add(new SqlParameter("@UDate", dvusd.UDate));
                    cmd.Parameters.Add(new SqlParameter("@TotalNumberOfDays", dvusd.TotalNumberOfDays));
                    cmd.Parameters.Add(new SqlParameter("@DayNumberOfUsers", dvusd.DayNumberOfUsers));
                    cmd.Parameters.Add(new SqlParameter("@TaskNumber", dvusd.TaskNumber));
                    cmd.Parameters.Add(new SqlParameter("@TaskNumberOfSuccess", dvusd.TaskNumberOfSuccess));
                    cmd.Parameters.Add(new SqlParameter("@createdate", dt));
                    cmd.Parameters.Add(new SqlParameter("@updatedate", dt));
                    strRtn = cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                //LogHelper.writeErrorLog(ex);
                return strRtn;
            }

            //LogHelper.writeInfoLog("strRtn = " + strRtn);
            //LogHelper.writeInfoLog("InsertDailyVisitUserStatistics For Task End");
            return strRtn;
        }

        public int InsertDailyVisitUserStatisticsForGo20(DailyVisitUserStatisticsData dvusd)
        {
            LogHelper.writeDebugLog("InsertDailyVisitUserStatisticsForGo20 Start");
            //LogHelper.writeInfoLog("InsertDailyVisitUserStatistics For Go20 Start");
            int strRtn = 0;
            try
            {
                DateTime dt = DateTime.Now;
                SqlConnection conn = ConnectionOpen();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "insert into DailyVisitUserStatistics"+
                        "(UType, UDate, TotalNumberOfDays, DayNumberOfUsers, NumberOfDaysNewUsers, NextDayNumberOfNewUsers, ThirdDayNumberOfNewUsers, ThreeDayNumberOfNewUsers, NumberOfNewUsersEgg1, DayNumberOfUsersKillInstallation, Extension1,Extension2,createdate, updatedate) " +
                        " values (@UType,@UDate,@TotalNumberOfDays,@DayNumberOfUsers,@NumberOfDaysNewUsers,@NextDayNumberOfNewUsers,@ThirdDayNumberOfNewUsers,@ThreeDayNumberOfNewUsers,@NumberOfNewUsersEgg1,@DayNumberOfUsersKillInstallation,@Extension1,@Extension2,@createdate,@updatedate)";
                    //清除上一次的参数
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@UType", dvusd.UType));
                    cmd.Parameters.Add(new SqlParameter("@UDate", dvusd.UDate));
                    cmd.Parameters.Add(new SqlParameter("@TotalNumberOfDays", dvusd.TotalNumberOfDays));
                    cmd.Parameters.Add(new SqlParameter("@DayNumberOfUsers", dvusd.DayNumberOfUsers));
                    cmd.Parameters.Add(new SqlParameter("@NumberOfDaysNewUsers", dvusd.NumberOfDaysNewUsers));
                    cmd.Parameters.Add(new SqlParameter("@NextDayNumberOfNewUsers", dvusd.NextDayNumberOfNewUsers));
                    cmd.Parameters.Add(new SqlParameter("@ThirdDayNumberOfNewUsers", dvusd.ThirdDayNumberOfNewUsers));
                    cmd.Parameters.Add(new SqlParameter("@ThreeDayNumberOfNewUsers", dvusd.ThreeDayNumberOfNewUsers));
                    cmd.Parameters.Add(new SqlParameter("@NumberOfNewUsersEgg1", dvusd.NumberOfNewUsersEgg1));
                    cmd.Parameters.Add(new SqlParameter("@DayNumberOfUsersKillInstallation", dvusd.DayNumberOfUsersKillInstallation));
                    cmd.Parameters.Add(new SqlParameter("@Extension1", dvusd.Extension1));
                    cmd.Parameters.Add(new SqlParameter("@Extension2", dvusd.Extension2));
                    cmd.Parameters.Add(new SqlParameter("@createdate", dt));
                    cmd.Parameters.Add(new SqlParameter("@updatedate", dt));
                    strRtn = cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                //LogHelper.writeErrorLog(ex);
                return strRtn;
            }

            LogHelper.writeDebugLog("InsertDailyVisitUserStatisticsForGo20 End");
            return strRtn;
        }

        public int InsertDailyVisitUserStatistics(DailyVisitUserStatisticsData dvusd)
        {
            //LogHelper.writeInfoLog("InsertDailyVisitUserStatistics Start");
            int strRtn = 0;
            try
            {
                DateTime dt = DateTime.Now;
                SqlConnection conn = ConnectionOpen();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "insert into DailyVisitUserStatistics" +
                        "(UType, UDate, TotalNumberOfDays, DayNumberOfUsers, NumberOfDaysNewUsers, NextDayNumberOfNewUsers, ThirdDayNumberOfNewUsers, ThreeDayNumberOfNewUsers, createdate, updatedate) " +
                        " values (@UType,@UDate,@TotalNumberOfDays,@DayNumberOfUsers,@NumberOfDaysNewUsers,@NextDayNumberOfNewUsers,@ThirdDayNumberOfNewUsers,@ThreeDayNumberOfNewUsers,@createdate,@updatedate)";
                    //清除上一次的参数
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@UType", dvusd.UType));
                    cmd.Parameters.Add(new SqlParameter("@UDate", dvusd.UDate));
                    cmd.Parameters.Add(new SqlParameter("@TotalNumberOfDays", dvusd.TotalNumberOfDays));
                    cmd.Parameters.Add(new SqlParameter("@DayNumberOfUsers", dvusd.DayNumberOfUsers));
                    cmd.Parameters.Add(new SqlParameter("@NumberOfDaysNewUsers", dvusd.NumberOfDaysNewUsers));
                    cmd.Parameters.Add(new SqlParameter("@NextDayNumberOfNewUsers", dvusd.NextDayNumberOfNewUsers));
                    cmd.Parameters.Add(new SqlParameter("@ThirdDayNumberOfNewUsers", dvusd.ThirdDayNumberOfNewUsers));
                    cmd.Parameters.Add(new SqlParameter("@ThreeDayNumberOfNewUsers", dvusd.ThreeDayNumberOfNewUsers));
                    cmd.Parameters.Add(new SqlParameter("@createdate", dt));
                    cmd.Parameters.Add(new SqlParameter("@updatedate", dt));
                    strRtn = cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                //LogHelper.writeErrorLog(ex);
                return strRtn;
            }

            //LogHelper.writeInfoLog("strRtn = " + strRtn);
            //LogHelper.writeInfoLog("InsertDailyVisitUserStatistics End");
            return strRtn;
        }

        /// <summary>
        /// 更新表(DailyUser)中数据
        /// </summary>
        public int UpdateDailyVisitUserStatistics(DailyVisitUserStatisticsData dvusd)
        {
            LogHelper.writeDebugLog("UpdateDailyVisitUserStatistics Start");

            int strRtn = 0;
            DateTime dt = DateTime.Now;
            try
            {
                SqlConnection conn = ConnectionOpen();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandTimeout = Convert.ToInt32(Resources.CommonResource.Command_Timeout);
                    if ("task".Equals(dvusd.UType))
                    {
                        cmd.CommandText = "UPDATE DailyVisitUserStatistics"
                            + " SET "
                            + "[TotalNumberOfDays] = " + dvusd.TotalNumberOfDays
                            + ",[DayNumberOfUsers] = " + dvusd.DayNumberOfUsers
                            + ",[TaskNumber] = " + dvusd.TaskNumber
                            + ",[TaskNumberOfSuccess] = " + dvusd.TaskNumberOfSuccess
                            + ",[updatedate] = '" + dt
                            + "' where keys = "
                            + dvusd.keys;
                    }
                    else if ("go2.0".Equals(dvusd.UType))
                    {
                        cmd.CommandText = "UPDATE DailyVisitUserStatistics"
                            + " SET "
                            + "[TotalNumberOfDays] = " + dvusd.TotalNumberOfDays
                            + ",[DayNumberOfUsers] = " + dvusd.DayNumberOfUsers
                            + ",[NumberOfDaysNewUsers] = " + dvusd.NumberOfDaysNewUsers
                            + ",[NextDayNumberOfNewUsers] = " + dvusd.NextDayNumberOfNewUsers
                            + ",[ThirdDayNumberOfNewUsers] = " + dvusd.ThirdDayNumberOfNewUsers
                            + ",[ThreeDayNumberOfNewUsers] = " + dvusd.ThreeDayNumberOfNewUsers
                            + ",[NumberOfNewUsersEgg1] = " + dvusd.NumberOfNewUsersEgg1
                            + ",[DayNumberOfUsersKillInstallation] = " + dvusd.DayNumberOfUsersKillInstallation
                            + ",[Extension1] = " + dvusd.Extension1
                            + ",[Extension2] = " + dvusd.Extension2
                            + ",[updatedate] = '" + dt
                            + "' where keys = "
                            + dvusd.keys;
                    }
                    else
                    {
                        cmd.CommandText = "UPDATE DailyVisitUserStatistics"
                            + " SET "
                            + "[TotalNumberOfDays] = " + dvusd.TotalNumberOfDays
                            + ",[DayNumberOfUsers] = " + dvusd.DayNumberOfUsers
                            + ",[NumberOfDaysNewUsers] = " + dvusd.NumberOfDaysNewUsers
                            + ",[NextDayNumberOfNewUsers] = " + dvusd.NextDayNumberOfNewUsers
                            + ",[ThirdDayNumberOfNewUsers] = " + dvusd.ThirdDayNumberOfNewUsers
                            + ",[ThreeDayNumberOfNewUsers] = " + dvusd.ThreeDayNumberOfNewUsers
                            + ",[updatedate] = '" + dt
                            + "' where keys = "
                            + dvusd.keys;
                    }
                    strRtn = cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            catch (SqlException se)
            {
                //LogHelper.writeErrorLog(se);
            }
            catch (Exception ex)
            {
                //LogHelper.writeErrorLog(ex);
            }

            LogHelper.writeDebugLog("UpdateDailyVisitUserStatistics End");

            return strRtn;
        }
        
        internal long GetLossCount(string strUITableName)
        {
            LogHelper.writeDebugLog("GetLossCount Start");

            int rtn = 0;

            try
            {
                SqlConnection conn = ConnectionOpen();

                string sql = "SELECT count(1) FROM "
                    + strUITableName
                    + " where DATEDIFF(day, convert(datetime, sadate, 110), convert(datetime, eadate, 110)) < 7";

                SqlCommand comm = new SqlCommand(sql, conn);
                comm.CommandTimeout = Convert.ToInt32(CommonResource.Command_Timeout);
                rtn = (int)comm.ExecuteScalar();
                conn.Close();
            }
            catch (SqlException se)
            {
                LogHelper.writeErrorLog(se);
            }
            catch (Exception ex)
            {
                LogHelper.writeErrorLog(ex);
            }
            LogHelper.writeDebugLog("GetLossCount End");

            return rtn;
        }

        internal int GetGo20UserInfoCount(string date, string strUITableName)
        {
            LogHelper.writeInfoLog("GetGo20UserInfoCount Start");
            int rtn = 0;

            SqlConnection conn = ConnectionOpen();
            string sql = "SELECT count(1) FROM "
                + strUITableName
                + " where udate < '" + date + "'";

            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = Convert.ToInt32(CommonResource.Command_Timeout); 
            rtn = (int)comm.ExecuteScalar();

            conn.Close();
            LogHelper.writeInfoLog("GetGo20UserInfoCount End");
            return rtn;
        }
        
        internal int GetWeekDAUCount(string weeks, string weeke, string strDUTableName)
        {
            int rtn = 0;

            SqlConnection conn = ConnectionOpen();
            string sql = "SELECT count(1) FROM "
                + strDUTableName
                + " where udate between '" + weeks + "' and '" + weeke + "'";

            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = Convert.ToInt32(Resources.CommonResource.Command_Timeout); 
            rtn = (int)comm.ExecuteScalar();
            conn.Close();
            return rtn;
        }

        internal int GetWeekDAUDisCount(string weeks, string weeke, string strDUTableName)
        {
            int rtn = 0;

            SqlConnection conn = ConnectionOpen();
            string sql = "SELECT count(distinct [uid]) FROM "
                + strDUTableName
                + " where udate between '" + weeks + "' and '" + weeke + "'";

            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = Convert.ToInt32(Resources.CommonResource.Command_Timeout);
            rtn = (int)comm.ExecuteScalar();
            conn.Close();
            return rtn;
        }

        internal int GetNextWeekDAUDisCount(string pweeks, string pweeke, string weeks, string weeke, string strDUTableName)
        {
            int rtn = 0;

            SqlConnection conn = ConnectionOpen();
            string sql = "SELECT count(distinct [uid]) FROM "
                + strDUTableName
                + " gsd where gsd.udate between '" + pweeks + "' and '" + pweeke + "' and gsd.uid in (SELECT distinct [uid] FROM "
                + strDUTableName
                + " where udate between '" + weeks + "' and '" + weeke + "')";
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = Convert.ToInt32(Resources.CommonResource.Command_Timeout);
            rtn = (int)comm.ExecuteScalar();
            conn.Close();
            return rtn;
        }
        
        internal long GetDnuThreeCount(string strInput, string strSecondDay, string strThirdDay, string strDUTableName, string strUITableName, string channelValue)
        {
            Int64 rtn = 0;

            SqlConnection conn = ConnectionOpen();

            string sql = "SELECT count(DISTINCT gsd.[uid]) FROM " + strDUTableName + " gsd where (gsd.udate = '"
                + strSecondDay
                + "' or gsd.udate = '"
                + strThirdDay
                + "') and gsd.uid in (select uif.uid from " + strDUTableName + " uif where uif.udate = '"
                + strInput
                + "' and channel like '"
                + channelValue
                + "%' and uid in (select uid from "
                + strUITableName
                + " where sadate = '"
                + strInput
                + "'))";

            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = Convert.ToInt32(Resources.CommonResource.Command_Timeout);
            int intUserInfoCount = (int)comm.ExecuteScalar();
            if (intUserInfoCount > 0)
            {
                rtn = intUserInfoCount;
            }
            conn.Close();
            return rtn;
        }

        internal long GetDnuThirdCount(string strInput, string strThirdDay, string strDUTableName, string strUITableName, string channelValue)
        {
            Int64 rtn = 0;

            SqlConnection conn = ConnectionOpen();

            string sql = "SELECT count(DISTINCT gsd.[uid]) FROM " + strDUTableName + " gsd where gsd.udate = '"
                + strThirdDay
                + "' and gsd.uid in (select uif.uid from " + strDUTableName + " uif where uif.udate = '"
                + strInput
                + "' and channel like '"
                + channelValue
                + "%' and uid in (select uid from "
                + strUITableName
                + " where sadate = '"
                + strInput
                + "'))";

            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = Convert.ToInt32(Resources.CommonResource.Command_Timeout);
            int intUserInfoCount = (int)comm.ExecuteScalar();
            if (intUserInfoCount > 0)
            {
                rtn = intUserInfoCount;
            }
            conn.Close();
            return rtn;
        }

        internal long GetDnuSecondCount(string strInput, string strSecondDay, string strDUTableName, string strUITableName, string strChannelValue)
        {
            Int64 rtn = 0;

            SqlConnection conn = ConnectionOpen();

            string sql = "SELECT count(DISTINCT gsd.[uid]) FROM " + strDUTableName + " gsd where gsd.udate = '"
                + strSecondDay
                + "' and gsd.uid in (select uif.uid from " + strDUTableName + " uif where uif.udate = '"
                + strInput
                + "' and channel like '"
                + strChannelValue
                + "%' and uid in (select uid from "
                + strUITableName
                + " where sadate = '"
                + strInput
                + "'))";

            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = Convert.ToInt32(Resources.CommonResource.Command_Timeout);
            int intUserInfoCount = (int)comm.ExecuteScalar();
            if (intUserInfoCount > 0)
            {
                rtn = intUserInfoCount;
            }
            conn.Close();
            return rtn;
        }

        internal long GetDnuChannelCount(string strInput, string strDUTableName, string strUITableName, string channelValue)
        {
            Int64 rtn = 0;

            SqlConnection conn = ConnectionOpen();
            string sql = "SELECT count(1) FROM "
                + strDUTableName
                + " where udate = '"
                + strInput
                + "' and channel like '"
                + channelValue
                + "%' and uid in (select [uid] from "
                + strUITableName
                + " where sadate = '"
                + strInput
                + "' )";
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = Convert.ToInt32(Resources.CommonResource.Command_Timeout);
            int intUserInfoCount = (int)comm.ExecuteScalar();
            if (intUserInfoCount > 0)
            {
                rtn = intUserInfoCount;
            }
            conn.Close();
            return rtn;
        }

        internal long GetDauChannelCount(string strInput, string strDUTableName, string channelValue)
        {
            Int64 rtn = 0;

            SqlConnection conn = ConnectionOpen();
            string sql = "SELECT count(1) FROM "
                + strDUTableName
                + " where udate = '"
                + strInput
                + "' and channel like '"
                + channelValue
                + "%'";
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = Convert.ToInt32(Resources.CommonResource.Command_Timeout);
            int intUserInfoCount = (int)comm.ExecuteScalar();
            if (intUserInfoCount > 0)
            {
                rtn = intUserInfoCount;
            }
            conn.Close();
            return rtn;
        }





        /// <summary>
        /// 从数据库中获取当前时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetDateTimeFromSQL()
        {
            SqlConnection conn = ConnectionOpen();
            string sql = "select getdate()";
            SqlCommand comm = new SqlCommand(sql, conn);
            SqlDataReader reader = comm.ExecuteReader();
            DateTime dt;
            if (reader.Read())
            {
                dt = (DateTime)reader[0];
                conn.Close();
                return dt;
            }
            conn.Close();
            return DateTime.MinValue;
        }
    }
}