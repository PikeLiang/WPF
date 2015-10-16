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

using System.Windows.Threading;//for timer
namespace WpfWord
{
	public partial class Window1 : Window
	{
		public Window1()
		{
			this.InitializeComponent();		
		}
		
        DispatcherTimer timer1 = new DispatcherTimer();//定时器
		RectangleGeometry rg=new RectangleGeometry();//矩形图形
		VisualBrush vb=new VisualBrush();//定义图形画刷
		int again,rs,count,total;
		double x,y;
		private void button1_Click(object sender, System.Windows.RoutedEventArgs e)
		{
		   x=this.image.Width;
		   y=20;
		   rs=0;
		   count=0;
           again=3;		   
		   total=(int)Math.Ceiling(this.image.Height/y);//剪裁总数
		   timer1.Interval = TimeSpan.FromMilliseconds(200);
           timer1.Tick += new EventHandler(timer1Tick);
		   timer1.Start();
		}
		void timer1Tick(object sender, EventArgs e)
       {
		   if (count<total && again>0){
		     rg.Rect = new Rect(0,rs,x,y);//定义剪裁矩形
		     this.image.Clip=rg;//剪裁图片
			 vb.Visual=this.image;//图形刷源
			 this.textblock1.Foreground=vb;
			 rs=rs+(int)y;
		     count++;
		   }else{
		     count=0;
			 rs=0;
			 again--;
		  }
		  if(again==0){
			timer1.Stop();
			this.textblock1.Foreground=Brushes.Red;//恢复
			this.image.Clip=null;//取消剪裁
		  }
	    }
        //百叶窗*******************************************
	    DispatcherTimer timer2 = new DispatcherTimer();//定时器
	    PathGeometry pg1=new PathGeometry(); //可组合由弧、曲线、椭圆、直线和矩形合成的复杂形状。
	    RectangleGeometry rg1=new RectangleGeometry();//矩形图形
	    double dura,x1,y1,rs1,total1;
	    int count1;
		private void button2_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			pg1.Clear();
			this.canvas1.Children.Clear();
			y1=this.canvas1.Height;
			count1=0;
			baiyechuang();//设置百叶窗
			timer2.Interval = TimeSpan.FromMilliseconds(100);
            timer2.Tick += new EventHandler(timer2Tick);
		    timer2.Start();
		}
		void timer2Tick(object sender, EventArgs e)
       {
		  pg1.Clear();//清除图形
		  this.canvas1.Children.Clear();//清除百叶窗
		  if (count1==0){
		       x1=0;//窗叶从右向左收起
		  }else{
		       x1=this.canvas1.Width-rs1;//窗叶从左向右收起
		  }
		  if (rs1>1){ 
		    rs1=rs1-1;
		    dura=dura+1;
		    for (int i=0;i<total1;i++){
			    rg1.Rect = new Rect(x1,0,rs1,y1);//定义矩形
			    pg1=Geometry.Combine(pg1,rg1,GeometryCombineMode.Union, null);//相加组合几何图形，没有转换。
			if (count1==0){
				x1=x1+rs1+dura;
			}else{
			    x1=x1-rs1-dura;
			}
			  Path rgpath = new Path();
			  rgpath.Fill=Brushes.LightGreen;//浅绿叶片
			  rgpath.Stroke = Brushes.LightGray; //浅灰边框
              rgpath.StrokeThickness = 1; 
			  rgpath.Data=pg1;
			  this.canvas1.Children.Add(rgpath); //填充
			}
		  }else{
		   count1++;
		   if (count1>1){
		       timer2.Stop();
			   this.canvas1.Children.Clear();
		     }else{
                baiyechuang();
			 }
		  }		
	    }
	    void baiyechuang(){
		    rs1=15;//窗叶宽
			x1=0;
			dura=1;//窗叶缝
			total1=(int)Math.Ceiling(this.textblock2.Width/(rs1+dura));//扫描总数
			for (int i=0;i<total1;i++){
			rg1.Rect = new Rect(x1,0,rs1,y1);//定义矩形
			pg1=Geometry.Combine(pg1,rg1,GeometryCombineMode.Union, null);//相加组合几何图形，没有转换。
			x1=x1+rs1+dura;
			Path rgpath = new Path();//路径定义
			rgpath.Fill=Brushes.LightGreen;//浅绿叶片
			rgpath.Stroke = Brushes.LightGray; //浅灰边框
            rgpath.StrokeThickness = 1; 
			rgpath.Data=pg1;//路径填充
			this.canvas1.Children.Add(rgpath);  //填充
			}
		}
		
		//探照灯效果****************************
		DispatcherTimer timer3 = new DispatcherTimer();//定时器
        EllipseGeometry ellipse = new EllipseGeometry();//创建椭圆图形，但不显示
		RectangleGeometry rg3=new RectangleGeometry();//矩形图形
		CombinedGeometry cg;
		Path rgpath3;
		double cx,cy;
		int count3;
	    private void button3_Click(object sender, System.Windows.RoutedEventArgs e)
	    {
			count3=3;//灯光往返计数
			again=0;//扫描方向判断，0-从左到右，1-从右到左
			rg3.Rect = new Rect(0,0,this.canvas2.Width,this.canvas2.Height);//定义剪裁矩形位置
			cx =this.canvas2.Height/2;
			cy =this.canvas2.Height/2;
			ellipse.RadiusX=this.canvas2.Height/2;
			ellipse.RadiusY=this.canvas2.Height/2;						
			rgpath3 = new Path();//路径定义
			rgpath3.Fill=Brushes.Black;//背景，黑
			rgpath3.Stroke = Brushes.LightGray; //浅灰边框
            rgpath3.StrokeThickness = 1; 			
			timer3.Interval = TimeSpan.FromMilliseconds(10);//定时10毫秒
            timer3.Tick += new EventHandler(timer3Tick);//设置访问程序
		    timer3.Start();//启动
	    }
		//定时访问程序
		void timer3Tick(object sender, EventArgs e)			
       {
		   if (count3>0){
		    if (again==0){
     		   if ((this.canvas2.Width-cx)>this.canvas2.Height/2 )
		          cx++;
			   else{
			      again=1;
			    }
			 }else{
			   if (cx>this.canvas2.Height/2 ){
		          cx--;
			   }else{
			      again=0;
			      count3--;
			   }
			}
			this.canvas2.Children.Clear();
			ellipse.Center =new Point(cx,cy);//灯光中心点
			cg=new CombinedGeometry(GeometryCombineMode.Exclude,rg3,ellipse);//相减合并图形			
			rgpath3.Data=cg;//路径填充
			this.canvas2.Children.Add(rgpath3);
		   }else{
			this.canvas2.Children.Clear();
		    timer3.Stop();}
	   }	   
	}
}