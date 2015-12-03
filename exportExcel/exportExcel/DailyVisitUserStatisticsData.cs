using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exportExcel
{
    public class DailyVisitUserStatisticsData
    {
        public int keys { get; set; }
        public string UType { get; set; }
        public string UDate { get; set; }
        public string TotalNumberOfDays { get; set; }
        public string DayNumberOfUsers { get; set; }
        public string NumberOfDaysNewUsers { get; set; }
        public string NextDayNumberOfNewUsers { get; set; }
        public string ThirdDayNumberOfNewUsers { get; set; }
        public string ThreeDayNumberOfNewUsers { get; set; }
        public string NumberOfNewUsersEgg1 { get; set; }
        public string DayNumberOfUsersKillInstallation { get; set; }
        public string TaskNumber { get; set; }
        public string TaskNumberOfSuccess { get; set; }
        public string Extension1 { get; set; }
        public string Extension2 { get; set; }
        public string Extension3 { get; set; }
        public string Extension4 { get; set; }
        public string Extension5 { get; set; }
        public DateTime createdate { get; set; }
        public DateTime updatedate { get; set; }
    }
}
