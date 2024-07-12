using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace translator.entities
{
    public class Config
    {
        public TransConfig Baidu { get; set; }

        public TransConfig Google { get; set; }

        public string DefaultLangs { get; set; } = "zh-CN,en-US";
        /// <summary>
        /// 忽略翻译的属性列表
        /// </summary>
        public string TransIgnoreList { get; set; }

        public string AppDataPath
        {
            get
            {
                string dir = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "fair.translator");
                return dir;
            }
        }

        public Config()
        {
            if (!Directory.Exists(AppDataPath))
            {
                Directory.CreateDirectory(AppDataPath);
            }
        }
    }

    public struct TransConfig
    {
        public string ApiAddress { get; set; }
        public string AppId { get; set; }
        public string Key { get; set; }
        /// <summary>
        /// 每月最大使用量
        /// </summary>
        public Int64 MonthMax { get; set; }
    }


}
