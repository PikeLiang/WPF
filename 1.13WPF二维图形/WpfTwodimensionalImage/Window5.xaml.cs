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
	/// Window5.xaml 的交互逻辑
	/// </summary>
	public partial class Window5 : Window
	{
		public Window5()
		{
			this.InitializeComponent();
			
			// 在此点之下插入创建对象所需的代码。
		}

		private void button1_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Path path = new Path();
			Canvas.SetLeft(path,00);//设置和canvas1的相对位置
			Canvas.SetTop(path,00);
			path.Stroke = Brushes.Red;
			path.StrokeThickness = 2;
			StreamGeometry sg=new StreamGeometry();
			sg.FillRule=FillRule.EvenOdd;//取消交叉部分
			//打开1个属于sg的StreamGeometryContext（sg.Open()）
			using (StreamGeometryContext sgc = sg.Open()){
				//设置图形的起点，true可利用图形剪裁，true构成封闭图形
			    sgc.BeginFigure(new Point(0,200), true, true);
				//绘制直线，终点，true描边，true平滑连接
			    sgc.LineTo(new Point(100, 150), true , true);
				Point p=new Point(200,150);//椭圆弧终点
				Size s=new Size(100,50);//圆弧宽、高
				//绘制圆弧参数：终点，大小，沿X轴旋转角度，大于180度弧线，旋转方向，描边，平滑连接
				sgc.ArcTo(p,s,0,true,SweepDirection.Clockwise,true,true);
				Point p1=new Point(200,150);//第1个控制点
				Point p2=new Point(300,10);//第2个控制点
				Point p3=new Point(400,230);//终点				
				sgc.BezierTo(p1,p2,p3,true,true);//绘制贝塞尔曲线
			}            
			path.Data = sg;
			this.canvas1.Children.Add(path);//canvas1是path的容器
			this.image.Clip=sg;//剪裁图片
		}
	}
}