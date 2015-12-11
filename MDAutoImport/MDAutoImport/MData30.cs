using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDAutoImport
{
    public class MData30ListItem
    {
        /// <summary>
        /// data
        /// </summary>
        public string data { get; set; }

        /// <summary>
        /// ip
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// MData30Item
        /// </summary>
        public MData30 MData30Item { get; set; }

    }

    public class MData30
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime udate { get; set; }
        
        /// <summary>
        /// channel
        /// </summary>
        public string Channel { get; set; }

        /// <summary>
        /// uid
        /// </summary>
        public string Uid { get; set; }

        /// <summary>
        /// sid
        /// </summary>
        public string Sid { get; set; }

        /// <summary>
        /// hid
        /// </summary>
        public string Hid { get; set; }

        /// <summary>
        /// sysid
        /// </summary>
        public string Sysid { get; set; }

        /// <summary>
        /// vm
        /// </summary>
        public string Vid { get; set; }

        /// <summary>
        /// vid
        /// </summary>
        public string Vm { get; set; }

        /// <summary>
        /// eggid
        /// </summary>
        public string Eggid { get; set; }

        /// <summary>
        /// version
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// workversion
        /// </summary>
        public string Workversion { get; set; }

        /// <summary>
        /// os
        /// </summary>
        public string OS { get; set; }

        /// <summary>
        /// amd64
        /// </summary>
        public bool Amd64 { get; set; }

        /// <summary>
        /// locale
        /// </summary>
        public int Locale { get; set; }

        /// <summary>
        /// event
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// data
        /// </summary>
        public Data30Item Data { get; set; }

    }

    public class Data30Item
    {
        /// <summary>
        /// antivirus
        /// </summary>
        public List<AntivirusItem30> Antivirus { get; set; }

        /// <summary>
        /// hardware
        /// </summary>
        public HardwareItem30 Hardware { get; set; }

        /// <summary>
        /// dx
        /// </summary>
        public string dx { get; set; }

        /// <summary>
        /// dotnet
        /// </summary>
        public List<string> dotnet { get; set; }

        /// <summary>
        /// browser
        /// </summary>
        public string browser { get; set; }

        /// <summary>
        /// bversion
        /// </summary>
        public string bversion { get; set; }

        /// <summary>
        /// ie
        /// </summary>
        public string ie { get; set; }

        /// <summary>
        /// kill
        /// </summary>
        public string kill { get; set; }

        /// <summary>
        /// software
        /// </summary>
        public SoftwareItem30 Software { get; set; }

    }

    public class AntivirusItem30
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

    public class HardwareItem30
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
    
    public class SoftwareItem30
    {
        /// <summary>
        /// chrome
        /// </summary>
        public List<Version> chrome { get; set; }

        /// <summary>
        /// gupdate
        /// </summary>
        public List<Version> gupdate { get; set; }
    }

    public class Version
    {
        /// <summary>
        /// hver
        /// </summary>
        public string hver { get; set; }

        /// <summary>
        /// ever
        /// </summary>
        public string ever { get; set; }
    }


    public class MDataDU30
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public string udate { get; set; }

        /// <summary>
        /// channel
        /// </summary>
        public string Channel { get; set; }

        /// <summary>
        /// uid
        /// </summary>
        public string Uid { get; set; }

        /// <summary>
        /// sid
        /// </summary>
        public string Sid { get; set; }

        /// <summary>
        /// hid
        /// </summary>
        public string Hid { get; set; }

        /// <summary>
        /// sysid
        /// </summary>
        public string Sysid { get; set; }

        /// <summary>
        /// vm
        /// </summary>
        public string Vid { get; set; }
        
        /// <summary>
        /// version
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// kill
        /// </summary>
        public string kill { get; set; }

        /// <summary>
        /// md5
        /// </summary>
        public string md5 { get; set; }
    }

}
