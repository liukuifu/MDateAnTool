using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDataIm20
{
    public class TaskData
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public long Date { get; set; }

        /// <summary>
        /// appid
        /// </summary>
        public Int64 Appid { get; set; }

        /// <summary>
        /// channel
        /// </summary>
        public string Channel { get; set; }

        /// <summary>
        /// event
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// eggid
        /// </summary>
        public string Eggid { get; set; }

        /// <summary>
        /// version
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// locale
        /// </summary>
        public int Locale { get; set; }

        /// <summary>
        /// os
        /// </summary>
        public string OS { get; set; }

        /// <summary>
        /// uid
        /// </summary>
        public string Uid { get; set; }

        /// <summary>
        /// amd64
        /// </summary>
        public bool Amd64 { get; set; }

        /// <summary>
        /// data
        /// </summary>
        public TaskResultDataItem Data { get; set; }

    }
    public class TaskResultDataItem
    {
        /// <summary>
        /// parameter
        /// </summary>
        public string Parameter { get; set; }

        /// <summary>
        /// return
        /// </summary>
        public string Return { get; set; }

        /// <summary>
        /// taskid
        /// </summary>
        public string Taskid { get; set; }

    }
}
