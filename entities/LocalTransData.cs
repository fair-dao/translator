using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace translator.entities
{
    public class LocalTransData
    {

        public string SrcLang { get; set; }

        public string DstLang { get; set; }
       

        public string Word { get; set; }

        /// <summary>
        /// 翻译结果 
        /// </summary>
        public string Result { get; set; } 
        /// <summary>
        /// 访问时间
        /// </summary>
        public long VisitedAt { get; set; }
    }
}
