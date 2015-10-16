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
	/// Window2.xaml 的交互逻辑
	/// </summary>
	public partial class Window2 : Window
	{
		public Window2()
		{
			this.InitializeComponent();
			
			// 在此点之下插入创建对象所需的代码。
		}

		private void button1_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Path path = new Path();
			Canvas.SetLeft(path,90);//设置和canvas1的相对位置
			Canvas.SetTop(path,0);
			RectangleGeometry rectanglegeometry = new RectangleGeometry(); 
			//矩形宽100,高160,在path中的坐标为0,60
            rectanglegeometry.Rect = new Rect(0,60,100,160);
  			EllipseGeometry ellipsegeometry = new EllipseGeometry();
			//path中的坐标
            ellipsegeometry.Center = new Point(50, 110);
            ellipsegeometry.RadiusX = 40;
            ellipsegeometry.RadiusY = 100;
            path.Fill = Brushes.Blue;
            path.Stroke = Brushes.DarkOrange;
            path.StrokeThickness = 6;
			//GeometryGroup对象
			GeometryGroup geometrygroup = new GeometryGroup();			
			geometrygroup.FillRule=FillRule.Nonzero;//填充规则设置
			geometrygroup.Children.Add(ellipsegeometry);
            geometrygroup.Children.Add(rectanglegeometry);			
			path.Data = geometrygroup;			
			this.canvas1.Children.Add(path);//canvas1是path的容器
		}

		private void button2_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Path path = new Path();
			Canvas.SetLeft(path,290);//设置和canvas1的相对位置
			Canvas.SetTop(path,0);
			RectangleGeometry rectanglegeometry = new RectangleGeometry(); 
			//矩形宽100,高160,在path中的坐标为0,60
            rectanglegeometry.Rect = new Rect(0,60,100,160);
  			EllipseGeometry ellipsegeometry = new EllipseGeometry();
			//path中的坐标
            ellipsegeometry.Center = new Point(50, 110);
            ellipsegeometry.RadiusX = 40;
            ellipsegeometry.RadiusY = 100;
            path.Fill = Brushes.Blue;
            path.Stroke = Brushes.DarkOrange;
            path.StrokeThickness = 6;
			//2个图形组合,相交模式,即取相交部分(还有相减Exclude,相加Union,异或Xor)
			CombinedGeometry combinedgeometry = new CombinedGeometry(GeometryCombineMode.Intersect,rectanglegeometry,ellipsegeometry);			
			path.Data = combinedgeometry;			
			this.canvas1.Children.Add(path);//canvas1是path的容器
		}
	}
}