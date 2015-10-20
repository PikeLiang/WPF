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

using Microsoft.Win32;
using System.Windows.Threading;

namespace WpfVideoPlayer
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
       // 创建 定时器
		private DispatcherTimer timer=new  DispatcherTimer();
		public MainWindow()
		{
			this.InitializeComponent();
			timer.Interval=TimeSpan.FromMilliseconds(500);
			timer.Tick+=new EventHandler(timerarrive);
		}

		private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			me.Source=new Uri(System.Environment.CurrentDirectory+@"\video1.wmv",UriKind.Absolute);
			me.Play();
			me.MediaOpened+=new RoutedEventHandler(meopened);
		}
		
		private void meopened(object sender, System.Windows.RoutedEventArgs e)
		{
			timer.Start();
		}
		
		private void button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			timer.Stop();
			me.Stop();
			me.Source=null;
			OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "选择WMV文件|*.wmv";
            openfile.Title = "选择SWMV文件";
            if (openfile.ShowDialog() == true)
            {
                if (openfile.FileName != "")
                {
					me.Source=new Uri(openfile.FileName,UriKind.Absolute);
					me.Play();
					me.MediaOpened+=new RoutedEventHandler(meopened);
				}
            }
		}
       	double melength;
		private void timerarrive(object sender,EventArgs e){
			//当前位置时间*滚动条的最大值/总时间
			melength=me.NaturalDuration.TimeSpan.TotalSeconds;//视频时间总长
		   	this.sbar.Value=me.Position.TotalSeconds*sbar.Maximum/melength;
		    int h=me.Position.Hours;
			int m=me.Position.Minutes;
			int s=me.Position.Seconds;
			this.tb1.Text=(h<10? "0"+h.ToString():h.ToString())+":"
			             +(m<10? "0"+m.ToString():m.ToString())+":"
			             +(s<10? "0"+s.ToString():s.ToString());
		}
		
		private void sbar_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
		{
			if (me.CanPause){
			timer.Stop();
			me.Stop();
			double times=this.sbar.Value*me.NaturalDuration.TimeSpan.TotalSeconds/sbar.Maximum;
			me.Position=TimeSpan.FromSeconds(times);
			me.Play();
			timer.Start();
			}
			//else
			 // sbar.Value=0;
		}

		private void pause_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			me.Pause();
		}

		private void play_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			me.Play();
		}

		private void stop_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			me.Stop();
		}

		private void repeat_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			me.Stop();
			me.Play();
		}

		private void forward_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			timer.Stop();
			double times=me.Position.TotalSeconds+me.NaturalDuration.TimeSpan.TotalSeconds/100;
			me.Stop();
			me.Position=TimeSpan.FromSeconds(times);
			me.Play();
			timer.Start();			
		}

		private void back_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			timer.Stop();
			double times=me.Position.TotalSeconds-me.NaturalDuration.TimeSpan.TotalSeconds/100;
			me.Stop();
			me.Position=TimeSpan.FromSeconds(times);
			me.Play();
			timer.Start();
		}

		private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			me.IsMuted=!me.IsMuted;
			if (me.IsMuted){
			   image.Source=new ImageSourceConverter().ConvertFromString(System.Environment.CurrentDirectory+@"\s2.png") as ImageSource;
			}else{
               image.Source=new ImageSourceConverter().ConvertFromString(System.Environment.CurrentDirectory+@"\s1.png") as ImageSource;
			}
		}
		
	}
}