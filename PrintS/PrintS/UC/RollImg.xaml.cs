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
    /// RollImg.xaml 的交互逻辑
    /// </summary>
    public partial class RollImg : UserControl
    {
        public RollImg()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 动画静止时间
        /// </summary>
        public double TimeHold
        {
            get { return (double)GetValue(TimeHoldProperty); }
            set { SetValue(TimeHoldProperty, value); }
        }
        public static readonly DependencyProperty TimeHoldProperty =
            DependencyProperty.Register("TimeHold", typeof(double), // 注册属性名
            typeof(RollImg),    // 所属控件
            new PropertyMetadata(5.0) // 默认值
            );

        /// <summary>
        /// 动画播放时间
        /// </summary>
        public double TimeChange
        {
            get { return (double)GetValue(TimeChangeProperty); }
            set { SetValue(TimeChangeProperty, value); }
        }
        public static readonly DependencyProperty TimeChangeProperty =
            DependencyProperty.Register("TimeChange", typeof(double), // 注册属性名
            typeof(RollImg),    // 所属控件
            new PropertyMetadata(3.0) // 默认值
            );

        // 枚举动画类型
        public enum StoryTypes
        {
            /// <summary>
            /// 从右向左
            /// </summary>
            RightToLeft = 1,
            /// <summary>
            /// 从左向右
            /// </summary>
            LeftToRight,
            /// <summary>
            /// 从上向下
            /// </summary>
            TopToBottom,
            /// <summary>
            /// 从下向上
            /// </summary>
            BottomToTop,
            /// <summary>
            /// 不透明度
            /// </summary>
            Opacity
        }
        /// <summary>
        /// 动画类型
        /// </summary>
        public StoryTypes StoryType
        {
            get { return (StoryTypes)GetValue(StoryTypeProperty); }
            set { SetValue(StoryTypeProperty, value); }
        }
        /// <summary>
        /// 在xaml中操作属性，需要声明为DependencyProperty
        /// </summary>
        public static readonly DependencyProperty StoryTypeProperty =
            DependencyProperty.Register("StoryType", typeof(StoryTypes),
            typeof(RollImg),
            new PropertyMetadata(StoryTypes.LeftToRight)
            );

        /// <summary>
        /// 是否开始滚动
        /// </summary>
        public bool isBegin = false;

        /// <summary>
        /// 本轮剩余滚动数
        /// </summary>
        public int rollNum = 0;

        private List<BitmapImage> _ls_images;
        /// <summary>
        /// 滚动图片组
        /// </summary>
        public List<BitmapImage> ls_images
        {
            set
            {
                if (rollNum > 0)
                {
                    // 本轮滚动未结束
                }
                else
                {
                    // 开始新的一轮滚动
                    _ls_images = value;
                    rollNum = _ls_images.Count();
                }
            }
            get { return _ls_images; }
        }

        /// <summary>
        /// 滚动宽度
        /// </summary>
        public double width
        {
            get { return this.canvas_board.Width; }
        }

        /// <summary>
        /// 滚动高度
        /// </summary>
        public double height
        {
            get { return this.canvas_board.Height; }
        }

        private int n_index = 0;    // 滚动索引

        private Storyboard _storyboard_RightToLeft;
        /// <summary>
        /// 滚动动画板，从右到左
        /// </summary>
        public Storyboard storyboard_RightToLeft
        {
            get
            {
                if (_storyboard_RightToLeft == null)
                {
                    _storyboard_RightToLeft = new Storyboard();

                    _storyboard_RightToLeft.Children.Add(
                        this.getDAUKF(0.0, -this.width, this.image1, new PropertyPath("(Canvas.Left)"))
                        );

                    _storyboard_RightToLeft.Children.Add(
                        this.getDAUKF(this.width, 0.0, this.image2, new PropertyPath("(Canvas.Left)"))
                        );

                    _storyboard_RightToLeft.FillBehavior = FillBehavior.Stop;
                    _storyboard_RightToLeft.Completed += new EventHandler(storyboard_Completed);
                }
                return _storyboard_RightToLeft;
            }
        }

        private Storyboard _storyboard_LeftToRight;
        /// <summary>
        /// 滚动动画板，从左到右
        /// </summary>
        public Storyboard storyboard_LeftToRight
        {
            get
            {
                if (_storyboard_LeftToRight == null)
                {
                    _storyboard_LeftToRight = new Storyboard();

                    _storyboard_LeftToRight.Children.Add(
                        this.getDAUKF(0.0, this.width, this.image1, new PropertyPath("(Canvas.Left)"))
                        );

                    _storyboard_LeftToRight.Children.Add(
                        this.getDAUKF(-this.width, 0.0, this.image2, new PropertyPath("(Canvas.Left)"))
                        );

                    _storyboard_LeftToRight.FillBehavior = FillBehavior.Stop;
                    _storyboard_LeftToRight.Completed += new EventHandler(storyboard_Completed);
                }
                return _storyboard_LeftToRight;
            }
        }

        private Storyboard _storyboard_TopToBottom;
        /// <summary>
        /// 滚动动画板，从上到下
        /// </summary>
        public Storyboard storyboard_TopToBottom
        {
            get
            {
                if (_storyboard_TopToBottom == null)
                {
                    _storyboard_TopToBottom = new Storyboard();

                    _storyboard_TopToBottom.Children.Add(
                        this.getDAUKF(0.0, this.height, this.image1, new PropertyPath("(Canvas.Top)"))
                        );

                    _storyboard_TopToBottom.Children.Add(
                        this.getDAUKF(-this.height, 0.0, this.image2, new PropertyPath("(Canvas.Top)"))
                        );

                    _storyboard_TopToBottom.FillBehavior = FillBehavior.Stop;
                    _storyboard_TopToBottom.Completed += new EventHandler(storyboard_Completed);
                }
                return _storyboard_TopToBottom;
            }
        }

        private Storyboard _storyboard_BottomToTop;
        /// <summary>
        /// 滚动动画板，从下到上
        /// </summary>
        public Storyboard storyboard_BottomToTop
        {
            get
            {
                if (_storyboard_BottomToTop == null)
                {
                    _storyboard_BottomToTop = new Storyboard();

                    _storyboard_BottomToTop.Children.Add(
                        this.getDAUKF(0.0, -this.height, this.image1, new PropertyPath("(Canvas.Top)"))
                        );

                    _storyboard_BottomToTop.Children.Add(
                        this.getDAUKF(this.height, 0.0, this.image2, new PropertyPath("(Canvas.Top)"))
                        );

                    _storyboard_BottomToTop.FillBehavior = FillBehavior.Stop;
                    _storyboard_BottomToTop.Completed += new EventHandler(storyboard_Completed);
                }
                return _storyboard_BottomToTop;
            }
        }

        private Storyboard _storyboard_Opacity;
        /// <summary>
        /// 滚动动画板，渐现
        /// </summary>
        public Storyboard storyboard_Opacity
        {
            get
            {
                if (_storyboard_Opacity == null)
                {
                    _storyboard_Opacity = new Storyboard();

                    _storyboard_Opacity.Children.Add(
                        this.getDAUKF(0.0, 1.0, this.image2, new PropertyPath("(Image.Opacity)"))
                        );

                    _storyboard_Opacity.FillBehavior = FillBehavior.Stop;
                    _storyboard_Opacity.Completed += new EventHandler(storyboard_Completed);
                }
                return _storyboard_Opacity;
            }
        }

        /// <summary>
        /// 获取动画中Double类型属性的关键帧组
        /// </summary>
        /// <param name="from">初始值</param>
        /// <param name="to">目标值</param>
        /// <param name="obj">动画控件</param>
        /// <param name="path">动画属性</param>
        /// <returns>关键帧组</returns>
        DoubleAnimationUsingKeyFrames getDAUKF(double from, double to, DependencyObject obj, PropertyPath path)
        {
            DoubleAnimationUsingKeyFrames daukf = new DoubleAnimationUsingKeyFrames();
            LinearDoubleKeyFrame k1 = new LinearDoubleKeyFrame(from, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(this.TimeHold)));
            LinearDoubleKeyFrame k2 = new LinearDoubleKeyFrame(to, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(this.TimeHold + this.TimeChange)));
            daukf.KeyFrames.Add(k1);
            daukf.KeyFrames.Add(k2);
            Storyboard.SetTarget(daukf, obj);
            Storyboard.SetTargetProperty(daukf, path);
            return daukf;
        }

        // 动画结束时
        void storyboard_Completed(object sender, EventArgs e)
        {
            rollNum--;

            // 显示图片
            this.ResetStory();

            // 继续下轮动画
            this.BeginStoty();
        }

        /// <summary>
        /// 开始滚动动画
        /// </summary>
        public void Begin()
        {
            if (!isBegin)
            {
                isBegin = true;

                // 显示图片
                this.ResetStory();

                // 开始动画
                this.BeginStoty();
            }
        }

        /// <summary>
        /// 开始播放动画
        /// </summary>
        void BeginStoty()
        {
            switch (this.StoryType)
            {
                case StoryTypes.LeftToRight:
                    this.storyboard_LeftToRight.Begin();
                    break;
                case StoryTypes.TopToBottom:
                    this.storyboard_TopToBottom.Begin();
                    break;
                case StoryTypes.BottomToTop:
                    this.storyboard_BottomToTop.Begin();
                    break;
                case StoryTypes.Opacity:
                    this.storyboard_Opacity.Begin();
                    break;
                default:
                    this.storyboard_RightToLeft.Begin();
                    break;
            }
        }

        /// <summary>
        /// 初始化动画版，显示动画中的图片
        /// </summary>
        void ResetStory()
        {
            // 图片复位
            switch (this.StoryType)
            {
                case StoryTypes.LeftToRight:
                    this.image1.SetValue(Canvas.LeftProperty, 0.0);
                    this.image2.SetValue(Canvas.LeftProperty, -this.width);
                    break;
                case StoryTypes.TopToBottom:
                    this.image1.SetValue(Canvas.TopProperty, 0.0);
                    this.image2.SetValue(Canvas.TopProperty, -this.height);
                    break;
                case StoryTypes.BottomToTop:
                    this.image1.SetValue(Canvas.TopProperty, 0.0);
                    this.image2.SetValue(Canvas.TopProperty, this.height);
                    break;
                case StoryTypes.Opacity:
                    this.image2.SetValue(Image.OpacityProperty, 0.0);
                    break;
                default:
                    this.image1.SetValue(Canvas.LeftProperty, 0.0);
                    this.image2.SetValue(Canvas.LeftProperty, this.width);
                    break;
            }

            // 显示图片
            if (this.ls_images.Count > 0)
            {
                try
                {
                    this.image1.Source = this.ls_images[this.n_index++ % this.ls_images.Count];
                    this.image2.Source = this.ls_images[this.n_index % this.ls_images.Count];
                }
                catch (Exception ex)
                {
                    this.image1.Source = new BitmapImage();
                    this.image2.Source = new BitmapImage();
                }
            }
            else
            {
                this.image1.Source = new BitmapImage();
                this.image2.Source = new BitmapImage();
            }
        }
    }
}
