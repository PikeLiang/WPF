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
	/// Window8.xaml 的交互逻辑
	/// </summary>
	public partial class Window8 : Window
	{
		public Window8()
		{
			this.InitializeComponent();
			
			// 在此点之下插入创建对象所需的代码。
		}

		private void button1_Click(object sender, System.Windows.RoutedEventArgs e)
		{			
			Glyphs gp=new Glyphs();
			gp.FontUri=new Uri(@"C:\WINDOWS\Fonts\SIMSUN.TTC");//宋体字
			gp.UnicodeString="GlyphRunDrawing绘制文本!";
			gp.FontRenderingEmSize=30;//大小
			gp.IsSideways=false;//是否旋转
			gp.StyleSimulations=StyleSimulations.BoldSimulation;//粗体字
     		GlyphRunDrawing grd = new GlyphRunDrawing();
			grd.ForegroundBrush=Brushes.Red;//字颜色
			grd.GlyphRun=gp.ToGlyphRun();//创建GlyphRun字符
		    DrawingImage di = new DrawingImage(grd);//绘制文字用于显示
			Image myimage = new Image();//定义图像对象
            myimage.Source = di;//作为图像对象
			Canvas.SetLeft(myimage,20);//绘图位置坐标
			Canvas.SetTop(myimage,100);
			this.canvas1.Children.Add(myimage); //装入容器显示	
		}
	}
}