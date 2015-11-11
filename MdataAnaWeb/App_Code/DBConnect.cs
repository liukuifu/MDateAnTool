using System;
using System.Collections.Generic;
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

        public Int64 GetNewCount(string date, string strDataTableName, string strUserTableName)
        {
            Int64 rtn = 0;

            SqlConnection conn = ConnectionOpen();
            string sql = "SELECT count(1) FROM "
                + strUserTableName 
                + " where Convert(varchar, udate,120) LIKE '" 
                + date 
                + "%'";
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = 120;
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
            comm.CommandTimeout = 240;
            rtn = (int)comm.ExecuteScalar();
            conn.Close();
            return rtn;
        }

        public Int64 GetDayCount(string date, string strDailyTableName)
        {
            Int64 rtn = 0;

            SqlConnection conn = ConnectionOpen();

            string sql = "SELECT count(1) FROM " 
                + strDailyTableName
                + " where udate = '" 
                + date 
                + "'";
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = 240;
            rtn = (int)comm.ExecuteScalar();

            //SqlDataReader reader = comm.ExecuteReader();
            //while (reader.Read())
            //{
            //    rtn = rtn + 1;
            //}
            conn.Close();
            return rtn;
        }

        public Int64 GetSecondNewCount(string date, string strSecondDay, string strDailyTableName, string strUserTableName)
        {
            Int64 rtn = 0;

            SqlConnection conn = ConnectionOpen();

            string sql = "SELECT DISTINCT gsd.uid FROM " + strDailyTableName + " gsd where gsd.udate = '"
                + strSecondDay 
                + "' and gsd.uid in (select uif.uid from "+ strUserTableName + " uif where Convert(varchar, uif.udate,120) like '" 
                + date 
                + "%') and uid like '{%'";

            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = 240;

            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                rtn = rtn + 1;
            }
            conn.Close();
            return rtn;
        }

        public Int64 GetThirdNewCount(string date, string strThirdDay, string strDailyTableName, string strUserTableName)
        {
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
            comm.CommandTimeout = 240;

            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                rtn = rtn + 1;
            }
            conn.Close();
            return rtn;
        }

        public Int64 GetThreeNewCount(string date, string strSecondDay, string strThirdDay, string strDailyTableName, string strUserTableName)
        {
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
            comm.CommandTimeout = 240;

            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                rtn = rtn + 1;
            }
            conn.Close();
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
            comm.CommandTimeout = 240;
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
            comm.CommandTimeout = 240;

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
            comm.CommandTimeout = 240;

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
            comm.CommandTimeout = 240;

            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                rtn = rtn + 1;
            }
            conn.Close();
            return rtn;
        }
        public Int64 GetOldCount(string date)
        {
            Int64 rtn = 0;

            SqlConnection conn = ConnectionOpen();
            string sql = "SELECT DISTINCT uid FROM GoSourceData where Convert(varchar, udate,120) LIKE '" + date + "%' and uid not like '{%' and uid <> '' group by uid";
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = 60;
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                rtn = rtn + 1;
            }
            conn.Close();
            return rtn;
        }


        public Int64 GetEgg1UserCount(string date, string strUserTableName)
        {
            Int64 rtn = 0;

            SqlConnection conn = ConnectionOpen();
            string sql = "SELECT count(DISTINCT uid) FROM "
                + strUserTableName
                + " where uid in (select uif.uid from UserInfo uif) and Convert(varchar, udate,120) LIKE '" + date + "%'";

            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = 240;
            rtn = (int)comm.ExecuteScalar();
            conn.Close();
            return rtn;
        }
        public Int64 GetKillUserCount(string date, string strDailyTableName)
        {
            Int64 rtn = 0;

            SqlConnection conn = ConnectionOpen();
            string sql = "SELECT count(1) FROM "
                + strDailyTableName
                + " where Convert(varchar, udate,120) LIKE '" + date + "' and [kill] <> ''";
            
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandTimeout = 240;
            rtn = (int)comm.ExecuteScalar();
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