using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ionic.Zip;
using System.Collections;
using System.Data;
using System.IO.MemoryMappedFiles;
using Newtonsoft.Json;
using System.Configuration;

namespace MDAutoImport
{
    class Program
    {
        //private static object lonic;

        private static string DownLoadPath = ConfigurationManager.AppSettings["DownLoadPath"];
        private static string UnZipPath = ConfigurationManager.AppSettings["UnZipPath"];
        private static int ZipCount = Convert.ToInt32(ConfigurationManager.AppSettings["ZipCount"]);
        static void Main(string[] args)
        {
            LogHelper.writeInfoLog("Main Start");
            DateTime dt = DateTime.Now.AddDays(-1);
            Dictionary<string, string> hm = new Dictionary<string, string>();
            Hashtable ht = new Hashtable();
            try {

                string DlFullPath = DownLoadPath + "\\" + dt.ToString("yyyy-MM-dd");
                string UnFullPath = UnZipPath + "\\" + dt.ToString("yyyy-MM-dd");

                //string DlFullPath = CommonResource.DownLoadPath.ToString() + "\\" + dt.ToString("yyyy-MM-dd");
                //string UnFullPath = CommonResource.UnZipPath.ToString() + "\\" + dt.ToString("yyyy-MM-dd");
                
                LogHelper.writeDebugLog("DlFullPath : " + DlFullPath);
                LogHelper.writeDebugLog("UnFullPath : " + UnFullPath);

                //判断文件路径是否存在，不存在则创建文件夹
                if (!Directory.Exists(UnFullPath))
                {
                    Directory.CreateDirectory(UnFullPath);//不存在就创建目录
                }
                //for (;;)
                for (int countIndex = 0; countIndex < 10000; countIndex++)
                {
                    //获取目录parentDir下的所有的文件，并过滤得到所有的文本文件
                    //string[] file = Directory.GetFiles(DlFullPath, ".zip");
                    string[] file = Directory.GetFiles(DlFullPath);
                    for (int i = 0; i < file.Length; i++)
                    {
                        //FileInfo fi = new FileInfo(file[i]);
                        //if (fi.Extension.ToLower() == "txt")
                        //{
                        LogHelper.writeDebugLog("file [" + i + "] : " + file[i]);
                        if (file[i].EndsWith(".zip"))
                        {
                            if (!ht.ContainsKey(file[i]))
                            {
                                //hm.Add(file[i], DlFullPath + "/" + file[i]);
                                LogHelper.writeDebugLog("Add file [" + i + "] : " + file[i]);
                                ht.Add(file[i], DlFullPath + "\\" + file[i]);
                                string fileName = string.Empty;
                                string unZipPath = string.Empty;
                                bool retF = UnZip(file[i], DlFullPath + "\\" + file[i], UnFullPath,out fileName,out unZipPath);
                                if (retF)
                                {
                                    ImportData(fileName, unZipPath);
                                }
                                
                                if (ht.Count == ZipCount)
                                //if (ht.Count == Convert.ToInt32(CommonResource.ZipCount))
                                //if (ht.Count == 3)
                                {
                                    break;
                                }
                            }
                        }
                        //}                
                    }
                    //if (ht.Count == Convert.ToInt32(CommonResource.ZipCount))
                    if (ht.Count == ZipCount)
                    {
                        break;
                    }
                    //Thread.Sleep(60000);
                    Thread.Sleep(30000);
                }

                //Hashtable imht = new Hashtable();

                //foreach (DictionaryEntry de in ht)//i = 0; i < hm.Count(); i++)
                //{
                //    string fileFullName = de.Key.ToString();
                //    string fileName = fileFullName.Substring(fileFullName.LastIndexOf("\\") + 1);
                //    string unZipToPath = UnFullPath + "\\" + fileName.Substring(0, fileName.IndexOf(".zip"));

                //    //判断文件路径是否存在，不存在则创建文件夹
                //    if (!Directory.Exists(unZipToPath))
                //    {
                //        Directory.CreateDirectory(unZipToPath);//不存在就创建目录
                //    }
                //    LogHelper.writeDebugLog("fileName : " + fileName);
                //    LogHelper.writeDebugLog("fileFullName : " + fileFullName);
                //    LogHelper.writeDebugLog("unZipToPath : " + unZipToPath);
                //    try
                //    {
                //        using (ZipFile zip = ZipFile.Read(fileFullName))
                //        {   //遍历zip文件中每一个文件对象，然后解压到指定目录
                //            foreach (ZipEntry e in zip)
                //            {
                //                e.Extract(unZipToPath);
                //            }
                //            //也可以通过索引访问文件对象
                //            //ZipEntry e = zip["MyReport.doc"];
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        LogHelper.writeErrorLog(ex);
                //        continue;
                //    }

                //    imht.Add(fileName, unZipToPath);
                //}
                ////imht.Add("cegg20151129.zip", @"E:\MD\UnZipFile\2015-12-01\cegg20151201");
                ////imht.Add("egg20151129.zip", @"E:\MD\UnZipFile\2015-12-01\egg20151201");
                ////imht.Add("kegg20151129.zip", @"E:\MD\UnZipFile\2015-12-01\kegg20151201");
                //bool flg = ImportData(imht);

                LogHelper.writeInfoLog("Main End");
            }catch(Exception ex)
            {
                LogHelper.writeErrorLog(ex);
            }
        }

