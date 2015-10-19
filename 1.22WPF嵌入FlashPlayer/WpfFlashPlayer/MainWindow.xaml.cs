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

using AxShockwaveFlashObjects;
using ShockwaveFlashObjects;
using Microsoft.Win32;
using System.Windows.Threading;
namespace WpfFlashPlayer
{
	public partial class MainWindow : Window
	{
		// 创建 FlashPlayer对象
		private AxShockwaveFlash FlashPlayer = new AxShockwaveFlash();
		// 创建 定时器
		private DispatcherTimer timer=new  DispatcherTimer();
		public MainWindow()
		{
			this.InitializeComponent();
			timer.Interval=TimeSpan.FromMilliseconds(200);
			timer.Tick+=new EventHandler(timerarrive);
			timer.Start();
		}

		private void button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "选择SWF文件|*.swf";
            openfile.Title = "选择SWF文件";
            if (openfile.ShowDialog() == true)
            {
                if (openfile.FileName != "")
                {
					FlashPlayer.Movie=openfile.FileName;
				}
            }
        }

		private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			wfh.Child = FlashPlayer;
            string swff=System.Environment.CurrentDirectory+@"\shufa.swf";
			FlashPlayer.Movie=swff;
			this.Window.Title="WPF中嵌入Flash Player："+FlashPlayer.ProductVersion;
			this.tb1.Text=System.Environment.CurrentDirectory;
		}

		private void timerarrive(object sender,EventArgs e){
		    this.sbar.Value=FlashPlayer.FrameNum*sbar.Maximum/FlashPlayer.TotalFrames;;//当前帧数*最大值/总帧数
			this.tb.Text="帧："+FlashPlayer.FrameNum.ToString();
		}
		
        private void sbar_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
		{
			timer.Stop();
			int frames=Convert.ToInt32(this.sbar.Value)*FlashPlayer.TotalFrames/Convert.ToInt32(sbar.Maximum);
			FlashPlayer.GotoFrame(frames);
			FlashPlayer.FrameNum=frames;
			FlashPlayer.Play();
			timer.Start();
		}
		
		private void button_Pause_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			FlashPlayer.Stop();
		}

		private void button_Play_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			FlashPlayer.Play();
		}

		private void button_Stop_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			FlashPlayer.StopPlay();
			FlashPlayer.Rewind();
		}

		private void button_Repeat_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			FlashPlayer.StopPlay();
			FlashPlayer.Rewind();
			FlashPlayer.Play();
		}

		private void button_Forward_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			FlashPlayer.Forward();
		}

		private void button_Back_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			FlashPlayer.Back();
		}

		private void repeatforward_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			FlashPlayer.Forward();
		}

		private void repeatback_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			FlashPlayer.Back();
		}

	}
}