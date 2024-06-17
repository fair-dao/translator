using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace translator.entities
{
    public class ProjectVerData
    {
        /// <summary>
        /// 自动改的
        /// </summary>
        public Dictionary<string, VerItem> VerPairs { get; set; } = new Dictionary<string, VerItem>();

        /// <summary>
        /// 手动改的
        /// </summary>
        public Dictionary<string, VerItem> WarnPairs { get; set; } = new Dictionary<string, VerItem>();
    }


    /// <summary>
    /// 版本英
    /// </summary>
    public struct VerItem
    {
        public string DstLang { get; set; }

        /// <summary>
        /// Json项路径
        /// </summary>
        [JsonIgnore]
        public string Path { get; set; }

        public string DstVer { get; set; }

        public string SrcVer { get; set; }
    }
}
