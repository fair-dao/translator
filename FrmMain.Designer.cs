namespace translator
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            toolStripContainer1 = new ToolStripContainer();
            statusStrip1 = new StatusStrip();
            statMsg = new ToolStripStatusLabel();
            scMain = new SplitContainer();
            groupBox1 = new GroupBox();
            clbFiles = new CheckedListBox();
            panel1 = new Panel();
            btnFormatFiles = new Button();
            cbTransTools = new ComboBox();
            btnTransAllFile = new Button();
            scTrans = new SplitContainer();
            gpSrc = new GroupBox();
            rtbSrc = new RichTextBox();
            groupBox2 = new GroupBox();
            rtbDes = new RichTextBox();
            panel2 = new Panel();
            cbTargetLangs = new ComboBox();
            label1 = new Label();
            menuStrip1 = new MenuStrip();
            mnuNew = new ToolStripMenuItem();
            mnuOpen = new ToolStripMenuItem();
            mnuTrans = new ToolStripMenuItem();
            mnuSetup = new ToolStripMenuItem();
            groupBox3 = new GroupBox();
            rtbMessage = new RichTextBox();
            toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            toolStripContainer1.ContentPanel.SuspendLayout();
            toolStripContainer1.TopToolStripPanel.SuspendLayout();
            toolStripContainer1.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)scMain).BeginInit();
            scMain.Panel1.SuspendLayout();
            scMain.Panel2.SuspendLayout();
            scMain.SuspendLayout();
            groupBox1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)scTrans).BeginInit();
            scTrans.Panel1.SuspendLayout();
            scTrans.Panel2.SuspendLayout();
            scTrans.SuspendLayout();
            gpSrc.SuspendLayout();
            groupBox2.SuspendLayout();
            panel2.SuspendLayout();
            menuStrip1.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            toolStripContainer1.BottomToolStripPanel.Controls.Add(statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            toolStripContainer1.ContentPanel.Controls.Add(scMain);
            toolStripContainer1.ContentPanel.Margin = new Padding(4, 4, 4, 4);
            toolStripContainer1.ContentPanel.Size = new Size(1238, 684);
            toolStripContainer1.Dock = DockStyle.Fill;
            toolStripContainer1.Location = new Point(0, 0);
            toolStripContainer1.Margin = new Padding(4, 4, 4, 4);
            toolStripContainer1.Name = "toolStripContainer1";
            toolStripContainer1.Size = new Size(1238, 738);
            toolStripContainer1.TabIndex = 0;
            toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            toolStripContainer1.TopToolStripPanel.Controls.Add(menuStrip1);
            // 
            // statusStrip1
            // 
            statusStrip1.Dock = DockStyle.None;
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { statMsg });
            statusStrip1.Location = new Point(0, 0);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1238, 26);
            statusStrip1.TabIndex = 0;
            // 
            // statMsg
            // 
            statMsg.Name = "statMsg";
            statMsg.Size = new Size(21, 20);
            statMsg.Text = "--";
            // 
            // scMain
            // 
            scMain.Dock = DockStyle.Fill;
            scMain.Location = new Point(0, 0);
            scMain.Margin = new Padding(4, 4, 4, 4);
            scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            scMain.Panel1.Controls.Add(groupBox1);
            // 
            // scMain.Panel2
            // 
            scMain.Panel2.Controls.Add(scTrans);
            scMain.Size = new Size(1238, 684);
            scMain.SplitterDistance = 385;
            scMain.SplitterWidth = 5;
            scMain.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(clbFiles);
            groupBox1.Controls.Add(panel1);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(4, 4, 4, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 4, 4, 4);
            groupBox1.Size = new Size(385, 684);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "文件列表";
            // 
            // clbFiles
            // 
            clbFiles.Dock = DockStyle.Fill;
            clbFiles.FormattingEnabled = true;
            clbFiles.Location = new Point(4, 205);
            clbFiles.Margin = new Padding(4, 4, 4, 4);
            clbFiles.Name = "clbFiles";
            clbFiles.Size = new Size(377, 475);
            clbFiles.TabIndex = 2;
            clbFiles.SelectedIndexChanged += clbFiles_SelectedIndexChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnFormatFiles);
            panel1.Controls.Add(cbTransTools);
            panel1.Controls.Add(btnTransAllFile);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(4, 24);
            panel1.Margin = new Padding(4, 4, 4, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(377, 181);
            panel1.TabIndex = 1;
            // 
            // btnFormatFiles
            // 
            btnFormatFiles.Location = new Point(203, 52);
            btnFormatFiles.Margin = new Padding(4, 4, 4, 4);
            btnFormatFiles.Name = "btnFormatFiles";
            btnFormatFiles.Size = new Size(126, 40);
            btnFormatFiles.TabIndex = 5;
            btnFormatFiles.Text = "美化原文档";
            btnFormatFiles.UseVisualStyleBackColor = true;
            btnFormatFiles.Click += btnFormatFiles_Click;
            // 
            // cbTransTools
            // 
            cbTransTools.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTransTools.FormattingEnabled = true;
            cbTransTools.Location = new Point(12, 131);
            cbTransTools.Margin = new Padding(4, 4, 4, 4);
            cbTransTools.Name = "cbTransTools";
            cbTransTools.Size = new Size(352, 28);
            cbTransTools.TabIndex = 4;
            // 
            // btnTransAllFile
            // 
            btnTransAllFile.Location = new Point(36, 52);
            btnTransAllFile.Margin = new Padding(4, 4, 4, 4);
            btnTransAllFile.Name = "btnTransAllFile";
            btnTransAllFile.Size = new Size(126, 40);
            btnTransAllFile.TabIndex = 3;
            btnTransAllFile.Text = "翻译所有文件";
            btnTransAllFile.UseVisualStyleBackColor = true;
            btnTransAllFile.Click += btnTransAll_Click;
            // 
            // scTrans
            // 
            scTrans.Dock = DockStyle.Fill;
            scTrans.Location = new Point(0, 0);
            scTrans.Margin = new Padding(4, 4, 4, 4);
            scTrans.Name = "scTrans";
            scTrans.Orientation = Orientation.Horizontal;
            // 
            // scTrans.Panel1
            // 
            scTrans.Panel1.Controls.Add(gpSrc);
            // 
            // scTrans.Panel2
            // 
            scTrans.Panel2.Controls.Add(groupBox2);
            scTrans.Panel2.Controls.Add(groupBox3);
            scTrans.Size = new Size(848, 684);
            scTrans.SplitterDistance = 299;
            scTrans.SplitterWidth = 5;
            scTrans.TabIndex = 0;
            // 
            // gpSrc
            // 
            gpSrc.Controls.Add(rtbSrc);
            gpSrc.Dock = DockStyle.Fill;
            gpSrc.Location = new Point(0, 0);
            gpSrc.Margin = new Padding(4, 4, 4, 4);
            gpSrc.Name = "gpSrc";
            gpSrc.Padding = new Padding(4, 4, 4, 4);
            gpSrc.Size = new Size(848, 299);
            gpSrc.TabIndex = 0;
            gpSrc.TabStop = false;
            gpSrc.Text = "原文";
            // 
            // rtbSrc
            // 
            rtbSrc.Dock = DockStyle.Fill;
            rtbSrc.Location = new Point(4, 24);
            rtbSrc.Margin = new Padding(4, 4, 4, 4);
            rtbSrc.Name = "rtbSrc";
            rtbSrc.Size = new Size(840, 271);
            rtbSrc.TabIndex = 0;
            rtbSrc.Text = "";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(rtbDes);
            groupBox2.Controls.Add(panel2);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(0, 0);
            groupBox2.Margin = new Padding(4, 4, 4, 4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 4, 4, 4);
            groupBox2.Size = new Size(848, 228);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "译文";
            // 
            // rtbDes
            // 
            rtbDes.Dock = DockStyle.Fill;
            rtbDes.Location = new Point(4, 75);
            rtbDes.Margin = new Padding(4, 4, 4, 4);
            rtbDes.Name = "rtbDes";
            rtbDes.Size = new Size(840, 149);
            rtbDes.TabIndex = 0;
            rtbDes.Text = "";
            // 
            // panel2
            // 
            panel2.Controls.Add(cbTargetLangs);
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(4, 24);
            panel2.Margin = new Padding(4, 4, 4, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(840, 51);
            panel2.TabIndex = 4;
            // 
            // cbTargetLangs
            // 
            cbTargetLangs.FormattingEnabled = true;
            cbTargetLangs.Location = new Point(138, 11);
            cbTargetLangs.Margin = new Padding(4, 4, 4, 4);
            cbTargetLangs.Name = "cbTargetLangs";
            cbTargetLangs.Size = new Size(345, 28);
            cbTargetLangs.TabIndex = 1;
            cbTargetLangs.SelectedIndexChanged += cbTargetLangs_SelectedIndexChanged;
            cbTargetLangs.TextUpdate += cbTargetLangs_TextUpdate;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 16);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(69, 20);
            label1.TabIndex = 2;
            label1.Text = "目标语言";
            // 
            // menuStrip1
            // 
            menuStrip1.Dock = DockStyle.None;
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { mnuNew, mnuOpen, mnuTrans, mnuSetup });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1238, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // mnuNew
            // 
            mnuNew.Name = "mnuNew";
            mnuNew.Size = new Size(53, 24);
            mnuNew.Text = "新建";
            mnuNew.Click += mnuNew_Click;
            // 
            // mnuOpen
            // 
            mnuOpen.Name = "mnuOpen";
            mnuOpen.Size = new Size(53, 24);
            mnuOpen.Text = "打开";
            mnuOpen.Click += mnuOpen_Click;
            // 
            // mnuTrans
            // 
            mnuTrans.Name = "mnuTrans";
            mnuTrans.Size = new Size(113, 24);
            mnuTrans.Text = "翻译所有文件";
            // 
            // mnuSetup
            // 
            mnuSetup.Name = "mnuSetup";
            mnuSetup.Size = new Size(53, 24);
            mnuSetup.Text = "设置";
            mnuSetup.Click += mnuSetup_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(rtbMessage);
            groupBox3.Dock = DockStyle.Bottom;
            groupBox3.Location = new Point(0, 228);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(848, 152);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "信息";
            // 
            // rtbMessage
            // 
            rtbMessage.Dock = DockStyle.Fill;
            rtbMessage.Location = new Point(3, 23);
            rtbMessage.Name = "rtbMessage";
            rtbMessage.Size = new Size(842, 126);
            rtbMessage.TabIndex = 0;
            rtbMessage.Text = "";
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1238, 738);
            Controls.Add(toolStripContainer1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4, 4, 4, 4);
            Name = "FrmMain";
            Text = "Form1";
            Load += FrmMain_Load;
            toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            toolStripContainer1.BottomToolStripPanel.PerformLayout();
            toolStripContainer1.ContentPanel.ResumeLayout(false);
            toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            toolStripContainer1.TopToolStripPanel.PerformLayout();
            toolStripContainer1.ResumeLayout(false);
            toolStripContainer1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            scMain.Panel1.ResumeLayout(false);
            scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)scMain).EndInit();
            scMain.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            scTrans.Panel1.ResumeLayout(false);
            scTrans.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)scTrans).EndInit();
            scTrans.ResumeLayout(false);
            gpSrc.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ToolStripContainer toolStripContainer1;
        private StatusStrip statusStrip1;
        private MenuStrip menuStrip1;
        private SplitContainer scMain;
        private GroupBox groupBox1;
        private SplitContainer scTrans;
        private GroupBox gpSrc;
        private RichTextBox rtbSrc;
        private GroupBox groupBox2;
        private RichTextBox rtbDes;
        private ToolStripMenuItem mnuOpen;
        private ToolStripMenuItem mnuTrans;
        private Panel panel1;
        private CheckedListBox clbFiles;
        private ToolStripMenuItem mnuSetup;
        private ToolStripMenuItem mnuNew;
        private Button btnTransAllFile;
        private ToolStripStatusLabel statMsg;
        private ComboBox cbTransTools;
        private Panel panel2;
        private ComboBox cbTargetLangs;
        private Label label1;
        private Button btnFormatFiles;
        private GroupBox groupBox3;
        private RichTextBox rtbMessage;
    }
}
