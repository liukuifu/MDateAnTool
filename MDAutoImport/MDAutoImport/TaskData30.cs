using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDAutoImport
{
    class TaskData30
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public long Date { get; set; }

        /// <summary>
        /// amd64
        /// </summary>
        public bool Amd64 { get; set; }

        /// <summary>
        /// channel
        /// </summary>
        public string Channel { get; set; }

        /// <summary>
        /// data
        /// </summary>
        public TaskResultDataItem Data { get; set; }

        /// <summary>
        /// eggid
        /// </summary>
        public string Eggid { get; set; }

        /// <summary>
        /// event
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// hid
        /// </summary>
        public string Hid { get; set; }

        /// <summary>
        /// locale
        /// </summary>
        public int Locale { get; set; }

        /// <summary>
        /// os
        /// </summary>
        public string OS { get; set; }

        /// <summary>
        /// sid
        /// </summary>
        public string Sid { get; set; }


        /// <summary>
        /// sysid
        /// </summary>
        public string Sysid { get; set; }

        /// <summary>
        /// uid
        /// </summary>
        public string Uid { get; set; }

        /// <summary>
        /// version
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// vid
        /// </summary>
        public string Vid { get; set; }
        /// <summary>
        /// vm
        /// </summary>
        public string Vm { get; set; }

    }
    public class Task30ResultDataItem
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
