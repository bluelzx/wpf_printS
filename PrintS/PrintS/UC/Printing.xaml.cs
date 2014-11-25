using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Media.Animation;

namespace PrintS.UC
{
    /// <summary>
    /// ProBar.xaml 的交互逻辑
    /// </summary>
    public partial class Printing : UserControl
    {
        public Printing()
        {
            InitializeComponent();
        }

        private Storyboard _sb_probar;
        /// <summary>
        /// 滚动条动画
        /// </summary>
        public Storyboard sb_probar
        {
            get 
            {
                if (_sb_probar == null)
                {
                    _sb_probar = new Storyboard();
                    DoubleAnimation da = new DoubleAnimation();
                    da.From = 0;
                    da.To = this.brPro.Width;
                    da.Duration = new Duration(new TimeSpan(0, 0, 38));
                    _sb_probar.Children.Add(da);
                    _sb_probar.Completed += new EventHandler(_sb_probar_Completed);
                    Storyboard.SetTarget(da, this.gdBar);
                    Storyboard.SetTargetProperty(da, new PropertyPath("(Grid.Width)"));
                }
                return _sb_probar;
            }
        }

        // 进度条完成后
        void _sb_probar_Completed(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 隐藏当前打印框
        /// </summary>
        public void Hidden()
        {
            this.gdMain.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// 显示当前打印框
        /// </summary>
        public void Show()
        {
            this.gdMain.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 显示进度条
        /// </summary>
        public void ShowProBar()
        {
            // 显示主框
            this.Show();

            // 隐藏缺纸框
            this.gdPaper.Visibility = Visibility.Hidden;

            // 显示进度条
            sb_probar.Begin();
            this.gdProBar.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 显示缺纸框
        /// </summary>
        public void ShowOTPaper()
        {
            // 显示主框
            this.Show();

            // 隐藏进度条
            this.gdProBar.Visibility = Visibility.Hidden;

            // 显示缺纸框
            this.gdPaper.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 补充纸的操作
        /// </summary>
        public event EventHandler BtnPaperEvent;
        private void btnParper_Click(object sender, RoutedEventArgs e)
        {
            // 显示进度条
            this.ShowProBar();

            // 需要的数据操作
            BtnPaperEvent.BeginInvoke(null, null, null, null);
        }

    }
}
