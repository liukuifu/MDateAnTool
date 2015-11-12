using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using log4net;
using System.Reflection;
using System.Data;
using System.IO.MemoryMappedFiles;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            LogHelper.writeInfoLog("Main Start");
            if (args == null)
            {
                Console.WriteLine("请输入数据类型名和数据文件");
                LogHelper.writeWarnLog("请输入数据类型名和数据文件");
                return;
            }

            if (args.Length < 2)
            {
                Console.WriteLine("请输入数据类型名和数据文件");
                LogHelper.writeWarnLog("请输入数据类型名和数据文件");
                return;
            }
            if (string.IsNullOrEmpty(args[0]))
            {
                Console.WriteLine("数据类型名不能为空");
                LogHelper.writeWarnLog("数据类型名不能为空");
                return;
            }

            if (string.IsNullOrEmpty(args[1]))
            {
                Console.WriteLine("数据文件不能为空");
                LogHelper.writeWarnLog("数据文件不能为空");
                return;
            }

            Int64 itemCount = 0;
            int intSourceDataCount = 0;
            int intInsertDU = 0;
            int intInsertUI = 0;

            string[] s;
            //List<MData> list = new List<MData>();
            MData md = null;
            DBConnect db = new DBConnect();
            DateTime dt = DateTime.Now;

            string strDBType = args[0];
            string strFileName = args[1];
            string strTableName = string.Empty;
            string strDUTableName = string.Empty;
            string strUITableName = string.Empty;
            //string strFileName = @"E:\数据\mdata.log.15.10.2015";
            try
            {
                if ("go".Equals(strDBType))
                {
                    strTableName = "GoSourceData";
                    strDUTableName = "GoDailyUser";
                    strUITableName = "UserInfo";
                    //strTableName = "GoSourceDataHW";
                }
                else if ("C#".Equals(strDBType))
                {
                    strTableName = "CSharpSourceData";
                    strDUTableName = "CsDailyUser";
                    strUITableName = "CsUserInfo";
                }
                else if ("killer".Equals(strDBType))
                {
                    strTableName = "KillerSourceData";
                    strDUTableName = "KillerDailyUser";
                    strUITableName = "KillerUserInfo";
                }
                //strTableName = "GoSourceDataTemp";

                //strTableName = "GoSourceData";

                FileStream fs = new FileStream(strFileName, FileMode.Open);
                //FileStream fs = new FileStream(@"E:\数据\mdata.log.11.10.2015", FileMode.Open);
                //FileStream fs = new FileStream(@"E:\数据\temp.mdata.log.11.10.2015", FileMode.Open);
                //FileStream fs = new FileStream(@"E:\数据\temp2.mdata.log.11.10.2015", FileMode.Open);

                long fileLength = fs.Length;//文件流的长度
                
                fs.Close();

                //long offset = 0x10000000; // 256 megabytes
                //long length = 0x20000000; // 512 megabytes

                // 读取开始位置
                long offset = 0x0; // 256 megabytes
                // 读取大小
                long length = 0x20000000; // 512 megabytes
                //long length = 0x5000; // 512 megabytes
                if(length> fileLength)
                {
                    length = fileLength;
                }
                //需要对文件读取的次数
                int readCount = (int)Math.Ceiling((double)(fileLength / length));
                //当前已经读取的次数

                int tempCount = 0;
                List<string> lsTemp = new List<string>();

                DataTable table = new DataTable();
                //为数据表创建相对应的数据列
                table.Columns.Add("udate");
                table.Columns.Add("id");
                table.Columns.Add("appid");
                table.Columns.Add("channel");
                table.Columns.Add("event");
                table.Columns.Add("langid");
                table.Columns.Add("locale");
                table.Columns.Add("os");
                table.Columns.Add("uid");
                table.Columns.Add("version");
                table.Columns.Add("antivirus_guid_1");
                table.Columns.Add("antivirus_name_1");
                table.Columns.Add("antivirus_guid_2");
                table.Columns.Add("antivirus_name_2");
                table.Columns.Add("antivirus_guid_3");
                table.Columns.Add("antivirus_name_3");
                table.Columns.Add("antivirus_guid_4");
                table.Columns.Add("antivirus_name_4");
                table.Columns.Add("antivirus_guid_5");
                table.Columns.Add("antivirus_name_5");

                //table.Columns.Add("hardwareinfo");

                table.Columns.Add("createdate");
                table.Columns.Add("updatedate");

                String line;

                do
                {
                    // 建立缓存文件
                    using (var mmf = MemoryMappedFile.CreateFromFile(strFileName, FileMode.Open))
                    //using (var mmf = MemoryMappedFile.CreateFromFile(@"E:\数据\mdata.log.11.10.2015", FileMode.Open))
                    //using (var mmf = MemoryMappedFile.CreateFromFile(@"E:\数据\temp.mdata.log.11.10.2015", FileMode.Open))
                    //using (var mmf = MemoryMappedFile.CreateFromFile(@"E:\数据\temp2.mdata.log.11.10.2015", FileMode.Open))

                    using (var stream = mmf.CreateViewStream(offset, length, MemoryMappedFileAccess.Read))
                    using (var reader = new StreamReader(stream, Encoding.UTF8))
                    {

                        do
                        {
                            //string t = reader.ReadLine();
                            line = string.Empty;
                            try
                            {
                                line = reader.ReadLine();
                            }
                            catch (OutOfMemoryException oome)
                            {                                
                                LogHelper.writeErrorLog("error: lsTemp = " + lsTemp.Count);
                                LogHelper.writeErrorLog(oome);

                                continue;
                            }
                            lsTemp.Add(line);
                            //lsTemp.Add(reader.ReadLine());

                            s = line.Split('#');
                            if (s.Length == 3)
                            {
                                md = new MData();
                                //创建数据行
                                DataRow dr = table.NewRow();
                                try
                                {
                                    JsonSerializer serializer = new JsonSerializer();
                                    StringReader srt = new StringReader(s[2]);
                                    md = serializer.Deserialize(new JsonTextReader(srt), typeof(MData)) as MData;
                                    srt.Close();
                                }
                                catch (JsonException je)
                                {
                                    LogHelper.writeDebugLog("debug: " + line);
                                    LogHelper.writeErrorLog("error: itemCount = " + itemCount);
                                    LogHelper.writeErrorLog(je);

                                    continue;
                                }
                                catch (OutOfMemoryException oome)
                                {
                                    LogHelper.writeDebugLog("debug: " + line);
                                    LogHelper.writeErrorLog("error: itemCount = " + itemCount);
                                    LogHelper.writeErrorLog(oome);

                                    continue;
                                }
                                catch (Exception ex)
                                {
                                    LogHelper.writeErrorLog("error: itemCount = " + itemCount);
                                    LogHelper.writeErrorLog("error: s[0] = " + s[0]);
                                    LogHelper.writeErrorLog(ex);
                                    continue;
                                }
                                if (md != null)
                                {
                                    if ("visit".Equals(md.Event) || "heartbeat".Equals(md.Event))
                                    {
                                        itemCount = itemCount + 1;
                                        try
                                        {
                                            md.Date = long.Parse(s[0]);
                                            md.Id = s[1];
                                            //list.Add(md);
                                            //往对应的 行中添加数据

                                            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                                            long lTime = long.Parse(((long.Parse(s[0])) / 1000000000).ToString() + "0000000");
                                            TimeSpan toNow = new TimeSpan(lTime);
                                            DateTime dtResult = dtStart.Add(toNow);

                                            //dr["udate"] = dtResult.AddHours(-8);// Convert.ToDateTime(md.Date);
                                            dr["udate"] = dtResult;// Convert.ToDateTime(md.Date);
                                            dr["id"] = s[1];
                                            dr["appid"] = md.Appid;

                                            dr["channel"] = md.Channel;

                                            if (md.Channel.Length > 50)
                                            {
                                                dr["channel"] = md.Channel.Remove(50);
                                            }
                                            else
                                            {
                                                dr["channel"] = md.Channel;
                                            }

                                            dr["event"] = md.Event == null ? "" : md.Event;
                                            dr["langid"] = md.Langid == null ? "" : md.Langid;
                                            dr["locale"] = md.Locale;
                                            //dr["os"] = md.OS == null ? "" : md.OS;
                                            if (md.OS != null && md.OS.Length > 50)
                                            {
                                                LogHelper.writeDebugLog("md.Uid    > 50    : " + md.OS);
                                                dr["os"] = md.OS.Remove(50);
                                            }
                                            else
                                            {
                                                dr["os"] = md.OS == null ? "" : md.OS;
                                            }

                                            dr["uid"] = md.Uid == null ? "" : md.Uid;

                                            if (md.Uid.Length > 50)
                                            {
                                                LogHelper.writeDebugLog("md.Uid    > 50    : " + md.Uid);
                                                dr["uid"] = md.Uid.Remove(50);
                                            }
                                            else
                                            {
                                                dr["uid"] = md.Uid;
                                            }

                                            dr["version"] = md.Version == null ? "" : md.Version;

                                            for (int i = 1; i <= 5; i++)
                                            {
                                                dr["antivirus_guid_" + i] = "";
                                                dr["antivirus_name_" + i] = "";
                                            }

                                            if (md.Antivirus != null && md.Antivirus.Count > 0)
                                            {
                                                for (int i = 0; i < md.Antivirus.Count; i++)
                                                {
                                                    if (i < 5)
                                                    {
                                                        dr["antivirus_guid_" + (i + 1)] = md.Antivirus[i].guid;

                                                        if (md.Antivirus[i].name.Length > 50)
                                                        {
                                                            dr["antivirus_name_" + (i + 1)] = md.Antivirus[i].name.Remove(50);//.Substring(0,50);
                                                        }
                                                        else
                                                        {
                                                            dr["antivirus_name_" + (i + 1)] = md.Antivirus[i].name;
                                                        }
                                                    }
                                                }
                                                //md.antivirusKey = db.InsertAntivirusList(md.Antivirus);
                                            }

                                            //dr["hardwareinfo"] = md.Hardwareinfo == null ? "" : md.Hardwareinfo;

                                            dr["createdate"] = dt;
                                            dr["updatedate"] = dt;

                                            //将创建的数据行添加到table中
                                            table.Rows.Add(dr);

                                        }
                                        catch (Exception ex)
                                        {
                                            LogHelper.writeErrorLog("error: itemCount = " + itemCount);
                                            LogHelper.writeErrorLog("error: s[0] = " + s[0]);
                                            LogHelper.writeErrorLog(ex);
                                            continue;
                                        }
                                    }
                                }
                            }

                            if (itemCount!=0 && itemCount % 1000 == 0)
                            {
                                try
                                {
                                    db.InsertTable(table, strTableName);
                                }
                                catch (Exception ex)
                                {
                                    LogHelper.writeErrorLog("itemCount : " + itemCount);
                                    LogHelper.writeErrorLog(ex);
                                    continue;
                                }
                                table.Clear();
                            }
                        } while (!reader.EndOfStream);

                        if (itemCount % 1000 > 0)
                        {
                            db.InsertTable(table, strTableName);
                            table.Clear();
                            itemCount = 0;
                        }

                        string t = lsTemp[lsTemp.Count-1];
                        offset = offset + length - t.Length;
                        if (tempCount == readCount-1)
                        {
                            length = fileLength - offset;
                        }
                        if(tempCount< readCount)
                        {
                            lsTemp.RemoveAt(lsTemp.Count - 1);
                        }
                    }

                    tempCount = tempCount + 1;
                }
                while (tempCount <= readCount);

                string strInputDateTemp = strFileName.Substring(strFileName.Length - 10);
                string strInputDate = strInputDateTemp.Substring(strInputDateTemp.Length - 4) +
                    "-" + strInputDateTemp.Substring(strInputDateTemp.Length - 7, 2) +
                    "-" + strInputDateTemp.Substring(strInputDateTemp.Length - 10, 2);

                // 取得(SourceData)单日件数
                Console.WriteLine("GetSourceDataCount Start.");
                intSourceDataCount = db.GetSourceDataCount(strInputDate, strTableName);
                Console.WriteLine("GetSourceDataCount Count = " + intSourceDataCount);
                Console.WriteLine("GetSourceDataCount Insert End.");

                if (intSourceDataCount > 0)
                {
                    // 向表(DailyUser)中插入数据
                    Console.WriteLine("DailyUser Insert Start.");
                    intInsertDU = db.InsertDailyUser(strInputDate, strTableName, strDUTableName);
                    Console.WriteLine("DailyUser Insert Count = " + intInsertDU);
                    Console.WriteLine("DailyUser Insert End.");

                    if (intInsertDU > 0)
                    {
                        Console.WriteLine("UserInfo Insert Start.");
                        // 向表(UserInfo)中插入数据
                        intInsertUI = db.InsertUserInfo(strInputDate, strDUTableName, strUITableName);
                        Console.WriteLine("UserInfo Insert Count = " + intInsertUI);
                        Console.WriteLine("UserInfo Insert End.");
                    }
                }

                Console.WriteLine("InsertDailyVisitUserStatistics Start.");
                int intCount = db.InsertDailyVisitUserStatistics(strDBType, strInputDate, intSourceDataCount, intInsertDU, intInsertUI);
                Console.WriteLine("InsertDailyVisitUserStatistics Count = " + intCount);
                Console.WriteLine("InsertDailyVisitUserStatistics End.");
            }
            catch (Exception ex)
            {
                LogHelper.writeErrorLog(ex);
            }

            //db.InsertList(list);

            LogHelper.writeInfoLog("Main End");
        }
    }
}
