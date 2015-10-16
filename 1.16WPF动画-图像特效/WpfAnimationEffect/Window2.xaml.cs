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

using System.Windows.Threading;//for TimeSpan
using System.Windows.Media.Animation;
namespace WpfAnimationEffect
{
	/// <summary>
	/// Window2.xaml 的交互逻辑
	/// </summary>
	public partial class Window2 : Window
	{
		public Window2()
		{
			this.InitializeComponent();
		}
		//画卷扇形展开
		private PathGeometry pg=new PathGeometry(); //可组合由弧、曲线、椭圆、直线和矩形合成的复杂形状。 
	    private PathFigure Pathfigure=null;
	    private LineSegment Linesegment1 = null; 
	    private LineSegment Linesegment2 = null;
	    private LineSegment Linesegment3 = null;
	    private LineSegment Linesegment0 = null;
	    private double roll=10;
	    private DispatcherTimer timer3 = new DispatcherTimer();//定时器
		private void button1_Click(object sender, System.Windows.RoutedEventArgs e)
		{
		    pg.Clear();
		    Pathfigure=new PathFigure();//定义二维几何线段图形组合体;
      	    Pathfigure.StartPoint = new Point(this.image.Width/2,0);//直线开始点
            Linesegment1 = new LineSegment();//定义1条直线，起点是Pathfigure.StartPoint
            Linesegment1.Point = new Point(this.image.Width,0);//下面每条线的终点坐标、起点同Linesegment1
            Pathfigure.Segments.Add(Linesegment1);		
		    Linesegment2 = new LineSegment();
            Linesegment2.Point =new Point(Linesegment1.Point.X,Linesegment1.Point.Y);
            Pathfigure.Segments.Add(Linesegment2);		
		    Linesegment3 = new LineSegment();
            Linesegment3.Point =new Point(Linesegment1.Point.X,Linesegment1.Point.Y);
            Pathfigure.Segments.Add(Linesegment3);
		    Linesegment0 = new LineSegment();
            Linesegment0.Point =new Point(Linesegment1.Point.X,Linesegment1.Point.Y);
            Pathfigure.Segments.Add(Linesegment0);
            pg.Figures.Add(Pathfigure);
		    this.image.Clip = pg;
		    timer3.Interval = TimeSpan.FromMilliseconds(100);
            timer3.Tick += new EventHandler(timer3Tick);
            timer3.IsEnabled = true;
		    timer3.Start();
		}
		void timer3Tick(object sender, EventArgs e)
       {
		    if (Linesegment0.Point.X==this.image.Width && Linesegment0.Point.Y<this.image.Height){
		      Linesegment0.Point = new Point(Linesegment0.Point.X,Linesegment0.Point.Y+roll);
		      Linesegment2.Point = new Point(Linesegment0.Point.X,Linesegment0.Point.Y); 
			  Linesegment3.Point = new Point(Linesegment0.Point.X,Linesegment0.Point.Y);
			  return;
			}
			if (Linesegment0.Point.X>=0 && Linesegment0.Point.Y>=this.image.Height){
		      Linesegment0.Point = new Point(Linesegment0.Point.X-roll,Linesegment0.Point.Y);
		      Linesegment3.Point = new Point(Linesegment0.Point.X,Linesegment0.Point.Y);
			return;
			}
			if (Linesegment0.Point.Y>=0){
		      Linesegment0.Point = new Point(Linesegment0.Point.X,Linesegment0.Point.Y-roll);
				if (Linesegment0.Point.Y<=0){
				   Linesegment0.Point = new Point(0,0);
				   timer3.Stop();
				}
			}		    
	    }

		private void exit_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}
        //画卷滚动展开*****************************
		private double rw,rh,hr,hhr;
	    private int w;
        private DispatcherTimer timer1 = new DispatcherTimer();//定时器
		private RectangleGeometry rg1=new RectangleGeometry();//矩形图形
	    private RectangleGeometry rg2=new RectangleGeometry();
	    private TranslateTransform tt=new TranslateTransform();//定义位移变换
	    private VisualBrush vb=new VisualBrush();//定义图形画刷
		private void button2_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			rw=this.image.Width;
			rh=0;//画卷展放量进度和
			rg1.Rect=new Rect(0,0,0,0);
			this.image.Clip=rg1;
			w=2;//画卷定时展放量
			hr=this.rectangle1.Height;//记忆
			hhr=this.rectangle1.Height*w/this.image.Height;//画卷收缩量
			this.canvas1.RenderTransform=tt;//变换对象canvas1
			tt.Y=0;
			this.canvas1.Visibility=Visibility.Visible;
			this.image_Copy.Visibility=Visibility.Visible;
			timer1.Interval = TimeSpan.FromMilliseconds(100);
            timer1.Tick += new EventHandler(timer1Tick);
		    timer1.Start();
		}
		void timer1Tick(object sender, EventArgs e)
       {
		   	if (rh<this.image.Height){
			rg1.Rect=new Rect(0,0,rw,rh);
			this.image.Clip=rg1;
			rg2.Rect=new Rect(0,rh,rw,this.rectangle1.Height);
			this.image_Copy.Clip=rg2;
			vb.Visual=this.image_Copy;//图形刷源
			this.rectangle1.Fill=vb;
			tt.X=0;
			tt.Y=tt.Y+w;//canvas1位移
			rh=rh+w;
				if (rh>=this.image.Height){
					this.canvas1.Visibility=Visibility.Hidden;//恢复初始值
			        this.rectangle1.Height=hr;
					this.rect1.Height=hr;					
                    timer1.Stop();
				   return;
				}
			    if (rh>this.image.Height/3 && this.rectangle1.Height>hhr){
				   this.rectangle1.Height=this.rectangle1.Height-hhr;//画卷收缩
				   this.rect1.Height=this.rect1.Height-hhr;
			    }
		}
			
	    }
	}
}