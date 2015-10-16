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
	/// Window7.xaml 的交互逻辑
	/// </summary>
	public partial class Window7 : Window
	{
		public Window7()
		{
			this.InitializeComponent();			
		}

		private void button1_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			DrawingGroup dg = new DrawingGroup();//定义复合绘图组
			ImageDrawing imaged1 = new ImageDrawing();//定义绘制图像对象
            imaged1.Rect = new Rect(0, 0, 200, 150);//绘制图像坐标和大小
			//绘制图片源
			imaged1.ImageSource=new BitmapImage(new Uri(@"im8.jpg", UriKind.Relative));
			dg.Children.Add(imaged1);//绘图组添加绘制图像
			ImageDrawing imaged2 = new ImageDrawing();
			imaged2.Rect = new Rect(180,60, 200,140);//绘制图像坐标和大小
			imaged2.ImageSource=new BitmapImage(new Uri(@"pict1.jpg", UriKind.Relative));
			dg.Children.Add(imaged2);//再添加
			DrawingImage di = new DrawingImage(dg);//绘制图像用于显示
			Image myimage = new Image();//定义图像对象
            myimage.Source = di;//赋予图像对象
			Canvas.SetLeft(myimage,40);//绘图位置坐标
			Canvas.SetTop(myimage,30);
			this.canvas1.Children.Add(myimage); //图像装入容器显示
		}
	}
}