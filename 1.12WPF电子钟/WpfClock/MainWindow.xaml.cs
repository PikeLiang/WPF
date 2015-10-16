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

using System.Windows.Threading;//for DispatcherTimer
namespace WpfClock
{
	public partial class MainWindow : Window
	{
		private DispatcherTimer timer=new  DispatcherTimer();
		public MainWindow()
		{
			this.InitializeComponent();	
			//System.Windows.MessageBox.Show(h.ToString());
			timer.Interval=TimeSpan.FromSeconds(1);//时间间隔1秒
			timer.Tick+=new EventHandler(timerarrive);//时间到事件
			timer.Start();			
		}

		private void ellipse2_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Application.Current.Shutdown();
		}
		
		private void timerarrive(object sender,EventArgs e){
			//秒针转动1秒6度
			this.sec.Angle=(System.DateTime.Now.Second*6+180)%360;
			//分针转动1分相当于6度
			this.minu.Angle=(System.DateTime.Now.Minute*6+180)%360;
			//时针转动12*60:360=当前分:旋转角度
			int h=System.DateTime.Now.Hour*60+System.DateTime.Now.Minute;
			this.hours.Angle=(h*0.5+180)%360;
			this.datetext.Text=System.DateTime.Now.ToLongDateString();
			this.weektext.Text=System.DateTime.Now.DayOfWeek.ToString();
		}
	}
}