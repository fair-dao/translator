using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace translator.entities
{
    public class Project
    {
        public string ProjectName { get; set; }

        /// <summary>
        /// 生成语言列表
        /// </summary>
        public List<LangItem> TgtLangs { get; set; }

        /// <summary>
        /// 源语言
        /// </summary>
        public string SrcLang { get; set; }

        [JsonIgnore]
        public string ProjectFullPath { get; set; }



    }
}
