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
                return;
                //return "请选择时间";
            }

            DateTime dt = DateTime.Parse(strInput);
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

            DBConnect dbc = new DBConnect();

            DataTable table = new DataTable();
            DataRow dr = null;

            LogHelper.writeDebugLog("strInput = "+ strInput);
            LogHelper.writeDebugLog("strDBType = " + strDBType);

            dvusd = dbc.GetDailyVisitUserStatistics(strInput, strDBType);

            LogHelper.writeDebugLog("dvusd = " + dvusd.ToString());

            if ("task".Equals(strDBType))
            {
                string inputTaskId = this.tbTaskId.Text;
                table.Columns.Add("date");
                table.Columns.Add("count");
                table.Columns.Add("daycount");
                table.Columns.Add("task");
                table.Columns.Add("taskp");
                table.Columns.Add("return");
                table.Columns.Add("returnp");
                //if (!string.IsNullOrEmpty(inputTaskId))
                //{
                    table.Columns.Add("taskid");
                    table.Columns.Add("taskidreturn");
                    table.Columns.Add("taskidreturnp");
                //}

                dr = table.NewRow();
                if (string.IsNullOrEmpty(dvusd.TotalNumberOfDays))
                {
                    intCount = dbc.GetTaskCount(strInput, strTableName);
                    updateFlg = true;
                }
                else
                {
                    intCount = Convert.ToInt64(dvusd.TotalNumberOfDays);
                }

                if (string.IsNullOrEmpty(dvusd.DayNumberOfUsers))
                {
                    intdaycount = dbc.GetTaskDayCount(strInput, strTableName);
                    updateFlg = true;
                }
                else
                {
                    intdaycount = Convert.ToInt64(dvusd.DayNumberOfUsers);
                }

                if (string.IsNullOrEmpty(dvusd.TaskNumber))
                {
                    inttaskcount = dbc.GetTaskResultCount(strInput, strTableName);
                    updateFlg = true;
                }
                else
                {
                    inttaskcount = Convert.ToInt64(dvusd.TaskNumber);
                }

                if (string.IsNullOrEmpty(dvusd.TaskNumberOfSuccess))
                {
                    intreturncount = dbc.GetTaskResultReturnCount(strInput, strTableName);
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

                if (!string.IsNullOrEmpty(inputTaskId))
                {
                    intTaskIdcount = dbc.GetTaskIdCount(strInput, inputTaskId, strDUTableName);
                    intTaskIdReturncount = dbc.GetTaskIdReturnCount(strInput, inputTaskId, strDUTableName);
                }
                else
                {
                    intTaskIdcount = 0;
                    intTaskIdReturncount = 0;
                }
                dr["taskid"] = intTaskIdcount;
                    dr["taskidreturn"] = intTaskIdReturncount;
                    dr["taskidreturnp"] = ((double)intTaskIdReturncount / (double)intTaskIdcount).ToString("P");
                table.Rows.Add(dr);

                if (string.IsNullOrEmpty(dvusd.UType) &&
                    string.IsNullOrEmpty(dvusd.UDate))
                {
                    dvusd.UType = strDBType;
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

                if (!string.IsNullOrEmpty(inputTaskId))
                {
                    //if (this.GridView2.Columns.Count < 9)
                    //{
                    //    DataControlField dcf = new DataControlField()
                    //    dcf.ShowHeader = true;
                    //    dcf.HeaderText = "指定taskID : " + inputTaskId + " 的 result 数";
                    //    this.GridView2.Columns.Insert(7, dcf);
                    //} else {
                        this.GridView2.HeaderRow.Cells[7].Text = "指定taskID : " + inputTaskId + " 的 result 数";
                        this.GridView2.HeaderRow.Cells[8].Text = "指定taskID : " + inputTaskId + " 的 result return == 0 数";
                        this.GridView2.HeaderRow.Cells[9].Text = "指定taskID : " + inputTaskId + " 的 result return == 0 比例";
                    //}
                }
                //else
                //{
                //    if (this.GridView2.Columns.Count >= 9)
                //    {
                //        this.GridView2.Columns.RemoveAt(9);
                //    }
                //    if (this.GridView2.Columns.Count >= 8)
                //    {
                //        this.GridView2.Columns.RemoveAt(8);
                //    }
                //    if (this.GridView2.Columns.Count >= 8)
                //    {
                //        this.GridView2.Columns.RemoveAt(7);
                //    }
                //}
                this.GridView2.DataSource = table;
                this.GridView2.DataBind();

            }
            else if ("go2.0".Equals(strDBType)
                || "C#2.0".Equals(strDBType)
                || "killer2.0".Equals(strDBType))
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
                table.Columns.Add("weekACount");
                table.Columns.Add("weekACountp");
                table.Columns.Add("egg1user");
                table.Columns.Add("killuser");
                table.Columns.Add("v112");
                table.Columns.Add("v107");
                table.Columns.Add("vother");

                dr = table.NewRow();

                if (string.IsNullOrEmpty(dvusd.TotalNumberOfDays))
                {
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
                    intthreenewcount = dbc.GetThreeNewCount(strInput, strSecondDay, strThirdDay, strDUTableName, strUITableName);
                    dvusd.ThreeDayNumberOfNewUsers = Convert.ToString(intthreenewcount);
                    updateFlg = true;
                }
                else
                {
                    intthreenewcount = Convert.ToInt64(dvusd.ThreeDayNumberOfNewUsers);
                }

                if (string.IsNullOrEmpty(dvusd.NumberOfNewUsersEgg1))
                {
                    integg1usercount = dbc.GetEgg1UserCount(strInput, strUITableName);
                    dvusd.NumberOfNewUsersEgg1 = Convert.ToString(integg1usercount);
                    updateFlg = true;
                }
                else
                {
                    integg1usercount = Convert.ToInt64(dvusd.NumberOfNewUsersEgg1);
                }

                if (string.IsNullOrEmpty(dvusd.DayNumberOfUsersKillInstallation))
                {
                    intkillusercount = dbc.GetKillUserCount(strInput, strDUTableName);
                    dvusd.DayNumberOfUsersKillInstallation = Convert.ToString(intkillusercount);
                    updateFlg = true;
                }
                else
                {
                    intkillusercount = Convert.ToInt64(dvusd.DayNumberOfUsersKillInstallation);
                }

                intAfterWeekcount = dbc.GetAfterWeekcount(strInput, strDUTableName);

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
                dr["weekACount"] = intAfterWeekcount;
                dr["weekACountp"] = ((double)intAfterWeekcount / (double)intdaycount).ToString("P"); ;

                intv112 = dbc.GetVCount(strInput, strDUTableName, "1000.0.0.112");
                v107 = dbc.GetVCount(strInput, strDUTableName, "1000.0.0.107");
                vother = dbc.GetNotVCount(strInput, strDUTableName, "1000.0.0.107", "1000.0.0.112"); ;
                dr["v112"] = intv112;
                dr["v107"] = v107;
                dr["vother"] = vother;

                table.Rows.Add(dr);

                if (string.IsNullOrEmpty(dvusd.UType) &&
                    string.IsNullOrEmpty(dvusd.UDate))
                {
                    dvusd.UType = strDBType;
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
    }
}