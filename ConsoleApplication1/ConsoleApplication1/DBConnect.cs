using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class DBConnect
    {
        //private string connectionString =
        //"Server = 10.1.7.126;" +
        //"Database = MDataAn;" +
        //"User ID = sa;" +
        //"Password = 12345678;";

        private static string connectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;

        //private string connectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;

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
        /// 向表(Table)中插入一条数据
        /// </summary>
        public void Insert(string value1, string value2, string value3, DateTime dateTime)
        {
            SqlConnection conn = ConnectionOpen();
            string sql =
                "insert into Table(row1, row2, row3, DateTime) values ('" +
                value1 + "', '" + value2 + "', '" + value3 + "', '" + dateTime + "')";
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.ExecuteReader();

            conn.Close();
        }

        /// <summary>
        /// 向表(Table)中插入一条数据
        /// </summary>
        public void InsertList(List<MData> list)
        {
            LogHelper.writeInfoLog("InsertList Start");
            try
            {
                SqlConnection conn = ConnectionOpen();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    foreach (MData mdItem in list)
                    {
                        try
                        {
                            cmd.CommandText = "insert into SourceData(date, id, appid, channel, event, langid, locale, os, uid, version, antivirusKey, hardwareinfo) values (@Date,@Id,@Appid,@Channel,@Event,@Langid,@Locale,@os,@Uid,@Version,@antivirusKey,@Hardwareinfo)";
                            //清除上一次的参数
                            cmd.Parameters.Clear();
                            cmd.Parameters.Add(new SqlParameter("@Date", mdItem.Date));
                            cmd.Parameters.Add(new SqlParameter("@Id", mdItem.Id));
                            cmd.Parameters.Add(new SqlParameter("@Appid", mdItem.Appid));
                            cmd.Parameters.Add(new SqlParameter("@Channel", mdItem.Channel));
                            cmd.Parameters.Add(new SqlParameter("@Event", mdItem.Event == null ? "" : mdItem.Event));
                            cmd.Parameters.Add(new SqlParameter("@Langid", mdItem.Langid == null ? "" : mdItem.Langid));
                            cmd.Parameters.Add(new SqlParameter("@Locale", mdItem.Locale));
                            cmd.Parameters.Add(new SqlParameter("@os", mdItem.OS == null ? "" : mdItem.OS));
                            cmd.Parameters.Add(new SqlParameter("@Uid", mdItem.Uid == null ? "" : mdItem.Uid));
                            cmd.Parameters.Add(new SqlParameter("@Version", mdItem.Version == null ? "" : mdItem.Version));
                            cmd.Parameters.Add(new SqlParameter("@antivirusKey", mdItem.antivirusKey == null ? "" : mdItem.antivirusKey));
                            //cmd.Parameters.Add(new SqlParameter("@Hardwareinfo", mdItem.Hardwareinfo == null ? "" : mdItem.Hardwareinfo));
                            cmd.ExecuteNonQuery();
                        }
                        catch (SqlException se)
                        {
                            LogHelper.writeErrorLog(se);
                            continue;
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                LogHelper.writeErrorLog(ex);
            }
            LogHelper.writeInfoLog("InsertList End");
        }

        /// <summary>
        /// 向表(Table)中插入一条数据
        /// </summary>
        public String InsertAntivirusList(List<AntivirusItem> list)
        {
            LogHelper.writeInfoLog("InsertAntivirusList Start");

            string strRtn = string.Empty;

            try
            {
                SqlConnection conn = ConnectionOpen();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //string sql = string.Empty;
                    foreach (AntivirusItem atItem in list)
                    {
                        cmd.CommandText = "insert into SourceDataantivirus(company, guid, name, version) values (@company, @guid, @name, @version) select @@IDENTITY as rtnkey;";
                        //清除上一次的参数
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@company", atItem.company == null ? "" : atItem.company));
                        cmd.Parameters.Add(new SqlParameter("@guid", atItem.guid == null ? "" : atItem.guid));
                        cmd.Parameters.Add(new SqlParameter("@name", atItem.name == null ? "" : atItem.name));
                        cmd.Parameters.Add(new SqlParameter("@version", atItem.version == null ? "" : atItem.version));
                        //cmd.ExecuteNonQuery();
                                   
                        Int64 Id = Convert.ToInt64(cmd.ExecuteScalar());
                        strRtn = strRtn + Id.ToString() + "|";
                    }
                }
                conn.Close();
            }
            catch (SqlException se)
            {
                foreach(AntivirusItem item in list)
                {
                    LogHelper.writeErrorLog(item.company + " | " + item.guid + " | " + item.name + " | " + item.version);
                }
                LogHelper.writeErrorLog(se);
            }
            catch (Exception ex)
            {
                LogHelper.writeErrorLog(ex);
            }

            if(!string.IsNullOrEmpty(strRtn))
            {
                strRtn = strRtn.Remove(strRtn.Length-1);
            }

            LogHelper.writeInfoLog("strRtn = " + strRtn);
            LogHelper.writeInfoLog("InsertAntivirusList End");
            return strRtn;
        }

        /// <summary>
        /// 向表(Table)中插入多条数据
        /// </summary>
        public void InsertTable(DataTable table, string strTableName)
        {
            LogHelper.writeInfoLog("InsertTable Start");
            try
            {
                SqlConnection conn = ConnectionOpen();
                SqlBulkCopy bulkCopy = new SqlBulkCopy(conn);

                bulkCopy.DestinationTableName = strTableName;//设置数据库中对象的表名

                //设置数据表table和数据库中表的列对应关系
                bulkCopy.ColumnMappings.Add("udate", "udate");
                bulkCopy.ColumnMappings.Add("id", "id");
                bulkCopy.ColumnMappings.Add("appid", "appid");
                bulkCopy.ColumnMappings.Add("channel", "channel");
                bulkCopy.ColumnMappings.Add("event", "event");
                bulkCopy.ColumnMappings.Add("langid", "langid");
                bulkCopy.ColumnMappings.Add("locale", "locale");
                bulkCopy.ColumnMappings.Add("os", "os");
                bulkCopy.ColumnMappings.Add("uid", "uid");
                bulkCopy.ColumnMappings.Add("version", "version");
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


                //bulkCopy.ColumnMappings.Add("hardwareinfo", "hardwareinfo");
                
                bulkCopy.ColumnMappings.Add("createdate", "createdate");
                bulkCopy.ColumnMappings.Add("updatedate", "updatedate");
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
                    cmd.CommandTimeout = 240;
                    //当日新规则访问用户
                    cmd.CommandText = "insert " 
                        + strDUTableName 
                        + " SELECT DISTINCT[uid],'"
                        + inputDate
                        + "' FROM " 
                        + strTableName 
                        + " where Convert(varchar, udate,120) like '"
                        + inputDate + "%' and uid like '{%' group by uid order by uid";

                    ////清除上一次的参数
                    //cmd.Parameters.Clear();
                    //cmd.Parameters.Add(new SqlParameter("@company", atItem.company == null ? "" : atItem.company));
                    //cmd.Parameters.Add(new SqlParameter("@guid", atItem.guid == null ? "" : atItem.guid));
                    //cmd.Parameters.Add(new SqlParameter("@name", atItem.name == null ? "" : atItem.name));
                    //cmd.Parameters.Add(new SqlParameter("@version", atItem.version == null ? "" : atItem.version));
                    strRtn = cmd.ExecuteNonQuery();

                    //Int64 Id = Convert.ToInt64(cmd.ExecuteScalar());
                    //strRtn = strRtn + Id.ToString() + "|";
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
        /// 向表(UserInfo)中插入数据
        /// </summary>
        public int InsertUserInfo(string inputDate, string strTableName, string strUITableName)
        {
            LogHelper.writeInfoLog("InsertUserInfo Start");

            int strRtn = 0;

            try
            {
                SqlConnection conn = ConnectionOpen();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandTimeout = 240;
                    cmd.CommandText = "insert " + strUITableName + " SELECT DISTINCT[uid],'"
                        + inputDate
                        + " 00:00:01.000' FROM " + strTableName + " where Convert(varchar, udate,120) like '"
                        + inputDate + "%' and uid not in (select uid from " + strUITableName + ") and uid like '{%'";
                    ////清除上一次的参数
                    //cmd.Parameters.Clear();
                    //cmd.Parameters.Add(new SqlParameter("@company", atItem.company == null ? "" : atItem.company));
                    //cmd.Parameters.Add(new SqlParameter("@guid", atItem.guid == null ? "" : atItem.guid));
                    //cmd.Parameters.Add(new SqlParameter("@name", atItem.name == null ? "" : atItem.name));
                    //cmd.Parameters.Add(new SqlParameter("@version", atItem.version == null ? "" : atItem.version));
                    strRtn = cmd.ExecuteNonQuery();

                    //Int64 Id = Convert.ToInt64(cmd.ExecuteScalar());
                    //strRtn = strRtn + Id.ToString() + "|";
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
                comm.CommandTimeout = 240;
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
    }
}
