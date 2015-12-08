using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exportExcel
{
    public static class Common
    {
        /// <summary>
        /// 计算本周的周一日期
        /// </summary>
        /// <returns></returns>
        public static DateTime getmondaydate()
        {
            return getmondaydate(DateTime.Now);
        }

        /// <summary>
        /// 计算本周周日的日期
        /// </summary>
        /// <returns></returns>
        public static DateTime getsundaydate()
        {
            return getsundaydate(DateTime.Now);
        }

        /// <summary>
        /// 计算某日起始日期（礼拜一的日期）
        /// </summary>
        /// <param name="somedate">该周中任意一天</param>
        /// <returns>返回礼拜一日期，后面的具体时、分、秒和传入值相等</returns>
        public static DateTime getmondaydate(DateTime somedate)
        {
            int i = somedate.DayOfWeek - DayOfWeek.Monday;
            if (i == -1) i = 6;// i值 > = 0 ，因为枚举原因，sunday排在最前，此时sunday-monday=-1，必须+7=6。
            TimeSpan ts = new TimeSpan(i, 0, 0, 0);
            return somedate.Subtract(ts);
        }

        /// <summary>
        /// 计算某日结束日期（礼拜日的日期）
        /// </summary>
        /// <param name="somedate">该周中任意一天</param>
        /// <returns>返回礼拜日日期，后面的具体时、分、秒和传入值相等</returns>
        public static DateTime getsundaydate(DateTime somedate)
        {
            int i = somedate.DayOfWeek - DayOfWeek.Sunday;
            if (i != 0) i = 7 - i;// 因为枚举原因，sunday排在最前，相减间隔要被7减。
            TimeSpan ts = new TimeSpan(i, 0, 0, 0);
            return somedate.Add(ts);
        }
    }
}
