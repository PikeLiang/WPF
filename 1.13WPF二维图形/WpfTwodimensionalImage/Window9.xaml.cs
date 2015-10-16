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
	/// Window9.xaml 的交互逻辑
	/// </summary>
	public partial class Window9 : Window
	{
		public Window9()
		{
			this.InitializeComponent();
		}
        //DrawingImage视频显示
		MediaPlayer mp = new MediaPlayer();//定义播放器
		VideoDrawing vd = new VideoDrawing();//定义绘制播放区对象
		private void button1_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			//视频文件源
			mp.Open(new Uri(@"video1.wmv", UriKind.Relative));
			vd.Rect = new Rect(0, 0, 460, 288);	//播放区坐标和大小		
			vd.Player = mp;//绘制的视频对象
			DrawingImage di = new DrawingImage(vd);//绘制用于显示
			Image myimage = new Image();//定义图像对象
            myimage.Source = di;//作为图像对象			
			Canvas.SetLeft(myimage,0);//绘图位置坐标
			Canvas.SetTop(myimage,0);
			this.canvas1.Children.Add(myimage); //装入容器显示
			mp.Play(); //播放
		}
		//DrawingBrush视频显示
        DrawingBrush db=new DrawingBrush();//定义绘制刷
		private void button2_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			mp.Open(new Uri(@"video1.wmv", UriKind.Relative));
			//绘制的播放区大小
			vd.Rect = new Rect(0, 0, 20, 20);	
			vd.Player = mp;//绘制的视频对象
			db.Drawing=vd;//视频作为绘制刷对象
			this.rectangle.Fill=db;//画刷填充
			mp.Play(); 
		}
        //暂停
		private void button3_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			mp.Pause();
		}
        //继续
		private void button4_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			mp.Play();
		}
		//停止
		private void button5_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			mp.Stop(); 
		}
        //重播
		private void button6_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			mp.Stop();
			mp.Play();
		}
	}
}