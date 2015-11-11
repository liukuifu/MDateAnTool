using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MdataAn
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable table = new DataTable();
                table.Columns.Add("date"); ;
                table.Columns.Add("count");
                table.Columns.Add("daycount");
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
        //protected void searchData(object sender, EventArgs e)
        //public string searchData(string strInput1,string strInput2)
        {
            //this.search.Enabled = false;
            //string rtn = string.Empty;
            string strInput = string.Empty;
            string strCType = string.Empty;

            strInput = this.input2.Value;
            //strInput = strInput1;

            strCType = this.ctype.Items[this.ctype.SelectedIndex].Text;
            //strCType = strInput2;
            if (string.IsNullOrEmpty(strInput))
            {
                return;
                //return "请选择时间";
            }

            DateTime dt = DateTime.Parse(strInput);
            string strSecondDay = string.Format("{0:yyyy-MM-dd}", dt.AddDays(1));
            string strThirdDay = string.Format("{0:yyyy-MM-dd}", dt.AddDays(2));

            string strDataTableName = "GoSourceData";
            string strUserTableName = "UserInfo";
            string strDailyTableName = "GoDailyUser";

            if ("go".Equals(strCType))
            {
                strDataTableName = "GoSourceData";
                strUserTableName = "UserInfo";
                strDailyTableName = "GoDailyUser";
                this.lblTitle.Text = "GO";
            }
            else if ("go 2.0".Equals(strCType))
            {
                strDataTableName = "Go20SourceData";
                strUserTableName = "Go20UserInfo";
                strDailyTableName = "Go20DailyUser";
                this.lblTitle.Text = "go 2.0";
            }
            else if ("C#".Equals(strCType))
            {
                strDataTableName = "CSharpSourceData";
                strUserTableName = "CsUserInfo";
                strDailyTableName = "CsDailyUser";
                this.lblTitle.Text = "C#";
            }
            else if ("killer".Equals(strCType))
            {
                strDataTableName = "KillerSourceData";
                strUserTableName = "KillerUserInfo";
                strDailyTableName = "KillerDailyUser";
                this.lblTitle.Text = "Killer";
            }
            else if ("task".Equals(strCType))
            {
                strDataTableName = "Go20TaskSD";
                this.lblTitle.Text = "Task Result";
            }

            DBConnect dbc = new DBConnect();

            DataTable table = new DataTable();
            DataRow dr = null;
            if ("task".Equals(strCType))
            {

                table.Columns.Add("date");
                table.Columns.Add("count");
                table.Columns.Add("daycount");
                table.Columns.Add("task");
                table.Columns.Add("taskp");
                table.Columns.Add("return");
                table.Columns.Add("returnp");

                dr = table.NewRow();

                Int64 intCount = dbc.GetTaskCount(strInput, strDataTableName);
                Int64 intdaycount = dbc.GetTaskDayCount(strInput, strDataTableName);
                
                Int64 inttaskcount = dbc.GetTaskResultCount(strInput, strDataTableName);
                Int64 intreturncount = dbc.GetTaskResultReturnCount(strInput, strDataTableName);

                dr["date"] = strInput;
                dr["count"] = intCount;
                dr["daycount"] = intdaycount;
                dr["task"] = inttaskcount;
                dr["taskp"] = ((double)inttaskcount / (double)intdaycount).ToString("P");
                dr["return"] = intreturncount;
                dr["returnp"] = ((double)intreturncount / (double)intdaycount).ToString("P");

                table.Rows.Add(dr);

                this.GridView1.Visible = false;
                this.GridView2.Visible = true;
                this.GridView3.Visible = false;

                this.GridView2.DataSource = table;
                this.GridView2.DataBind();

            }
            else if ("go 2.0".Equals(strCType))
            {
                table.Columns.Add("date");
                table.Columns.Add("count");
                table.Columns.Add("daycount");
                table.Columns.Add("new");
                table.Columns.Add("secondnew");
                table.Columns.Add("secondnewp");
                table.Columns.Add("thirdnew");
                table.Columns.Add("thirdnewp");
                table.Columns.Add("threenew");
                table.Columns.Add("threenewp");
                table.Columns.Add("egg1user");
                table.Columns.Add("killuser");

                dr = table.NewRow();

                Int64 intCount = dbc.GetTCount(strInput, strDataTableName);
                Int64 intdaycount = dbc.GetDayCount(strInput, strDailyTableName);
                Int64 intnewcount = dbc.GetNewCount(strInput, strDataTableName, strUserTableName);
                Int64 intsecondnewcount = dbc.GetSecondNewCount(strInput, strSecondDay, strDailyTableName, strUserTableName);
                Int64 intthirdnewcount = dbc.GetThirdNewCount(strInput, strThirdDay, strDailyTableName, strUserTableName);
                Int64 intthreenewcount = dbc.GetThreeNewCount(strInput, strSecondDay, strThirdDay, strDailyTableName, strUserTableName);
                Int64 integg1usercount = dbc.GetEgg1UserCount(strInput, strUserTableName);
                Int64 intkillusercount = dbc.GetKillUserCount(strInput, strDailyTableName);

                dr["date"] = strInput;
                dr["count"] = intCount;
                dr["daycount"] = intdaycount;
                dr["new"] = intnewcount;
                dr["secondnew"] = intsecondnewcount;
                dr["secondnewp"] = ((double)intsecondnewcount / (double)intnewcount).ToString("P");
                dr["thirdnew"] = intthirdnewcount;
                dr["thirdnewp"] = ((double)intthirdnewcount / (double)intnewcount).ToString("P");
                dr["threenew"] = intthreenewcount;
                dr["threenewp"] = ((double)intthreenewcount / (double)intnewcount).ToString("P");
                dr["egg1user"] = integg1usercount;
                dr["killuser"] = intkillusercount;

                table.Rows.Add(dr);

                this.GridView3.DataSource = table;
                this.GridView3.DataBind();

                this.GridView1.Visible = false;
                this.GridView2.Visible = false;
                this.GridView3.Visible = true;
            }
            else
            {
                table.Columns.Add("date");
                table.Columns.Add("count");
                table.Columns.Add("daycount");
                table.Columns.Add("new");
                table.Columns.Add("secondnew");
                table.Columns.Add("secondnewp");
                table.Columns.Add("thirdnew");
                table.Columns.Add("thirdnewp");
                table.Columns.Add("threenew");
                table.Columns.Add("threenewp");

                dr = table.NewRow();

                Int64 intCount = dbc.GetTCount(strInput, strDataTableName);
                Int64 intdaycount = dbc.GetDayCount(strInput, strDailyTableName);
                Int64 intnewcount = dbc.GetNewCount(strInput, strDataTableName, strUserTableName);
                Int64 intsecondnewcount = dbc.GetSecondNewCount(strInput, strSecondDay, strDailyTableName, strUserTableName);
                Int64 intthirdnewcount = dbc.GetThirdNewCount(strInput, strThirdDay, strDailyTableName, strUserTableName);
                Int64 intthreenewcount = dbc.GetThreeNewCount(strInput, strSecondDay, strThirdDay, strDailyTableName, strUserTableName);

                dr["date"] = strInput;
                dr["count"] = intCount;
                dr["daycount"] = intdaycount;
                dr["new"] = intnewcount;
                dr["secondnew"] = intsecondnewcount;
                dr["secondnewp"] = ((double)intsecondnewcount / (double)intnewcount).ToString("P");
                dr["thirdnew"] = intthirdnewcount;
                dr["thirdnewp"] = ((double)intthirdnewcount / (double)intnewcount).ToString("P");
                dr["threenew"] = intthreenewcount;
                dr["threenewp"] = ((double)intthreenewcount / (double)intnewcount).ToString("P");

                table.Rows.Add(dr);

                this.GridView1.DataSource = table;
                this.GridView1.DataBind();

                this.GridView1.Visible = true;
                this.GridView2.Visible = false;
                this.GridView3.Visible = false;

            }
            this.search.Enabled = true;
        }
    }
}