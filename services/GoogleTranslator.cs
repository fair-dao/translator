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

    public class GoogleTranslator : Translator
    {

        private Config config;

        public override string Name => "Google翻译";

        public GoogleTranslator(Config config, Helper helper) : base(config.Google, helper)
        {
            this.config = config;
            LangMap.Add("zh-CN", "zh");
        }

        private DateTime lastTransTime;

        private static readonly object lockObj = new object();


        /// <summary>
        /// 翻译
        /// </summary>
        /// <param name="src"></param>
        /// <param name="des"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        protected override string  ToTans(string src,string des,string text)
        {
            string salt= System.DateTime.Now.Millisecond.ToString();
            string address = $"https://translate.google.com.hk/translate_a/single?client=gtx&dt=t&dj=1&ie=UTF-8&sl=auto&tl={des}&q={text}";
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
                    try
                    {
                        string back = hc.GetStringAsync(address).Result;
                        lastTransTime = DateTime.Now.AddSeconds(1);
                        dynamic result = System.Text.Json.JsonSerializer.Deserialize<dynamic>(back);
                        if (result.error_code != null)
                        {
                            return "";
                        }

                        if (result.sentences != null && result.sentences.Count > 0)
                        {
                            return (string)result.sentences[0].trans;
                        }
                    }catch(Exception ex)
                    {
                        throw new Exception($"Google翻译出错:{src},{des},{text},错误信息:{ex.Message}");
                    }
                }

            }
            return "";

        }
    }
}
