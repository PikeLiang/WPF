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

namespace WpfTwodimensionalImage
{
	/// <summary>
	/// Window3.xaml 的交互逻辑
	/// </summary>
	public partial class Window3 : Window
	{
		public Window3()
		{
			this.InitializeComponent();			

		}

		private void button1_Click(object sender, System.Windows.RoutedEventArgs e)
		{			
			Path path = new Path();
			Canvas.SetLeft(path,20);//设置和canvas1的相对位置
			Canvas.SetTop(path,40);
			PathFigure pathfigure = new PathFigure();//PathFigure对象
			pathfigure.StartPoint = new Point(0,0);//绘制图形的起点,Path中的开始点
			//添加线段(直线),确定终止点坐标,true设置描边，false不描边
			pathfigure.Segments.Add( new LineSegment(new Point(80,200),true));
			PathGeometry pg = new PathGeometry();//实例化路径几何图形
			pg.Figures.Add(pathfigure);	//添加		
            path.Stroke = Brushes.Red;
			path.StrokeThickness = 2;
			path.Data = pg;
			this.canvas1.Children.Add(path);//canvas1是path的容器
		}

		private void button2_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Path path = new Path();
			Canvas.SetLeft(path,80);//设置和canvas1的相对位置
			Canvas.SetTop(path,40);
			PathFigure pathfigure = new PathFigure();//PathFigure对象
			pathfigure.StartPoint = new Point(40,0);//绘制图形的起点,Path中的开始点			
			ArcSegment arcs=new ArcSegment();//定义线段(弧线)
			arcs.Point=new Point(40,150);//弧线终点
			arcs.Size=new Size(80,80);//弧线的宽高大小
			arcs.RotationAngle=0;//沿X轴旋转的度数
			arcs.IsLargeArc=false;//小于180度的弧线
			arcs.SweepDirection=SweepDirection.Clockwise;//旋转方向
			arcs.IsStroked=true;//描边
			pathfigure.Segments.Add(arcs );  
			PathGeometry pg = new PathGeometry();//实例化路径几何图形
			pg.Figures.Add(pathfigure);	//添加		
            path.Stroke = Brushes.Red;
			path.StrokeThickness = 2;
			path.Data = pg;
			this.canvas1.Children.Add(path);//canvas1是path的容器
		}

		private void button3_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Path path = new Path();
			Canvas.SetLeft(path,170);//设置和canvas1的相对位置
			Canvas.SetTop(path,40);
			PathFigure pathfigure = new PathFigure();//PathFigure对象
			pathfigure.StartPoint = new Point(50,0);//绘制图形的起点,Path中的开始点			
			BezierSegment bezier=new BezierSegment();//定义线段(贝塞尔曲线)
			bezier.Point1=new Point(10,100);//起点,第1控制点
			bezier.Point2=new Point(150,80);//第2控制点
			bezier.Point3=new Point(120,180);//终点
			bezier.IsStroked=true;//描边
			pathfigure.Segments.Add(bezier);
			PathGeometry pg = new PathGeometry();//实例化路径几何图形
			pg.Figures.Add(pathfigure);	//添加		
            path.Stroke = Brushes.Red;
			path.StrokeThickness = 2;
			path.Data = pg;
			this.canvas1.Children.Add(path);//canvas1是path的容器
		}

		private void button4_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Path path = new Path();
			Canvas.SetLeft(path,300);//设置和canvas1的相对位置
			Canvas.SetTop(path,0);
			PathFigure pathfigure = new PathFigure();//PathFigure对象
			pathfigure.StartPoint = new Point(0,0);//绘制图形的起点,Path中的开始点
			BezierSegment bezier=new BezierSegment();//定义线段(贝塞尔曲线)
			bezier.Point1=new Point(10,100);//起点,第1控制点
			bezier.Point2=new Point(60,0);//第2控制点
			bezier.Point3=new Point(60,100);//终点
			bezier.IsStroked=true;//描边
			pathfigure.Segments.Add(bezier);
			//添加线段(直线),确定终止点坐标,true设置描边，false不描边
			pathfigure.Segments.Add( new LineSegment(new Point(100,100),true));
			ArcSegment arcs=new ArcSegment();//定义线段(弧线)
			arcs.Point=new Point(100,200);//弧线终点
			arcs.Size=new Size(80,80);//弧线的宽高大小
			arcs.RotationAngle=0;//沿X轴旋转的度数
			arcs.IsLargeArc=true;//大于180度的弧线
			arcs.SweepDirection=SweepDirection.Clockwise;//旋转方向
			arcs.IsStroked=true;//描边
			pathfigure.Segments.Add(arcs);
			PathGeometry pg = new PathGeometry();//实例化路径几何图形
			pg.Figures.Add(pathfigure);	//添加		
            path.Stroke = Brushes.Red;
			path.StrokeThickness = 2;
			path.Data = pg;
			this.canvas1.Children.Add(path);//canvas1是path的容器
		}
	}
}