﻿namespace translator
{
    partial class FrmSetup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            flpLangs = new FlowLayoutPanel();
            btnOk = new Button();
            panel1 = new Panel();
            lblSrcLangDetail = new Label();
            txtProjectName = new TextBox();
            label1 = new Label();
            txtSrcLang = new TextBox();
            lblsrcLang = new Label();
            panel2 = new Panel();
            groupBox1.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(flpLangs);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 91);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(965, 577);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "输出语种";
            // 
            // flpLangs
            // 
            flpLangs.AutoScroll = true;
            flpLangs.AutoSize = true;
            flpLangs.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flpLangs.Dock = DockStyle.Fill;
            flpLangs.Location = new Point(3, 19);
            flpLangs.Name = "flpLangs";
            flpLangs.Size = new Size(959, 555);
            flpLangs.TabIndex = 0;
            // 
            // btnOk
            // 
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Location = new Point(398, 9);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(197, 50);
            btnOk.TabIndex = 1;
            btnOk.Text = "确定";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(lblSrcLangDetail);
            panel1.Controls.Add(txtProjectName);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(txtSrcLang);
            panel1.Controls.Add(lblsrcLang);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(965, 91);
            panel1.TabIndex = 2;
            // 
            // lblSrcLangDetail
            // 
            lblSrcLangDetail.AutoSize = true;
            lblSrcLangDetail.Location = new Point(320, 50);
            lblSrcLangDetail.Name = "lblSrcLangDetail";
            lblSrcLangDetail.Size = new Size(0, 17);
            lblSrcLangDetail.TabIndex = 4;
            // 
            // txtProjectName
            // 
            txtProjectName.Location = new Point(92, 7);
            txtProjectName.Name = "txtProjectName";
            txtProjectName.Size = new Size(209, 23);
            txtProjectName.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 9);
            label1.Name = "label1";
            label1.Size = new Size(56, 17);
            label1.TabIndex = 2;
            label1.Text = "项目名称";
            // 
            // txtSrcLang
            // 
            txtSrcLang.Location = new Point(92, 47);
            txtSrcLang.Name = "txtSrcLang";
            txtSrcLang.Size = new Size(209, 23);
            txtSrcLang.TabIndex = 1;
            txtSrcLang.TextChanged += txtSrcLang_TextChanged;
            // 
            // lblsrcLang
            // 
            lblsrcLang.AutoSize = true;
            lblsrcLang.Location = new Point(27, 50);
            lblsrcLang.Name = "lblsrcLang";
            lblsrcLang.Size = new Size(44, 17);
            lblsrcLang.TabIndex = 0;
            lblsrcLang.Text = "源语言";
            // 
            // panel2
            // 
            panel2.Controls.Add(btnOk);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 668);
            panel2.Name = "panel2";
            panel2.Size = new Size(965, 68);
            panel2.TabIndex = 3;
            // 
            // FrmSetup
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(965, 736);
            Controls.Add(groupBox1);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "FrmSetup";
            Text = "FrmSetup";
            Load += FrmSetup_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button btnOk;
        private Panel panel1;
        private Label lblsrcLang;
        private TextBox txtSrcLang;
        private Panel panel2;
        private Label label1;
        private TextBox txtProjectName;
        private Label lblSrcLangDetail;
        private FlowLayoutPanel flpLangs;
    }
}