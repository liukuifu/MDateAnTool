using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class MData
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

        //"appid":2312335602,
        /// <summary>
        /// appid
        /// </summary>
        public Int64 Appid { get; set; }

        //"channel":"1111111111111",
        /// <summary>
        /// channel
        /// </summary>
        public string Channel { get; set; }

        //"event":"heartbeat",
        /// <summary>
        /// event
        /// </summary>
        public string Event { get; set; }

        //"langid":"7",
        /// <summary>
        /// langid
        /// </summary>
        public string Langid { get; set; }

        //"locale":1055,
        /// <summary>
        /// locale
        /// </summary>
        public int Locale { get; set; }

        //"os":"6.1",
        /// <summary>
        /// os
        /// </summary>
        public string OS { get; set; }

        //"uid":"dc4f94f5e3299b19cda58e84fc420a8b9c2f07e9",
        /// <summary>
        /// uid
        /// </summary>
        public string Uid { get; set; }

        //"version":"1000.0.0.43",
        /// <summary>
        /// version
        /// </summary>
        public string Version { get; set; }

        //"antivirus":[
        //	{"company":"",
        //	"guid":"{AAF74A68-8713-CDF1-004F-30003398BE9E}",
        //	"name":"Panda Free Antivirus",
        //	"version":""}
        //	]
        /// <summary>
        /// antivirus
        /// </summary>
        public List<AntivirusItem> Antivirus { get; set; }

        //"hardwareinfo":"|BIOSTAR Group|P4M900-M7 FE|OEM_Serial|Ver:1.0|,|Phoenix Technologies, LTD|OEM_Serial|P4M900 - 42302e31|,|VIA Technologies, Inc.|Adaptador Fast Ethernet compatible VIA|00:E0:4D:7B:E3:E0|,|Microsoft|Minipuerto del administrador de paquetes|00:E0:4D:7B:E3:E0|,|Microsoft|Minipuerto WAN (PPTP)|50:50:54:50:30:30|,|Microsoft|Minipuerto WAN (PPPOE)|33:50:6F:45:30:30|,|Microsoft|Minipuerto del administrador de paquetes|68:A0:20:52:41:53|,"
        /// <summary>
        /// hardwareinfo
        /// </summary>
        public string Hardwareinfo { get; set; }

    }

    public class AntivirusItem
    {
        //"antivirus":[
        //	{"company":"",
        //	"guid":"{AAF74A68-8713-CDF1-004F-30003398BE9E}",
        //	"name":"Panda Free Antivirus",
        //	"version":""}
        //	]

        //"company":"",
        /// <summary>
        /// company
        /// </summary>
        public string company { get; set; }

        //"guid":"{AAF74A68-8713-CDF1-004F-30003398BE9E}",
        /// <summary>
        /// guid
        /// </summary>
        public string guid { get; set; }

        //"name":"Panda Free Antivirus",
        /// <summary>
        /// name
        /// </summary>
        public string name { get; set; }

        //"version":""}
        /// <summary>
        /// version
        /// </summary>
        public string version { get; set; }
    }
}
