using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Windows.Media.Animation;//for DoubleAnimation 
using System.Windows.Threading;//for timer
namespace WPFEasing
{
	public partial class MainWindow : Window
	{
		//画显示区坐标线，线间隔deg
 		double deg=30;
		//动画重复属性
		double repeatbehavior=1;
		//小球初始位置
		double topStart;
		//动画时间计数
		int i=1;
		//复位标志
		bool rs=false;
		int sel=0;//曲线选择
		int comboboxSelected=0;
		BackEase easeBack=new BackEase();
		BounceEase easeBounce=new BounceEase();
		CircleEase easeCircle=new CircleEase();
		CubicEase easeCubic=new CubicEase();
		ElasticEase easeElastic=new ElasticEase();
		ExponentialEase easeExponential=new ExponentialEase();
		PowerEase easePower=new PowerEase();
		QuadraticEase easeQuadratic=new QuadraticEase();
		QuarticEase easeQuartic=new QuarticEase();
		QuinticEase easeQuintic=new QuinticEase();
		SineEase easeSine=new SineEase();
		//小球运动动画
		DoubleAnimation da=new DoubleAnimation();
		//复位动画
		DoubleAnimation rt=new DoubleAnimation();
		//定时器
		DispatcherTimer timer=new DispatcherTimer();
		//运动轨迹线段绘制
		PathFigure pf = new PathFigure();
		//用于绘制的线段组合
		PathGeometry pg = new PathGeometry();
		//绘制轨迹曲线的容器，用于显示
		Path pa = new Path();
		public MainWindow()
		{
			this.InitializeComponent();
			topStart=Canvas.GetTop(this.ellipse);
			this.tb.Text=topStart.ToString();
			this.combobox1.SelectedIndex=0;
			//画显示区坐标线
			drawinggrid();
			//确定绘制的开始点
			startpoint();
			timer.Interval=TimeSpan.FromMilliseconds(100);
			timer.Tick+=new System.EventHandler(timer_Tick); 
			da.Completed+=new System.EventHandler(da_Completed);
			rt.Completed+=new System.EventHandler(rt_Completed);
		}

