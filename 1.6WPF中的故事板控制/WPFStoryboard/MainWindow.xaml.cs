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

using System.Windows.Media.Animation;//for Storyboard RepeatBehavior
using System.Windows.Threading;
namespace WPFStoryboard
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		double repeatbehavior=1;
		DispatcherTimer timer=new DispatcherTimer();
		public MainWindow()
		{
			this.InitializeComponent();
			this.textbox1.Text=repeatbehavior.ToString();
			this.mystoryboardTrigger.Storyboard.Completed+=new System.EventHandler(Storyboard_Completed);
			timer.Interval=TimeSpan.FromMilliseconds(100);
			timer.Tick+=new System.EventHandler(timer_Tick);
		}

		private void timer_Tick(object sender, System.EventArgs e)
		{
			this.tb1.Text=this.mystoryboardTrigger.Storyboard.GetCurrentTime().ToString();
			this.tb2.Text=this.mystoryboardTrigger.Storyboard.GetCurrentProgress().ToString();
		}

		private void Storyboard_Completed(object sender, System.EventArgs e)
		{
            timer.Stop();
		}

		private void rpb2_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			repeatbehavior++;
			if (repeatbehavior>30){
				repeatbehavior=30;
				this.textbox1.Text="Forever";
			}
			else{
				this.textbox1.Text=repeatbehavior.ToString();
			}
		}

		private void rpb1_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (repeatbehavior==1){
				repeatbehavior=1;
			}else{
			    repeatbehavior--;
			}
			this.textbox1.Text=repeatbehavior.ToString();
		}
		
		private void button1_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (repeatbehavior==30){
			   this.mystoryboardTrigger.Storyboard.RepeatBehavior=RepeatBehavior.Forever;
			}else{
			   this.mystoryboardTrigger.Storyboard.RepeatBehavior=new RepeatBehavior(repeatbehavior);	
			}
			
			if ((bool)this.checkbox1.IsChecked){
				this.mystoryboardTrigger.Storyboard.AutoReverse=true;
			}
			if ((bool)this.checkbox2.IsChecked){
				this.mystoryboardTrigger.Storyboard.FillBehavior=FillBehavior.HoldEnd;
			}else
			{
			    this.mystoryboardTrigger.Storyboard.FillBehavior=FillBehavior.Stop;
			}
			timer.Start();
			this.mystoryboardTrigger.Storyboard.Begin();
		}

		private void button2_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.mystoryboardTrigger.Storyboard.Pause();
		}
		private void button3_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.mystoryboardTrigger.Storyboard.Resume();
		}
		
		private void button4_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.mystoryboardTrigger.Storyboard.Stop();
		}

		private void button5_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.mystoryboardTrigger.Storyboard.SkipToFill();
		}

		private void slider1_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
		{
			this.mystoryboardTrigger.Storyboard.SetSpeedRatio(this.slider1.Value);
		}

		private void slider2_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
		{
			this.mystoryboardTrigger.Storyboard.SeekAlignedToLastTick(TimeSpan.FromSeconds(this.slider2.Value));
		}
	
	}
}