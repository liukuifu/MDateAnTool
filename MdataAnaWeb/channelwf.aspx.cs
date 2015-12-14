using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MdataAn
{
    public partial class channelwf : System.Web.UI.Page
    {
        public string strMsg = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strMsg = string.Empty;
                DataTable table = new DataTable();
                table.Columns.Add("date"); ;
                table.Columns.Add("daycount");
                table.Columns.Add("daychcount");
                table.Columns.Add("daychcountp");
                table.Columns.Add("new");
                table.Columns.Add("secondnew");
                table.Columns.Add("secondnewp");
                table.Columns.Add("thirdnew");
                table.Columns.Add("thirdnewp");
                table.Columns.Add("threenew");
                table.Columns.Add("threenewp");
                DataRow dr = table.NewRow();
                table.Rows.Add(dr);

                this.GridView1.DataSource = table;
                this.GridView1.DataBind();

                //this.GridView3.DataSource = table;
                //this.GridView3.DataBind();
                //search.Attributes.Add("OnClientClick", "return enf()");

            }
            //给button1添加客户端事件
            search.Attributes.Add("OnClick", "return  jsFunction()");
            //jsFunction()是js函数
        }

        protected void search_Click(object sender, EventArgs e)
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
            string strDBType = string.Empty;
            DailyVisitUserStatisticsData dvusd = new DailyVisitUserStatisticsData();

            strInput = this.input2.Value;
            //strInput = strInput1;

            strDBType = this.ctype.Items[this.ctype.SelectedIndex].Text;
            //strDBType = strInput2;
            if (string.IsNullOrEmpty(strInput))
            {
                strMsg = "请选择时间";
                return;
                //return "请选择时间";
            }

            string inputChannel = this.tbChannel.Text;

            DateTime dt = DateTime.Parse(strInput);
            string dayCount = this.tbDayCount.Text;
            string strSecondDay = string.Format("{0:yyyy-MM-dd}", dt.AddDays(1));
            string strThirdDay = string.Format("{0:yyyy-MM-dd}", dt.AddDays(2));

            string strTableName = "GoSourceData";
            string strUITableName = "UserInfo";
            string strDUTableName = "GoDailyUser";

            if ("go2.0".Equals(strDBType))
            {
                strTableName = "Go20SourceData";
                strUITableName = "Go20UserInfo";
                strDUTableName = "Go20DailyUser";
                this.lblTitle.Text = "go2.0";
            }

            if (string.IsNullOrEmpty(dayCount))
            {
                dayCount = "15";
            }

            DBConnect dbc = new DBConnect();

            DataTable table = new DataTable();
            DataRow dr = null;

            LogHelper.writeDebugLog("strInput = " + strInput);
            LogHelper.writeDebugLog("strDBType = " + strDBType);

            //dvusd = dbc.GetDailyVisitUserStatistics(strInput, strDBType);

            LogHelper.writeDebugLog("dvusd = " + dvusd.ToString());

            table = DayStatisticsLogic.Get20ChannelDataToTable(dt, strTableName, strUITableName, strDUTableName, strDBType, inputChannel, Convert.ToInt32(dayCount));

            GridView1.AutoGenerateColumns = false;//设置自动产生列为false

            GridViewBind(GridView1, table, "日期");

            this.search.Enabled = true;

            LogHelper.writeInfoLog("search_Click End");
        }

        private void GridViewBind(GridView gdv, DataTable table, string strDataKey)
        {
            gdv.Columns.Clear();

            gdv.AutoGenerateColumns = false;
            gdv.DataSource = table;
            gdv.DataKeyNames = new string[] { strDataKey };

            for (int i = 0; i < table.Columns.Count; i++)   //绑定普通数据列
            {
                BoundField bfColumn = new BoundField();
                bfColumn.DataField = table.Columns[i].ColumnName;
                bfColumn.HeaderText = table.Columns[i].Caption;
                gdv.Columns.Add(bfColumn);
            }
            gdv.DataBind();//绑定
        }
    }
}