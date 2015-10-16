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
	/// Window1.xaml 的交互逻辑
	/// </summary>
	public partial class Window1 : Window
	{
		public Window1()
		{
			this.InitializeComponent();
		}

		private void button1_Click(object sender, System.Windows.RoutedEventArgs e)
		{			
			Path path = new Path();
			Canvas.SetLeft(path,10);//设置和canvas1的相对位置
			Canvas.SetTop(path,40);
			LineGeometry linegeometry = new LineGeometry();
            linegeometry.StartPoint = new Point(0,0);//path中的坐标
			linegeometry.EndPoint = new Point(130,180);
            path.Stroke = Brushes.Red;
            path.StrokeThickness =4;
            path.Data = linegeometry;			
			this.canvas1.Children.Add(path);//canvas1是path的容器
		}

		private void button2_Click(object sender, System.Windows.RoutedEventArgs e)
		{			
            Path path = new Path();
			Canvas.SetLeft(path,185);//设置和canvas1的相对位置
			Canvas.SetTop(path,0);
			RectangleGeometry rectanglegeometry = new RectangleGeometry(); 
			//矩形宽100,高160,在path中的坐标为0,60
            rectanglegeometry.Rect = new Rect(0,60,100,160);
            path.Fill = Brushes.Yellow;
            path.Stroke = Brushes.Green;
            path.StrokeThickness = 3;
            path.Data = rectanglegeometry;			
			this.canvas1.Children.Add(path);//canvas1是path的容器
		}

		private void button3_Click(object sender, System.Windows.RoutedEventArgs e)
		{			
            Path path = new Path();
			Canvas.SetLeft(path,325);//设置和canvas1的相对位置
			Canvas.SetTop(path,40);
			EllipseGeometry ellipsegeometry = new EllipseGeometry();
			//path中的坐标
            ellipsegeometry.Center = new Point(50, 100);
            ellipsegeometry.RadiusX = 40;
            ellipsegeometry.RadiusY = 100;
            path.Fill = Brushes.Blue;
            path.Stroke = Brushes.DarkOrange;
            path.StrokeThickness = 6;
            path.Data = ellipsegeometry;
			this.canvas1.Children.Add(path);//canvas1是path的容器
		}
	}
}