using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace translator.entities
{
    /// <summary>
    /// json元素信息
    /// </summary>
    public class JsonElementInfo
    {
        public JsonElement Element { get; set; }    
        public string path { get; set; }
    }
}
