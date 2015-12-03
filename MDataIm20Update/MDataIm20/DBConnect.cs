using System;
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

        public void UpdateGo20UserInfo(string strInputDate, string strTableName, string strUITableName)
        {
            try
            {
                string qssd = "SELECT [udate],[locale],[uid],[amd64],[antivirus_guid_1],[antivirus_name_1],[antivirus_guid_2],"
                    +"[antivirus_name_2],[antivirus_guid_3],[antivirus_name_3],[antivirus_guid_4],[antivirus_name_4],[antivirus_guid_5],"
                    +"[antivirus_name_5],[browser],[bversion],[dotnet_1],[dotnet_2],[dotnet_3],[dotnet_4],[dotnet_5],[dx],[base],[bios],"
                    + "[disk],[network],[ie] FROM "
                    + strTableName + "  a where a.udate = (select max(udate) from "
                    + strTableName + " where uid = a.uid) order by a.[uid] desc";
                DataTable dtsd = new DataTable();
                //1.SqlConnection
                SqlConnection connsd = ConnectionOpen();
                //2.SqlCommand
                using (SqlCommand cmdsd = new SqlCommand(qssd, connsd))
                {
                    cmdsd.CommandTimeout = intTimeout;

                    //3.SqlDataAdapter
                    using (SqlDataAdapter dasd = new SqlDataAdapter(cmdsd))
                    {
                        //4.建立DataSet類別或DataTable類別
                        //使用Fill方法
                        //===========================================
                        dtsd.BeginLoadData();
                        dasd.Fill(dtsd);
                        dtsd.EndLoadData();

                    }
                }
                connsd.Close();
                DataTable dt = new DataTable();
                string qs = "SELECT * FROM " + strUITableName + " where Convert(varchar,udate,120) LIKE '" + strInputDate + "%' and [locale] is null";
                //1.SqlConnection
                SqlConnection conn = ConnectionOpen();
                //2.SqlCommand
                using (SqlCommand cmd = new SqlCommand(qs, conn))
                {
                    cmd.CommandTimeout = intTimeout;

                    //3.SqlDataAdapter
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("UPDATE "+ strUITableName);
                        sb.Append(" SET [locale] = @locale");
                        sb.Append(",[amd64] = @amd64");
                        sb.Append(",[antivirus_guid_1] = @antivirus_guid_1");
                        sb.Append(",[antivirus_name_1] = @antivirus_name_1");
                        sb.Append(",[antivirus_guid_2] = @antivirus_guid_2");
                        sb.Append(",[antivirus_name_2] = @antivirus_name_2");
                        sb.Append(",[antivirus_guid_3] = @antivirus_guid_3");
                        sb.Append(",[antivirus_name_3] = @antivirus_name_3");
                        sb.Append(",[antivirus_guid_4] = @antivirus_guid_4");
                        sb.Append(",[antivirus_name_4] = @antivirus_name_4");
                        sb.Append(",[antivirus_guid_5] = @antivirus_guid_5");
                        sb.Append(",[antivirus_name_5] = @antivirus_name_5");
                        sb.Append(",[browser] = @browser  ");
                        sb.Append(",[dotnet_1] = @dotnet_1");
                        sb.Append(",[dotnet_2] = @dotnet_2");
                        sb.Append(",[dotnet_3] = @dotnet_3");
                        sb.Append(",[dotnet_4] = @dotnet_4");
                        sb.Append(",[dotnet_5] = @dotnet_5");
                        sb.Append(",[base] = @base");
                        sb.Append(",[bios] = @bios");
                        sb.Append(",[disk] = @disk");
                        sb.Append(",[network] = @network");
                        sb.Append(",[ie] = @ie");
                        sb.Append(" WHERE [uid] = @uid");

                        da.UpdateCommand = new SqlCommand(sb.ToString(), conn);

                        da.UpdateCommand.Parameters.Add("@locale", SqlDbType.Int, 5, "locale");
                        da.UpdateCommand.Parameters.Add("@amd64", SqlDbType.Int, 1, "amd64");
                        da.UpdateCommand.Parameters.Add("@antivirus_guid_1", SqlDbType.NVarChar, 50, "antivirus_guid_1");
                        da.UpdateCommand.Parameters.Add("@antivirus_name_1", SqlDbType.NVarChar, 100, "antivirus_name_1");
                        da.UpdateCommand.Parameters.Add("@antivirus_guid_2", SqlDbType.NVarChar, 50, "antivirus_guid_2");
                        da.UpdateCommand.Parameters.Add("@antivirus_name_2", SqlDbType.NVarChar, 100, "antivirus_name_2");
                        da.UpdateCommand.Parameters.Add("@antivirus_guid_3", SqlDbType.NVarChar, 50, "antivirus_guid_3");
                        da.UpdateCommand.Parameters.Add("@antivirus_name_3", SqlDbType.NVarChar, 100, "antivirus_name_3");
                        da.UpdateCommand.Parameters.Add("@antivirus_guid_4", SqlDbType.NVarChar, 50, "antivirus_guid_4");
                        da.UpdateCommand.Parameters.Add("@antivirus_name_4", SqlDbType.NVarChar, 100, "antivirus_name_4");
                        da.UpdateCommand.Parameters.Add("@antivirus_guid_5", SqlDbType.NVarChar, 50, "antivirus_guid_5");
                        da.UpdateCommand.Parameters.Add("@antivirus_name_5", SqlDbType.NVarChar, 100, "antivirus_name_5");
                        da.UpdateCommand.Parameters.Add("@browser", SqlDbType.NVarChar, 100, "browser");
                        da.UpdateCommand.Parameters.Add("@dotnet_1", SqlDbType.NVarChar, 50, "dotnet_1");
                        da.UpdateCommand.Parameters.Add("@dotnet_2", SqlDbType.NVarChar, 50, "dotnet_2");
                        da.UpdateCommand.Parameters.Add("@dotnet_3", SqlDbType.NVarChar, 50, "dotnet_3");
                        da.UpdateCommand.Parameters.Add("@dotnet_4", SqlDbType.NVarChar, 50, "dotnet_4");
                        da.UpdateCommand.Parameters.Add("@dotnet_5", SqlDbType.NVarChar, 50, "dotnet_5");
                        da.UpdateCommand.Parameters.Add("@base", SqlDbType.NVarChar, 2000, "base");
                        da.UpdateCommand.Parameters.Add("@bios", SqlDbType.NVarChar, 2000, "bios");
                        da.UpdateCommand.Parameters.Add("@disk", SqlDbType.NVarChar, 2000, "disk");
                        da.UpdateCommand.Parameters.Add("@network", SqlDbType.NVarChar, 2000, "network");
                        da.UpdateCommand.Parameters.Add("@ie", SqlDbType.NVarChar, 50, "ie");
                        da.UpdateCommand.Parameters.Add("@uid", SqlDbType.NVarChar, 50, "uid");

                        //SqlParameter prams_ID = da.UpdateCommand.Parameters.Add("@uid", SqlDbType.NVarChar, 50, "uid");
                        //prams_ID.SourceColumn = "[uid]";
                        //prams_ID.SourceVersion = DataRowVersion.Original;
                        //4.建立DataSet類別或DataTable類別
                        //使用Fill方法
                        //===========================================

                        DataSet ds = new DataSet();
                        da.Fill(ds, "guit");

                        //dt.BeginLoadData();
                        //da.Fill(dt);
                        //dt.EndLoadData();

                        //if (dt != null && dt.Rows.Count > 0)
                        if (ds != null && ds.Tables["guit"] != null && ds.Tables["guit"].Rows.Count > 0)
                        {
                            //LogHelper.writeErrorLog("dt.Rows.Count = " + dt.Rows.Count);

                            //for (int i = 0; i < dt.Rows.Count; i++)
                            for (int i = 0; i <= ds.Tables["guit"].Rows.Count - 1; i++)
                            {
                                //DataRow dr = dt.Rows[0];
                                DataRow[] foundRows = dtsd.Select("uid='" + ds.Tables["guit"].Rows[i]["uid"] + "'", "udate desc");
                                if (foundRows != null && foundRows.Length > 0)
                                {
                                    ds.Tables["guit"].Rows[i]["locale"] = foundRows[0]["locale"];
                                    ds.Tables["guit"].Rows[i]["amd64"] = foundRows[0]["amd64"];
                                    ds.Tables["guit"].Rows[i]["antivirus_guid_1"] = foundRows[0]["antivirus_guid_1"];
                                    ds.Tables["guit"].Rows[i]["antivirus_name_1"] = foundRows[0]["antivirus_name_1"];
                                    ds.Tables["guit"].Rows[i]["antivirus_guid_2"] = foundRows[0]["antivirus_guid_2"];
                                    ds.Tables["guit"].Rows[i]["antivirus_name_2"] = foundRows[0]["antivirus_name_2"];
                                    ds.Tables["guit"].Rows[i]["antivirus_guid_3"] = foundRows[0]["antivirus_guid_3"];
                                    ds.Tables["guit"].Rows[i]["antivirus_name_3"] = foundRows[0]["antivirus_name_3"];
                                    ds.Tables["guit"].Rows[i]["antivirus_guid_4"] = foundRows[0]["antivirus_guid_4"];
                                    ds.Tables["guit"].Rows[i]["antivirus_name_4"] = foundRows[0]["antivirus_name_4"];
                                    ds.Tables["guit"].Rows[i]["antivirus_guid_5"] = foundRows[0]["antivirus_guid_5"];
                                    ds.Tables["guit"].Rows[i]["antivirus_name_5"] = foundRows[0]["antivirus_name_5"];
                                    ds.Tables["guit"].Rows[i]["browser"] = foundRows[0]["browser"];
                                    ds.Tables["guit"].Rows[i]["dotnet_1"] = foundRows[0]["dotnet_1"];
                                    ds.Tables["guit"].Rows[i]["dotnet_2"] = foundRows[0]["dotnet_2"];
                                    ds.Tables["guit"].Rows[i]["dotnet_3"] = foundRows[0]["dotnet_3"];
                                    ds.Tables["guit"].Rows[i]["dotnet_4"] = foundRows[0]["dotnet_4"];
                                    ds.Tables["guit"].Rows[i]["dotnet_5"] = foundRows[0]["dotnet_5"];
                                    ds.Tables["guit"].Rows[i]["base"] = foundRows[0]["base"];
                                    ds.Tables["guit"].Rows[i]["bios"] = foundRows[0]["bios"];
                                    ds.Tables["guit"].Rows[i]["disk"] = foundRows[0]["disk"];
                                    ds.Tables["guit"].Rows[i]["network"] = foundRows[0]["network"];
                                    ds.Tables["guit"].Rows[i]["ie"] = foundRows[0]["ie"];
                                }
                            }
                            da.Update(ds, "guit");
                        }
                        ds.Clear();
                        ds.Dispose();
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                LogHelper.writeErrorLog(ex);
            }
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
                    if ("Go20DailyUser".Equals(strDUTableName))
                    {
                        //当日新规则访问用户
                        cmd.CommandText = "insert "
                        + strDUTableName
                        + " SELECT DISTINCT uid,'"
                        + inputDate
                        + "',null,null,null FROM "
                        + strTableName
                        + " where Convert(varchar, udate,120) like '"
                        + inputDate + "%' group by uid,[kill] order by uid";

                    }
                    else
                    {
                        //当日新规则访问用户
                        cmd.CommandText = "insert "
                        + strDUTableName
                        + " SELECT DISTINCT uid,'"
                        + inputDate
                        + "',null,null FROM "
                        + strTableName
                        + " where Convert(varchar, udate,120) like '"
                        + inputDate + "%' group by uid,[kill] order by uid";


                    }
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
                if ("Go20SourceData".Equals(strTableName))
                {
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
                }
                // 更新channel
                using (SqlCommand cmd5 = conn.CreateCommand())
                {
                    cmd5.CommandTimeout = intTimeout;
                    //当日新规则访问用户
                    cmd5.CommandText = "UPDATE "
                        + strDUTableName
                        + " SET [channel] = g2sd.[channel] from "
                        + strDUTableName
                        + " g2du , "
                        + strTableName
                        + " g2sd "
                        + " where g2du.uid = g2sd.uid and g2du.udate = '"
                        + inputDate + "' and Convert(varchar, g2sd.udate,120) like '"
                        + inputDate + "%' and g2sd.[channel] <> ''";
                    strRtn = cmd5.ExecuteNonQuery();
                    Console.WriteLine("DailyUser Update channel Count = " + strRtn);
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

        internal int UpdateSadateForUserInfo20(string strImportDate, string strUITableName, string strDUTableName)
        {
            LogHelper.writeInfoLog("UpdateSadateForUserInfo20 Start");

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
                        + strUITableName
                        + " SET [sadate] = (select min(udate) from "
                        + strDUTableName
                        + " where uid = "
                        + strUITableName
                        + ".uid) "
                        + " where Convert(varchar, " + strUITableName + ".udate,120) like '"
                        + strImportDate + "%'";

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

            LogHelper.writeInfoLog("UpdateSadateForUserInfo20 End");
            return strRtn;
        }

        internal int UpdateEadateForUserInfo20(string strImportDate, string strUITableName, string strDUTableName)
        {
            LogHelper.writeInfoLog("UpdateEadateForUserInfo20 Start");

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
                        + strUITableName
                        + " SET [eadate] = (select max(udate) from "
                        + strDUTableName
                        + " where uid = "
                        + strUITableName
                        + ".uid) ";

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

            LogHelper.writeInfoLog("UpdateEadateForUserInfo20 End");
            return strRtn;
        }

        internal int GetLossCount(string strUITableName)
        {
            LogHelper.writeInfoLog("UpdateEadateForUserInfo20 Start");

            int rtn = 0;

            try
            {
                SqlConnection conn = ConnectionOpen();

                string sql = "SELECT count(1) FROM "
                    + strUITableName
                    + " where DATEDIFF(day, convert(datetime, sadate, 110), convert(datetime, eadate, 110)) < 7";

                SqlCommand comm = new SqlCommand(sql, conn);
                comm.CommandTimeout = intTimeout;
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
            LogHelper.writeInfoLog("UpdateEadateForUserInfo20 End");

            return rtn;
        }

        internal int InsertDailyVisitUserStatisticsForLoss(string strDBType,
            string strImportDate,
            int intSourceDataCount,
            int intInsertDU,
            int intInsertUI,
            int intLossCount)
        {
            LogHelper.writeInfoLog("InsertDailyVisitUserStatisticsForLoss Start");
            int strRtn = 0;
            try
            {
                DateTime dt = DateTime.Now;
                SqlConnection conn = ConnectionOpen();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "insert into DailyVisitUserStatistics(UType, UDate, TotalNumberOfDays, DayNumberOfUsers, NumberOfDaysNewUsers, Extension1,createdate, updatedate) values (@UType,@UDate,@TotalNumberOfDays,@DayNumberOfUsers,@NumberOfDaysNewUsers,@Extension1,@createdate,@updatedate)";
                    //清除上一次的参数
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@UType", strDBType));
                    cmd.Parameters.Add(new SqlParameter("@UDate", strImportDate));
                    cmd.Parameters.Add(new SqlParameter("@TotalNumberOfDays", intSourceDataCount));
                    cmd.Parameters.Add(new SqlParameter("@DayNumberOfUsers", intInsertDU));
                    cmd.Parameters.Add(new SqlParameter("@NumberOfDaysNewUsers", intInsertUI));
                    cmd.Parameters.Add(new SqlParameter("@Extension1", intLossCount));
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
            LogHelper.writeInfoLog("InsertDailyVisitUserStatisticsForLoss End");
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
                    if ("Go20UserInfo".Equals(strUITableName)
                        || "Killer20UserInfo".Equals(strUITableName))
                    {
                        cmd.CommandText = "insert " + strUITableName + " SELECT DISTINCT [uid],'"
                            + inputDate
                            + " 00:00:01.000',null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null FROM " + strDUTableName + " g2du where g2du.udate = '"
                            + inputDate + "' and g2du.uid not in (select uid from " + strUITableName + ")";
                    }
                    else
                    {
                        cmd.CommandText = "insert " + strUITableName + " SELECT DISTINCT [uid],'"
                            + inputDate
                            + " 00:00:01.000' FROM " + strDUTableName + " g2du where g2du.udate = '"
                            + inputDate + "' and g2du.uid not in (select uid from " + strUITableName + ")";

                    }
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