        private static bool UnZip(string deKey, string v2,string UnFullPath,out string fileName,out string unZipToPath)
        {
            string fileFullName = deKey;
            fileName = fileFullName.Substring(fileFullName.LastIndexOf("\\") + 1);
            unZipToPath = UnFullPath + "\\" + fileName.Substring(0, fileName.IndexOf(".zip"));

            //判断文件路径是否存在，不存在则创建文件夹
            if (!Directory.Exists(unZipToPath))
            {
                Directory.CreateDirectory(unZipToPath);//不存在就创建目录
            }
            LogHelper.writeDebugLog("fileName : " + fileName);
            LogHelper.writeDebugLog("fileFullName : " + fileFullName);
            LogHelper.writeDebugLog("unZipToPath : " + unZipToPath);
            try
            {
                using (ZipFile zip = ZipFile.Read(fileFullName))
                {   //遍历zip文件中每一个文件对象，然后解压到指定目录
                    foreach (ZipEntry e in zip)
                    {
                        e.Extract(unZipToPath);
                    }
                    //也可以通过索引访问文件对象
                    //ZipEntry e = zip["MyReport.doc"];
                }
            }
            catch (Exception ex)
            {
                LogHelper.writeErrorLog(ex);
                return false;
            }

            //imht.Add(fileName, unZipToPath);
            return true;
        }

        private static bool ImportData(Hashtable imht)
        {
            LogHelper.writeDebugLog("ImportData 1 start");
            bool retFlag = true;
            DateTime dt = DateTime.Now.AddDays(-1);
            foreach (DictionaryEntry de in imht)
            {
                LogHelper.writeDebugLog("de.Key : " + de.Key);
                LogHelper.writeDebugLog("de.Value : " + de.Value);

                //de.Key : mdata.csharp.26.zip
                //de.Value : E:\MD\UnZipFile\2015 - 11 - 26\mdata.csharp.26
                if (de.Key.ToString().IndexOf("mdata.csharp") == 0)
                {
                    retFlag = FileToTable10("C#", dt.ToString("yyyy-MM-dd"), de.Value.ToString());
                }
                //de.Key : mdata.killer.26.zip
                //de.Value : E:\MD\UnZipFile\2015 - 11 - 26\mdata.killer.26
                else if (de.Key.ToString().IndexOf("mdata.killer") == 0)
                {
                    retFlag = FileToTable10("killer", dt.ToString("yyyy-MM-dd"), de.Value.ToString());
                }
                //de.Key : mdata.go.26.zip
                //de.Value : E:\MD\UnZipFile\2015 - 11 - 26\mdata.go.26
                else if (de.Key.ToString().IndexOf("mdata.go") == 0)
                {
                    retFlag = FileToTable10("go", dt.ToString("yyyy-MM-dd"), de.Value.ToString());
                }
                //de.Key : kegg20151126.zip
                //de.Value : E:\MD\UnZipFile\2015 - 11 - 26\kegg20151126
                else if (de.Key.ToString().IndexOf("kegg") == 0)
                {
                    retFlag = FileToTable20("killer2.0", dt.ToString("yyyy-MM-dd"), de.Value.ToString());
                }
                //de.Key : cegg20151126.zip
                //de.Value : E:\MD\UnZipFile\2015 - 11 - 26\cegg20151126
                else if (de.Key.ToString().IndexOf("cegg") == 0)
                {
                    retFlag = FileToTable20("C#2.0", dt.ToString("yyyy-MM-dd"), de.Value.ToString());
                }
                //de.Key : egg20151126.zip
                //de.Value : E:\MD\UnZipFile\2015 - 11 - 26\egg20151126
                else
                {
                    retFlag = FileToTable20("go2.0", dt.ToString("yyyy-MM-dd"), de.Value.ToString());

                    retFlag = FileToTable20ForTask("task2.0", dt.ToString("yyyy-MM-dd"), de.Value.ToString());
                }

            }
            LogHelper.writeDebugLog("ImportData end");
            return retFlag;
        }

