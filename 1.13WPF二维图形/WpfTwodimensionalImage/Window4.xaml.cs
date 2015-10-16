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
	public partial class Window4 : Window
	{
		public Window4()
		{
			this.InitializeComponent();
		}
        PathGeometry pg=new PathGeometry();//定义路径几何图形 
	    RectangleGeometry rg=new RectangleGeometry();//矩形几何图形
		EllipseGeometry eg=new EllipseGeometry();//椭圆几何图形
		private void button1_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            rg.Rect = new Rect(0,0,150,150);//矩形参数设置
			//和矩形几何图形组合
            pg=Geometry.Combine(pg,rg,GeometryCombineMode.Union, null);
			//椭圆参数设置
		    eg.Center=new Point(this.image.ActualWidth/2,this.image.ActualHeight/2);
			eg.RadiusX=150;
			eg.RadiusY=60;
			//和椭圆几何图形组合
			pg=Geometry.Combine(pg,eg,GeometryCombineMode.Union, null);
			rg.Rect = new Rect(280,180,186,114); 
			//再和1个矩形几何图形组合
            pg=Geometry.Combine(pg,rg,GeometryCombineMode.Union, null);
		    this.image.Clip=pg;//用组合图形剪裁图片
		}
	}
}