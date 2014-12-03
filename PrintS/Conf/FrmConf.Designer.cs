namespace Conf
{
    partial class FrmConf
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpConfig = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txt_appId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_dbPath = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txt_printer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_pLimit = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ckb_isData = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_urlAdv = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_urlTask = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_urlPost = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_urlCode = new System.Windows.Forms.TextBox();
            this.btnConfCancel = new System.Windows.Forms.Button();
            this.btnConfOK = new System.Windows.Forms.Button();
            this.tpPrint = new System.Windows.Forms.TabPage();
            this.btnPrint = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.wb_Print = new System.Windows.Forms.WebBrowser();
            this.img_logo = new System.Windows.Forms.PictureBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnUpdateBOT = new System.Windows.Forms.Button();
            this.btnResetBOT = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tpConfig.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tpPrint.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.img_logo)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpConfig);
            this.tabControl1.Controls.Add(this.tpPrint);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(606, 473);
            this.tabControl1.TabIndex = 0;
            // 
            // tpConfig
            // 
            this.tpConfig.Controls.Add(this.groupBox5);
            this.tpConfig.Controls.Add(this.groupBox4);
            this.tpConfig.Controls.Add(this.groupBox3);
            this.tpConfig.Controls.Add(this.groupBox2);
            this.tpConfig.Controls.Add(this.btnConfCancel);
            this.tpConfig.Controls.Add(this.btnConfOK);
            this.tpConfig.Location = new System.Drawing.Point(4, 22);
            this.tpConfig.Name = "tpConfig";
            this.tpConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tpConfig.Size = new System.Drawing.Size(598, 447);
            this.tpConfig.TabIndex = 0;
            this.tpConfig.Text = "设置";
            this.tpConfig.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txt_appId);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.txt_dbPath);
            this.groupBox5.Location = new System.Drawing.Point(27, 7);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(450, 83);
            this.groupBox5.TabIndex = 24;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "基本设置";
            // 
            // txt_appId
            // 
            this.txt_appId.Location = new System.Drawing.Point(86, 23);
            this.txt_appId.Name = "txt_appId";
            this.txt_appId.Size = new System.Drawing.Size(200, 21);
            this.txt_appId.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "设备id";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "数据库名";
            // 
            // txt_dbPath
            // 
            this.txt_dbPath.Location = new System.Drawing.Point(86, 50);
            this.txt_dbPath.Name = "txt_dbPath";
            this.txt_dbPath.Size = new System.Drawing.Size(200, 21);
            this.txt_dbPath.TabIndex = 3;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txt_printer);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.txt_pLimit);
            this.groupBox4.Location = new System.Drawing.Point(27, 104);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(450, 83);
            this.groupBox4.TabIndex = 23;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "打印设置";
            // 
            // txt_printer
            // 
            this.txt_printer.Location = new System.Drawing.Point(86, 20);
            this.txt_printer.Name = "txt_printer";
            this.txt_printer.Size = new System.Drawing.Size(200, 21);
            this.txt_printer.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "打印机名";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "打印预警数";
            // 
            // txt_pLimit
            // 
            this.txt_pLimit.Location = new System.Drawing.Point(86, 47);
            this.txt_pLimit.Name = "txt_pLimit";
            this.txt_pLimit.Size = new System.Drawing.Size(200, 21);
            this.txt_pLimit.TabIndex = 7;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ckb_isData);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txt_urlAdv);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Location = new System.Drawing.Point(27, 327);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(450, 79);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "数据更新";
            // 
            // ckb_isData
            // 
            this.ckb_isData.AutoSize = true;
            this.ckb_isData.Location = new System.Drawing.Point(86, 20);
            this.ckb_isData.Name = "ckb_isData";
            this.ckb_isData.Size = new System.Drawing.Size(48, 16);
            this.ckb_isData.TabIndex = 20;
            this.ckb_isData.Text = "更新";
            this.ckb_isData.UseVisualStyleBackColor = true;
            this.ckb_isData.CheckedChanged += new System.EventHandler(this.ckb_isData_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "数据更新";
            // 
            // txt_urlAdv
            // 
            this.txt_urlAdv.Location = new System.Drawing.Point(86, 42);
            this.txt_urlAdv.Name = "txt_urlAdv";
            this.txt_urlAdv.Size = new System.Drawing.Size(322, 21);
            this.txt_urlAdv.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 16;
            this.label9.Text = "是否更新";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_urlTask);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txt_urlPost);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txt_urlCode);
            this.groupBox2.Location = new System.Drawing.Point(27, 203);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(450, 109);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "请求地址";
            // 
            // txt_urlTask
            // 
            this.txt_urlTask.Location = new System.Drawing.Point(86, 20);
            this.txt_urlTask.Name = "txt_urlTask";
            this.txt_urlTask.Size = new System.Drawing.Size(322, 21);
            this.txt_urlTask.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "请求任务";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "返回结果";
            // 
            // txt_urlPost
            // 
            this.txt_urlPost.Location = new System.Drawing.Point(86, 47);
            this.txt_urlPost.Name = "txt_urlPost";
            this.txt_urlPost.Size = new System.Drawing.Size(322, 21);
            this.txt_urlPost.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "请求打印码";
            // 
            // txt_urlCode
            // 
            this.txt_urlCode.Location = new System.Drawing.Point(86, 74);
            this.txt_urlCode.Name = "txt_urlCode";
            this.txt_urlCode.Size = new System.Drawing.Size(322, 21);
            this.txt_urlCode.TabIndex = 13;
            // 
            // btnConfCancel
            // 
            this.btnConfCancel.Location = new System.Drawing.Point(108, 415);
            this.btnConfCancel.Name = "btnConfCancel";
            this.btnConfCancel.Size = new System.Drawing.Size(75, 23);
            this.btnConfCancel.TabIndex = 19;
            this.btnConfCancel.Text = "取消";
            this.btnConfCancel.UseVisualStyleBackColor = true;
            this.btnConfCancel.Click += new System.EventHandler(this.btnConfCancel_Click);
            // 
            // btnConfOK
            // 
            this.btnConfOK.Location = new System.Drawing.Point(27, 415);
            this.btnConfOK.Name = "btnConfOK";
            this.btnConfOK.Size = new System.Drawing.Size(75, 23);
            this.btnConfOK.TabIndex = 18;
            this.btnConfOK.Text = "更新";
            this.btnConfOK.UseVisualStyleBackColor = true;
            this.btnConfOK.Click += new System.EventHandler(this.btnConfOK_Click);
            // 
            // tpPrint
            // 
            this.tpPrint.Controls.Add(this.groupBox6);
            this.tpPrint.Controls.Add(this.btnPrint);
            this.tpPrint.Controls.Add(this.groupBox1);
            this.tpPrint.Location = new System.Drawing.Point(4, 22);
            this.tpPrint.Name = "tpPrint";
            this.tpPrint.Padding = new System.Windows.Forms.Padding(3);
            this.tpPrint.Size = new System.Drawing.Size(598, 447);
            this.tpPrint.TabIndex = 1;
            this.tpPrint.Text = "打印";
            this.tpPrint.UseVisualStyleBackColor = true;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(309, 418);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "测试打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.wb_Print);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(297, 435);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "打印预览";
            // 
            // wb_Print
            // 
            this.wb_Print.Location = new System.Drawing.Point(6, 20);
            this.wb_Print.MinimumSize = new System.Drawing.Size(20, 20);
            this.wb_Print.Name = "wb_Print";
            this.wb_Print.ScrollBarsEnabled = false;
            this.wb_Print.Size = new System.Drawing.Size(282, 400);
            this.wb_Print.TabIndex = 1;
            // 
            // img_logo
            // 
            this.img_logo.Location = new System.Drawing.Point(11, 20);
            this.img_logo.Name = "img_logo";
            this.img_logo.Size = new System.Drawing.Size(266, 80);
            this.img_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.img_logo.TabIndex = 6;
            this.img_logo.TabStop = false;
            this.img_logo.Click += new System.EventHandler(this.img_logo_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnResetBOT);
            this.groupBox6.Controls.Add(this.btnUpdateBOT);
            this.groupBox6.Controls.Add(this.img_logo);
            this.groupBox6.Location = new System.Drawing.Point(309, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(283, 140);
            this.groupBox6.TabIndex = 7;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "打印底图";
            // 
            // btnUpdateBOT
            // 
            this.btnUpdateBOT.Location = new System.Drawing.Point(11, 106);
            this.btnUpdateBOT.Name = "btnUpdateBOT";
            this.btnUpdateBOT.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateBOT.TabIndex = 7;
            this.btnUpdateBOT.Text = "网络更新";
            this.btnUpdateBOT.UseVisualStyleBackColor = true;
            this.btnUpdateBOT.Click += new System.EventHandler(this.btnUpdateBOT_Click);
            // 
            // btnResetBOT
            // 
            this.btnResetBOT.Location = new System.Drawing.Point(92, 106);
            this.btnResetBOT.Name = "btnResetBOT";
            this.btnResetBOT.Size = new System.Drawing.Size(75, 23);
            this.btnResetBOT.TabIndex = 8;
            this.btnResetBOT.Text = "还原默认";
            this.btnResetBOT.UseVisualStyleBackColor = true;
            this.btnResetBOT.Click += new System.EventHandler(this.btnResetBOT_Click);
            // 
            // FrmConf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 497);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmConf";
            this.Text = "Conf";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpConfig.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tpPrint.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.img_logo)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpConfig;
        private System.Windows.Forms.TabPage tpPrint;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_urlAdv;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_urlCode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_urlPost;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_urlTask;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_pLimit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_printer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_dbPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_appId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConfCancel;
        private System.Windows.Forms.Button btnConfOK;
        private System.Windows.Forms.CheckBox ckb_isData;
        private System.Windows.Forms.WebBrowser wb_Print;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.PictureBox img_logo;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnUpdateBOT;
        private System.Windows.Forms.Button btnResetBOT;
    }
}

