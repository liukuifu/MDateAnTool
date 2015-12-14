using Microsoft.VisualStudio.TestTools.UnitTesting;
using MDAutoImport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDAI.Tests
{
    [TestClass()]
    public class MDAITests
    {
        [TestMethod()]
        public void FileToTable30Test()
        {
            LogHelper.writeInfoLog("FileToTable30Test Start");
            Program pg = new Program();
            //bool flg = pg.FileToTable30("go3.0", "2015-12-08", @"E:\temp\test");
            LogHelper.writeInfoLog("FileToTable30Test End");
            //bool flg = pg.FileToTable30("C#3.0", "2015-12-07", @"E:\temp\test");
            //flg = Program.FileToTable30ForTask("task3.0", "2015-12-03", @"E:\temp\test");
            //Assert.AreEqual(true, flg);
            //Assert.Fail();
        }
    }
}