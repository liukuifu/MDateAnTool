using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;
using System.Threading.Tasks;

namespace MDataIm20
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
            string[] s;
            //List<MData> list = new List<MData>();
            MData md = null;
            TaskData td = null;
            DBConnect db = new DBConnect();
            DateTime dt = DateTime.Now;

            string strJson = string.Empty;
            string strDBType = args[0];
            //string strDBType = "task";
            string strFileName = args[1];
            //string strFileName = @"E:\导入数据\temp.eggdata.log.2015-10-29.001";
            string strTableName = string.Empty;
            string strDUTableName = string.Empty;
            string strUITableName = string.Empty;
            try
            {
                if ("go2.0".Equals(strDBType))
                {
                    strTableName = "Go20SourceData";
                    strDUTableName = "Go20DailyUser";
                    strUITableName = "Go20UserInfo";
                }
                //else if ("C#".Equals(strDBType))
                //{
                //    strTableName = "CSharp20SourceData";
                //}
                //else if ("killer".Equals(strDBType))
                //{
                //    strTableName = "Killer20SourceData";
                //}
                else if ("task".Equals(strDBType))
                {
                    strTableName = "Go20TaskSD";
                    strDUTableName = string.Empty;
                    strUITableName = string.Empty;
                }

                FileStream fs = new FileStream(strFileName, FileMode.Open);

                long fileLength = fs.Length;//文件流的长度

                fs.Close();

                // 读取开始位置
                long offset = 0x0; // 256 megabytes
                // 读取大小
                long length = 0x20000000; // 512 megabytes

                if (length > fileLength)
                {
                    length = fileLength;
                }
                //需要对文件读取的次数
                int readCount = (int)Math.Ceiling((double)(fileLength / length));
                //当前已经读取的次数

                int tempCount = 0;
                List<string> lsTemp = new List<string>();

                DataTable table = new DataTable();

                if ("task".Equals(strDBType))
                {
                    //为数据表创建相对应的数据列
                    table.Columns.Add("keys");
                    table.Columns.Add("udate");
                    table.Columns.Add("appid");
                    table.Columns.Add("channel");
                    table.Columns.Add("event");
                    table.Columns.Add("eggid");
                    table.Columns.Add("version");
                    table.Columns.Add("locale");
                    table.Columns.Add("os");
                    table.Columns.Add("uid");
                    table.Columns.Add("amd64");
                    table.Columns.Add("data_parameter");
                    table.Columns.Add("data_return");
                    table.Columns.Add("data_taskid");
                    table.Columns.Add("createdate");
                    table.Columns.Add("updatedate");
                }
                else
                {
                    //为数据表创建相对应的数据列
                    table.Columns.Add("keys");
                    table.Columns.Add("udate");
                    table.Columns.Add("id");
                    table.Columns.Add("appid");
                    table.Columns.Add("channel");
                    table.Columns.Add("event");
                    table.Columns.Add("eggid");
                    table.Columns.Add("locale");
                    table.Columns.Add("os");
                    table.Columns.Add("uid");
                    table.Columns.Add("version");
                    table.Columns.Add("amd64");
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
                    table.Columns.Add("browser");
                    table.Columns.Add("bversion");
                    table.Columns.Add("dotnet_1");
                    table.Columns.Add("dotnet_2");
                    table.Columns.Add("dotnet_3");
                    table.Columns.Add("dotnet_4");
                    table.Columns.Add("dotnet_5");
                    table.Columns.Add("dx");
                    table.Columns.Add("base");
                    table.Columns.Add("bios");
                    table.Columns.Add("disk");
                    table.Columns.Add("network");
                    table.Columns.Add("ie");
                    table.Columns.Add("kill");
                    table.Columns.Add("createdate");
                    table.Columns.Add("updatedate");
                }
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
                            strJson = string.Empty;
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
                            if ("task".Equals(strDBType))
                            {

                                if (!string.IsNullOrEmpty(line)
                                    && line.IndexOf("{") > 0
                                    && line.IndexOf("\"event\":\"taskresult") > 0)
                                {

                                    s = line.Split(' ');

                                    if (s.Length >= 7 && "req".Equals(s[5]))
                                    {
                                        strJson = line.Substring(line.IndexOf("{"));
                                        td = new TaskData();
                                        //创建数据行
                                        DataRow dr = table.NewRow();
                                        try
                                        {
                                            JsonSerializer serializer = new JsonSerializer();
                                            StringReader srt = new StringReader(strJson);

                                            td = serializer.Deserialize(new JsonTextReader(srt), typeof(TaskData)) as TaskData;
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
                                        if (td != null)
                                        {
                                            itemCount = itemCount + 1;
                                            try
                                            {

                                                dr["udate"] = Convert.ToDateTime(s[0] + " " + s[1]);
                                                dr["appid"] = td.Appid;

                                                dr["channel"] = td.Channel;

                                                if ((!string.IsNullOrEmpty(td.Channel)) && td.Channel.Length > 50)
                                                {
                                                    dr["channel"] = td.Channel.Remove(50);
                                                }
                                                else
                                                {
                                                    dr["channel"] = td.Channel == null ? "" : td.Channel;
                                                }

                                                dr["event"] = td.Event == null ? "" : td.Event;
                                                dr["eggid"] = td.Eggid == null ? "" : td.Eggid;
                                                dr["locale"] = td.Locale;
                                                dr["os"] = td.OS == null ? "" : td.OS;
                                                dr["uid"] = td.Uid == null ? "" : td.Uid;

                                                if ((!string.IsNullOrEmpty(td.Uid)) && td.Uid.Length > 50)
                                                {
                                                    LogHelper.writeDebugLog("md.Uid    > 50    : " + td.Uid);
                                                    dr["uid"] = td.Uid.Remove(50);
                                                }
                                                else
                                                {
                                                    dr["uid"] = td.Uid == null ? "" : td.Uid;
                                                }

                                                dr["version"] = td.Version == null ? "" : td.Version;

                                                if (td.Amd64)
                                                {
                                                    dr["amd64"] = 1;
                                                }
                                                else
                                                {
                                                    dr["amd64"] = 0;
                                                }

                                                if (td.Data != null)
                                                {
                                                    dr["data_parameter"] = td.Data.Parameter == null ? "" : td.Data.Parameter;

                                                    //LogHelper.writeErrorLog("td.Data.Return = " + td.Data.Return);
                                                    if (string.IsNullOrEmpty(td.Data.Return) )
                                                    {
                                                        dr["data_return"] = "";
                                                    }
                                                    else
                                                    {
                                                        if("0".Equals(td.Data.Return) || td.Data.Return == "-1")
                                                        {
                                                            dr["data_return"] = td.Data.Return;
                                                        }
                                                        else
                                                        {
                                                            //LogHelper.writeErrorLog("Convert.ToInt64(td.Data.Return) = " + Convert.ToInt64(td.Data.Return));
                                                            //LogHelper.writeErrorLog("(Convert.ToInt64(td.Data.Return)).ToString(X) = " + (Convert.ToInt64(td.Data.Return)).ToString("X"));
                                                            //LogHelper.writeErrorLog("((Convert.ToInt64(td.Data.Return)).ToString(X)).ToString() = " + ((Convert.ToInt64(td.Data.Return)).ToString("X")).ToString());
                                                            dr["data_return"] = "0x"+((Convert.ToInt64(td.Data.Return)).ToString("X")).ToString();
                                                        }
                                                    }
                                                    //dr["data_return"] = td.Data.Return == null ? "" : td.Data.Return;
                                                    dr["data_taskid"] = td.Data.Taskid == null ? "" : td.Data.Taskid;

                                                }

                                                dr["createdate"] = dt;
                                                dr["updatedate"] = dt;

                                                //将创建的数据行添加到table中
                                                table.Rows.Add(dr);

                                            }
                                            catch (Exception ex)
                                            {
                                                LogHelper.writeErrorLog("error: itemCount = " + itemCount);
                                                LogHelper.writeErrorLog("error: s[0]+s[1] = " + s[0] + s[1]);
                                                LogHelper.writeErrorLog(ex);
                                                continue;
                                            }
                                        }
                                    }
                                }
                            }
                            else 
                            {

                                if (!string.IsNullOrEmpty(line)
                                    && line.IndexOf("{") > 0
                                    && line.IndexOf("\"event\":\"checkupdate") > 0)
                                {

                                    s = line.Split(' ');

                                    if (s.Length >= 7 && "req".Equals(s[5]))
                                    {
                                        strJson = line.Substring(line.IndexOf("{"));
                                        md = new MData();
                                        //创建数据行
                                        DataRow dr = table.NewRow();
                                        try
                                        {
                                            JsonSerializer serializer = new JsonSerializer();
                                            StringReader srt = new StringReader(strJson);

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
                                            itemCount = itemCount + 1;
                                            try
                                            {
                                                //md.Date = long.Parse(s[0]);
                                                //md.Id = s[1];

                                                //往对应的 行中添加数据

                                                //DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                                                //long lTime = long.Parse(((long.Parse(s[0])) / 1000000000).ToString() + "0000000");
                                                //TimeSpan toNow = new TimeSpan(lTime);
                                                //DateTime dtResult = dtStart.Add(toNow);

                                                //dr["udate"] = dtResult.AddHours(-8);// Convert.ToDateTime(md.Date);
                                                dr["udate"] = Convert.ToDateTime(s[0] + " " + s[1]);
                                                dr["id"] = "";
                                                dr["appid"] = md.Appid;

                                                dr["channel"] = md.Channel;

                                                if ((!string.IsNullOrEmpty(md.Channel)) && md.Channel.Length > 50)
                                                {
                                                    dr["channel"] = md.Channel.Remove(50);
                                                }
                                                else
                                                {
                                                    dr["channel"] = md.Channel == null ? "" : md.Channel;
                                                }

                                                dr["event"] = md.Event == null ? "" : md.Event;
                                                dr["eggid"] = md.Eggid == null ? "" : md.Eggid;
                                                dr["locale"] = md.Locale;
                                                dr["os"] = md.OS == null ? "" : md.OS;
                                                dr["uid"] = md.Uid == null ? "" : md.Uid;

                                                if ((!string.IsNullOrEmpty(md.Uid)) && md.Uid.Length > 50)
                                                {
                                                    LogHelper.writeDebugLog("md.Uid    > 50    : " + md.Uid);
                                                    dr["uid"] = md.Uid.Remove(50);
                                                }
                                                else
                                                {
                                                    dr["uid"] = md.Uid == null ? "" : md.Uid;
                                                }

                                                dr["version"] = md.Version == null ? "" : md.Version;
                                                if (md.Amd64)
                                                {
                                                    dr["amd64"] = 1;
                                                }
                                                else
                                                {
                                                    dr["amd64"] = 0;
                                                }

                                                for (int i = 1; i <= 5; i++)
                                                {
                                                    dr["antivirus_guid_" + i] = "";
                                                    dr["antivirus_name_" + i] = "";
                                                    dr["dotnet_" + i] = "";
                                                }
                                                if (md.Data != null)
                                                {
                                                    if (md.Data.Antivirus != null && md.Data.Antivirus.Count > 0)
                                                    {
                                                        for (int i = 0; i < md.Data.Antivirus.Count; i++)
                                                        {
                                                            if (i < 5)
                                                            {
                                                                dr["antivirus_guid_" + (i + 1)] = md.Data.Antivirus[i].guid;

                                                                if (md.Data.Antivirus[i].name.Length > 50)
                                                                {
                                                                    dr["antivirus_name_" + (i + 1)] = md.Data.Antivirus[i].name.Remove(50);
                                                                }
                                                                else
                                                                {
                                                                    dr["antivirus_name_" + (i + 1)] = md.Data.Antivirus[i].name;
                                                                }
                                                            }
                                                        }
                                                    }

                                                    dr["browser"] = md.Data.browser;
                                                    dr["bversion"] = md.Data.bversion;
                                                    if (md.Data.dotnet != null)
                                                    {
                                                        for (int i = 0; i < md.Data.dotnet.Count; i++)
                                                        {
                                                            if (i < 5)
                                                            {
                                                                dr["dotnet_" + (i + 1)] = md.Data.dotnet[i];
                                                            }
                                                        }
                                                    }

                                                    dr["dx"] = md.Data.dx;
                                                    if (md.Data.hardware != null)
                                                    {
                                                        dr["base"] = md.Data.hardware.Base;
                                                        dr["bios"] = md.Data.hardware.Bios;
                                                        dr["disk"] = md.Data.hardware.Disk;
                                                        dr["network"] = md.Data.hardware.Network;
                                                    }

                                                    dr["ie"] = md.Data.ie;
                                                    dr["kill"] = md.Data.kill;

                                                }

                                                dr["createdate"] = dt;
                                                dr["updatedate"] = dt;

                                                //将创建的数据行添加到table中
                                                table.Rows.Add(dr);

                                            }
                                            catch (Exception ex)
                                            {
                                                LogHelper.writeErrorLog("error: itemCount = " + itemCount);
                                                LogHelper.writeErrorLog("error: s[0]+s[1] = " + s[0] + s[1]);
                                                LogHelper.writeErrorLog(ex);
                                                continue;
                                            }
                                        }

                                    }
                                }
                            }

                            if (itemCount != 0 && itemCount % 1000 == 0)
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

                        string t = lsTemp[lsTemp.Count - 1];
                        offset = offset + length - t.Length;
                        if (tempCount == readCount - 1)
                        {
                            length = fileLength - offset;
                        }
                        if (tempCount < readCount)
                        {
                            lsTemp.RemoveAt(lsTemp.Count - 1);
                        }
                    }

                    tempCount = tempCount + 1;
                }
                while (tempCount <= readCount);

                //if ("go".Equals(strDBType))
                //{
                //    string strInputDateTemp = strFileName.Substring(strFileName.Length - 10);
                //    string strInputDate = strInputDateTemp.Substring(strInputDateTemp.Length - 4) +
                //        "-" + strInputDateTemp.Substring(strInputDateTemp.Length - 7, 2) +
                //        "-" + strInputDateTemp.Substring(strInputDateTemp.Length - 10, 2);

                //    // 向表(DailyUser)中插入数据
                //    Console.WriteLine("DailyUser Insert Start.");
                //    int intInsertDU = db.InsertDailyUser(strInputDate, strTableName, strDUTableName);
                //    Console.WriteLine("DailyUser Insert Count = " + intInsertDU);
                //    Console.WriteLine("DailyUser Insert End.");

                //    if (intInsertDU > 0)
                //    {
                //        // 更新表(DailyUser)中数据
                //        Console.WriteLine("DailyUser Update Start.");
                //        int intUpdateDU = db.UpdateDailyUser(strInputDate, strTableName, strDUTableName);
                //        Console.WriteLine("DailyUser Update Count = " + intInsertDU);
                //        Console.WriteLine("DailyUser Update End.");

                //        Console.WriteLine("UserInfo Insert Start.");
                //        // 向表(UserInfo)中插入数据
                //        int intInsertUI = db.InsertUserInfo(strInputDate, strDUTableName, strUITableName);
                //        Console.WriteLine("UserInfo Insert Count = " + intInsertUI);
                //        Console.WriteLine("UserInfo Insert End.");
                //    }
                //}

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