		private void da_Completed(object sender, System.EventArgs e)
		{
			timer.Stop();		
		}
		private void combobox1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (comboboxSelected==0){
			   comboboxSelected=1;
			   return;
			}
			repeatbehavior=this.combobox1.SelectedIndex+1;
			rs=true;
			reset();		
		}
		private void checkbox_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			rs=true;
			reset();
		}      
		
		private void rect1a_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=11;
		}

		private void rect1b_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=12;
		}
		private void rect1c_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=13;
		}
		private void rect2a_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=21;
		}
		private void rect2b_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=22;
		}	
		private void rect2c_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=23;
		}
		private void rect3a_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=31;
		}
		private void rect3b_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=32;
		}
		private void rect3c_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=33;
		}
		private void rect4a_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=41;
		}
		private void rect4b_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=42;
		}
		private void rect4c_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=43;
		}
		
		private void rect5a_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=51;
		}
		private void rect5b_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=52;
		}		
		private void rect5c_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=53;
		}
		
		private void rect6a_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=61;
		}
		private void rect6b_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=62;
		}
		private void rect6c_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=63;
		}
		private void rect7a_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=71;
		}
		private void rect7b_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=72;
		}
		private void rect7c_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=73;
		}
		
		private void rect8a_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=81;
		}
		private void rect8b_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=82;
		}
		private void rect8c_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=83;
		}
		private void rect9a_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=91;
		}
		private void rect9b_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=92;
		}
		private void rect9c_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=93;
		}
		private void rect10a_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=101;
		}		
        private void rect10b_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=102;
		}
		private void rect10c_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=103;
		}
		private void rect11a_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=111;
		}
		private void rect11b_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=112;
		}
		private void rect11c_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			reset();
			sel=113;
		}
		//清除显示区的曲线，小球返回起点
		private void reset(){
			rt.Duration=TimeSpan.FromSeconds(0);
			rt.From=Canvas.GetTop(this.ellipse);
			rt.To=topStart;
			this.ellipse.RenderTransform=new TranslateTransform();
			ellipse.BeginAnimation(Canvas.TopProperty,rt);
			pf.Segments.Clear();
			i=1;
			timer.Stop();
			this.tb1.Text="";
			this.tb.Text="";
		}
		//
		private void rt_Completed(object sender, System.EventArgs e)
		{
			if (rs){
			   rs=false;
			   return;
			}
			switch (sel){
				case 11:
					easeBack.EasingMode=EasingMode.EaseIn;
					da.EasingFunction=easeBack;
					easeBack.Amplitude=Convert.ToDouble(this.text1.Text);
					this.tb1.Text="倒退曲线:EaseIn";
					break;
				case 12:
					easeBack.EasingMode=EasingMode.EaseOut;
					da.EasingFunction=easeBack;
					easeBack.Amplitude=Convert.ToDouble(this.text1.Text);
					this.tb1.Text="倒退曲线:EaseOut";
					break;
				case 13:
					easeBack.EasingMode=EasingMode.EaseInOut;
					da.EasingFunction=easeBack;
					easeBack.Amplitude=Convert.ToDouble(this.text1.Text);
					this.tb1.Text="倒退曲线:EaseInOut";
					break;
				case 21:
				    easeBounce.EasingMode=EasingMode.EaseIn;
					da.EasingFunction=easeBounce;
					easeBounce.Bounces=Convert.ToInt32(this.text2.Text);
					easeBounce.Bounciness=Convert.ToDouble(this.text3.Text);
					this.tb1.Text="弹跳曲线:EaseIn";
					break;
				case 22:
				    easeBounce.EasingMode=EasingMode.EaseOut;
					da.EasingFunction=easeBounce;
					easeBounce.Bounces=Convert.ToInt32(this.text2.Text);
					easeBounce.Bounciness=Convert.ToDouble(this.text3.Text);
					this.tb1.Text="弹跳曲线:EaseOut";
					break;
				case 23:
				    easeBounce.EasingMode=EasingMode.EaseInOut;
					da.EasingFunction=easeBounce;
					easeBounce.Bounces=Convert.ToInt32(this.text2.Text);
					easeBounce.Bounciness=Convert.ToDouble(this.text3.Text);
					this.tb1.Text="弹跳曲线:EaseInOut";
					break;
				case 31:
				    easeCircle.EasingMode=EasingMode.EaseIn;
					da.EasingFunction=easeCircle;
					this.tb1.Text="园曲线:EaseIn";
					break;
				case 32:
				    easeCircle.EasingMode=EasingMode.EaseOut;
					da.EasingFunction=easeCircle;
					this.tb1.Text="园曲线:EaseOut";
					break;
				case 33:
				    easeCircle.EasingMode=EasingMode.EaseInOut;
					da.EasingFunction=easeCircle;
					this.tb1.Text="园曲线:EaseInOut";
					break;
				case 41:
				    easeCubic.EasingMode=EasingMode.EaseIn;
					da.EasingFunction=easeCubic;
					this.tb1.Text="立方体曲线:EaseIn";
					break;
				case 42:
				    easeCubic.EasingMode=EasingMode.EaseOut;
					da.EasingFunction=easeCubic;
					this.tb1.Text="立方体曲线:EaseOut";
					break;
				case 43:
				    easeCubic.EasingMode=EasingMode.EaseInOut;
					da.EasingFunction=easeCubic;
					this.tb1.Text="立方体曲线:EaseInOut";
					break;
				case 51:
				    easeElastic.EasingMode=EasingMode.EaseIn;
					da.EasingFunction=easeElastic;
					easeElastic.Oscillations=Convert.ToInt32(this.text4.Text);
					easeElastic.Springiness=Convert.ToDouble(this.text5.Text);
					this.tb1.Text="振荡曲线:EaseIn";
					break;
				case 52:
				    easeElastic.EasingMode=EasingMode.EaseOut;
					da.EasingFunction=easeElastic;
					easeElastic.Oscillations=Convert.ToInt32(this.text4.Text);
					easeElastic.Springiness=Convert.ToDouble(this.text5.Text);
					this.tb1.Text="振荡曲线:EaseOut";
					break;
				case 53:
				    easeElastic.EasingMode=EasingMode.EaseInOut;
					da.EasingFunction=easeElastic;
					easeElastic.Oscillations=Convert.ToInt32(this.text4.Text);
					easeElastic.Springiness=Convert.ToDouble(this.text5.Text);
					this.tb1.Text="振荡曲线:EaseInOut";
					break;
				case 61:
					easeExponential.EasingMode=EasingMode.EaseIn;
					da.EasingFunction=easeExponential;
					easeExponential.Exponent=Convert.ToDouble(this.text6.Text);
					this.tb1.Text="指数曲线:EaseIn";
					break;
				case 62:
					easeExponential.EasingMode=EasingMode.EaseOut;
					da.EasingFunction=easeExponential;
					easeExponential.Exponent=Convert.ToDouble(this.text6.Text);
					this.tb1.Text="指数曲线:EaseOut";
					break;
				case 63:
					easeExponential.EasingMode=EasingMode.EaseInOut;
					da.EasingFunction=easeExponential;
					easeExponential.Exponent=Convert.ToDouble(this.text6.Text);
					this.tb1.Text="指数曲线:EaseInOut";
					break;
				case 71:
					easePower.EasingMode=EasingMode.EaseIn;
					da.EasingFunction=easePower;
					easePower.Power=Convert.ToDouble(this.text7.Text);
					this.tb1.Text="乘方曲线:EaseIn";
					break;
				case 72:
					easePower.EasingMode=EasingMode.EaseOut;
					da.EasingFunction=easePower;
					easePower.Power=Convert.ToDouble(this.text7.Text);
					this.tb1.Text="乘方曲线:EaseOut";
					break;
				case 73:
					easePower.EasingMode=EasingMode.EaseInOut;
					da.EasingFunction=easePower;
					easePower.Power=Convert.ToDouble(this.text7.Text);
					this.tb1.Text="乘方曲线:EaseInOut";
					break;
				case 81:
				    easeQuadratic.EasingMode=EasingMode.EaseIn;
					da.EasingFunction=easeQuadratic;
					this.tb1.Text="平方曲线:EaseIn";
					break;
				case 82:
				    easeQuadratic.EasingMode=EasingMode.EaseOut;
					da.EasingFunction=easeQuadratic;
					this.tb1.Text="平方曲线:EaseOut";
					break;
				case 83:
				    easeQuadratic.EasingMode=EasingMode.EaseInOut;
					da.EasingFunction=easeQuadratic;
					this.tb1.Text="平方曲线:EaseInOut";
					break;
				case 91:
				    easeQuartic.EasingMode=EasingMode.EaseIn;
					da.EasingFunction=easeQuartic;
					this.tb1.Text="四次方曲线:EaseIn";
					break;
				case 92:
				    easeQuartic.EasingMode=EasingMode.EaseOut;
					da.EasingFunction=easeQuartic;
					this.tb1.Text="四次方曲线:EaseOut";
					break;
				case 93:
				    easeQuartic.EasingMode=EasingMode.EaseInOut;
					da.EasingFunction=easeQuartic;
					this.tb1.Text="四次方曲线:EaseInOut";
					break;
				case 101:
				    easeQuintic.EasingMode=EasingMode.EaseIn;
					da.EasingFunction=easeQuintic;
					this.tb1.Text="五次方曲线:EaseIn";
					break;
				case 102:
				    easeQuintic.EasingMode=EasingMode.EaseOut;
					da.EasingFunction=easeQuintic;
					this.tb1.Text="五次方曲线:EaseOut";
					break;
				case 103:
				    easeQuintic.EasingMode=EasingMode.EaseInOut;
					da.EasingFunction=easeQuintic;
					this.tb1.Text="五次方曲线:EaseInOut";
					break;
				case 111:
				    easeSine.EasingMode=EasingMode.EaseIn;
					da.EasingFunction=easeSine;
					this.tb1.Text="正弦曲线:EaseIn";
					break;
				case 112:
				    easeSine.EasingMode=EasingMode.EaseOut;
					da.EasingFunction=easeSine;
					this.tb1.Text="正弦曲线:EaseOut";
					break;
				case 113:
				    easeSine.EasingMode=EasingMode.EaseInOut;
					da.EasingFunction=easeSine;
					this.tb1.Text="正弦曲线:EaseInOut";
					break;
			}
			animation();
		}
		//
		private void animation(){
			da.BeginTime=TimeSpan.FromSeconds(0);
		    da.Duration=TimeSpan.FromSeconds(5);
			da.From=Canvas.GetTop(this.ellipse);
			da.To=300;
			if ((bool)this.checkbox.IsChecked){
			   da.AutoReverse=true;
		    }else{
			   da.AutoReverse=false;
			}
			if (repeatbehavior==7){
			   da.RepeatBehavior=RepeatBehavior.Forever;
			}else
			{
			   da.RepeatBehavior=new RepeatBehavior(repeatbehavior);				
			}	
			this.ellipse.RenderTransform=new TranslateTransform();
			ellipse.BeginAnimation(Canvas.TopProperty,da);
			timer.Start();
		}
		
		private void timer_Tick(object sender, System.EventArgs e)
		{
			if (repeatbehavior==1){
			   double x=Canvas.GetLeft(this.ellipse);
			   double y=Canvas.GetTop(this.ellipse);
               pf.Segments.Add(new LineSegment(new Point(3*i,(topStart-y)),true/* IsStroked */ ));
			   i++;
			   this.tb.Text=Canvas.GetTop(this.ellipse).ToString();
			}
		}
		//画显示区坐标线
		private void drawinggrid(){
			//X轴
			Line lnx=new Line();
			lnx.X1=0;
			lnx.Y1=this.canvas2.Height*2/3;
			lnx.X2=this.canvas2.Width;
			lnx.Y2=lnx.Y1;
			lnx.Stroke=Brushes.LightGreen;
			lnx.StrokeThickness=2;
			this.canvas2.Children.Add(lnx);
			//Y轴
			Line lny=new Line();
			lny.X1=0;
			lny.Y1=0;			
			lny.X2=0;
			lny.Y2=this.canvas2.Height;
			lny.Stroke=Brushes.LightGreen;
			lny.StrokeThickness=2;
			this.canvas2.Children.Add(lny);
			//垂直格线
			Line lines1y=new Line();
			Line lines2y=new Line();
			Line lines3y=new Line();
			Line lines4y=new Line();
			Line lines5y=new Line();
			Line lines6y=new Line();
			Line lines7y=new Line();
			Line lines8y=new Line();
			Line lines9y=new Line();
			Line lines10y=new Line();
			Line[] linesy={lines1y,lines2y,lines3y,lines4y,lines5y,lines6y,lines7y,lines8y,lines9y,lines10y};
			for (int k=0;k<10;k++){
  		        linesy[k].X1=deg*(k+1);
				linesy[k].Y1=0;
				linesy[k].X2=deg*(k+1);
				linesy[k].Y2=this.canvas2.Height;
				linesy[k].Stroke=Brushes.Green;
				linesy[k].StrokeThickness=1;
				this.canvas2.Children.Add(linesy[k]);
			}		
           	//水平格线			
			//X轴上方
			Line lines1a=new Line();
			Line lines2a=new Line();
			Line lines3a=new Line();
			Line lines4a=new Line();
			Line lines5a=new Line();
			Line lines6a=new Line();
			Line lines7a=new Line();
			Line lines8a=new Line();
			Line lines9a=new Line();
			Line[] linesa={lines1a,lines2a,lines3a,lines4a,lines5a,lines6a,lines7a,lines8a,lines9a};
			for (int k=0;k<9;k++){
  		        linesa[k].X1=0;
				linesa[k].Y1=this.canvas2.Height*2/3-deg*(k+1);
				linesa[k].X2=this.canvas2.Width;
				linesa[k].Y2=this.canvas2.Height*2/3-deg*(k+1);
				linesa[k].Stroke=Brushes.Green;
				linesa[k].StrokeThickness=1;
				this.canvas2.Children.Add(linesa[k]);
			}
			//X轴下方
			Line lines1b=new Line();
			Line lines2b=new Line();
			Line lines3b=new Line();
			Line lines4b=new Line();
			Line[] linesb={lines1b,lines2b,lines3b,lines4b};
			for (int k=0;k<4;k++){
  		        linesb[k].X1=0;
				linesb[k].Y1=this.canvas2.Height*2/3+deg*(k+1);
				linesb[k].X2=this.canvas2.Width;
				linesb[k].Y2=this.canvas2.Height*2/3+deg*(k+1);
				linesb[k].Stroke=Brushes.Green;
				linesb[k].StrokeThickness=1;
				this.canvas2.Children.Add(linesb[k]);
			}
		}
		
		private void startpoint(){
			pa.Stroke=Brushes.LightGreen;
			pa.StrokeThickness=2;
			pf.StartPoint = new Point(0,0);
            pf.Segments.Add(new LineSegment(new Point(0,0),true/* IsStroked */ ));
            pg.Figures.Add(pf);
			pa.Data=pg;
			this.canvas2.Children.Add(pa);
			Canvas.SetLeft(pa,0);
			Canvas.SetTop(pa,this.canvas2.Height*2/3);
		}

	}
}