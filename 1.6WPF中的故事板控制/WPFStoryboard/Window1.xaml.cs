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
namespace WPFStoryboard
{
	/// <summary>
	/// Window1.xaml 的交互逻辑
	/// </summary>
	public partial class Window1 : Window
	{
		double repeatbehavior=1;
		Storyboard mystoryboard=new Storyboard();
		public Window1()
		{
			this.InitializeComponent();
			this.textbox1.Text=repeatbehavior.ToString();
			mystoryboard=(Storyboard)this.FindResource("Storyboard1");
			mystoryboard.Completed+=new System.EventHandler(mystoryboard_Completed);
		}
		private void mystoryboard_Completed(object sender, System.EventArgs e)
		{
           //this.textBlock1.Text="故事板完成";
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
			   mystoryboard.RepeatBehavior=RepeatBehavior.Forever;
			}else{
			   mystoryboard.RepeatBehavior=new RepeatBehavior(repeatbehavior);	
			}
			
			if ((bool)this.checkbox1.IsChecked){
				mystoryboard.AutoReverse=true;
			}
			if ((bool)this.checkbox2.IsChecked){
				mystoryboard.FillBehavior=FillBehavior.HoldEnd;
			}else
			{
			    mystoryboard.FillBehavior=FillBehavior.Stop;
			}			
			mystoryboard.Begin();
		}

		private void button2_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			mystoryboard.Pause();
		}
		private void button3_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			mystoryboard.Resume();
		}
		
		private void button4_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			mystoryboard.Stop();
		}

		private void button5_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			mystoryboard.SkipToFill();
		}

		private void button6_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			mystoryboard.Seek(TimeSpan.FromSeconds(this.slider2.Value));
		}

		private void slider1_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
		{
			mystoryboard.SetSpeedRatio(this.slider1.Value);
		}

		private void slider2_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
		{
			//mystoryboard.Seek(TimeSpan.FromSeconds(this.slider2.Value));
			mystoryboard.SeekAlignedToLastTick(TimeSpan.FromSeconds(this.slider2.Value));
		}
	
	}
}