using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// DataContract.dll
using DataContract.Model;
using DataContract.Controller;

using System.IO;

// Newtonsoft.Json.dll
using Newtonsoft.Json.Linq;

namespace Conf
{
    public partial class FrmConf : Form
    {
        public FrmConf()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.loadAppConf();
            this.loadPrintConf();
        }

        private void btnConfOK_Click(object sender, EventArgs e)
        {
            this.saveAppConf();
        }

        private void btnConfCancel_Click(object sender, EventArgs e)
        {
            this.loadAppConf();
        }

        /// <summary>
        /// 显示当前的打印预览
        /// </summary>
        void loadPrintConf()
        {
            // 用户照片
            string photo = string.Format(@"{0}\photo.png", AppClient.pathPrint);
            // 打印底图            
            string logo = string.Format(@"{0}\logo.png", AppClient.pathPrint);
            Data bot = DB.bot.getDataModel();
            if (bot.pid > 0)
            {
                logo = bot.url; // 加载url地址
                logo = string.Format(@"{0}\{1}", AppClient.pathBot, bot.pic);    // 暂不显示本地的.jpg打印底图
            }
            // 打印底图位置
            this.img_logo.Image = Image.FromFile(logo);
            // 打印预览
            string html = File.ReadAllText(string.Format(@"{0}\Print.html", AppClient.pathPrint));
            html = html.Replace("$photo", photo);
            html = html.Replace("$logo", logo);
            this.wb_Print.DocumentText = html;
        }

        /// <summary>
        /// 显示当前配置
        /// </summary>
        void loadAppConf()
        {
            this.txt_appId.Text = AppClient.appId;
            this.txt_dbPath.Text = AppClient.dbPath;
            this.txt_printer.Text = AppClient.printer;
            this.txt_pLimit.Text = AppClient.pLimit.ToString();
            this.txt_urlTask.Text = AppClient.urlTask;
            this.txt_urlPost.Text = AppClient.urlPost;
            this.txt_urlCode.Text = AppClient.urlCode;
            this.txt_urlAdv.Text = AppClient.urlAdv;
            this.ckb_isData.Checked = AppClient.isData;
            this.txt_urlAdv.Enabled = this.ckb_isData.Checked;
        }

        /// <summary>
        /// 保存本次的配置
        /// </summary>
        void saveAppConf()
        {
            AppClient.appId = this.txt_appId.Text;
            AppClient.dbPath = this.txt_dbPath.Text;
            AppClient.printer = this.txt_printer.Text;
            AppClient.pLimit = Convert.ToInt32(this.txt_pLimit.Text);
            AppClient.urlTask = this.txt_urlTask.Text;
            AppClient.urlPost = this.txt_urlPost.Text;
            AppClient.urlCode = this.txt_urlCode.Text;
            AppClient.urlAdv = this.txt_urlAdv.Text;
            AppClient.isData = this.ckb_isData.Checked;
            MessageBox.Show("更新成功!");
        }

        // 更改是否可以数据更新
        private void ckb_isData_CheckedChanged(object sender, EventArgs e)
        {
            this.txt_urlAdv.Enabled = this.ckb_isData.Checked;
        }

        // 测试打印
        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.wb_Print.Print();
        }

        // 单击打印底图更换为本地图片
        private void img_logo_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string file_path = fileDialog.FileName;
                string file_ext = Path.GetExtension(file_path);
                string[] ext_arr = new string[] { ".png", ".jpge", ".jpg" };
                if (!ext_arr.Contains(file_ext))
                {
                    MessageBox.Show("仅能上传png,jpge,jpg格式的图片！");
                    return;
                }
                // 更新为本地图片资源
                DB.bot.setLocalFile(file_path);
                // 刷新预览区
                this.loadPrintConf();
                MessageBox.Show("更新成功!");
            }
        }

        // 立即更新打印底图，从网络更新
        private void btnUpdateBOT_Click(object sender, EventArgs e)
        {
            // 网络请求
            JObject result = DB.adv.postUrl();
            // 更新轮播图
            DB.bot.Update(result);
            // 刷新预览区
            this.loadPrintConf();
            MessageBox.Show("更新成功!");
        }

        // 还原打印底图为默认图片
        private void btnResetBOT_Click(object sender, EventArgs e)
        {
            // 清空数据
            DB.bot.delOtherData(new string[]{});
            // 刷新预览区
            this.loadPrintConf();
            MessageBox.Show("还原成功!");
        }
    }
}
