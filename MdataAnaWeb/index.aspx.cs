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

            Int64 intv112 = 0;
            Int64 v107 = 0;
            Int64 vother = 0;

            //this.search.Enabled = false;
            //string rtn = string.Empty;
            string strInput = string.Empty;
            string strCType = string.Empty;
            DailyVisitUserStatisticsData dvusd = new DailyVisitUserStatisticsData();

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
            else if ("go2.0".Equals(strCType))
            {
                strDataTableName = "Go20SourceData";
                strUserTableName = "Go20UserInfo";
                strDailyTableName = "Go20DailyUser";
                this.lblTitle.Text = "go2.0";
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
            else if ("C#2.0".Equals(strCType))
            {
                strDataTableName = "Cs20SourceData";
                strUserTableName = "Cs20UserInfo";
                strDailyTableName = "Cs20DailyUser";
                this.lblTitle.Text = "C#2.0";
            }

            DBConnect dbc = new DBConnect();

            DataTable table = new DataTable();
            DataRow dr = null;

            LogHelper.writeDebugLog("strInput = "+ strInput);
            LogHelper.writeDebugLog("strCType = " + strCType);

            dvusd = dbc.GetDailyVisitUserStatistics(strInput, strCType);

            LogHelper.writeDebugLog("dvusd = " + dvusd.ToString());

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
                if (string.IsNullOrEmpty(dvusd.TotalNumberOfDays))
                {
                    intCount = dbc.GetTaskCount(strInput, strDataTableName);
                    updateFlg = true;
                }
                else
                {
                    intCount = Convert.ToInt64(dvusd.TotalNumberOfDays);
                }

                if (string.IsNullOrEmpty(dvusd.DayNumberOfUsers))
                {
                    intdaycount = dbc.GetTaskDayCount(strInput, strDataTableName);
                    updateFlg = true;
                }
                else
                {
                    intdaycount = Convert.ToInt64(dvusd.DayNumberOfUsers);
                }

                if (string.IsNullOrEmpty(dvusd.TaskNumber))
                {
                    inttaskcount = dbc.GetTaskResultCount(strInput, strDataTableName);
                    updateFlg = true;
                }
                else
                {
                    inttaskcount = Convert.ToInt64(dvusd.TaskNumber);
                }

                if (string.IsNullOrEmpty(dvusd.TaskNumberOfSuccess))
                {
                    intreturncount = dbc.GetTaskResultReturnCount(strInput, strDataTableName);
                    updateFlg = true;
                }
                else
                {
                    intreturncount = Convert.ToInt64(dvusd.TaskNumberOfSuccess);
                }

                dr["date"] = strInput;
                dr["count"] = intCount;
                dr["daycount"] = intdaycount;
                dr["task"] = inttaskcount;
                dr["taskp"] = ((double)inttaskcount / (double)intdaycount).ToString("P");
                dr["return"] = intreturncount;
                dr["returnp"] = ((double)intreturncount / (double)intdaycount).ToString("P");

                table.Rows.Add(dr);

                if (string.IsNullOrEmpty(dvusd.UType) &&
                    string.IsNullOrEmpty(dvusd.UDate))
                {
                    dvusd.UType = strCType;
                    dvusd.UDate = strInput;
                    dvusd.TotalNumberOfDays = Convert.ToString(intCount);
                    dvusd.DayNumberOfUsers = Convert.ToString(intdaycount);
                    dvusd.TaskNumber = Convert.ToString(inttaskcount);
                    dvusd.TaskNumberOfSuccess = Convert.ToString(intreturncount);
                    dbc.InsertDailyVisitUserStatisticsForTask(dvusd);
                }
                else
                {
                    if (updateFlg)
                    {
                        dvusd.TotalNumberOfDays = Convert.ToString(intCount);
                        dvusd.DayNumberOfUsers = Convert.ToString(intdaycount);
                        dvusd.TaskNumber = Convert.ToString(inttaskcount);
                        dvusd.TaskNumberOfSuccess = Convert.ToString(intreturncount);
                        dbc.UpdateDailyVisitUserStatistics(dvusd);
                    }
                }

                this.GridView1.Visible = false;
                this.GridView2.Visible = true;
                this.GridView3.Visible = false;

                this.GridView2.DataSource = table;
                this.GridView2.DataBind();

            }
            else if ("go2.0".Equals(strCType)
                || "C#2.0".Equals(strCType))
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
                table.Columns.Add("v112");
                table.Columns.Add("v107");
                table.Columns.Add("vother");

                dr = table.NewRow();

                if (string.IsNullOrEmpty(dvusd.TotalNumberOfDays))
                {
                    intCount = dbc.GetTCount(strInput, strDataTableName);
                    dvusd.TotalNumberOfDays = Convert.ToString(intCount);
                    updateFlg = true;
                }
                else
                {
                    intCount = Convert.ToInt64(dvusd.TotalNumberOfDays);
                }


                if (string.IsNullOrEmpty(dvusd.DayNumberOfUsers))
                {
                    intdaycount = dbc.GetDayCount(strInput, strDailyTableName);
                    dvusd.DayNumberOfUsers = Convert.ToString(intdaycount);
                    updateFlg = true;
                }
                else
                {
                    intdaycount = Convert.ToInt64(dvusd.DayNumberOfUsers);
                }

                if (string.IsNullOrEmpty(dvusd.NumberOfDaysNewUsers))
                {
                    intnewcount = dbc.GetNewCount(strInput, strDataTableName, strUserTableName);
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
                    intsecondnewcount = dbc.GetSecondNewCount(strInput, strSecondDay, strDailyTableName, strUserTableName);
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
                    intthirdnewcount = dbc.GetThirdNewCount(strInput, strThirdDay, strDailyTableName, strUserTableName);
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
                    intthreenewcount = dbc.GetThreeNewCount(strInput, strSecondDay, strThirdDay, strDailyTableName, strUserTableName);
                    dvusd.ThreeDayNumberOfNewUsers = Convert.ToString(intthreenewcount);
                    updateFlg = true;
                }
                else
                {
                    intthreenewcount = Convert.ToInt64(dvusd.ThreeDayNumberOfNewUsers);
                }

                if (string.IsNullOrEmpty(dvusd.NumberOfNewUsersEgg1))
                {
                    integg1usercount = dbc.GetEgg1UserCount(strInput, strUserTableName);
                    dvusd.NumberOfNewUsersEgg1 = Convert.ToString(integg1usercount);
                    updateFlg = true;
                }
                else
                {
                    integg1usercount = Convert.ToInt64(dvusd.NumberOfNewUsersEgg1);
                }

                if (string.IsNullOrEmpty(dvusd.DayNumberOfUsersKillInstallation))
                {
                    intkillusercount = dbc.GetKillUserCount(strInput, strDailyTableName);
                    dvusd.DayNumberOfUsersKillInstallation = Convert.ToString(intkillusercount);
                    updateFlg = true;
                }
                else
                {
                    intkillusercount = Convert.ToInt64(dvusd.DayNumberOfUsersKillInstallation);
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
                dr["egg1user"] = integg1usercount;
                dr["killuser"] = intkillusercount;

                intv112 = dbc.GetVCount(strInput, strDailyTableName, "1000.0.0.112");
                v107 = dbc.GetVCount(strInput, strDailyTableName, "1000.0.0.107");
                vother = dbc.GetNotVCount(strInput, strDailyTableName, "1000.0.0.107", "1000.0.0.112"); ;
                dr["v112"] = intv112;
                dr["v107"] = v107;
                dr["vother"] = vother;

                table.Rows.Add(dr);

                if (string.IsNullOrEmpty(dvusd.UType) &&
                    string.IsNullOrEmpty(dvusd.UDate))
                {
                    dvusd.UType = strCType;
                    dvusd.UDate = strInput;
                    dbc.InsertDailyVisitUserStatisticsForGo20(dvusd);
                }
                else
                {
                    if (updateFlg)
                    {
                        dbc.UpdateDailyVisitUserStatistics(dvusd);
                    }
                }

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

                if (string.IsNullOrEmpty(dvusd.TotalNumberOfDays))
                {
                    LogHelper.writeDebugLog("1111111");

                    intCount = dbc.GetTCount(strInput, strDataTableName);
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

                    intdaycount = dbc.GetDayCount(strInput, strDailyTableName);
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

                    intnewcount = dbc.GetNewCount(strInput, strDataTableName, strUserTableName);
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

                    intsecondnewcount = dbc.GetSecondNewCount(strInput, strSecondDay, strDailyTableName, strUserTableName);
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

                    intthirdnewcount = dbc.GetThirdNewCount(strInput, strThirdDay, strDailyTableName, strUserTableName);
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

                    intthreenewcount = dbc.GetThreeNewCount(strInput, strSecondDay, strThirdDay, strDailyTableName, strUserTableName);
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

                    dvusd.UType = strCType;
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
    }
}