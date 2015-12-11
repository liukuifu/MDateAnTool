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
        public string strMsg = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strMsg = string.Empty;
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

            string inputTaskId = this.tbTaskId.Text;

            DateTime dt = DateTime.Parse(strInput);
            string dayCount = this.tbDayCount.Text;
            string strSecondDay = string.Format("{0:yyyy-MM-dd}", dt.AddDays(1));
            string strThirdDay = string.Format("{0:yyyy-MM-dd}", dt.AddDays(2));

            string strTableName = "GoSourceData";
            string strUITableName = "UserInfo";
            string strDUTableName = "GoDailyUser";

            if ("go".Equals(strDBType))
            {
                strTableName = "GoSourceData";
                strUITableName = "UserInfo";
                strDUTableName = "GoDailyUser";
                this.lblTitle.Text = "GO";
            }
            else if ("go2.0".Equals(strDBType))
            {
                strTableName = "Go20SourceData";
                strUITableName = "Go20UserInfo";
                strDUTableName = "Go20DailyUser";
                this.lblTitle.Text = "go2.0";
            }
            else if ("C#".Equals(strDBType))
            {
                strTableName = "CSharpSourceData";
                strUITableName = "CsUserInfo";
                strDUTableName = "CsDailyUser";
                this.lblTitle.Text = "C#";
            }
            else if ("killer".Equals(strDBType))
            {
                strTableName = "KillerSourceData";
                strUITableName = "KillerUserInfo";
                strDUTableName = "KillerDailyUser";
                this.lblTitle.Text = "Killer";
            }
            else if ("task".Equals(strDBType))
            {
                strTableName = "Go20TaskSD";
                strDUTableName = "Go20TaskInfo";
                this.lblTitle.Text = "Task Result";
            }
            else if ("task2.0".Equals(strDBType))
            {
                strTableName = "Go20TaskSD";
                strDUTableName = "Go20TaskInfo";
                this.lblTitle.Text = "Task Result";
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

            if (string.IsNullOrEmpty(dayCount))
            {
                dayCount = "15";
            }

            DBConnect dbc = new DBConnect();

            DataTable table = new DataTable();
            DataRow dr = null;

            LogHelper.writeDebugLog("strInput = "+ strInput);
            LogHelper.writeDebugLog("strDBType = " + strDBType);

            dvusd = dbc.GetDailyVisitUserStatistics(strInput, strDBType);

            LogHelper.writeDebugLog("dvusd = " + dvusd.ToString());

            if ("task2.0".Equals(strDBType))
            {
                table = DayStatisticsLogic.GetTask20DataToTable(dt, strTableName, strUITableName, strDUTableName, strDBType, Convert.ToInt32(dayCount), inputTaskId);


                GridView2.AutoGenerateColumns = false;//设置自动产生列为false
                GridViewBind(GridView2, table, "统计日期");
                
                this.GridView1.Visible = false;
                this.GridView2.Visible = true;
                this.GridView3.Visible = false;
                
                //this.GridView2.DataSource = table;
                //this.GridView2.DataBind();

            }
            //else if ("C#2.0".Equals(strDBType)
            //    || "killer2.0".Equals(strDBType))
            //{ }
            else if ("go2.0".Equals(strDBType)
                || "C#2.0".Equals(strDBType)
                || "killer2.0".Equals(strDBType))
            {
                table = DayStatisticsLogic.Get20DataToTable(dt, strTableName, strUITableName, strDUTableName, strDBType, Convert.ToInt32(dayCount));

                GridView3.AutoGenerateColumns = false;//设置自动产生列为false
                GridViewBind(GridView3, table, "统计日期");

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

                if (string.IsNullOrEmpty(dvusd.TotalNumberOfDays))
                {
                    LogHelper.writeDebugLog("1111111");

                    intCount = dbc.GetTCount(strInput, strTableName);
                    dvusd.TotalNumberOfDays = Convert.ToString(intCount);
                    updateFlg = true;
                }
                else
                {
                    intCount = Convert.ToInt64(dvusd.TotalNumberOfDays);
                }

                if (string.IsNullOrEmpty(dvusd.DayNumberOfUsers))
                {
                    LogHelper.writeDebugLog("222222");

                    intdaycount = dbc.GetDayCount(strInput, strDUTableName);
                    dvusd.DayNumberOfUsers = Convert.ToString(intdaycount);
                    updateFlg = true;
                }
                else
                {
                    intdaycount = Convert.ToInt64(dvusd.DayNumberOfUsers);
                }

                if (string.IsNullOrEmpty(dvusd.NumberOfDaysNewUsers))
                {
                    LogHelper.writeDebugLog("33333");

                    intnewcount = dbc.GetNewCount(strInput, strTableName, strUITableName);
                    dvusd.NumberOfDaysNewUsers = Convert.ToString(intnewcount);
                    updateFlg = true;
                }
                else
                {
                    intnewcount = Convert.ToInt64(dvusd.NumberOfDaysNewUsers);
                }

                if (string.IsNullOrEmpty(dvusd.NextDayNumberOfNewUsers)
                    || "0".Equals(dvusd.NextDayNumberOfNewUsers))
                {
                    LogHelper.writeDebugLog("44444");

                    intsecondnewcount = dbc.GetSecondNewCount(strInput, strSecondDay, strDUTableName, strUITableName);
                    dvusd.NextDayNumberOfNewUsers = Convert.ToString(intsecondnewcount);
                    updateFlg = true;
                }
                else
                {
                    intsecondnewcount = Convert.ToInt64(dvusd.NextDayNumberOfNewUsers);
                }


                if (string.IsNullOrEmpty(dvusd.ThirdDayNumberOfNewUsers)
                    || "0".Equals(dvusd.ThirdDayNumberOfNewUsers))
                {
                    LogHelper.writeDebugLog("55555");

                    intthirdnewcount = dbc.GetThirdNewCount(strInput, strThirdDay, strDUTableName, strUITableName);
                    dvusd.ThirdDayNumberOfNewUsers = Convert.ToString(intthirdnewcount);
                    updateFlg = true;
                    ThreeDayNumberOfNewUsersUpdateFlg = true;
                }
                else
                {
                    intthirdnewcount = Convert.ToInt64(dvusd.ThirdDayNumberOfNewUsers);
                }

                if (string.IsNullOrEmpty(dvusd.ThreeDayNumberOfNewUsers)
                    || "0".Equals(dvusd.ThreeDayNumberOfNewUsers)
                    || ThreeDayNumberOfNewUsersUpdateFlg)
                {
                    LogHelper.writeDebugLog("666666");

                    intthreenewcount = dbc.GetThreeNewCount(strInput, strSecondDay, strThirdDay, strDUTableName, strUITableName);
                    dvusd.ThreeDayNumberOfNewUsers = Convert.ToString(intthreenewcount);
                    updateFlg = true;
                }
                else
                {
                    intthreenewcount = Convert.ToInt64(dvusd.ThreeDayNumberOfNewUsers);
                }

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

                if (string.IsNullOrEmpty(dvusd.UType) &&
                    string.IsNullOrEmpty(dvusd.UDate))
                {
                    LogHelper.writeDebugLog("77777");

                    dvusd.UType = strDBType;
                    dvusd.UDate = strInput;
                    dbc.InsertDailyVisitUserStatistics(dvusd);
                }
                else
                {
                    LogHelper.writeDebugLog("888888");

                    if (updateFlg)
                    {
                        LogHelper.writeDebugLog("9999999");

                        dbc.UpdateDailyVisitUserStatistics(dvusd);
                    }
                }

                this.GridView1.DataSource = table;
                this.GridView1.DataBind();

                this.GridView1.Visible = true;
                this.GridView2.Visible = false;
                this.GridView3.Visible = false;

            }
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