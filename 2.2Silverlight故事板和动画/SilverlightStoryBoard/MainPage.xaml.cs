using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using System.Windows.Threading;//for Timer
namespace SilverlightStoryBoard
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();			
		}
		//椭圆剪裁
        EllipseGeometry ellipse = new EllipseGeometry();//创建椭圆几何图形		
		DoubleAnimation da1,da2;//定义动画对象
		Storyboard sb1;//定义故事板对象
		private void button1_Click(object sender, System.Windows.RoutedEventArgs e)
		{			
			da1 = new DoubleAnimation();//创建
			da2 = new DoubleAnimation();
			sb1=new Storyboard();//创建故事板
			this.sb1.Completed+=new System.EventHandler(Storyboard_Completed);
			this.button1.IsEnabled=false;
			this.button2.IsEnabled=false;
			this.button3.IsEnabled=false;
			this.button4.IsEnabled=false;
			double cx =this.image.ActualWidth/2;//image的宽、高
            double cy =this.image.ActualHeight/2;
			ellipse.Center = new Point(cx, cy);//椭圆中心点
			this.image.Clip = ellipse; //用ellipse对图片剪裁           
            da1.From = 0;//动画起点,椭圆中心
			da2.From=0;
			da1.To=cx;//动画终点
			da2.To=cy;
			//动画持续时间3000毫秒
            da1.Duration = new Duration(TimeSpan.FromMilliseconds(3000));
			da2.Duration=da1.Duration;
			sb1.Duration=da1.Duration;//故事板时间间隔，可省
			sb1.SpeedRatio=0.5;//控制故事板的速率,默认1
			sb1.Children.Add(da1);//故事板添加动画对象
			sb1.Children.Add(da2);
			//故事板引起动画对象的属性改变
			Storyboard.SetTargetProperty(da1, new PropertyPath("RadiusX"));
			Storyboard.SetTargetProperty(da2, new PropertyPath("RadiusY"));
			//设置故事板启动的动画及目标对象
			Storyboard.SetTarget(da1,ellipse);
			Storyboard.SetTarget(da2,ellipse);			
			sb1.Begin();//故事板启动
		}
		private void Storyboard_Completed(object sender, System.EventArgs e)
		{
            this.button1.IsEnabled=true;
			this.button2.IsEnabled=true;
			this.button3.IsEnabled=true;
			this.button4.IsEnabled=true;
		}
        //颜色变化
		ColorAnimation ca;
		Ellipse ep;
		Storyboard sb2;
		byte a,r,g,b;
		private void button2_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            ca=new ColorAnimation();
			ep=new Ellipse();
			double cx =this.image.ActualWidth;//image的宽、高
            double cy =this.image.ActualHeight;
			ep.Width=cx;
			ep.Height=cy;
			a=255;r=0;g=0;b=255;//不透明度和色彩初始值设置
			SolidColorBrush scb=new SolidColorBrush();//创建纯色画刷
			scb.Color=Color.FromArgb(a,r,g,b);//色彩设置		
			ep.Fill=scb;//椭圆填充色
			this.canvas1.Children.Add(ep);//椭圆作为canvas1的子对象
			sb2=new Storyboard();
			this.sb2.Completed+=new System.EventHandler(Storyboard2_Completed);
			this.button1.IsEnabled=false;
			this.button2.IsEnabled=false;
			this.button3.IsEnabled=false;
			this.button4.IsEnabled=false;
			ca.From = Color.FromArgb(255,r,g,b);//动画不透明度和起点颜色
			ca.To=Color.FromArgb(0,255,0,0);//动画终点不透明度和颜色
			//动画持续时间5000毫秒
            ca.Duration = new Duration(TimeSpan.FromMilliseconds(5000));
			sb2.Duration=ca.Duration;//故事板时间间隔
			sb2.AutoReverse=true;//故事板翻转
			sb2.RepeatBehavior=new RepeatBehavior(1);//故事板运行1次
			sb2.Children.Add(ca);//故事板添加对象
			sb2.BeginTime=TimeSpan.FromSeconds(0.5);//故事板延时0.5秒开始
			//故事板引起动画对象ca1的Color属性改变
			Storyboard.SetTargetProperty(ca, new PropertyPath("Color"));
			//设置故事板启动的动画及目标对象
			Storyboard.SetTarget(ca,scb);
			sb2.Begin();//故事板启动			
		}
		private void Storyboard2_Completed(object sender, System.EventArgs e)
		{
            this.button1.IsEnabled=true;
			this.button2.IsEnabled=true;
			this.button3.IsEnabled=true;
			this.button4.IsEnabled=true;
			//自定义的椭圆对象不显示，且不在内存中保留
			//ep.Visibility=Visibility.Collapsed;
			this.canvas1.Children.Remove(ep);
		}
        //随机运动
		PointAnimation pa;
		Storyboard sb3;
		Point p1,p2;
		Path path;
		DispatcherTimer timer = new DispatcherTimer();//定时器
		Random rdx=new Random();//产生随机数	
		int w,h;
		private void button3_Click(object sender, System.Windows.RoutedEventArgs e)
		{								
			w =(int)this.image.ActualWidth;//image的宽、高
            h =(int)this.image.ActualHeight;
			EllipseGeometry eg = new EllipseGeometry();
			p1=new Point(0,0);
			eg.Center=p1;
			eg.RadiusX=12;
			eg.RadiusY=12;
			path = new Path();
			path.Fill=new SolidColorBrush(Color.FromArgb(255,0,255,0));//绿色
			path.StrokeThickness=0;
			path.Data=eg;
			//层次设置，SetZIndex设置Z轴（正方向面对读者）顺序，数值越大离桌面越远
			//默认值0，附于桌面
			Canvas.SetZIndex(path,3);//置于现有对象顶层
			this.canvas1.Children.Add(path);//椭圆作为canvas1的子对象
			timer.Interval = TimeSpan.FromMilliseconds(2000);//定时
            timer.Tick += new EventHandler(timerTick);
			pa=new PointAnimation();
			pa.Duration = new Duration(TimeSpan.FromMilliseconds(1000));
			sb3=new Storyboard();
			this.sb3.Completed+=new System.EventHandler(Storyboard3_Completed);
			this.button1.IsEnabled=false;
			this.button2.IsEnabled=false;
			this.button3.IsEnabled=false;
			this.button4.IsEnabled=false;
			sb3.RepeatBehavior=new RepeatBehavior(5);//故事板运行5次
			sb3.Duration=pa.Duration;//故事板时间间隔
			sb3.Children.Add(pa);//故事板添加动画对象
			Storyboard.SetTargetProperty(pa, new PropertyPath("Center"));
			//设置故事板启动的动画及目标对象
			Storyboard.SetTarget(pa,eg);
			sb3.BeginTime=TimeSpan.FromSeconds(2);
			timer.Start();
			sb3.Begin();
		}
		//定时访问程序,产生点和点之间的随机运动
		void timerTick(object sender, EventArgs e){
		    int x=rdx.Next(0,w);//取0和W间的随机数
			int y=rdx.Next(0,h);
			p1=p2;
			p2=new Point(x,y);
			pa.From=p1;
			pa.To=p2;
		}
		private void Storyboard3_Completed(object sender, System.EventArgs e)
		{
            this.button1.IsEnabled=true;
			this.button2.IsEnabled=true;
			this.button3.IsEnabled=true;
			this.button4.IsEnabled=true;
			this.canvas1.Children.Remove(path);
		}
        //图片旋转
		Storyboard sb4;
		private void button4_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			//定义多关键帧动画
			DoubleAnimationUsingKeyFrames daukf = new DoubleAnimationUsingKeyFrames();
			sb4=new Storyboard();
			this.sb4.Completed+=new System.EventHandler(Storyboard_Completed);
			this.button1.IsEnabled=false;
			this.button2.IsEnabled=false;
			this.button3.IsEnabled=false;
			this.button4.IsEnabled=false;
            sb4.Children.Add(daukf);
			//动画影响属性:元素的三维旋转(以Y轴为旋转轴)
            Storyboard.SetTargetProperty(daukf, new PropertyPath("(UIElement.Projection).(PlaneProjection.RotationY)"));
			Storyboard.SetTarget(daukf, image);
			//定义第1个可插入的关键帧(可以和Silverlight目前提供的11种缓动函数关联）
			//（缓动函数设置了关键帧之间的动画过渡状态)
            EasingDoubleKeyFrame kf1 = new EasingDoubleKeyFrame();
            kf1.Value = 80;//设置此关键帧的目标值(旋转度数)
            kf1.KeyTime = new TimeSpan(0, 0, 0, 3);//设置达到目标值的时间(日，小时，分，秒)
			BackEase kf1be=new BackEase();//定义具有收回效果的缓动函数对象
			//EasingMode有3种选择:EaseIn、EaseOut和EaseInOut
			kf1be.EasingMode=EasingMode.EaseOut;//EaseOut方式
			kf1.EasingFunction=kf1be;//第1帧使用缓动函数BackEase的EaseOut方式过渡
			//定义第2个可插入的关键帧
            EasingDoubleKeyFrame kf2 = new EasingDoubleKeyFrame();
            kf2.Value = 0;
            kf2.KeyTime = new TimeSpan(0, 0, 0, 6);//第2帧达到第6秒
			BounceEase  kf2be=new BounceEase();//定义具有反弹效果的缓动函数对象
			kf2be.EasingMode=EasingMode.EaseIn;//EaseIn方式
			kf2be.Bounces=3;//反弹3次
			kf2.EasingFunction=kf2be;//第2帧使用缓动函数BounceEase的EaseIn方式过渡
			//定义第3个可插入的关键帧
            EasingDoubleKeyFrame kf3 = new EasingDoubleKeyFrame();
            kf3.Value = -80;
            kf3.KeyTime = new TimeSpan(0, 0, 0, 9);//第3帧达到第9秒
			ElasticEase kf3ee=new ElasticEase();//定义具有衰减振荡效果的缓动函数对象
			kf3ee.EasingMode=EasingMode.EaseInOut;//EaseInOut方式
			kf3ee.Oscillations=4;//振荡次数
			kf3ee.Ease(4);//衰减振荡持续时间
			kf3.EasingFunction=kf3ee;//第3帧使用缓动函数ElasticEase的EaseInOut方式过渡
			//定义第4个可插入的关键帧
			EasingDoubleKeyFrame kf4 = new EasingDoubleKeyFrame();
            kf4.Value = 0;
            kf4.KeyTime = new TimeSpan(0, 0, 0, 12);//第4帧达到第12秒
            daukf.KeyFrames.Add(kf1);//添加关键帧
            daukf.KeyFrames.Add(kf2);
            daukf.KeyFrames.Add(kf3);
			daukf.KeyFrames.Add(kf4);
			daukf.AutoReverse=true;//允许翻转
			daukf.RepeatBehavior=new RepeatBehavior(1);//反复1次			
            sb4.Begin();
		}
		
	}
}