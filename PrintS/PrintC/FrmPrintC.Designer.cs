namespace PrintC
{
    partial class FrmPrintC
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
            this.lsTask = new System.Windows.Forms.ListBox();
            this.wbPrint = new System.Windows.Forms.WebBrowser();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lsCode = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lbPrint = new System.Windows.Forms.Label();
            this.lbTask = new System.Windows.Forms.Label();
            this.lbApp = new System.Windows.Forms.Label();
            this.lbPaper = new System.Windows.Forms.Label();
            this.lsAdv = new System.Windows.Forms.ListBox();
            this.lsEwm = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbData = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lsBot = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lsTask
            // 
            this.lsTask.FormattingEnabled = true;
            this.lsTask.ItemHeight = 12;
            this.lsTask.Location = new System.Drawing.Point(12, 150);
            this.lsTask.Name = "lsTask";
            this.lsTask.Size = new System.Drawing.Size(120, 196);
            this.lsTask.TabIndex = 1;
            // 
            // wbPrint
            // 
            this.wbPrint.Location = new System.Drawing.Point(264, 27);
            this.wbPrint.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbPrint.Name = "wbPrint";
            this.wbPrint.ScrollBarsEnabled = false;
            this.wbPrint.Size = new System.Drawing.Size(301, 320);
            this.wbPrint.TabIndex = 2;
            this.wbPrint.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wbPrint_DocumentCompleted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "打印任务队列：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(136, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "打印码：";
            // 
            // lsCode
            // 
            this.lsCode.FormattingEnabled = true;
            this.lsCode.ItemHeight = 12;
            this.lsCode.Location = new System.Drawing.Point(138, 150);
            this.lsCode.Name = "lsCode";
            this.lsCode.Size = new System.Drawing.Size(120, 196);
            this.lsCode.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(264, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "打印预览";
            // 
            // lbPrint
            // 
            this.lbPrint.AutoSize = true;
            this.lbPrint.Location = new System.Drawing.Point(12, 36);
            this.lbPrint.Name = "lbPrint";
            this.lbPrint.Size = new System.Drawing.Size(77, 12);
            this.lbPrint.TabIndex = 7;
            this.lbPrint.Text = "打印机状态：";
            // 
            // lbTask
            // 
            this.lbTask.AutoSize = true;
            this.lbTask.Location = new System.Drawing.Point(12, 61);
            this.lbTask.Name = "lbTask";
            this.lbTask.Size = new System.Drawing.Size(89, 12);
            this.lbTask.TabIndex = 8;
            this.lbTask.Text = "请求任务状态：";
            // 
            // lbApp
            // 
            this.lbApp.AutoSize = true;
            this.lbApp.Location = new System.Drawing.Point(12, 12);
            this.lbApp.Name = "lbApp";
            this.lbApp.Size = new System.Drawing.Size(65, 12);
            this.lbApp.TabIndex = 9;
            this.lbApp.Text = "程序状态：";
            // 
            // lbPaper
            // 
            this.lbPaper.AutoSize = true;
            this.lbPaper.Location = new System.Drawing.Point(12, 108);
            this.lbPaper.Name = "lbPaper";
            this.lbPaper.Size = new System.Drawing.Size(77, 12);
            this.lbPaper.TabIndex = 10;
            this.lbPaper.Text = "剩余打印纸：";
            // 
            // lsAdv
            // 
            this.lsAdv.FormattingEnabled = true;
            this.lsAdv.ItemHeight = 12;
            this.lsAdv.Location = new System.Drawing.Point(571, 27);
            this.lsAdv.Name = "lsAdv";
            this.lsAdv.Size = new System.Drawing.Size(120, 88);
            this.lsAdv.TabIndex = 11;
            // 
            // lsEwm
            // 
            this.lsEwm.FormattingEnabled = true;
            this.lsEwm.ItemHeight = 12;
            this.lsEwm.Location = new System.Drawing.Point(571, 136);
            this.lsEwm.Name = "lsEwm";
            this.lsEwm.Size = new System.Drawing.Size(120, 88);
            this.lsEwm.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(569, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "轮播图：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(571, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "二维码：";
            // 
            // lbData
            // 
            this.lbData.AutoSize = true;
            this.lbData.Location = new System.Drawing.Point(12, 85);
            this.lbData.Name = "lbData";
            this.lbData.Size = new System.Drawing.Size(65, 12);
            this.lbData.TabIndex = 15;
            this.lbData.Text = "数据更新：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(571, 229);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "打印底图：";
            // 
            // lsBot
            // 
            this.lsBot.FormattingEnabled = true;
            this.lsBot.ItemHeight = 12;
            this.lsBot.Location = new System.Drawing.Point(571, 244);
            this.lsBot.Name = "lsBot";
            this.lsBot.Size = new System.Drawing.Size(120, 88);
            this.lsBot.TabIndex = 16;
            // 
            // FrmPrintC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 404);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lsBot);
            this.Controls.Add(this.lbData);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lsEwm);
            this.Controls.Add(this.lsAdv);
            this.Controls.Add(this.lbPaper);
            this.Controls.Add(this.lbApp);
            this.Controls.Add(this.lbTask);
            this.Controls.Add(this.lbPrint);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lsCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.wbPrint);
            this.Controls.Add(this.lsTask);
            this.Name = "FrmPrintC";
            this.Text = "PrintC";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lsTask;
        private System.Windows.Forms.WebBrowser wbPrint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lsCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbPrint;
        private System.Windows.Forms.Label lbTask;
        private System.Windows.Forms.Label lbApp;
        private System.Windows.Forms.Label lbPaper;
        private System.Windows.Forms.ListBox lsAdv;
        private System.Windows.Forms.ListBox lsEwm;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbData;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox lsBot;

    }
}

