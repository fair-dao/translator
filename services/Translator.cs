using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using translator.entities;
using static System.Net.Mime.MediaTypeNames;

namespace translator.services
{
    public class Translator
    {
        protected Config _Config;
        protected TransConfig _TransConfig;
        protected Helper _Helper;


        public virtual string Name { get; }

        public static Dictionary<string, LocalTransData> LocalTransData;
        public override string ToString()
        {
            return $"{Name}-{(this._TransConfig.Key?.Length > 3 ? this._TransConfig.Key.Substring(0, 4) : this._TransConfig.Key)} ({this.TransChars}/{this._TransConfig.MonthMax})";
        }

        string localTransDataFile;

        public Translator(Config config, TransConfig transConfig, Helper helper)
        {
            this._Config = config;
            this._TransConfig = transConfig;
            this._Helper = helper;
            string key = helper.GetMD5($"{transConfig.AppId}{transConfig.Key}").Substring(0, 6);
            totalFile = Path.Combine(AppContext.BaseDirectory, $"{key}.total.txt");
            localTransDataFile = Path.Combine(this._Config.AppDataPath, "localtrans.json");
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
            if (LocalTransData == null && File.Exists(localTransDataFile))
            {
                LocalTransData = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, LocalTransData>>(File.ReadAllText(localTransDataFile, Encoding.UTF8));
            }
            if (LocalTransData == null) LocalTransData = new ();
        }


        bool localLibChanged = false;
        public Int64 TransChars { get; set; }

        string totalFile;

        int curMonth = 0;

        /// <summary>
        /// 语言代码映射表
        /// </summary>
        public Dictionary<string, string> LangMap = new Dictionary<string, string>();


        public virtual void SaveCharsTotal()
        {
            File.WriteAllText(totalFile, $"{curMonth},{TransChars}");
            string data = System.Text.Json.JsonSerializer.Serialize(LocalTransData);
            if (localLibChanged)
            {
                File.WriteAllText(localTransDataFile, data, Encoding.UTF8);
                localLibChanged = false;
            }
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
                var found = LangMap.FirstOrDefault(m => m.Key == desLangCode);
                if (string.IsNullOrEmpty(found.Value))
                {

                    if (desLangCode.IndexOf('-') < 0)
                    {
                        string desCode = desLangCode + "-";
                        found = LangMap.FirstOrDefault(m => m.Key == desCode);
                        if (!string.IsNullOrEmpty(found.Value))
                        {
                            des = found.Value;
                        }
                    }
                }
                else des = found.Value;
            }
            #endregion
            if (TransChars + text.Length >= _TransConfig.MonthMax)
            {
                throw new exs.DisTransException($"本月翻译字符量已接近最大值 {_TransConfig.MonthMax}，请下个月再使用");
            }
            string key = $"{src}-{des}-{text}";
            string sha = this._Helper.GetSHA256(key);

            if (LocalTransData.ContainsKey(sha))
            {
                if (!string.IsNullOrEmpty(LocalTransData[sha]?.Result))
                    return LocalTransData[sha].Result;
            }
            var txt = ToTans(src, des, text);
            if (!string.IsNullOrEmpty(txt))
            {
                WriteLocalData(sha,src,des,text, txt);
                TransChars += text.Length;
            }
            return txt;
        }


        public void WriteLocalData(string sha, string srcLang,string dstLang,string word,string val)
        {

            if (LocalTransData.ContainsKey(sha) && LocalTransData[sha]?.Result == val) {
                LocalTransData[sha].VisitedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                return; 
            }

            if (!string.IsNullOrEmpty(val))
            {
                LocalTransData[sha] = new entities.LocalTransData { SrcLang= srcLang,DstLang=dstLang, Word=word, Result=val , VisitedAt=DateTimeOffset.UtcNow.ToUnixTimeSeconds() }; 
                if (!localLibChanged) localLibChanged = true;
            }

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
