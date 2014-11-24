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

namespace PrintS.UC
{
    /// <summary>
    /// PrintTask.xaml 的交互逻辑
    /// </summary>
    public partial class PrintTask : UserControl
    {
        public PrintTask()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 显示当前的任务队列
        /// </summary>
        /// <param name="ls_img"></param>
        public void ShowTask(List<BitmapImage> ls_img)
        {
            // 图片队列
            List<Image> listImg = new List<Image>();
            listImg.Add(this.image1);
            listImg.Add(this.image2);
            listImg.Add(this.image3);
            listImg.Add(this.image4);
            listImg.Add(this.image5);
            listImg.Add(this.image6);
            for (int i = 0; i < 6; i++)
            {
                BitmapImage img;

                if (i >= ls_img.Count())
                {
                    img = new BitmapImage();
                }
                else
                {
                    try
                    {
                        img = ls_img[i];
                    }
                    catch (Exception ex)
                    {
                        img = new BitmapImage();
                    }
                }

                listImg[i].Source = img;
            }

        }
    }
}
