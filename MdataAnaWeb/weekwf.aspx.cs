using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MdataAn
{
    public partial class weekwf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable table = new DataTable();
                table.Columns.Add("week"); ;
                table.Columns.Add("weekcount");
                table.Columns.Add("weeknewcount");
                table.Columns.Add("nextweekcount");
                table.Columns.Add("weekACountp");
                DataRow dr = table.NewRow();
                table.Rows.Add(dr);

                this.GridView3.DataSource = table;
                this.GridView3.DataBind();

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
            Int64 intdaycount = 0;
            Int64 intAfterWeekcount = 0;

            string strTableName = "GoSourceData";
            string strUITableName = "UserInfo";
            string strDUTableName = "GoDailyUser";

            string strInput = string.Empty;
            string strDBType = string.Empty;
            strInput = this.input2.Value;
            if (string.IsNullOrEmpty(strInput))
            {
                return;
            }

            strDBType = this.ctype.Items[this.ctype.SelectedIndex].Text;

            DateTime dt = DateTime.Parse(strInput);
            string strSearchStarDay = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(strInput).AddDays(7));
            string strSesrChEndDay = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(strInput).AddDays(13));

            if ("go2.0".Equals(strDBType))
            {
                strTableName = "Go20SourceData";
                strUITableName = "Go20UserInfo";
                strDUTableName = "Go20DailyUser";
                this.lblTitle.Text = "go2.0";
            }
            else if ("C#2.0".Equals(strDBType))
            {
                strTableName = "Cs20SourceData";
                strUITableName = "Cs20UserInfo";
                strDUTableName = "Cs20DailyUser";
                this.lblTitle.Text = "C#2.0";
            }
            else if ("killer2.0".Equals(strDBType))
            {
                strTableName = "Killer20SourceData";
                strDUTableName = "Killer20DailyUser";
                strUITableName = "Killer20UserInfo";
                this.lblTitle.Text = "killer2.0";
            }
            //DBConnect dbc = new DBConnect();

            DataTable table = new DataTable();
            //DataRow dr = null;

            //table.Columns.Add("date");
            //table.Columns.Add("week");
            //table.Columns.Add("daycount");
            //table.Columns.Add("weekACount");
            //table.Columns.Add("weekACountp");

            //dbc.GetWeekCount(strInput, strDUTableName,ref table);
            table = WeekStatisticsLogic.GetGo20WeekDataToTable(dt, strTableName, strUITableName, strDUTableName, "go2.0");

            //this.GridView3.DataSource = table;
            //this.GridView3.DataBind();
            this.search.Enabled = true;

            LogHelper.writeInfoLog("search_Click End");

        }
    }

}