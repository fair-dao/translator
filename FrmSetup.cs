using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using translator.entities;

namespace translator
{
    public partial class FrmSetup : Form
    {
        private Helper helper;
        public FrmSetup(Helper helper)
        {
            InitializeComponent();
            this.helper = helper;
        }

        private void FrmSetup_Load(object sender, EventArgs e)
        {
            if (this.helper?.CurProject == null) return;
            this.txtProjectName.Text = this.helper.CurProject.ProjectName;
            this.txtSrcLang.Text = this.helper.CurProject.SrcLang;
            var allCultures = CultureInfo.GetCultures(CultureTypes.NeutralCultures);
            foreach (var c in allCultures)
            {
                if (c.Name?.Length > 1)
                {
                 
                    var found = this.helper.CurProject.TgtLangs.FirstOrDefault(m => m.Name == c.Name);
                    LangItem lang = new LangItem { Name = c.Name, DisplayName = found?.DisplayName ?? c.DisplayName, EnglishName = c.EnglishName };
                    CheckBox cb = new CheckBox();
                    cb.Text = lang.ToString();
                    cb.Tag = lang;
                    cb.Width = 256;
                    cb.Checked = found != null;
                    this.flpLangs.Controls.Add(cb);
                    
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            List<LangItem> items = new List<LangItem>();
            int count = this.flpLangs.Controls.Count;
            this.helper.CurProject.TgtLangs = null;
            for (int i=0;i<count;i++)
            {
                if (this.flpLangs.Controls[i] is CheckBox c)
                {
                    if (c.Checked &&  c.Tag is LangItem a)
                    {
                        this.helper.AddLang(a.Name);
                    }
                }
            }
            helper.CurProject.ProjectName = this.txtProjectName.Text.Trim();
            helper.SaveProcject();
            this.Close();
        }

        private void txtSrcLang_TextChanged(object sender, EventArgs e)
        {
            this.lblSrcLangDetail.Text = "";
            try
            {
                CultureInfo c = CultureInfo.GetCultureInfo(this.txtSrcLang.Text);
                this.lblSrcLangDetail.Text = $"{c.DisplayName} - {c.EnglishName}";
            }
            catch
            {

            }
        }
    }
}
