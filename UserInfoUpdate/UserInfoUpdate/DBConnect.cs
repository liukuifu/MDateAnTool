using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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

        public void GetGo20UserInfo()
        {
            try
            {
                //string qssd = "SELECT [keys]      ,[udate]      ,[id]      ,[appid]      ,[channel]      ,[event]      ,[eggid]      ,[locale]      ,[os]      ,[uid]      ,[version]      ,[amd64]      ,[antivirus_guid_1]      ,[antivirus_name_1]      ,[antivirus_guid_2]      ,[antivirus_name_2]      ,[antivirus_guid_3]      ,[antivirus_name_3]      ,[antivirus_guid_4]      ,[antivirus_name_4]      ,[antivirus_guid_5]      ,[antivirus_name_5]      ,[browser]      ,[bversion]      ,[dotnet_1]      ,[dotnet_2]      ,[dotnet_3]      ,[dotnet_4]      ,[dotnet_5]      ,[dx]      ,[base]      ,[bios]      ,[disk]      ,[network]      ,[ie]      ,[kill]      ,[createdate]      ,[updatedate] FROM [Go20SourceData] a where a.udate = (select max(udate) from [Go20SourceData] where uid = a.uid) order by a.[uid] desc";
                string qssd = "SELECT [udate],[locale],[uid],[amd64],[antivirus_guid_1],[antivirus_name_1],[antivirus_guid_2],[antivirus_name_2],[antivirus_guid_3],[antivirus_name_3],[antivirus_guid_4],[antivirus_name_4],[antivirus_guid_5],[antivirus_name_5],[browser],[bversion],[dotnet_1],[dotnet_2],[dotnet_3],[dotnet_4],[dotnet_5],[dx],[base],[bios],[disk],[network],[ie] FROM [Go20SourceData]  a where a.udate = (select max(udate) from [Go20SourceData] where uid = a.uid) order by a.[uid] desc";
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
                string qs = "SELECT * FROM Go20UserInfo where Convert(varchar,udate,120) LIKE '2015-11-17%' and [locale] is null";
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
                        sb.Append("UPDATE [Go20UserInfo]");
                        sb.Append("SET [locale] = @locale");
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
                            LogHelper.writeErrorLog("dt.Rows.Count = " + dt.Rows.Count);

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
    }
}
