using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA1Test.Tests
{
    [TestClass()]
    public class DBConnectTests
    {
        [TestMethod()]
        public void InserDailyVisitUserStatisticsTest()
        {

            DBConnect db = new DBConnect();

            int intCount = db.InsertDailyVisitUserStatistics("go","2015-01-01",1234567,234567,34567);
            Assert.AreEqual(intCount, 1);
        }
    }
}