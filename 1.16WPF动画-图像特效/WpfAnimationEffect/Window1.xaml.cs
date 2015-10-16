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

using System.Windows.Threading;//for Timer
using System.Windows.Media.Animation;//for DoubleAnimation
using System.Collections;//for HashTable
namespace WpfAnimationEffect
{
	public partial class Window1 : Window
	{
		public Window1()
		{
			this.InitializeComponent();			
		}
        //圆形展开******************************
		private void button1_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			EllipseGeometry ellipse = new EllipseGeometry();//创建椭圆图形，但不显示
			double cx =this.image.Width/2;//image的宽、高不要设为自动
            double cy =this.image.Height/2;
			ellipse.Center = new Point(cx, cy);//创建中心点,RadiusX默认为0
			this.image.Clip = ellipse; //用ellipse对图片剪裁,开始的剪裁区域是0
			DoubleAnimation da = new DoubleAnimation();
            da.From = 0;//从中心点
			da.To = Math.Max(cx*2,cy*2); 
            da.Duration = new Duration(TimeSpan.Parse("0:0:6"));//动画时间
            ellipse.BeginAnimation(EllipseGeometry.RadiusXProperty, da);
            ellipse.BeginAnimation(EllipseGeometry.RadiusYProperty, da);
		}

		private void exit_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}
        // 随机方块****************************
		private int rs;//正方形边长
		private int x;//图片x方向剪裁数
		private int y;//图片y方向剪裁数
        private int count;//计数变量
		private int total;//剪裁图片的正方形总数
		private DispatcherTimer timer = new DispatcherTimer();//定时器
		private PathGeometry pg=new PathGeometry(); //可组合由弧、曲线、椭圆、直线和矩形合成的复杂形状。 
		private RectangleGeometry rg=new RectangleGeometry();//正方形图形
		private int [,] xy;//二维数组存正方形的x、y坐标
		private Random rdx=new Random();//产生随机数
		private Hashtable rd= new Hashtable();//哈希表存放对应xy数组的行数，随机数，对应剪裁正方形的（x、y坐标）
		private void button2_Click(object sender, System.Windows.RoutedEventArgs e)
		{
		   rs=30;//初始化变量值
		   count=0;//坐标系：image的左上角为坐标原点，image上边为X轴，正方向向右。image左边为Y轴，正方向向下
		   rd.Clear();
		   pg.Clear();
		   x=(int)Math.Ceiling(this.image.Width/rs);//取x方向剪裁的整数大者。image的宽、高不要设为自动
		   y=(int)Math.Ceiling(this.image.Height/rs);//取y方向剪裁的整数大者
		   total=x*y;//剪裁总数
		   xy=new int [total,2];		
		   getxy();
		   random();
		   timer.Interval = TimeSpan.FromMilliseconds(10);
           timer.Tick += new EventHandler(timerTick);
           timer.IsEnabled = true;
		   timer.Start();
		}
		void timerTick(object sender, EventArgs e)
       {
		   if (count==total){
			timer.Stop();
			//System.Windows.MessageBox.Show(rd.Count.ToString());
			return;
		   }
		   rg.Rect = new Rect(xy[(int)rd[count],0],xy[(int)rd[count],1],rs,rs);//定义剪裁正方形位置
		   pg=Geometry.Combine(pg,rg,GeometryCombineMode.Union, null);//相加组合几何图形，没有转换。 
		   this.image.Clip=pg;//用组合图形剪裁图片
		   count++;
	    }
        void getxy(){
		  int h=0;
		  for (int i=0;i<=y-1;i++){
			for (int j=0;j<=x-1;j++){
			    xy[h,0]=j*rs;//从左到右取剪裁正方形x、y坐标存数组
				xy[h,1]=i*rs;
				h++;
			}
		  }
		}
	    void random(){
		    int j=0;
			while (rd.Count<total)//产生0-total之间的随机数，对应剪裁正方形x、y坐标
            {
 				int rdv=rdx.Next(0,total);
                if (!rd.ContainsValue(rdv))
                {
                    rd.Add(j,rdv);
					j++;
                }
            }
		}
		//交错扫描******************************
		private DispatcherTimer timer1 = new DispatcherTimer();//定时器
		private int again;//用于横向扫描计数
		private void button3_Click(object sender, System.Windows.RoutedEventArgs e)
	    {
		   y=0;
		   rs=3;
		   again=-1;
		   count=0;
		   pg.Clear();
		   x=(int)this.image.Width;
		   total=(int)Math.Ceiling(this.image.Height/(rs+1));//扫描总数
		   timer1.Interval = TimeSpan.FromMilliseconds(100);
           timer1.Tick += new EventHandler(timer1Tick);
           //timer1.IsEnabled = true;
		   timer1.Start();
	    }
       void timer1Tick(object sender, EventArgs e)
       {
			if (total==0){
			    total=-1;
			    x=5;
				y=0;
			    rs=(int)this.image.Height;
			    again=(int)Math.Ceiling(this.image.Width/x);
			}
		    if (again==0){
			    timer1.Stop();
			    return;
			}
		   if (total>0){
		        rg.Rect = new Rect(0,y,x,rs);//定义剪裁矩形位置
			    y=y+rs+1;
			    total--;
		    }else{
			    rg.Rect = new Rect(y,0,x,rs);
				y=y+x;
				again--;
			}
			pg=Geometry.Combine(pg,rg,GeometryCombineMode.Union, null);//相加组合几何图形，没有转换。 
		    this.image.Clip=pg;//用组合图形剪裁图片
	    }
       //水帘******************************
	   private DispatcherTimer timer2 = new DispatcherTimer();//定时器
       private void button4_Click(object sender, System.Windows.RoutedEventArgs e)
       {
       	   y=(int)this.image.Height/3;
		   x=2;
		   pg.Clear();
		   count=0;
		   rs=(int)this.image.Width/2;
		   total=(int)this.image.Height;
		   again=-1;
		   timer2.Interval = TimeSpan.FromMilliseconds(100);
           timer2.Tick += new EventHandler(timer2Tick);
           timer2.IsEnabled = true;
		   timer2.Start();
       }
	   void timer2Tick(object sender, EventArgs e)
       {
			if (count>total){
			   count=total;
			   x=(int)this.image.Width;
			   again=0;
			   rs=10;
			}
			if (again>total){
			    timer2.Stop();
				//System.Windows.MessageBox.Show(again.ToString());
			    return;
			}
			if (count<total){
			   for (int i=0;i<=rs;i=i+4){
			     rg.Rect = new Rect(i,count,x,rdx.Next(0,y));//定义剪裁矩形位置
			     pg=Geometry.Combine(pg,rg,GeometryCombineMode.Union, null);//相加组合几何图形，没有转换。
			     rg.Rect = new Rect(rs*2-i,count,x,rdx.Next(0,y));
			     pg=Geometry.Combine(pg,rg,GeometryCombineMode.Union, null);			   
			   }
			   count=count+y/3;
			}else{
			   rg.Rect = new Rect(0,again,x,rs);//定义剪裁矩形位置
			   pg=Geometry.Combine(pg,rg,GeometryCombineMode.Union, null);//相加组合几何图形，没有转换。
			   again=again+rs;
			}
		    this.image.Clip=pg;//用组合图形剪裁图片
	    }
       //扇形******************************
	   private int y1;
	   private PathFigure Pathfigure=null;
	   private LineSegment Linesegment1 = null; 
	   private LineSegment Linesegment2 = null;
	   private LineSegment Linesegment3 = null;
	   private LineSegment Linesegment4 = null;
	   private LineSegment Linesegment0 = null;
	   private double roll=10;
	   private DispatcherTimer timer3 = new DispatcherTimer();//定时器
	   private void button5_Click(object sender, System.Windows.RoutedEventArgs e)
	   {
		    y1=0;
		    pg.Clear();
		    Pathfigure=new PathFigure();//定义二维几何线段图形组合体;
      	    Pathfigure.StartPoint = new Point(this.image.Width/2,this.image.Height/2);//直线开始点
            //本例image宽448，高310，开始点坐标：224,155
		    //System.Windows.MessageBox.Show(Pathfigure.StartPoint.X.ToString()+","+Pathfigure.StartPoint.Y.ToString());
            Linesegment1 = new LineSegment();//定义1条直线，起点是Pathfigure.StartPoint
            Linesegment1.Point = new Point(0,0);// Linesegment1的终点
            Pathfigure.Segments.Add(Linesegment1);		
		    Linesegment2 = new LineSegment();
            Linesegment2.Point =new Point(Linesegment1.Point.X,Linesegment1.Point.Y);
            Pathfigure.Segments.Add(Linesegment2);		
		    Linesegment3 = new LineSegment();
            Linesegment3.Point =new Point(Linesegment1.Point.X,Linesegment1.Point.Y);
            Pathfigure.Segments.Add(Linesegment3);
		    Linesegment4 = new LineSegment();
            Linesegment4.Point =new Point(Linesegment1.Point.X,Linesegment1.Point.Y);
            Pathfigure.Segments.Add(Linesegment4);
		    Linesegment0 = new LineSegment();
            Linesegment0.Point =new Point(Linesegment1.Point.X,Linesegment1.Point.Y);
		    Pathfigure.Segments.Add(Linesegment0);
		    pg.Figures.Add(Pathfigure);
		    //Path path=new Path();
		    //path.Fill=null;
		    //path.Stroke=Brushes.Red;
		    //path.StrokeThickness=2;
		    //path.Data=pg;
		    //this.canvas1.Children.Add(path);
		    //return;
            this.image.Clip = pg;		
		    timer3.Interval = TimeSpan.FromMilliseconds(10);
            timer3.Tick += new EventHandler(timer3Tick);
            timer3.IsEnabled = true;
		    timer3.Start();
	   }
	   void timer3Tick(object sender, EventArgs e)
       {
		    if (y1==1){
			    timer3.Stop();
			    return;
			}
			if (Linesegment0.Point.Y==0 && Linesegment0.Point.X<this.image.Width){
				Linesegment0.Point = new Point(Linesegment0.Point.X+roll,0);
				Linesegment2.Point = new Point(Linesegment0.Point.X,0); 
				Linesegment3.Point = new Point(Linesegment0.Point.X,0);
		        Linesegment4.Point = new Point(Linesegment0.Point.X,0);
				return;
			}
			
			if (Linesegment0.Point.X>=this.image.Width && Linesegment0.Point.Y<this.image.Height){
				Linesegment0.Point = new Point(Linesegment0.Point.X,Linesegment0.Point.Y+roll);
				Linesegment3.Point = new Point(Linesegment0.Point.X,Linesegment0.Point.Y);
				Linesegment4.Point = new Point(Linesegment0.Point.X,Linesegment0.Point.Y); 
				return;
			}
			//timer3.Stop();
			if (Linesegment0.Point.Y>=this.image.Height && Linesegment0.Point.X>=0){
				Linesegment0.Point = new Point(Linesegment0.Point.X-roll,Linesegment0.Point.Y);
				Linesegment4.Point = new Point(Linesegment0.Point.X,Linesegment0.Point.Y); 
				return;
			}
			
			if (Linesegment0.Point.X<=0 && Linesegment0.Point.Y>=0){
				Linesegment0.Point = new Point(Linesegment0.Point.X,Linesegment0.Point.Y-roll);
				if (Linesegment0.Point.Y<=0){
				  Linesegment0.Point = new Point(0,0);//回0
			      y1=1;//停止标志
				}
			}
			
	    }
	}
}