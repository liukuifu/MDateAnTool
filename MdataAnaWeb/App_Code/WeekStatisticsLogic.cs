using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MdataAn
{
    /// <summary>
    /// Summary description for WeekStatisticsLogic
    /// </summary>
    public class WeekStatisticsLogic
    {
        public WeekStatisticsLogic()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static DataTable GetGo20WeekDataToTable(DateTime startDt, string strTableName, string strUITableName, string strDUTableName, string strSheetName)
        {

            LogHelper.writeInfoLog("GetGo20WeekDataToTable Start");

            int intWeekDAUCount = 0;
            int intWeekDAUDisCount = 0;
            int intNextWeekDAUDisCount = 0;

            string strInput = string.Empty;
            string strSecondDay = string.Empty;
            string strThirdDay = string.Empty;

            DateTime dtNow = DateTime.Now;

            DailyVisitUserStatisticsData dvusd = new DailyVisitUserStatisticsData();

            DBConnect dbc = new DBConnect();

            System.Data.DataTable table = new System.Data.DataTable();

            table.Columns.Add("自然周单位");
            table.Columns.Add("本周总访问数(DAU)");
            table.Columns.Add("本周用户");
            table.Columns.Add("次周存活");
            table.Columns.Add("次周存活/本周新用户");

            DataRow dr = null;

            DateTime dtTwoWeeksAgoS = Common.getmondaydate(dtNow.AddDays(-14));
            DateTime dtTwoWeeksAgoE = Common.getsundaydate(dtNow.AddDays(-14));
            DateTime ThePreviousWeekS = Common.getmondaydate(dtNow.AddDays(-7));
            DateTime ThePreviousWeekE = Common.getsundaydate(dtNow.AddDays(-7));
            DateTime ThisWeekS = Common.getmondaydate(dtNow);
            DateTime ThisWeekE = Common.getsundaydate(dtNow);

            // 前两周
            dr = table.NewRow();

            dr["自然周单位"] = dtTwoWeeksAgoS.ToString("yyyy-MM-dd") + "——" + dtTwoWeeksAgoE.ToString("yyyy-MM-dd");

            intWeekDAUCount = dbc.GetWeekDAUCount(dtTwoWeeksAgoS.ToString("yyyy-MM-dd"), dtTwoWeeksAgoE.ToString("yyyy-MM-dd"), strDUTableName);
            dr["本周总访问数(DAU)"] = intWeekDAUCount;

            intWeekDAUDisCount = dbc.GetWeekDAUDisCount(dtTwoWeeksAgoS.ToString("yyyy-MM-dd"), dtTwoWeeksAgoE.ToString("yyyy-MM-dd"), strDUTableName);
            dr["本周用户"] = intWeekDAUDisCount;

            intNextWeekDAUDisCount = dbc.GetNextWeekDAUDisCount(dtTwoWeeksAgoS.ToString("yyyy-MM-dd"),
                dtTwoWeeksAgoE.ToString("yyyy-MM-dd"),
                ThePreviousWeekS.ToString("yyyy-MM-dd"),
                ThePreviousWeekE.ToString("yyyy-MM-dd"),
                strDUTableName);

            dr["次周存活"] = intNextWeekDAUDisCount;
            dr["次周存活/本周新用户"] = ((double)intNextWeekDAUDisCount / (double)intWeekDAUDisCount).ToString("P");

            table.Rows.Add(dr);
            intWeekDAUCount = 0;
            intWeekDAUDisCount = 0;
            intNextWeekDAUDisCount = 0;

            // 前一周
            dr = table.NewRow();

            dr["自然周单位"] = ThePreviousWeekS.ToString("yyyy-MM-dd") + "——" + ThePreviousWeekE.ToString("yyyy-MM-dd");

            intWeekDAUCount = dbc.GetWeekDAUCount(ThePreviousWeekS.ToString("yyyy-MM-dd"), ThePreviousWeekE.ToString("yyyy-MM-dd"), strDUTableName);
            dr["本周总访问数(DAU)"] = intWeekDAUCount;

            intWeekDAUDisCount = dbc.GetWeekDAUDisCount(ThePreviousWeekS.ToString("yyyy-MM-dd"), ThePreviousWeekE.ToString("yyyy-MM-dd"), strDUTableName);
            dr["本周用户"] = intWeekDAUDisCount;

            intNextWeekDAUDisCount = dbc.GetNextWeekDAUDisCount(ThePreviousWeekS.ToString("yyyy-MM-dd"),
                ThePreviousWeekE.ToString("yyyy-MM-dd"),
                ThisWeekS.ToString("yyyy-MM-dd"),
                ThisWeekE.ToString("yyyy-MM-dd"),
                strDUTableName);

            dr["次周存活"] = intNextWeekDAUDisCount;
            dr["次周存活/本周新用户"] = ((double)intNextWeekDAUDisCount / (double)intWeekDAUDisCount).ToString("P");

            table.Rows.Add(dr);
            intWeekDAUCount = 0;
            intWeekDAUDisCount = 0;
            intNextWeekDAUDisCount = 0;

            // 本周
            dr = table.NewRow();

            dr["自然周单位"] = ThisWeekS.ToString("yyyy-MM-dd") + "——" + ThisWeekE.ToString("yyyy-MM-dd");

            intWeekDAUCount = dbc.GetWeekDAUCount(ThisWeekS.ToString("yyyy-MM-dd"), ThisWeekE.ToString("yyyy-MM-dd"), strDUTableName);
            dr["本周总访问数(DAU)"] = intWeekDAUCount;

            intWeekDAUDisCount = dbc.GetWeekDAUDisCount(ThisWeekS.ToString("yyyy-MM-dd"), ThisWeekE.ToString("yyyy-MM-dd"), strDUTableName);
            dr["本周用户"] = intWeekDAUDisCount;

            dr["次周存活"] = intNextWeekDAUDisCount;
            dr["次周存活/本周新用户"] = ((double)intNextWeekDAUDisCount / (double)intWeekDAUDisCount).ToString("P");

            table.Rows.Add(dr);
            intWeekDAUCount = 0;
            intWeekDAUDisCount = 0;
            intNextWeekDAUDisCount = 0;

            return table;
        }

    }
}