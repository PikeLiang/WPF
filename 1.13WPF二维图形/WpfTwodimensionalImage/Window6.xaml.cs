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
	/// Window6.xaml 的交互逻辑
	/// </summary>
	public partial class Window6 : Window
	{
		public Window6()
		{
			this.InitializeComponent();			
		}

		private void button1_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			StreamGeometry sg=new StreamGeometry();
			sg.FillRule=FillRule.EvenOdd;
			using (StreamGeometryContext sgc = sg.Open()){
			    sgc.BeginFigure(new Point(0,200), true, true);
			    sgc.LineTo(new Point(100, 150), true , true);
				Point p=new Point(200,150);//椭圆弧终点
				Size s=new Size(100,50);//圆弧宽、高
				sgc.ArcTo(p,s,0,true,SweepDirection.Clockwise,true,true);
				Point p1=new Point(200,150);//第1个控制点
				Point p2=new Point(300,10);//第2个控制点
				Point p3=new Point(400,230);//终点				
				sgc.BezierTo(p1,p2,p3,true,true);//绘制贝塞尔曲线
			}
			GeometryDrawing gd = new GeometryDrawing();//定义绘制几何形状对象
            gd.Geometry = sg;//绘制的几何图形
			LinearGradientBrush lgb=new LinearGradientBrush();//定义线性渐变
            //默认的线性渐变是沿对角线方向进行的，
			//StartPoint是被填充区域的左上角 (0,0)，EndPoint是被填充区域的右下角 (1,1)，
			//EndPoint(1,0)水平方向，EndPoint(0,1)垂直方向。
			lgb.StartPoint=new Point(0,0);
			lgb.EndPoint=new Point(1,0);
			lgb.GradientStops.Add(new GradientStop(Colors.Red, 0.1));//插入渐变停止点
            lgb.GradientStops.Add(new GradientStop(Colors.LightSkyBlue, 0.25));
			lgb.GradientStops.Add(new GradientStop(Colors.Blue, 0.5));
			lgb.GradientStops.Add(new GradientStop(Colors.LightGreen, 0.75));
			gd.Brush=lgb;//绘制图形填充色
			gd.Pen = new Pen(Brushes.Red, 2);//描边色
            DrawingImage di = new DrawingImage(gd);//绘制图像用于显示
            Image myimage = new Image();//定义图像对象
            myimage.Source = di;//赋予图像对象
			Canvas.SetLeft(myimage,60);//位置坐标
			Canvas.SetTop(myimage,30);
            this.canvas1.Children.Add(myimage); //图像装入容器显示
		}		
	}
}