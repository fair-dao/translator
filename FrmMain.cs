using System.Globalization;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using translator.entities;
using translator.exs;
using translator.services;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static translator.Helper;

namespace translator
{
    public partial class FrmMain : Form
    {

        private Helper helper;
        private Config config;

        private Translator translator;
        List<string>? transIgnoreList;


        public FrmMain(BaiduTranslator baiduTranslator, GoogleTranslator googleTranslator, Helper helper, Config config)
        {

            InitializeComponent();
            this.helper = helper;
            this.config = config;
            this.cbTransTools.Items.Add(baiduTranslator);
            this.cbTransTools.Items.Add(googleTranslator);
            this.cbTransTools.SelectedIndex = 0;
            transIgnoreList = config.TransIgnoreList?.Split(',').ToList();

        }

        record LangFile(string FileName, string FilePath)
        {
            public override string ToString() => FileName;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            ReloadUI();
        }

        private void mnuSetup_Click(object sender, EventArgs e)
        {
            if (new FrmSetup(this.helper).ShowDialog() == DialogResult.OK)
            {
                ReloadUI();
            }
        }

        private void mnuNew_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                helper.LoadProject(dialog.FileName);
                ReloadUI();
            }
        }

        string projectPath;
        void ReloadUI()
        {
            if (this.helper.CurProject?.ProjectFullPath != null)
            {
                this.clbFiles.Items.Clear();
                this.Text = $"{this.helper.CurProject.ProjectName}({this.helper.CurProject.ProjectFullPath})";
                CultureInfo c = CultureInfo.GetCultureInfo(this.helper.CurProject.SrcLang);
                if (c != null)
                {
                    this.gpSrc.Text = $"来源语言:  {c.Name} ( {c.DisplayName} - {c.EnglishName} )";
                }
                projectPath = Path.GetDirectoryName(this.helper.CurProject.ProjectFullPath);
                DirectoryInfo d = new DirectoryInfo(Path.Combine(projectPath, this.helper.CurProject.SrcLang));
                if (d.Exists)
                {
                    var fs = d.GetFiles("*");
                    foreach (var f in fs)
                    {
                        LangFile file = new LangFile(f.Name, f.FullName);
                        this.clbFiles.Items.Add(file);
                        string d2 = File.ReadAllText(f.FullName, Encoding.UTF8);
                        if (d2.IndexOf("\\u") > 0)
                        {
                            d2 = Regex.Replace(d2, @"\\u[\da-fA-F]{4}", m => ((char)Convert.ToInt32(m.Value.Substring(2), 16)).ToString());

                            File.WriteAllText(f.FullName, d2, Encoding.UTF8);
                        }

                    }
                }
                this.cbTargetLangs.Items.Clear();
                foreach (var lang in this.helper.CurProject.TgtLangs)
                {
                    this.cbTargetLangs.Items.Add(lang);
                }

                this.mnuTrans.Enabled = true;
                this.mnuSetup.Enabled = true;

                this.scMain.Enabled = true;
            }
            else
            {
                this.mnuTrans.Enabled = false;
                this.mnuSetup.Enabled = false;
                this.scMain.Enabled = false;
            }

        }



        private void mnuOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                helper.LoadProject(dialog.FileName);
                ReloadUI();
            }

        }


        DateTime lastInputTime;

        private void cbTargetLangs_TextUpdate(object sender, EventArgs e)
        {
            lastInputTime = DateTime.Now;
            string langName = this.cbTargetLangs.Text;
            if (cbTargetLangs.SelectedItem is LangItem a)
            {
                if (langName == a.ToString()) return;
            }
            CheckAndAddLang(langName);

        }

        void CheckAndAddLang(string langName)
        {
            new Task(async () =>
            {
                DateTime oldTime = lastInputTime;
                await Task.Delay(2000);
                if (lastInputTime == oldTime)
                {
                    var lang = helper.CurProject.TgtLangs.FirstOrDefault(m => m.Name == langName);
                    if (lang == null)
                    {
                        try
                        {
                            var newLang = this.helper.AddLang(langName);
                            if (newLang != null)
                            {
                                helper.SaveProcject();
                                this.Invoke(() =>
                                {
                                    this.cbTargetLangs.Items.Add(newLang);
                                    this.cbTargetLangs.SelectedItem = newLang;
                                });
                            }


                        }
                        catch { }
                    }
                }

            }).Start();
        }

        private void btnTransAll_Click(object sender, EventArgs e)
        {
            trans();
        }



        private void trans()
        {

            //源文件列表
            List<string> files = new List<string>();
            if (!this.isTranAllFile)
            {
                foreach (var item in this.clbFiles.SelectedItems)
                {
                    files.Add((item as LangFile).FilePath);
                }
            }
            else
            {
                string srcDir = Path.GetDirectoryName(this.helper.CurProject.ProjectFullPath);
                DirectoryInfo d = new DirectoryInfo(Path.Combine(srcDir, this.helper.CurProject.SrcLang));

                if (d.Exists)
                {
                    var fs = d.GetFiles("*");
                    foreach (var f in fs)
                    {
                        files.Add(f.FullName);
                    }
                }
            }
            this.translator = this.cbTransTools.SelectedItem as Translator;
            this.btnTransAllFile.Enabled = false;
            this.btnTransAllFile.Text = "正在翻译";

            Task.Run(() => TransTask(files));
        }

        void TransTask(List<string> files)
        {
            WriteMsg("开始翻译...");
            try
            {
                foreach (string file in files)
                {
                    WriteMsg2($"正在翻译{file}...");
                    TransFile(file);
                }
             
                this.helper.SaveProcject();
                WriteMsg($"翻译完成，共翻译了{this.translator.TransChars}个字符");

            }
            catch (Exception dis)
            {
                WriteMsg($"翻译中断:{dis.Message}");
            }
            //写入使用的翻译额度
            this.translator.SaveCharsTotal();
            this.Invoke(() =>
            {
                this.btnTransAllFile.Text = "翻译所有文件";
                this.btnTransAllFile.Enabled = true;
            });
        }

        private void TransFile(string file)
        {
            string fileName = Path.GetFileName(file);

            string data = File.ReadAllText(file, Encoding.UTF8);
            // Dictionary<string, object> dict = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(data);
            JsonDocument srcDocument = JsonDocument.Parse(data);

            foreach (var targetLang in this.helper.CurProject.TgtLangs)
            {
                if (targetLang.Name == this.helper.CurProject.SrcLang) continue;
                string langPath = Path.Combine(projectPath, targetLang.Name);
                if (!Directory.Exists(langPath))
                {
                    Directory.CreateDirectory(langPath);
                }
                string targetFile = Path.Combine(langPath, fileName);

                Dictionary<string, object> targetdict = new Dictionary<string, object>();
                JsonDocument targetDocument = null;
                if (File.Exists(targetFile))
                {
                    data = File.ReadAllText(targetFile, Encoding.UTF8);
                    try
                    {
                        targetDocument = System.Text.Json.JsonDocument.Parse(data);
                    }
                    catch { }
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    using var writer = new Utf8JsonWriter(ms, options: new JsonWriterOptions
                    {
                        Indented = true,
                        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                    });
                    TransEnv env = new TransEnv(targetLang.Name, langPath, writer);
                    ProcessElementValue(env, new JsonElementInfo { Element = srcDocument.RootElement, path = "" }
                    , targetDocument?.RootElement, targetLang.Name);
                    writer.Flush();
                    byte[] d1 = ms.ToArray();
                    File.WriteAllBytes(targetFile, d1);

                }
                this.Invoke(() =>
                {
                    this.cbTransTools.SelectedItem = this.cbTransTools.SelectedItem;

                });
                WriteMsg($"已翻译了{this.translator.TransChars}个字符");

            }
        }

        void WriteMsg(string msg)
        {
            this.Invoke(() =>
            {
                this.statMsg.Text = msg;
            });
        }


        void ProcessElementValue(TransEnv env, JsonElementInfo src, JsonElement? target, string targetLang)
        {
            switch (src.Element.ValueKind)
            {
                case JsonValueKind.Array:
                    ProcessArrayElement(env, src, target, targetLang);
                    break;
                case JsonValueKind.Object:
                    ProcessObjectElement(env, src, target, targetLang);
                    break;
                case JsonValueKind.String: //需要翻译的值 
                    string srcValue = src.Element.GetString();
                    string newVal = target?.GetString();
                    string srcVal = srcValue.Trim();
                    bool? changed = helper.CheckVerChanged(env, src.path, newVal, srcVal);

                    if (changed == true)
                    {
                        try
                        {
                            //在本地翻译库中查找
                            //  newVal = googleTranslator.Tans(this.helper.CurProject.SrcLang, targetLang, src.GetString()) ?? "";
                            // if (string.IsNullOrWhiteSpace(newVal)) {
                            newVal = this.translator.Tans(this.helper.CurProject.SrcLang, targetLang, srcVal) ?? "";
                            // }
                            helper.AddVer(env, src.path, newVal, srcVal);
                        }
                        catch (exs.DisTransException dis) {

                            WriteMsg2(dis.Message);
                            throw dis; }
                        catch (Exception e)
                        {
                            WriteMsg2(e.Message);

                        }
                    }
                    else
                    {

                        string key = $"{this.helper.CurProject.SrcLang}-{targetLang}-{srcVal}";
                        string sha = this.helper.GetSHA256(key);
                        {
                            this.translator.WriteLocalData(sha, this.helper.CurProject.SrcLang, targetLang,srcValue , newVal);
                        }
                    }
                    env.JsonWriter.WriteStringValue(newVal);
                    break;
                default: //其它普通类型直接写
                    src.Element.WriteTo(env.JsonWriter);
                    break;
            }
        }

        void WriteMsg2(string msg)
        {

            this.Invoke(() =>
            {
                this.rtbMessage.AppendText(msg+System.Environment.NewLine);
                

            });

        }

        void ProcessArrayElement(TransEnv env, JsonElementInfo src, JsonElement? target, string targetLang)
        {
            env.JsonWriter.WriteStartArray();
            int length = src.Element.GetArrayLength();
            for (int i = 0; i < length; i++)
            {
                JsonElement e = src.Element[i];
                JsonElement? tar = null;
                if (target != null)
                {
                    try
                    {
                        tar = target.Value[i];
                    }
                    catch
                    {
                    }
                }
                ProcessElementValue(env, new JsonElementInfo { Element = e, path = $"{src.path}" }, tar, targetLang);
            }

            env.JsonWriter.WriteEndArray();
        }

        void ProcessObjectElement(TransEnv env, JsonElementInfo src, JsonElement? target, string targetLang)
        {
            env.JsonWriter.WriteStartObject();
            // Write all the properties of the first document.
            // If a property exists in both documents, either:
            // * Merge them, if the value kinds match (e.g. both are objects or arrays),
            // * Completely override the value of the first with the one from the second, if the value kind mismatches (e.g. one is object, while the other is an array or string),
            // * Or favor the value of the first (regardless of what it may be), if the second one is null (i.e. don't override the first).
            foreach (JsonProperty property in src.Element.EnumerateObject())
            {
                string propertyName = property.Name;
                if (!string.IsNullOrEmpty(propertyName))
                {
                    env.JsonWriter.WritePropertyName(propertyName);
                    if (propertyName.StartsWith("_")) //不需要翻译的字段
                    {
                        property.Value.WriteTo(env.JsonWriter);
                        continue;
                    }
                }
                JsonElement? e = null;
                try
                {
                    e = target?.GetProperty(propertyName);
                }
                catch
                {

                }
                //忽略项直接跳过
                if (transIgnoreList?.FirstOrDefault(m => m == propertyName) != null)
                {
                    property.Value.WriteTo(env.JsonWriter);
                }
                else ProcessElementValue(env, new JsonElementInfo { Element = property.Value, path = $"{src.path}-{propertyName}" }, e, targetLang);
            }
            env.JsonWriter.WriteEndObject();

        }

        bool isTranAllFile = true;

        private void clbFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.btnTransAllFile.Enabled)
            {
                if (clbFiles.SelectedIndex >= 0)
                {
                    isTranAllFile = false;
                    if (this.btnTransAllFile.Enabled)
                    {
                        this.btnTransAllFile.Text = "翻译选中的文件";
                    }
                }
                else
                {
                    isTranAllFile = true;
                    if (this.btnTransAllFile.Enabled) this.btnTransAllFile.Text = "翻译所有文件";
                }
            }
            this.rtbSrc.Text = "";
            if (clbFiles.SelectedItem is LangFile file)
            {
                this.rtbSrc.Text = File.ReadAllText(file.FilePath, Encoding.UTF8);
                if (this.cbTargetLangs.SelectedIndex == -1)
                {
                    if (this.cbTargetLangs.Items.Count > 0) this.cbTargetLangs.SelectedIndex = 0;
                }
                else FlushFileData();

            }

        }

        void FlushFileData()
        {
            this.rtbDes.Text = "";
            if (clbFiles.SelectedItem is LangFile file)
            {
                if (this.cbTargetLangs.SelectedItem is LangItem item)
                {

                    string targetFileName = Path.Combine(projectPath, item.Name, file.FileName);
                    if (File.Exists(targetFileName))
                    {
                        this.rtbDes.Text = File.ReadAllText(targetFileName, Encoding.UTF8);
                    }
                }
            }
        }

        private void cbTargetLangs_SelectedIndexChanged(object sender, EventArgs e)
        {
            FlushFileData();
        }

        private void btnFormatFiles_Click(object sender, EventArgs e)
        {
            DirectoryInfo d = new DirectoryInfo(Path.Combine(projectPath, this.helper.CurProject.SrcLang));
            if (d.Exists)
            {
                var fs = d.GetFiles("*");
                foreach (var f in fs)
                {
                    string d2 = File.ReadAllText(f.FullName, Encoding.UTF8);
                    if (d2.IndexOf("\\u") > 0)
                    {
                        d2 = Regex.Replace(d2, @"\\u[\da-fA-F]{4}", m => ((char)Convert.ToInt32(m.Value.Substring(2), 16)).ToString());
                    }

                    var obj = System.Text.Json.JsonSerializer.Deserialize<object>(d2);
                    d2 = System.Text.Json.JsonSerializer.Serialize(obj, helper.JsonSerializerOptions);
                    File.WriteAllText(f.FullName, d2, Encoding.UTF8);


                }
            }
        }


    }
}
