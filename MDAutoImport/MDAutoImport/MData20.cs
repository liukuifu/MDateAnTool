using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDAutoImport
{
    public class MData20
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public long Date { get; set; }

        /// <summary>
        /// id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// antivirusKey
        /// </summary>
        public string antivirusKey { get; set; }

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
        public DataItem Data { get; set; }

    }
    public class DataItem
    {
        /// <summary>
        /// antivirus
        /// </summary>
        public List<AntivirusItem> Antivirus { get; set; }

        /// <summary>
        /// browser
        /// </summary>
        public string browser { get; set; }

        /// <summary>
        /// bversion
        /// </summary>
        public string bversion { get; set; }

        /// <summary>
        /// dotnet
        /// </summary>
        public List<string> dotnet { get; set; }

        /// <summary>
        /// dx
        /// </summary>
        public string dx { get; set; }

        /// <summary>
        /// hardware
        /// </summary>
        public HardwareItem hardware { get; set; }

        /// <summary>
        /// ie
        /// </summary>
        public string ie { get; set; }

        /// <summary>
        /// kill
        /// </summary>
        public string kill { get; set; }
    }

    public class AntivirusItem20
    {
        /// <summary>
        /// guid
        /// </summary>
        public string guid { get; set; }

        /// <summary>
        /// name
        /// </summary>
        public string name { get; set; }
    }
    public class HardwareItem
    {
        /// <summary>
        /// base
        /// </summary>
        public string Base { get; set; }
        /// <summary>
        /// bios
        /// </summary>
        public string Bios { get; set; }
        /// <summary>
        /// disk
        /// </summary>
        public string Disk { get; set; }
        /// <summary>
        /// network
        /// </summary>
        public string Network { get; set; }

    }
}
