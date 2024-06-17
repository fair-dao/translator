using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using translator.entities;

namespace translator.services
{

    public class BaiduTranslator : Translator
    {
     



        int curMonth = 0;
        public override string Name => "百度翻译";
        public BaiduTranslator(Config config,Helper helper):base(config.Baidu,helper)
        {
        
            this.lastTransTime = DateTime.Now;
            LangMap.Add("vi-", "vie"); //越南
            LangMap.Add("sw", "swe"); //瑞典
            LangMap.Add("my", "bur");//缅甸语
            LangMap.Add("km-KH", "hkm");//柬埔寨
            LangMap.Add("zh-CN", "zh"); //中国
            LangMap.Add("en-", "en"); //英语
            LangMap.Add("fr-", "fra"); //法语
            LangMap.Add("ru-", "ru"); //俄语
            LangMap.Add("de-", "de"); //德语
            LangMap.Add("ko-", "kor"); //韩语
            LangMap.Add("es-", "spa");//西班牙
            LangMap.Add("ja-", "jp"); //日语


        }

        void Map(string lang,string mapLang)
        {

        }

      
        private DateTime lastTransTime;

        record BaiDuTransResult(string src,string dst);
        record BaiDuResponseResult(string? from,string? to,string? error_code,string? error_msg, BaiDuTransResult[]? trans_result);

        private static readonly object lockObj = new object();
        /// <summary>
        /// 翻译
        /// </summary>
        /// <param name="src"></param>
        /// <param name="des"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        protected override string ToTans(string src,string des,string text)
        {          
            string salt= System.DateTime.Now.Millisecond.ToString();
            string str = $"{this.config.AppId}{text}{salt}{this.config.Key}";
            string md5 = this.helper.GetMD5(str);

            string t2 = HttpUtility.UrlEncode(text, Encoding.UTF8);
            string address = $"{this.config.ApiAddress}?q={t2}&from={src}&to={des}&appid={this.config.AppId}&salt={salt}&sign={md5}";
 
                lock (lockObj) //保证每秒只调用一次
                {
                    DateTime now = DateTime.Now;
                    if (lastTransTime > now)
                    {
                        int time = (int)((lastTransTime - now).TotalMilliseconds);
                        Thread.Sleep(time);
                    }
                using (HttpClient hc = new HttpClient())
                {
                    string back = hc.GetStringAsync(address).Result;
                    lastTransTime = DateTime.Now.AddSeconds(1);
                    var result= System.Text.Json.JsonSerializer.Deserialize<BaiDuResponseResult>(back);
                    if (!string.IsNullOrEmpty(result.error_code))
                    {
                        if(result.error_code== "58001") //不能转换的语种
                        {
                            throw new exs.DisTransException($"from:{src},to:{des},txt:{text}, {result.error_msg}");

                        }
                        throw new Exception($"百度翻译出错:{src},{des},{text},错误代码:{result.error_code}");
                    }
                   
                    if (result.trans_result.Length>0 )
                    {
                        return result.trans_result[0].dst;
                    }
                }
            }
            return "";

        }
    }
}
