using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UserInfoUpdate
{
    public class DBConnect
    {
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

        public void UpdateGo20UserInfo(string strInputDate, string strTableName, string strUITableName)
        {
            try
            {
                //string qssd = "SELECT [keys]      ,[udate]      ,[id]      ,[appid]      ,[channel]      ,[event]      ,[eggid]      ,[locale]      ,[os]      ,[uid]      ,[version]      ,[amd64]      ,[antivirus_guid_1]      ,[antivirus_name_1]      ,[antivirus_guid_2]      ,[antivirus_name_2]      ,[antivirus_guid_3]      ,[antivirus_name_3]      ,[antivirus_guid_4]      ,[antivirus_name_4]      ,[antivirus_guid_5]      ,[antivirus_name_5]      ,[browser]      ,[bversion]      ,[dotnet_1]      ,[dotnet_2]      ,[dotnet_3]      ,[dotnet_4]      ,[dotnet_5]      ,[dx]      ,[base]      ,[bios]      ,[disk]      ,[network]      ,[ie]      ,[kill]      ,[createdate]      ,[updatedate] FROM [Go20SourceData] a where a.udate = (select max(udate) from [Go20SourceData] where uid = a.uid) order by a.[uid] desc";

                string qssd = "SELECT [udate],[locale],[uid],[amd64],[antivirus_guid_1],[antivirus_name_1],[antivirus_guid_2],"
                    + "[antivirus_name_2],[antivirus_guid_3],[antivirus_name_3],[antivirus_guid_4],[antivirus_name_4],[antivirus_guid_5],"
                    + "[antivirus_name_5],[browser],[bversion],[dotnet_1],[dotnet_2],[dotnet_3],[dotnet_4],[dotnet_5],[dx],[base],[bios],"
                    + "[disk],[network],[ie] FROM "
                    + strTableName + "  a where a.udate = (select max(udate) from "
                    + strTableName + " where uid = a.uid and Convert(varchar,udate,120) LIKE '" + strInputDate + "%') order by a.[uid] desc";
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
                        //da.UpdateBatchSize = 500;
                        StringBuilder sb = new StringBuilder();
                        sb.Append("UPDATE " + strUITableName);
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
                        //da.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;
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
                                    //ds.Tables["guit"].Rows[i]["antivirus_guid_5"] = "";
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
                                //SqlCommand cmd1 = conn.CreateCommand();
                                //cmd1.CommandTimeout = intTimeout;
                                //cmd1.CommandText = "SELECT top(1) *  FROM [Go20SourceData-bak1105] where uid = '" + dr["uid"] + "' order by udate";
                                //SqlDataReader reader = cmd1.ExecuteReader();
                                //while (reader.Read())
                                //{
                                //    dr["locale"] = reader["locale"];
                                //    dr["amd64"] = reader["amd64"];
                                //    dr["antivirus_guid_1"] = reader["antivirus_guid_1"];
                                //    dr["antivirus_name_1"] = reader["antivirus_name_1"];
                                //    dr["antivirus_guid_2"] = reader["antivirus_guid_2"];
                                //    dr["antivirus_name_2"] = reader["antivirus_name_2"];
                                //    dr["antivirus_guid_3"] = reader["antivirus_guid_3"];
                                //    dr["antivirus_name_3"] = reader["antivirus_name_3"];
                                //    dr["antivirus_guid_4"] = reader["antivirus_guid_4"];
                                //    dr["antivirus_name_4"] = reader["antivirus_name_4"];
                                //    dr["antivirus_guid_5"] = reader["antivirus_guid_5"];
                                //    dr["antivirus_name_5"] = reader["antivirus_name_5"];
                                //    dr["browser"] = reader["browser"];
                                //    dr["dotnet_1"] = reader["dotnet_1"];
                                //    dr["dotnet_2"] = reader["dotnet_2"];
                                //    dr["dotnet_3"] = reader["dotnet_3"];
                                //    dr["dotnet_4"] = reader["dotnet_4"];
                                //    dr["dotnet_5"] = reader["dotnet_5"];
                                //    dr["base"] = reader["base"];
                                //    dr["bios"] = reader["bios"];
                                //    dr["disk"] = reader["disk"];
                                //    dr["network"] = reader["network"];
                                //    dr["ie"] = reader["ie"];
                                //}
                                //reader.Close();
                                //cmd1.Dispose();
                            }
                            //da.Update(dt);
                            da.Update(ds, "guit");
                        }
                        ds.Clear();
                        ds.Dispose();
                    }
                    ////3.SqlDataAdapter
                    //using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    //{
                    //    //4.建立DataSet類別或DataTable類別
                    //    //使用Fill方法
                    //    //===========================================
                    //    dt.BeginLoadData();
                    //    da.Fill(dt);
                    //    dt.EndLoadData();

                    //    if (dt != null && dt.Rows.Count > 0)
                    //    {
                    //        LogHelper.writeErrorLog("dt.Rows.Count = " + dt.Rows.Count);

                    //        for (int i = 0; i < dt.Rows.Count; i++)
                    //        {
                    //            DataRow dr = dt.Rows[0];
                    //            using (SqlCommand cmd1 = conn.CreateCommand())
                    //            {
                    //                cmd1.CommandTimeout = intTimeout;
                    //                cmd1.CommandText = "SELECT top(1) *  FROM [Go20SourceData-bak1105] where uid = '" + dr["uid"] + "' order by udate";
                    //                SqlDataReader reader = cmd1.ExecuteReader();
                    //                while (reader.Read())
                    //                {

                    //                }
                    //            }
                    //        }
                    //        da.Update(dt);
                    //    }
                    //}
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                LogHelper.writeErrorLog(ex);
            }
        }

        internal void UpdateGo30UserInfo(string strInputDate, string strTableName, string strUITableName)
        {
            DateTime dtNow = DateTime.Now;
            string strJson = string.Empty;
            string strMd5 = string.Empty;
            try
            {
                MData30Data md = new MData30Data();
                //string qssd = "SELECT [keys]      ,[udate]      ,[id]      ,[appid]      ,[channel]      ,[event]      ,[eggid]      ,[locale]      ,[os]      ,[uid]      ,[version]      ,[amd64]      ,[antivirus_guid_1]      ,[antivirus_name_1]      ,[antivirus_guid_2]      ,[antivirus_name_2]      ,[antivirus_guid_3]      ,[antivirus_name_3]      ,[antivirus_guid_4]      ,[antivirus_name_4]      ,[antivirus_guid_5]      ,[antivirus_name_5]      ,[browser]      ,[bversion]      ,[dotnet_1]      ,[dotnet_2]      ,[dotnet_3]      ,[dotnet_4]      ,[dotnet_5]      ,[dx]      ,[base]      ,[bios]      ,[disk]      ,[network]      ,[ie]      ,[kill]      ,[createdate]      ,[updatedate] FROM [Go20SourceData] a where a.udate = (select max(udate) from [Go20SourceData] where uid = a.uid) order by a.[uid] desc";

                string qssd = "SELECT [keys],[udate],[channel],[uid],[sid],[hid],[sysid],[vid],[vm],[eggid]"
                    + ",[version],[workversion],[os],[amd64],[locale],[event],[dx],[ie],[kill],[data],[createdate],[updatedate] FROM "
                    + strTableName + "  a where a.udate = (select max(udate) from "
                    + strTableName + " where uid = a.uid and Convert(varchar,udate,120) LIKE '" + strInputDate + "%') order by a.[uid] desc";
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
                        sb.Append("UPDATE " + strUITableName);
                        sb.Append(" SET [locale] = @locale");
                        sb.Append(",[os] = @os");
                        sb.Append(",[sid] = @sid");
                        sb.Append(",[hid] = @hid");
                        sb.Append(",[sysid] = @sysid");
                        sb.Append(",[vid] = @vid");
                        sb.Append(",[vm] = @vm");
                        sb.Append(",[workversion] = @workversion");
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
                        sb.Append(",[base] = @base");
                        sb.Append(",[bios] = @bios");
                        sb.Append(",[disk] = @disk");
                        sb.Append(",[network] = @network");
                        sb.Append(",[dx] = @dx");
                        sb.Append(",[dotnet_1] = @dotnet_1");
                        sb.Append(",[dotnet_2] = @dotnet_2");
                        sb.Append(",[dotnet_3] = @dotnet_3");
                        sb.Append(",[dotnet_4] = @dotnet_4");
                        sb.Append(",[dotnet_5] = @dotnet_5");
                        sb.Append(",[browser] = @browser");
                        sb.Append(",[bversion] = @bversion");
                        sb.Append(",[ie] = @ie");
                        sb.Append(",[kill] = @kill");
                        sb.Append(",[software_chrome_hver_1] = @software_chrome_hver_1");
                        sb.Append(",[software_chrome_hver_2] = @software_chrome_hver_2");
                        sb.Append(",[software_chrome_hver_3] = @software_chrome_hver_3");
                        sb.Append(",[software_chrome_hver_4] = @software_chrome_hver_4");
                        sb.Append(",[software_chrome_hver_5] = @software_chrome_hver_5");
                        sb.Append(",[software_chrome_ever_1] = @software_chrome_ever_1");
                        sb.Append(",[software_chrome_ever_2] = @software_chrome_ever_2");
                        sb.Append(",[software_chrome_ever_3] = @software_chrome_ever_3");
                        sb.Append(",[software_chrome_ever_4] = @software_chrome_ever_4");
                        sb.Append(",[software_chrome_ever_5] = @software_chrome_ever_5");
                        sb.Append(",[software_gupdate_hver_1] = @software_gupdate_hver_1");
                        sb.Append(",[software_gupdate_hver_2] = @software_gupdate_hver_2");
                        sb.Append(",[software_gupdate_hver_3] = @software_gupdate_hver_3");
                        sb.Append(",[software_gupdate_hver_4] = @software_gupdate_hver_4");
                        sb.Append(",[software_gupdate_hver_5] = @software_gupdate_hver_5");
                        sb.Append(",[software_gupdate_ever_1] = @software_gupdate_ever_1");
                        sb.Append(",[software_gupdate_ever_2] = @software_gupdate_ever_2");
                        sb.Append(",[software_gupdate_ever_3] = @software_gupdate_ever_3");
                        sb.Append(",[software_gupdate_ever_4] = @software_gupdate_ever_4");
                        sb.Append(",[software_gupdate_ever_5] = @software_gupdate_ever_5");
                        sb.Append(",[md5] = @md5");
                        sb.Append(",[updatedate] = @updatedate");
                        sb.Append(" WHERE [uid] = @uid");

                        da.UpdateCommand = new SqlCommand(sb.ToString(), conn);
                        
                        da.UpdateCommand.Parameters.Add("@locale", SqlDbType.Int, 5, "locale");
                        da.UpdateCommand.Parameters.Add("@os", SqlDbType.NVarChar, 50, "os");
                        da.UpdateCommand.Parameters.Add("@sid", SqlDbType.NVarChar, 100, "sid");
                        da.UpdateCommand.Parameters.Add("@hid", SqlDbType.NVarChar, 100, "hid");
                        da.UpdateCommand.Parameters.Add("@sysid", SqlDbType.NVarChar, 50, "sysid");
                        da.UpdateCommand.Parameters.Add("@vid", SqlDbType.NVarChar, 50, "vid");
                        da.UpdateCommand.Parameters.Add("@vm", SqlDbType.NVarChar, 50, "vm");
                        da.UpdateCommand.Parameters.Add("@workversion", SqlDbType.NVarChar, 50, "workversion");
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
                        da.UpdateCommand.Parameters.Add("@base", SqlDbType.NVarChar, 2000, "base");
                        da.UpdateCommand.Parameters.Add("@bios", SqlDbType.NVarChar, 2000, "bios");
                        da.UpdateCommand.Parameters.Add("@disk", SqlDbType.NVarChar, 2000, "disk");
                        da.UpdateCommand.Parameters.Add("@network", SqlDbType.NVarChar, 2000, "network");
                        da.UpdateCommand.Parameters.Add("@dx", SqlDbType.NVarChar, 50, "dx");
                        da.UpdateCommand.Parameters.Add("@dotnet_1", SqlDbType.NVarChar, 50, "dotnet_1");
                        da.UpdateCommand.Parameters.Add("@dotnet_2", SqlDbType.NVarChar, 50, "dotnet_2");
                        da.UpdateCommand.Parameters.Add("@dotnet_3", SqlDbType.NVarChar, 50, "dotnet_3");
                        da.UpdateCommand.Parameters.Add("@dotnet_4", SqlDbType.NVarChar, 50, "dotnet_4");
                        da.UpdateCommand.Parameters.Add("@dotnet_5", SqlDbType.NVarChar, 50, "dotnet_5");
                        da.UpdateCommand.Parameters.Add("@browser", SqlDbType.NVarChar, 50, "browser");
                        da.UpdateCommand.Parameters.Add("@bversion", SqlDbType.NVarChar, 100, "bversion");
                        da.UpdateCommand.Parameters.Add("@ie", SqlDbType.NVarChar, 50, "ie");
                        da.UpdateCommand.Parameters.Add("@kill", SqlDbType.NVarChar, 50, "kill");
                        da.UpdateCommand.Parameters.Add("@software_chrome_hver_1", SqlDbType.NVarChar, 50, "software_chrome_hver_1");
                        da.UpdateCommand.Parameters.Add("@software_chrome_hver_2", SqlDbType.NVarChar, 50, "software_chrome_hver_2");
                        da.UpdateCommand.Parameters.Add("@software_chrome_hver_3", SqlDbType.NVarChar, 50, "software_chrome_hver_3");
                        da.UpdateCommand.Parameters.Add("@software_chrome_hver_4", SqlDbType.NVarChar, 50, "software_chrome_hver_4");
                        da.UpdateCommand.Parameters.Add("@software_chrome_hver_5", SqlDbType.NVarChar, 50, "software_chrome_hver_5");
                        da.UpdateCommand.Parameters.Add("@software_chrome_ever_1", SqlDbType.NVarChar, 50, "software_chrome_ever_1");
                        da.UpdateCommand.Parameters.Add("@software_chrome_ever_2", SqlDbType.NVarChar, 50, "software_chrome_ever_2");
                        da.UpdateCommand.Parameters.Add("@software_chrome_ever_3", SqlDbType.NVarChar, 50, "software_chrome_ever_3");
                        da.UpdateCommand.Parameters.Add("@software_chrome_ever_4", SqlDbType.NVarChar, 50, "software_chrome_ever_4");
                        da.UpdateCommand.Parameters.Add("@software_chrome_ever_5", SqlDbType.NVarChar, 50, "software_chrome_ever_5");
                        da.UpdateCommand.Parameters.Add("@software_gupdate_hver_1", SqlDbType.NVarChar, 50, "software_gupdate_hver_1");
                        da.UpdateCommand.Parameters.Add("@software_gupdate_hver_2", SqlDbType.NVarChar, 50, "software_gupdate_hver_2");
                        da.UpdateCommand.Parameters.Add("@software_gupdate_hver_3", SqlDbType.NVarChar, 50, "software_gupdate_hver_3");
                        da.UpdateCommand.Parameters.Add("@software_gupdate_hver_4", SqlDbType.NVarChar, 50, "software_gupdate_hver_4");
                        da.UpdateCommand.Parameters.Add("@software_gupdate_hver_5", SqlDbType.NVarChar, 50, "software_gupdate_hver_5");
                        da.UpdateCommand.Parameters.Add("@software_gupdate_ever_1", SqlDbType.NVarChar, 50, "software_gupdate_ever_1");
                        da.UpdateCommand.Parameters.Add("@software_gupdate_ever_2", SqlDbType.NVarChar, 50, "software_gupdate_ever_2");
                        da.UpdateCommand.Parameters.Add("@software_gupdate_ever_3", SqlDbType.NVarChar, 50, "software_gupdate_ever_3");
                        da.UpdateCommand.Parameters.Add("@software_gupdate_ever_4", SqlDbType.NVarChar, 50, "software_gupdate_ever_4");
                        da.UpdateCommand.Parameters.Add("@software_gupdate_ever_5", SqlDbType.NVarChar, 50, "software_gupdate_ever_5");
                        da.UpdateCommand.Parameters.Add("@md5", SqlDbType.NVarChar, 100, "md5");
                        da.UpdateCommand.Parameters.Add("@updatedate", SqlDbType.DateTimeOffset, 50, "updatedate");
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
                                strJson = string.Empty;
                                strMd5 = string.Empty;
                                md = new MData30Data();
                                //DataRow dr = dt.Rows[0];
                                DataRow[] foundRows = dtsd.Select("uid='" + ds.Tables["guit"].Rows[i]["uid"] + "'", "udate desc");

                                if (foundRows != null && foundRows.Length > 0)
                                {
                                    strJson = foundRows[0]["data"].ToString();
                                    strMd5 = MD5Encrypt(strJson);
                                    try
                                    {
                                        //LogHelper.writeInfoLog("intline = " + intline);
                                        JsonSerializer serializer = new JsonSerializer();
                                        StringReader srt = new StringReader(strJson);

                                        md = serializer.Deserialize(new JsonTextReader(srt), typeof(MData30Data)) as MData30Data;
                                        srt.Close();
                                    }
                                    catch (JsonException je)
                                    {
                                        //LogHelper.writeInfoLog("intline = " + intline);
                                        //LogHelper.writeDebugLog("debug: " + line);
                                        //LogHelper.writeErrorLog("error: itemCount = " + itemCount);
                                        LogHelper.writeErrorLog(je);

                                        //continue;
                                    }
                                    catch (OutOfMemoryException oome)
                                    {
                                        //LogHelper.writeInfoLog("intline = " + intline);
                                        //LogHelper.writeDebugLog("debug: " + line);
                                        //LogHelper.writeErrorLog("error: itemCount = " + itemCount);
                                        LogHelper.writeErrorLog(oome);

                                        //continue;
                                    }
                                    catch (Exception ex)
                                    {
                                        //LogHelper.writeInfoLog("intline = " + intline);
                                        //LogHelper.writeErrorLog("error: itemCount = " + itemCount);
                                        //LogHelper.writeErrorLog("error: s[0] = " + s[0]);
                                        LogHelper.writeErrorLog(ex);
                                        //continue;
                                    }

                                    ds.Tables["guit"].Rows[i]["locale"] = foundRows[0]["locale"];
                                    ds.Tables["guit"].Rows[i]["os"] = foundRows[0]["os"];
                                    ds.Tables["guit"].Rows[i]["sid"] = foundRows[0]["sid"];
                                    ds.Tables["guit"].Rows[i]["hid"] = foundRows[0]["hid"];
                                    ds.Tables["guit"].Rows[i]["sysid"] = foundRows[0]["sysid"];
                                    ds.Tables["guit"].Rows[i]["vid"] = foundRows[0]["vid"];
                                    ds.Tables["guit"].Rows[i]["vm"] = foundRows[0]["vm"];
                                    ds.Tables["guit"].Rows[i]["workversion"] = foundRows[0]["workversion"];
                                    ds.Tables["guit"].Rows[i]["amd64"] = foundRows[0]["amd64"];

                                    for (int n = 1; n <= 5; n++)
                                    {
                                        ds.Tables["guit"].Rows[i]["antivirus_guid_" + n] = "";
                                        ds.Tables["guit"].Rows[i]["antivirus_name_" + n] = "";
                                        ds.Tables["guit"].Rows[i]["dotnet_" + n] = "";
                                        ds.Tables["guit"].Rows[i]["software_chrome_hver_" + n] = "";
                                        ds.Tables["guit"].Rows[i]["software_chrome_ever_" + n] = "";
                                        ds.Tables["guit"].Rows[i]["software_gupdate_hver_" + n] = "";
                                        ds.Tables["guit"].Rows[i]["software_gupdate_ever_" + n] = "";
                                    }
                                    if (md != null)
                                    {
                                        if (md.Antivirus != null && md.Antivirus.Count > 0)
                                        {
                                            for (int m = 0; m < md.Antivirus.Count; m++)
                                            {
                                                if (m < 5)
                                                {
                                                    ds.Tables["guit"].Rows[i]["antivirus_guid_" + (m + 1)] = md.Antivirus[m].guid;

                                                    if (md.Antivirus[m].name.Length > 50)
                                                    {
                                                        ds.Tables["guit"].Rows[i]["antivirus_name_" + (m + 1)] = md.Antivirus[m].name.Remove(50);
                                                    }
                                                    else
                                                    {
                                                        ds.Tables["guit"].Rows[i]["antivirus_name_" + (m + 1)] = md.Antivirus[m].name;
                                                    }
                                                }
                                            }
                                        }

                                        if (md.Hardware != null)
                                        {
                                            ds.Tables["guit"].Rows[i]["base"] = md.Hardware.Base;
                                            ds.Tables["guit"].Rows[i]["bios"] = md.Hardware.Bios;
                                            ds.Tables["guit"].Rows[i]["disk"] = md.Hardware.Disk;
                                            ds.Tables["guit"].Rows[i]["network"] = md.Hardware.Network;
                                        }
                                                                                
                                        if (md.dotnet != null && md.dotnet.Count > 0)
                                        {
                                            for (int m = 0; m < md.dotnet.Count; m++)
                                            {
                                                if (m < 5)
                                                {
                                                    ds.Tables["guit"].Rows[i]["dotnet_" + (m + 1)] = md.dotnet[m];
                                                }
                                            }
                                        }

                                        if (md.Software != null)
                                        {
                                            if (md.Software.chrome != null && md.Software.chrome.Count > 0)
                                            {
                                                for (int m = 0; m < md.Software.chrome.Count; m++)
                                                {
                                                    if (m < 5)
                                                    {
                                                        ds.Tables["guit"].Rows[i]["software_chrome_hver_" + (m + 1)] = md.Software.chrome[m].hver;
                                                        ds.Tables["guit"].Rows[i]["software_chrome_ever_" + (m + 1)] = md.Software.chrome[m].ever;
                                                    }
                                                }
                                            }
                                            if (md.Software.gupdate != null && md.Software.gupdate.Count > 0)
                                            {
                                                for (int m = 0; m < md.Software.gupdate.Count; m++)
                                                {
                                                    if (m < 5)
                                                    {
                                                        ds.Tables["guit"].Rows[i]["software_gupdate_hver_" + (m + 1)] = md.Software.gupdate[m].hver;
                                                        ds.Tables["guit"].Rows[i]["software_gupdate_ever_" + (m + 1)] = md.Software.gupdate[m].ever;
                                                    }
                                                }
                                            }
                                        }


                                    }
                                    ds.Tables["guit"].Rows[i]["md5"] = strMd5;
                                    ds.Tables["guit"].Rows[i]["dx"] = foundRows[0]["dx"];
                                    ds.Tables["guit"].Rows[i]["ie"] = foundRows[0]["ie"];
                                    ds.Tables["guit"].Rows[i]["kill"] = foundRows[0]["kill"];
                                    
                                    ds.Tables["guit"].Rows[i]["updatedate"] = dtNow;
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

        ///   <summary>
        ///   给一个字符串进行MD5加密
        ///   </summary>
        ///   <param   name="strText">待加密字符串</param>
        ///   <returns>加密后的字符串</returns>
        public static string MD5Encrypt(string strText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(strText));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("x2"));
            }
            return sb.ToString();
            //return System.Text.Encoding.Default.GetString(result);
        }
    }
}
