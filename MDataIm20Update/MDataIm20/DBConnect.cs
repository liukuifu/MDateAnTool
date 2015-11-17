﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDataIm20Update
{
    public class DBConnect
    {
        //private string connectionString =
        //"Server = 10.1.7.126;" +
        //"Database = MDataAn;" +
        //"User ID = sa;" +
        //"Password = 12345678;";

        private static string connectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
        private static int intTimeout = Convert.ToInt32(ConfigurationSettings.AppSettings["DBCommandTimeout"]);

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

        /// <summary>
        /// 向表(Table)中插入数据
        /// </summary>
        public void InsertTable(DataTable table, string strTableName)
        {
            LogHelper.writeInfoLog("InsertTable Start");
            try
            {
                SqlConnection conn = ConnectionOpen();
                SqlBulkCopy bulkCopy = new SqlBulkCopy(conn);

                bulkCopy.DestinationTableName = strTableName;//设置数据库中对象的表名
                if ("Go20TaskSD".Equals(strTableName))
                {
                    //设置数据表table和数据库中表的列对应关系
                    bulkCopy.ColumnMappings.Add("keys", "keys");
                    bulkCopy.ColumnMappings.Add("udate", "udate");
                    bulkCopy.ColumnMappings.Add("appid", "appid");
                    bulkCopy.ColumnMappings.Add("channel", "channel");
                    bulkCopy.ColumnMappings.Add("event", "event");
                    bulkCopy.ColumnMappings.Add("eggid", "eggid");
                    bulkCopy.ColumnMappings.Add("version", "version");
                    bulkCopy.ColumnMappings.Add("locale", "locale");
                    bulkCopy.ColumnMappings.Add("os", "os");
                    bulkCopy.ColumnMappings.Add("uid", "uid");
                    bulkCopy.ColumnMappings.Add("amd64", "amd64");
                    bulkCopy.ColumnMappings.Add("data_parameter", "data_parameter");
                    bulkCopy.ColumnMappings.Add("data_return", "data_return");
                    bulkCopy.ColumnMappings.Add("data_taskid", "data_taskid");
                    bulkCopy.ColumnMappings.Add("createdate", "createdate");
                    bulkCopy.ColumnMappings.Add("updatedate", "updatedate");
                }
                else
                {
                    //设置数据表table和数据库中表的列对应关系
                    bulkCopy.ColumnMappings.Add("keys", "keys");
                    bulkCopy.ColumnMappings.Add("udate", "udate");
                    bulkCopy.ColumnMappings.Add("id", "id");
                    bulkCopy.ColumnMappings.Add("appid", "appid");
                    bulkCopy.ColumnMappings.Add("channel", "channel");
                    bulkCopy.ColumnMappings.Add("event", "event");
                    bulkCopy.ColumnMappings.Add("eggid", "eggid");
                    bulkCopy.ColumnMappings.Add("locale", "locale");
                    bulkCopy.ColumnMappings.Add("os", "os");
                    bulkCopy.ColumnMappings.Add("uid", "uid");
                    bulkCopy.ColumnMappings.Add("version", "version");
                    bulkCopy.ColumnMappings.Add("amd64", "amd64");
                    bulkCopy.ColumnMappings.Add("antivirus_guid_1", "antivirus_guid_1");
                    bulkCopy.ColumnMappings.Add("antivirus_name_1", "antivirus_name_1");
                    bulkCopy.ColumnMappings.Add("antivirus_guid_2", "antivirus_guid_2");
                    bulkCopy.ColumnMappings.Add("antivirus_name_2", "antivirus_name_2");
                    bulkCopy.ColumnMappings.Add("antivirus_guid_3", "antivirus_guid_3");
                    bulkCopy.ColumnMappings.Add("antivirus_name_3", "antivirus_name_3");
                    bulkCopy.ColumnMappings.Add("antivirus_guid_4", "antivirus_guid_4");
                    bulkCopy.ColumnMappings.Add("antivirus_name_4", "antivirus_name_4");
                    bulkCopy.ColumnMappings.Add("antivirus_guid_5", "antivirus_guid_5");
                    bulkCopy.ColumnMappings.Add("antivirus_name_5", "antivirus_name_5");
                    bulkCopy.ColumnMappings.Add("browser", "browser");
                    bulkCopy.ColumnMappings.Add("bversion", "bversion");
                    bulkCopy.ColumnMappings.Add("dotnet_1", "dotnet_1");
                    bulkCopy.ColumnMappings.Add("dotnet_2", "dotnet_2");
                    bulkCopy.ColumnMappings.Add("dotnet_3", "dotnet_3");
                    bulkCopy.ColumnMappings.Add("dotnet_4", "dotnet_4");
                    bulkCopy.ColumnMappings.Add("dotnet_5", "dotnet_5");
                    bulkCopy.ColumnMappings.Add("dx", "dx");
                    bulkCopy.ColumnMappings.Add("base", "base");
                    bulkCopy.ColumnMappings.Add("bios", "bios");
                    bulkCopy.ColumnMappings.Add("disk", "disk");
                    bulkCopy.ColumnMappings.Add("network", "network");
                    bulkCopy.ColumnMappings.Add("ie", "ie");
                    bulkCopy.ColumnMappings.Add("kill", "kill");
                    bulkCopy.ColumnMappings.Add("createdate", "createdate");
                    bulkCopy.ColumnMappings.Add("updatedate", "updatedate");
                }

                bulkCopy.WriteToServer(table);//将数据表table复制到数据库中
                conn.Close();
            }
            catch (Exception ex)
            {
                LogHelper.writeErrorLog(ex);
                LogHelper.writeErrorLog("<<<<<<<<<<<<<<<<<<      sssss           >>>>>>>>>>>>>>>>>");
                int ri = 1;
                foreach (DataRow dr in table.Rows)
                {
                    LogHelper.writeErrorLog("<<<<<<<<<<<<<<<<<<      " + ri + "           >>>>>>>>>>>>>>>>>");

                    for (int m = 0; m < dr.ItemArray.Length; m++)
                    //foreach (string t in dr.ItemArray)
                    {
                        LogHelper.writeErrorLog(m + " : " + dr.ItemArray[m]);
                    }
                    ri = ri + 1;
                }
                LogHelper.writeErrorLog("<<<<<<<<<<<<<<<<<<      eeeee           >>>>>>>>>>>>>>>>>");
            }
            LogHelper.writeInfoLog("InsertTable End");
        }

        /// <summary>
        /// 向表(TaskInfo)中插入数据
        /// </summary>
        public int InsertTaskInfo(string strInputDate, string strTableName, string strDUTableName)
        {
            LogHelper.writeInfoLog("InsertTaskInfo Start");

            int strRtn = 0;

            try
            {
                SqlConnection conn = ConnectionOpen();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandTimeout = intTimeout;
                    
                    //当日新规则访问用户
                    cmd.CommandText = "insert "
                        + strDUTableName
                        + " SELECT DISTINCT '"
                        + strInputDate
                        + "', [uid], data_taskid, data_return, data_parameter FROM "
                        + strTableName
                        + " where Convert(varchar, udate,120) like '"
                        + strInputDate +
                        "%' and [uid] <> '' and [uid] is not null group by [uid], data_taskid, data_return, data_parameter order by [uid]";

                    strRtn = cmd.ExecuteNonQuery();
                }
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

            LogHelper.writeInfoLog("strRtn = " + strRtn);
            LogHelper.writeInfoLog("InsertTaskInfo End");
            return strRtn;
        }

        /// <summary>
        /// 向表(DailyUser)中插入数据
        /// </summary>
        public int InsertDailyUser(string inputDate, string strTableName, string strDUTableName)
        {
            LogHelper.writeInfoLog("InsertDailyUser Start");

            int strRtn = 0;

            try
            {
                SqlConnection conn = ConnectionOpen();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandTimeout = intTimeout;
                    //当日新规则访问用户
                    cmd.CommandText = "insert "
                        + strDUTableName
                        + " SELECT DISTINCT uid,'"
                        + inputDate
                        + "',null,null FROM "
                        + strTableName
                        + " where Convert(varchar, udate,120) like '"
                        + inputDate + "%' group by uid,[kill] order by uid";

                    LogHelper.writeInfoLog("cmd.CommandText =  " + cmd.CommandText);

                    strRtn = cmd.ExecuteNonQuery();                    
                }
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

            LogHelper.writeInfoLog("strRtn = " + strRtn);
            LogHelper.writeInfoLog("InsertDailyUser End");
            return strRtn;
        }

        /// <summary>
        /// 更新表(DailyUser)中数据
        /// </summary>
        public int UpdateDailyUser(string inputDate, string strTableName, string strDUTableName)
        {
            LogHelper.writeInfoLog("UpdateDailyUser Start");

            int strRtn = 0;

            try
            {
                SqlConnection conn = ConnectionOpen();

                // 更新KILL
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandTimeout = intTimeout;
                    //当日新规则访问用户
                    cmd.CommandText = "UPDATE "
                        + strDUTableName
                        + " SET [kill] = g2sd.[kill] from "
                        + strDUTableName
                        + " g2du , "
                        + strTableName
                        + " g2sd "
                        + " where g2du.uid = g2sd.uid and g2du.udate = '"
                        + inputDate + "' and Convert(varchar, g2sd.udate,120) like '"
                        + inputDate + "%' and g2sd.[kill] <> ''";
                    strRtn = cmd.ExecuteNonQuery();
                    Console.WriteLine("DailyUser Update kill Count = " + strRtn);
                }

                // 更新version
                using (SqlCommand cmd2 = conn.CreateCommand())
                {
                    cmd2.CommandTimeout = intTimeout;
                    //当日新规则访问用户
                    cmd2.CommandText = "UPDATE "
                        + strDUTableName
                        + " SET version = g2sd.version from "
                        + strDUTableName
                        + " g2du , "
                        + strTableName
                        + " g2sd "
                        + " where g2du.uid = g2sd.uid and g2du.udate = '"
                        + inputDate + "' and Convert(varchar, g2sd.udate,120) like '"
                        + inputDate + "%' and g2sd.version = '1000.0.0.107'";

                    strRtn = cmd2.ExecuteNonQuery();
                    Console.WriteLine("DailyUser Update version 1000.0.0.107 Count = " + strRtn);
                }

                using (SqlCommand cmd3 = conn.CreateCommand())
                {
                    cmd3.CommandTimeout = intTimeout;
                    //当日新规则访问用户
                    cmd3.CommandText = "UPDATE "
                        + strDUTableName
                        + " SET version = g2sd.version from "
                        + strDUTableName
                        + " g2du , "
                        + strTableName
                        + " g2sd "
                        + " where g2du.uid = g2sd.uid and g2du.udate = '"
                        + inputDate + "' and Convert(varchar, g2sd.udate,120) like '"
                        + inputDate + "%' and g2sd.version = '1000.0.0.112'";

                    strRtn = cmd3.ExecuteNonQuery();
                    Console.WriteLine("DailyUser Update version 1000.0.0.112 Count = " + strRtn);
                }

                using (SqlCommand cmd4 = conn.CreateCommand())
                {
                    cmd4.CommandTimeout = intTimeout;
                    //当日新规则访问用户
                    cmd4.CommandText = "UPDATE "
                        + strDUTableName
                        + " SET version = g2sd.version from "
                        + strDUTableName
                        + " g2du , "
                        + strTableName
                        + " g2sd "
                        + " where g2du.uid = g2sd.uid and g2du.udate = '"
                        + inputDate + "' and Convert(varchar, g2sd.udate,120) like '"
                        + inputDate 
                        + "%' and g2sd.uid in (SELECT uid FROM "
                        + strDUTableName
                        + " where udate = '" + inputDate + "' and version is null)";
                    
                    strRtn = cmd4.ExecuteNonQuery();
                    Console.WriteLine("DailyUser Update version 1000.0.0.107 1000.0.0.112 以外 Count = " + strRtn);
                }


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

            LogHelper.writeInfoLog("strRtn = " + strRtn);
            LogHelper.writeInfoLog("UpdateDailyUser End");
            return strRtn;
        }

        /// <summary>
        /// 向表(UserInfo)中插入数据
        /// </summary>
        public int InsertUserInfo(string inputDate, string strDUTableName, string strUITableName)
        {
            LogHelper.writeInfoLog("InsertUserInfo Start");

            int strRtn = 0;

            try
            {
                SqlConnection conn = ConnectionOpen();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandTimeout = intTimeout;
                    cmd.CommandText = "insert " + strUITableName + " SELECT DISTINCT [uid],'"
                        + inputDate
                        + " 00:00:01.000' FROM " + strDUTableName + " g2du where g2du.udate = '"
                        + inputDate + "' and g2du.uid not in (select uid from " + strUITableName + ")";

                    strRtn = cmd.ExecuteNonQuery();
                }
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

            LogHelper.writeInfoLog("strRtn = " + strRtn);
            LogHelper.writeInfoLog("InsertUserInfo End");
            return strRtn;
        }


        /// <summary>
        /// 取得(SourceData)单日件数
        /// </summary>
        public int GetSourceDataCount(string date, string strDataTableName)
        {
            LogHelper.writeInfoLog("GetSourceDataCount Start");

            int strRtn = 0;

            try
            {
                SqlConnection conn = ConnectionOpen();
                string sql = "SELECT count(1) FROM "
                    + strDataTableName
                    + " where Convert(varchar, udate,120) LIKE '" + date + "%'";
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.CommandTimeout = intTimeout;
                strRtn = (int)comm.ExecuteScalar();
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

            LogHelper.writeInfoLog("strRtn = " + strRtn);
            LogHelper.writeInfoLog("GetSourceDataCount End");
            return strRtn;
        }

        public int InsertDailyVisitUserStatistics(string strDBType, string strInputDate, int intSourceDataCount, int intInsertDU, int intInsertUI)
        {
            LogHelper.writeInfoLog("InsertDailyVisitUserStatistics Start");
            int strRtn = 0;
            try
            {
                DateTime dt = DateTime.Now;
                SqlConnection conn = ConnectionOpen();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "insert into DailyVisitUserStatistics(UType, UDate, TotalNumberOfDays, DayNumberOfUsers, NumberOfDaysNewUsers, createdate, updatedate) values (@UType,@UDate,@TotalNumberOfDays,@DayNumberOfUsers,@NumberOfDaysNewUsers,@createdate,@updatedate)";
                    //清除上一次的参数
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@UType", strDBType));
                    cmd.Parameters.Add(new SqlParameter("@UDate", strInputDate));
                    cmd.Parameters.Add(new SqlParameter("@TotalNumberOfDays", intSourceDataCount));
                    cmd.Parameters.Add(new SqlParameter("@DayNumberOfUsers", intInsertDU));
                    cmd.Parameters.Add(new SqlParameter("@NumberOfDaysNewUsers", intInsertUI));
                    cmd.Parameters.Add(new SqlParameter("@createdate", dt));
                    cmd.Parameters.Add(new SqlParameter("@updatedate", dt));
                    strRtn = cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                LogHelper.writeErrorLog(ex);
                return strRtn;
            }

            LogHelper.writeInfoLog("strRtn = " + strRtn);
            LogHelper.writeInfoLog("InsertDailyVisitUserStatistics End");
            return strRtn;
        }


        public int GetTaskDayCount(string date, string strDataTableName)
        {
            int rtn = 0;

            SqlConnection conn = ConnectionOpen();

            string sql = "SELECT count(distinct uid) FROM "
                + strDataTableName
                + " where Convert(varchar, udate,120) LIKE '" + date + "%'";
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = intTimeout;
            rtn = (int)comm.ExecuteScalar();
            conn.Close();

            return rtn;
        }

        public int GetTaskResultCount(string date, string strDataTableName)
        {
            int rtn = 0;

            SqlConnection conn = ConnectionOpen();

            string sql = "SELECT count(distinct uid) FROM "
                + strDataTableName
                + " where Convert(varchar, udate,120) LIKE '"
                + date
                + "%' and data_parameter = 'task result'";
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = intTimeout;
            rtn = (int)comm.ExecuteScalar();
            conn.Close();
            return rtn;
        }


        public int GetTaskResultReturnCount(string date, string strDataTableName)
        {
            int rtn = 0;

            SqlConnection conn = ConnectionOpen();

            string sql = "SELECT count(distinct uid) FROM "
                + strDataTableName
                + " where Convert(varchar, udate,120) LIKE '"
                + date
                + "%' and data_parameter = 'task result' and data_return = '0'";
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = intTimeout;
            rtn = (int)comm.ExecuteScalar();
            conn.Close();
            return rtn;
        }

        public int InsertDailyVisitUserStatistics(string strDBType, 
            string strInputDate, 
            int intSourceDataCount, 
            int intdaycount, 
            int inttaskcount,
            int intreturncount)
        {
            LogHelper.writeInfoLog("InsertDailyVisitUserStatistics For Task Start");
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
                    cmd.Parameters.Add(new SqlParameter("@UType", strDBType));
                    cmd.Parameters.Add(new SqlParameter("@UDate", strInputDate));
                    cmd.Parameters.Add(new SqlParameter("@TotalNumberOfDays", intSourceDataCount));
                    cmd.Parameters.Add(new SqlParameter("@DayNumberOfUsers", intdaycount));
                    cmd.Parameters.Add(new SqlParameter("@TaskNumber", inttaskcount));
                    cmd.Parameters.Add(new SqlParameter("@TaskNumberOfSuccess", intreturncount));
                    cmd.Parameters.Add(new SqlParameter("@createdate", dt));
                    cmd.Parameters.Add(new SqlParameter("@updatedate", dt));
                    strRtn = cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                LogHelper.writeErrorLog(ex);
                return strRtn;
            }

            LogHelper.writeInfoLog("strRtn = " + strRtn);
            LogHelper.writeInfoLog("InsertDailyVisitUserStatistics For Task End");
            return strRtn;
        }

    }
}
