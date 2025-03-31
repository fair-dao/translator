using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using translator.entities;

namespace translator
{
    public class Helper
    {

        private Config config;
        public Project CurProject { get; set; }

        public Helper(Config config)
        {
            this.config = config;

        }

       public record TransEnv(string dstLang, string FilePath,  Utf8JsonWriter JsonWriter);

        /// <summary>
        /// 版本检测文件
        /// </summary>
        string projectVerFile;

        /// <summary>
        /// 版本映射 
        /// </summary>
        private ProjectVerData VerData = new ();

        public string GetMD5(string txt)
        {
            try
            {
                MD5 md5 = MD5.Create();
                byte[] bytValue, bytHash;
                bytValue = System.Text.Encoding.UTF8.GetBytes(txt);
                bytHash = md5.ComputeHash(bytValue);
                md5.Clear();
                string sTemp = "";
                for (int i = 0; i < bytHash.Length; i++)
                {
                    sTemp += bytHash[i].ToString("X").PadLeft(2, '0');
                }
                txt = sTemp.ToLower();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return txt;
        }

        public string GetSHA256(string txt)
        {
            byte[] datas = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(txt));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in datas)
            {
                sb.Append(b.ToString("X2"));
            }
            string sha = sb.ToString();
            return sb.ToString();

        }

        /// <summary>
        /// 添加新语言
        /// </summary>
        /// <param name="langName"></param>
        /// <returns></returns>
        public LangItem AddLang(string langName)
        {
            if (this.CurProject == null) return null;
            if (this.CurProject.TgtLangs==null) this.CurProject.TgtLangs = new List<LangItem>();
            if (this.CurProject.TgtLangs.Any(m => m.Name == langName)) return null;
            var old = Thread.CurrentThread.CurrentCulture;
            CultureInfo c = CultureInfo.CreateSpecificCulture(langName);
            if (c != null)
            {
                LangItem item = new LangItem();
                item.Name = langName;
                item.EnglishName = c.EnglishName;
                Thread.CurrentThread.CurrentCulture = c;
                Thread.CurrentThread.CurrentUICulture = c;
                c = CultureInfo.GetCultureInfo(langName);
                item.DisplayName = c.DisplayName;
                c = CultureInfo.CreateSpecificCulture(this.CurProject.SrcLang);
                Thread.CurrentThread.CurrentCulture = c;
                Thread.CurrentThread.CurrentUICulture = c;
                c = CultureInfo.GetCultureInfo(langName);
                item.SrcName = c.DisplayName;
                this.CurProject.TgtLangs.Add(item);
                Thread.CurrentThread.CurrentCulture = old;
                return item;
            }
            return null;
      

        }
      
        public void LoadProject(string path)
        {
            VerData = new ();
            Project p = null;
            if (File.Exists(path))
            {
                string data = File.ReadAllText(path, Encoding.UTF8);
                p = System.Text.Json.JsonSerializer.Deserialize<Project>(data);
                p.ProjectFullPath = path;
                this.CurProject = p;
            }
            else
            {
                p = new Project();
                p.ProjectName = "new";
                p.ProjectFullPath = path;
                p.TgtLangs = new List<LangItem>();
                CultureInfo c = CultureInfo.CurrentCulture;
                p.SrcLang = c.Name;
                p.TgtLangs = p.TgtLangs = new List<LangItem>();
                if (!string.IsNullOrEmpty(config.DefaultLangs))
                {
                    string[] langs = config.DefaultLangs.Split(',');
                    foreach (var lang in langs)
                    {
                        if (lang != p.SrcLang)
                        {
                            try
                            {
                                var c2 = CultureInfo.GetCultureInfo(lang);
                                if (c2 != null)
                                {
                                    p.TgtLangs.Add(new LangItem { Name = c2.Name, EnglishName = c2.EnglishName, DisplayName = c2.DisplayName });
                                }
                            }
                            catch
                            {

                            }
                        }
                    }

                }

                this.CurProject = p;
                SaveProcject();
            }
            //加载版本数据
            projectVerFile = $"{path}.ver";
            if (File.Exists(projectVerFile))
            {
                string data=File.ReadAllText(projectVerFile);   
                VerData= System.Text.Json.JsonSerializer.Deserialize<ProjectVerData>(data);
            }
        }

        /// <summary>
        /// 添加版本
        /// </summary>
        /// <param name="env"></param>
        /// <param name="dstText"></param>
        /// <param name="srcText"></param>
        public void AddVer(TransEnv env, string jsonPath, string dstText,string srcText)
        {
            if (string.IsNullOrEmpty(srcText) || string.IsNullOrEmpty(dstText)) return;
            string key = GetMD5($"{jsonPath}{srcText}");
            string val = GetMD5(srcText);
            string des = GetMD5(dstText);
            VerData.VerPairs[key]=new VerItem() { DstLang=env.dstLang, DstVer= des, SrcVer=val };

        }

        /// <summary>
        /// 检测版本是否更改
        /// </summary>
        /// <param name="dstText"></param>
        /// <param name="srcText"></param>
        /// <returns></returns>
        public bool? CheckVerChanged(TransEnv env,string jsonPath, string dstText, string srcText)
        {    
            if (string.IsNullOrWhiteSpace(srcText)) return false;
            if (string.IsNullOrEmpty(dstText)) return true;
            string key = GetMD5($"{jsonPath}{srcText}");
            VerItem val;
            if (VerData.VerPairs.TryGetValue(key, out val))
            {
                string desVal = GetMD5(dstText);
                if (desVal != val.DstVer) //已手动更新
                {
                    //目标版本手动更新了
                    VerData.WarnPairs[key] = val;
                    VerData.VerPairs.Remove(key);
                    return false;
                }
                string oldVal = GetMD5(srcText);
                if (oldVal == val.SrcVer) return false;
                else
                {
                    //源版本更新
                    VerData.VerPairs.Remove(key);
                    return true;
                }
            }
            else
            {
                //if (VerData.WarnPairs.ContainsKey(key)) return false;
                return true;
            }


        }

        public JsonSerializerOptions JsonSerializerOptions
        {
            get => new JsonSerializerOptions
            {
                WriteIndented = true, // 设置为true以格式化输出
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping // 使用不安全的编码器
            };
        }
        public void SaveProcject()
        {
            if (CurProject?.ProjectFullPath != null)
            {
                File.WriteAllText(CurProject.ProjectFullPath, System.Text.Json.JsonSerializer.Serialize(CurProject, JsonSerializerOptions));
                projectVerFile = $"{CurProject?.ProjectFullPath}.ver";
                File.WriteAllText(projectVerFile, System.Text.Json.JsonSerializer.Serialize(VerData, JsonSerializerOptions));
            }

        }

        public void SaveConfig()
        {

            File.WriteAllText(Path.Combine(AppContext.BaseDirectory, "app.json"), System.Text.Json.JsonSerializer.Serialize(config, JsonSerializerOptions));

        }

    }
}
