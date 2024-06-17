using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using translator.entities;

namespace translator.services
{
    public class Translator
    {
        protected TransConfig config;
        protected Helper helper;
       
        public virtual string Name { get; }

        public override string ToString()
        {
            return $"{Name}-{(this.config.Key?.Length>3? this.config.Key.Substring(0,4):this.config.Key)} ({this.TransChars}/{ this.config.MonthMax})";
        }


        public Translator(TransConfig config, Helper helper) {
            this.config = config;
            this.helper = helper;
            string key = helper.GetMD5($"{config.AppId}{config.Key}").Substring(0, 6);
            totalFile = Path.Combine(AppContext.BaseDirectory, $"{key}.total.txt");
            if (File.Exists(totalFile))
            {
                string d = File.ReadAllText(totalFile, Encoding.UTF8);
                string[] datas = d.Split(',');
                if (d.Length > 1)
                {
                    int mounth = Int32.Parse(datas[0]);
                    curMonth = DateTime.Now.Year * 100 + DateTime.Now.Month;
                    if (mounth == curMonth)
                    {
                        TransChars = Int64.Parse(datas[1]);
                    }
                }
            }
        }

        public Int64 TransChars { get; set; }

        string totalFile;

        int curMonth = 0;

        /// <summary>
        /// 语言代码映射表
        /// </summary>
        public Dictionary<string, string> LangMap=new Dictionary<string, string>();


        public virtual void SaveCharsTotal()
        {
            File.WriteAllText(totalFile, $"{curMonth},{TransChars}");
        }

        /// <summary>
        /// fa,far,fa
        /// </summary>
        /// <param name="srcLangCode"></param>
        /// <param name="desLangCode"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public string Tans(string srcLangCode, string desLangCode, string text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            text = text.Trim();
   
            string src = srcLangCode;
            string des = desLangCode;

            #region 对src和des语言代码进行转换，以便适用于具体的翻译器
            if (string.IsNullOrEmpty(src)) src = "auto";
            else
            {
                var found= LangMap.FirstOrDefault(m => m.Key == desLangCode);
                if (string.IsNullOrEmpty(found.Value))
                {
                  
                    if (desLangCode.IndexOf('-') <0)
                    {
                        string desCode = desLangCode + "-";
                        found = LangMap.FirstOrDefault(m => m.Key == desCode);
                        if (!string.IsNullOrEmpty(found.Value))
                        {
                            des = found.Value;
                        }
                    }
                } else des = found.Value;
            }
            #endregion
            if (TransChars+text.Length>= config.MonthMax)
            {
                throw new exs.DisTransException($"本月翻译字符量已接近最大值 {config.MonthMax}，请下个月再使用");
            }
            var txt= ToTans(src, des, text);
            if (!string.IsNullOrEmpty(txt))
            {
                TransChars += text.Length;
            }
            return txt;
        }


        /// <summary>
        /// 转换方法
        /// </summary>
        /// <param name="src">源语言代码</param>
        /// <param name="des">目标语言代码</param>
        /// <param name="text">待翻译的文本</param>
        /// <returns></returns>
        protected virtual string ToTans(string src, string des, string text)
        {
            throw new Exception("请实现ToTans方法");
        }

    }


}