        private static bool ImportData(string strFileName,string strFilePath)
        {
            LogHelper.writeDebugLog("ImportData 2 start");
            bool retFlag = true;
            DateTime dt = DateTime.Now.AddDays(-1);

            //strFileName : mdata.csharp.26.zip
            //strFilePath : E:\MD\UnZipFile\2015 - 11 - 26\mdata.csharp.26
            if (strFileName.ToString().IndexOf("mdata.csharp") == 0)
            {
                retFlag = FileToTable10("C#", dt.ToString("yyyy-MM-dd"), strFilePath);
            }
            //strFileName : mdata.killer.26.zip
            //strFilePath : E:\MD\UnZipFile\2015 - 11 - 26\mdata.killer.26
            else if (strFileName.ToString().IndexOf("mdata.killer") == 0)
            {
                retFlag = FileToTable10("killer", dt.ToString("yyyy-MM-dd"), strFilePath);
            }
            //strFileName : mdata.go.26.zip
            //strFilePath : E:\MD\UnZipFile\2015 - 11 - 26\mdata.go.26
            else if (strFileName.ToString().IndexOf("mdata.go") == 0)
            {
                retFlag = FileToTable10("go", dt.ToString("yyyy-MM-dd"), strFilePath);
            }
            //strFileName : kegg20151126.zip
            //strFilePath : E:\MD\UnZipFile\2015 - 11 - 26\kegg20151126
            else if (strFileName.ToString().IndexOf("kegg") == 0)
            {
                retFlag = FileToTable20("killer2.0", dt.ToString("yyyy-MM-dd"), strFilePath);
            }
            //strFileName : cegg20151126.zip
            //strFilePath : E:\MD\UnZipFile\2015 - 11 - 26\cegg20151126
            else if (strFileName.ToString().IndexOf("cegg") == 0)
            {
                retFlag = FileToTable20("C#2.0", dt.ToString("yyyy-MM-dd"), strFilePath);
            }
            //strFileName : egg20151126.zip
            //strFilePath : E:\MD\UnZipFile\2015 - 11 - 26\egg20151126
            else
            {
                retFlag = FileToTable20("go2.0", dt.ToString("yyyy-MM-dd"), strFilePath);

                retFlag = FileToTable20ForTask("task2.0", dt.ToString("yyyy-MM-dd"), strFilePath);
            }

            LogHelper.writeDebugLog("ImportData 2 end");
            return retFlag;
        }

