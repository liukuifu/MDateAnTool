using Microsoft.VisualStudio.TestTools.UnitTesting;
using MDataIm20Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDataIm20Update.Tests
{
    [TestClass()]
    public class DBConnectTests
    {
        [TestMethod()]
        public void InsertTaskInfoTest()
        {
            DBConnect dbc = new DBConnect();
            int intCount = dbc.InsertTaskInfo("2015-11-12", "Go20TaskSD", "Go20TaskInfo");
            Assert.AreNotEqual(intCount,0);
        }
    }
}