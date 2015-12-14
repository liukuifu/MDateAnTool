using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInfoUpdate
{
    class MData30Data
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

}