        private static bool FileToTable10(string strDBType, string strImportDate, string strPath)
        {
            LogHelper.writeDebugLog("FileToTable10 start");
            LogHelper.writeDebugLog("DBType : " + strDBType);
            bool rt = true;

            Int64 itemCount = 0;
            int intSourceDataCount = 0;
            int intInsertDU = 0;
            int intInsertUI = 0;

            string strTableName = string.Empty;
            string strDUTableName = string.Empty;
            string strUITableName = string.Empty;
            string strFileName = string.Empty;
            string[] s;

            MData md = null;
            DateTime dtNow = DateTime.Now;
            DBConnect db = new DBConnect();
            try
            {
                if ("go".Equals(strDBType))
                {
                    strTableName = "GoSourceData";
                    strDUTableName = "GoDailyUser";
                    strUITableName = "UserInfo";
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
                string[] file = Directory.GetFiles(strPath);

                for (int index = 0; index < file.Length; index++)
                {
                    strFileName = string.Empty;
                    strFileName = file[index];
                    LogHelper.writeDebugLog("file [" + index + "] : " + strFileName);

                    FileStream fs = new FileStream(strFileName, FileMode.Open);
                    long fileLength = fs.Length;//文件流的长度

                    fs.Close();

                    //long offset = 0x10000000; // 256 megabytes
                    //long length = 0x20000000; // 512 megabytes

                    // 读取开始位置
                    long offset = 0x0; // 256 megabytes
                                       // 读取大小
                    long length = 0x20000000; // 512 megabytes
                                              //long length = 0x5000; // 512 megabytes
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
                    table.Columns.Add("createdate");
                    table.Columns.Add("updatedate");

                    String line;

                    do
                    {
                        // 建立缓存文件
                        using (var mmf = MemoryMappedFile.CreateFromFile(strFileName, FileMode.Open))
                        using (var stream = mmf.CreateViewStream(offset, length, MemoryMappedFileAccess.Read))
                        using (var reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            do
                            {
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

                                                dr["createdate"] = dtNow;
                                                dr["updatedate"] = dtNow;

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

                                if (itemCount != 0 && itemCount % 50000 == 0)
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

                            if (itemCount % 50000 > 0)
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
                }

                //string strInputDateTemp = strFileName.Substring(strFileName.Length - 10);
                //string strInputDate = strInputDateTemp.Substring(strInputDateTemp.Length - 4) +
                //    "-" + strInputDateTemp.Substring(strInputDateTemp.Length - 7, 2) +
                //    "-" + strInputDateTemp.Substring(strInputDateTemp.Length - 10, 2);

                // for test sssssssssssssssssssss

                // 取得(SourceData)单日件数
                Console.WriteLine("GetSourceDataCount Start.");
                intSourceDataCount = db.GetSourceDataCount(strImportDate, strTableName);
                Console.WriteLine("GetSourceDataCount Count = " + intSourceDataCount);
                Console.WriteLine("GetSourceDataCount Insert End.");

                if (intSourceDataCount > 0)
                {
                    // 向表(DailyUser)中插入数据
                    Console.WriteLine("DailyUser Insert Start.");
                    intInsertDU = db.InsertDailyUser(strImportDate, strTableName, strDUTableName);
                    Console.WriteLine("DailyUser Insert Count = " + intInsertDU);
                    Console.WriteLine("DailyUser Insert End.");

                    if (intInsertDU > 0)
                    {
                        Console.WriteLine("UserInfo Insert Start.");
                        // 向表(UserInfo)中插入数据
                        intInsertUI = db.InsertUserInfo(strImportDate, strDUTableName, strUITableName);
                        Console.WriteLine("UserInfo Insert Count = " + intInsertUI);
                        Console.WriteLine("UserInfo Insert End.");
                    }
                }

                Console.WriteLine("InsertDailyVisitUserStatistics Start.");
                int intCount = db.InsertDailyVisitUserStatistics(strDBType, strImportDate, intSourceDataCount, intInsertDU, intInsertUI);
                Console.WriteLine("InsertDailyVisitUserStatistics Count = " + intCount);
                Console.WriteLine("InsertDailyVisitUserStatistics End.");
                // for test eeeeeeeeeeeeeeeeee

            }
            catch (Exception ex)
            {
                LogHelper.writeErrorLog(ex);
                return false;
            }
            LogHelper.writeDebugLog("FileToTable10 end");
            return rt;
        }

        private static bool FileToTable20(string strDBType, string strImportDate, string strPath)
        {
            LogHelper.writeDebugLog("FileToTable20 start");
            LogHelper.writeDebugLog("DBType : " + strDBType);
            bool rt = true;

            Int64 itemCount = 0;
            int intSourceDataCount = 0;
            int intInsertDU = 0;
            int intInsertUI = 0;
            int intCount = 0;
            string[] s;
            MData20 md = null;
            DBConnect db = new DBConnect();
            DateTime dt = DateTime.Now;

            string strJson = string.Empty;
            string strFileName = string.Empty;
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
                else if ("C#2.0".Equals(strDBType))
                {
                    strTableName = "Cs20SourceData";
                    strDUTableName = "Cs20DailyUser";
                    strUITableName = "Cs20UserInfo";
                }
                else if ("killer2.0".Equals(strDBType))
                {
                    strTableName = "Killer20SourceData";
                    strDUTableName = "Killer20DailyUser";
                    strUITableName = "Killer20UserInfo";
                }

                string[] file = Directory.GetFiles(strPath);

                FileStream fs;

                DataTable table = new DataTable();

                for (int index = 0; index < file.Length; index++)
                {
                    strFileName = string.Empty;
                    strFileName = file[index];
                    LogHelper.writeDebugLog("file [" + index + "] : " + strFileName);


                    fs = new FileStream(strFileName, FileMode.Open);

                    long fileLength = fs.Length;//文件流的长度
                    fs.Dispose();
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

                    table = new DataTable();

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

                    String line;
                    
                    do
                    {
                        // 建立缓存文件
                        using (var mmf = MemoryMappedFile.CreateFromFile(strFileName, FileMode.Open))
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
                                    //intline = intline + 1;
                                }
                                catch (OutOfMemoryException oome)
                                {
                                    //LogHelper.writeErrorLog("error: intline = " + intline);
                                    LogHelper.writeErrorLog("error: lsTemp = " + lsTemp.Count);
                                    LogHelper.writeErrorLog(oome);

                                    continue;
                                }
                                lsTemp.Add(line);

                                if (!string.IsNullOrEmpty(line)
                                    && line.IndexOf("{") > 0
                                    && line.IndexOf("\"event\":\"checkupdate") > 0)
                                {

                                    s = line.Split(' ');

                                    if (s.Length >= 7 && "req".Equals(s[5]))
                                    {
                                        //LogHelper.writeInfoLog("intline = " + intline);
                                        strJson = line.Substring(line.IndexOf("{"));
                                        md = new MData20();
                                        //创建数据行
                                        DataRow dr = table.NewRow();
                                        try
                                        {
                                            //LogHelper.writeInfoLog("intline = " + intline);
                                            JsonSerializer serializer = new JsonSerializer();
                                            StringReader srt = new StringReader(strJson);

                                            md = serializer.Deserialize(new JsonTextReader(srt), typeof(MData20)) as MData20;
                                            srt.Close();
                                        }
                                        catch (JsonException je)
                                        {
                                            //LogHelper.writeInfoLog("intline = " + intline);
                                            LogHelper.writeDebugLog("debug: " + line);
                                            LogHelper.writeErrorLog("error: itemCount = " + itemCount);
                                            LogHelper.writeErrorLog(je);

                                            continue;
                                        }
                                        catch (OutOfMemoryException oome)
                                        {
                                            //LogHelper.writeInfoLog("intline = " + intline);
                                            LogHelper.writeDebugLog("debug: " + line);
                                            LogHelper.writeErrorLog("error: itemCount = " + itemCount);
                                            LogHelper.writeErrorLog(oome);

                                            continue;
                                        }
                                        catch (Exception ex)
                                        {
                                            //LogHelper.writeInfoLog("intline = " + intline);
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
                                                dr["udate"] = (Convert.ToDateTime(s[0] + " " + s[1])).AddHours(8);
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
                                                //LogHelper.writeInfoLog("intline = " + intline);
                                                LogHelper.writeErrorLog("error: itemCount = " + itemCount);
                                                LogHelper.writeErrorLog("error: s[0]+s[1] = " + s[0] + s[1]);
                                                LogHelper.writeErrorLog(ex);
                                                continue;
                                            }
                                        }

                                    }
                                }
                                if (itemCount != 0 && itemCount % 100000 == 0)
                                {
                                    try
                                    {
                                        db.InsertTable20(table, strTableName);
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

                            if (itemCount % 100000 > 0)
                            {
                                db.InsertTable20(table, strTableName);
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

                            stream.Dispose();
                            stream.Close();
                            mmf.Dispose();                            
                        }

                        tempCount = tempCount + 1;
                    }
                    while (tempCount <= readCount);
                }

                // for test sssssssssssssssssssss

                int intLossCount = 0;
                //int intWeekCount = 0;

                Console.WriteLine("SDTableName ： "+ strTableName);
                // 取得(SourceData)单日件数
                Console.WriteLine("GetSourceDataCount20 Start.");
                intSourceDataCount = db.GetSourceDataCount(strImportDate, strTableName);
                Console.WriteLine("GetSourceDataCount20 Count = " + intSourceDataCount);
                Console.WriteLine("GetSourceDataCount20 Insert End.");

                // 向表(DailyUser)中插入数据
                Console.WriteLine("DailyUser2.0 Insert Start.");
                intInsertDU = db.InsertDailyUser20(strImportDate, strTableName, strDUTableName);
                Console.WriteLine("DailyUser2.0 Insert Count = " + intInsertDU);
                Console.WriteLine("DailyUser2.0 Insert End.");

                if (intInsertDU > 0)
                {
                    if ("go2.0".Equals(strDBType))
                    {
                        // 更新表(DailyUser)中数据
                        Console.WriteLine("DailyUser2.0 Update Start.");
                        int intUpdateDU = db.UpdateDailyUser20(strImportDate, strTableName, strDUTableName);
                        Console.WriteLine("DailyUser2.0 Update Count = " + intUpdateDU);
                        Console.WriteLine("DailyUser2.0 Update End.");
                    }

                    Console.WriteLine("UserInfo2.0 Insert Start.");
                    // 向表(UserInfo)中插入数据
                    intInsertUI = db.InsertUserInfo20(strImportDate, strDUTableName, strUITableName);
                    Console.WriteLine("UserInfo2.0 Insert Count = " + intInsertUI);
                    Console.WriteLine("UserInfo2.0 Insert End.");

                    if ("go2.0".Equals(strDBType))
                    {
                        int intUpdateUI = 0;
                        // 更新第一次访问时间
                        Console.WriteLine("UserInfo2.0 Update Start.");
                        intUpdateUI = db.UpdateSadateForUserInfo20(strImportDate, strUITableName, strDUTableName);
                        Console.WriteLine("UserInfo2.0 Update Count = " + intUpdateUI);
                        Console.WriteLine("UserInfo2.0 Update End.");

                        intUpdateUI = 0;
                        // 更新最后一次访问时间
                        Console.WriteLine("UserInfo2.0 Update Start.");
                        intUpdateUI = db.UpdateEadateForUserInfo20(strImportDate, strUITableName, strDUTableName);
                        Console.WriteLine("UserInfo2.0 Update Count = " + intUpdateUI);
                        Console.WriteLine("UserInfo2.0 Update End.");

                        intLossCount = db.GetLossCount(strUITableName);
                    }
                }

                Console.WriteLine("InsertDailyVisitUserStatistics For 2.0 Start.");
                if ("go2.0".Equals(strDBType))
                {
                    intCount = db.InsertDailyVisitUserStatisticsForLoss(strDBType, strImportDate, intSourceDataCount, intInsertDU, intInsertUI, intLossCount);
                }
                else
                {
                    intCount = db.InsertDailyVisitUserStatistics(strDBType, strImportDate, intSourceDataCount, intInsertDU, intInsertUI);
                }
                Console.WriteLine("InsertDailyVisitUserStatistics For 2.0 Count = " + intCount);
                Console.WriteLine("InsertDailyVisitUserStatistics For 2.0 End.");

                // for test eeeeeeeeeeeeeeeeeeeee
            }
            catch (Exception ex)
            {
                LogHelper.writeErrorLog(ex);
                return false;
            }


            LogHelper.writeDebugLog("FileToTable20 end");
            return rt;
        }
        
        private static bool FileToTable20ForTask(string strDBType, string strImportDate, string strPath)
        {
            LogHelper.writeDebugLog("FileToTable20ForTask start");
            LogHelper.writeDebugLog("DBType : " + strDBType);
            bool rt = true;
            Int64 itemCount = 0;
            int intSourceDataCount = 0;
            int intInsertDU = 0;
            //int intInsertUI = 0;
            int intCount = 0;
            string[] s;
            //List<MData> list = new List<MData>();
            //MData md = null;
            TaskData20 td = null;
            DBConnect db = new DBConnect();
            DateTime dt = DateTime.Now;

            string strJson = string.Empty;
            string strFileName = string.Empty;
            string strTableName = string.Empty;
            string strDUTableName = string.Empty;
            string strUITableName = string.Empty;
            try
            {

                if ("task2.0".Equals(strDBType))
                {
                    strTableName = "Go20TaskSD";
                    strDUTableName = "Go20TaskInfo";
                    strUITableName = string.Empty;
                }

                string[] file = Directory.GetFiles(strPath);

                for (int index = 0; index < file.Length; index++)
                {
                    strFileName = string.Empty;
                    strFileName = file[index];
                    LogHelper.writeDebugLog("file [" + index + "] : " + strFileName);

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

                    String line;
                    //int intline = 0;
                    do
                    {
                        // 建立缓存文件
                        using (var mmf = MemoryMappedFile.CreateFromFile(strFileName, FileMode.Open))
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
                                    //intline = intline + 1;
                                }
                                catch (OutOfMemoryException oome)
                                {
                                    //LogHelper.writeErrorLog("error: intline = " + intline);
                                    LogHelper.writeErrorLog("error: lsTemp = " + lsTemp.Count);
                                    LogHelper.writeErrorLog(oome);

                                    continue;
                                }
                                lsTemp.Add(line);
                                if (!string.IsNullOrEmpty(line)
                                    && line.IndexOf("{") > 0
                                    && line.IndexOf("\"event\":\"taskresult") > 0)
                                {

                                    //LogHelper.writeInfoLog("intline = " + intline);
                                    s = line.Split(' ');

                                    if (s.Length >= 7 && "req".Equals(s[5]))
                                    {
                                        //LogHelper.writeInfoLog("intline = " + intline);
                                        strJson = line.Substring(line.IndexOf("{"));
                                        td = new TaskData20();
                                        //创建数据行
                                        DataRow dr = table.NewRow();
                                        try
                                        {
                                            JsonSerializer serializer = new JsonSerializer();
                                            StringReader srt = new StringReader(strJson);

                                            td = serializer.Deserialize(new JsonTextReader(srt), typeof(TaskData20)) as TaskData20;
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

                                                //dr["udate"] = Convert.ToDateTime(s[0] + " " + s[1]);
                                                dr["udate"] = (Convert.ToDateTime(s[0] + " " + s[1])).AddHours(8);
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
                                                    if (string.IsNullOrEmpty(td.Data.Return))
                                                    {
                                                        dr["data_return"] = "";
                                                    }
                                                    else
                                                    {
                                                        if ("0".Equals(td.Data.Return) || td.Data.Return == "-1")
                                                        {
                                                            dr["data_return"] = td.Data.Return;
                                                        }
                                                        else
                                                        {
                                                            //LogHelper.writeErrorLog("Convert.ToInt64(td.Data.Return) = " + Convert.ToInt64(td.Data.Return));
                                                            //LogHelper.writeErrorLog("(Convert.ToInt64(td.Data.Return)).ToString(X) = " + (Convert.ToInt64(td.Data.Return)).ToString("X"));
                                                            //LogHelper.writeErrorLog("((Convert.ToInt64(td.Data.Return)).ToString(X)).ToString() = " + ((Convert.ToInt64(td.Data.Return)).ToString("X")).ToString());
                                                            dr["data_return"] = "0x" + ((Convert.ToInt64(td.Data.Return)).ToString("X")).ToString();
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
                                if (itemCount != 0 && itemCount % 100000 == 0)
                                {
                                    try
                                    {
                                        db.InsertTable20(table, strTableName);
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

                            if (itemCount % 100000 > 0)
                            {
                                db.InsertTable20(table, strTableName);
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
                }

                // 取得(SourceData)单日件数
                Console.WriteLine("GetSourceDataCount20 Start.");
                intSourceDataCount = db.GetSourceDataCount20(strImportDate, strTableName);
                Console.WriteLine("GetSourceDataCount20 Count = " + intSourceDataCount);
                Console.WriteLine("GetSourceDataCount20 Insert End.");

                // 向表(DailyUser)中插入数据
                Console.WriteLine("TaskInfo20 Insert Start.");
                Console.WriteLine("strDUTableName = " + strDUTableName);
                intInsertDU = db.InsertTaskInfo20(strImportDate, strTableName, strDUTableName);
                Console.WriteLine("TaskInfo20 Insert Count = " + intInsertDU);
                Console.WriteLine("TaskInfo20 Insert End.");

                int intdaycount = 0;
                int inttaskcount = 0;
                int intreturncount = 0;

                intdaycount = db.GetTask20DayCount(strImportDate, strTableName);
                inttaskcount = db.GetTask20ResultCount(strImportDate, strTableName);
                intreturncount = db.GetTask20ResultReturnCount(strImportDate, strTableName);

                Console.WriteLine("InsertDailyVisitUserStatistics For Task20 Start.");
                intCount = db.InsertDailyVisitUserStatistics(strDBType, strImportDate, intSourceDataCount, intdaycount, inttaskcount, intreturncount);
                Console.WriteLine("InsertDailyVisitUserStatistics For Task20 Count = " + intCount);
                Console.WriteLine("InsertDailyVisitUserStatistics For Task20 End.");
            }
            catch (Exception ex)
            {
                LogHelper.writeErrorLog(ex);
                return false;
            }

            LogHelper.writeDebugLog("FileToTable20ForTask end");
            return rt;
        }
    }
}
