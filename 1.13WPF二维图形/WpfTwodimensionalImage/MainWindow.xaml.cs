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
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			this.InitializeComponent();
		}

		private void button1_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Line line = new Line();//直线对象
            line.Stroke = System.Windows.Media.Brushes.Red;//红色
            line.X1 = 0;//起点
			line.Y1 = 0;
            line.X2 = 150;//终点
            line.Y2 = 320;
            line.StrokeThickness = 5;//粗细
            this.LayoutRoot.Children.Add(line);//装入容器
		}

		private void button2_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Ellipse ellipse = new Ellipse();//椭圆对象
			ellipse.Fill=System.Windows.Media.Brushes.Green;//红色
            ellipse.StrokeThickness = 4;//边缘粗细
            ellipse.Stroke =System.Windows.Media.Brushes.Gold;//金黄色
            ellipse.Width = 80;//宽
            ellipse.Height = 200;//高
			Canvas.SetLeft(ellipse,165);//左边距
            Canvas.SetTop(ellipse,80);//上边距
            this.LayoutRoot.Children.Add(ellipse);//装入容器
		}

		private void button3_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Rectangle rectangle = new Rectangle();//矩形对象
			rectangle.Fill=System.Windows.Media.Brushes.Blue;//蓝色
            rectangle.StrokeThickness = 4;//边缘粗细
            rectangle.Stroke =System.Windows.Media.Brushes.Pink;//粉色 
            rectangle.Width = 80;//宽
            rectangle.Height = 200;//高
			Canvas.SetLeft(rectangle,260);//左边距
            Canvas.SetTop(rectangle,80);//上边距
            this.LayoutRoot.Children.Add(rectangle);//装入容器
		}

		private void button4_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Polygon polygon = new Polygon();//Polygon对象
            polygon.Stroke = System.Windows.Media.Brushes.Red;//红色
            polygon.Fill = System.Windows.Media.Brushes.LightSeaGreen;//浅海蓝色
            polygon.StrokeThickness = 2;//变宽
            Point Point1 = new Point(360,80);//5个点
            Point Point2 = new Point(390,90);
            Point Point3 = new Point(435,150);
			Point Point4 = new Point(390,200);
			Point Point5 = new Point(360,300);
            PointCollection pointCollection = new PointCollection();//点集合
            pointCollection.Add(Point1);//添加到点集合
            pointCollection.Add(Point2);
            pointCollection.Add(Point3);
			pointCollection.Add(Point4);
            pointCollection.Add(Point5);
            polygon.Points = pointCollection;//Polygon对象点集
            this.LayoutRoot.Children.Add(polygon);
		}

		private void button5_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Polyline polyline = new Polyline();//Polyline对象
            polyline.Stroke = System.Windows.Media.Brushes.Red;//红色
            polyline.Fill = System.Windows.Media.Brushes.LightSeaGreen;//浅海蓝色
            polyline.StrokeThickness = 2;//边宽
            Point Point1 = new Point(460,80);//5个点
            Point Point2 = new Point(490,90);
            Point Point3 = new Point(535,150);
			Point Point4 = new Point(490,200);
			Point Point5 = new Point(460,300);
            PointCollection pointCollection = new PointCollection();//点集合对象
            pointCollection.Add(Point1);//添加到点集合
            pointCollection.Add(Point2);
            pointCollection.Add(Point3);
			pointCollection.Add(Point4);
            pointCollection.Add(Point5);
            polyline.Points = pointCollection;//Polyline对象点集
            this.LayoutRoot.Children.Add(polyline);
		}
	}
}