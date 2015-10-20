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
using System.Xml;
namespace WpfExercises
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	public partial class MainWindow : Window
	{
		public static MainWindow mw;
		public static int fenmt=5;//定义每题的分值
		public static int danxuanenter=0;//已经选择单选题数
		public static int danxuanright=0;//单选题正确数
		public static int [ ] danxuanFinished=new int [40];//用作单选已做题的标志，1为已做
		public static int [ ] danxuanSelected=new int [40];//单选已做题的选择
		public static string [ ] danxuanYesNo=new string [40];//单选题是否正确×或√
		public static int duoxuanenter=0;//已经选择多选题数
		public static int duoxuanright=0;//多选题正确数
		public static int [ ] duoxuanFinished=new int [40];//用作多选已做题的标志，1为已做
		public static int [ ,] duoxuanSelected=new int [40,4];//多选题已做题的选择
		public static string [ ] duoxuanYesNo=new string [40];//多选题是否正确×或√
		public static int panduanenter=0;//已经选择判断题数
		public static int panduanright=0;//判断题正确数		
		public static int [ ] panduanFinished=new int [40];//用作判断题已做题的标志，1为已做
		public static int [ ] panduanSelected=new int [40];//判断题已做题的选择
		public static string [ ] panduanYesNo=new string [40];//判断题是否正确×或√
		private DispatcherTimer timer=new  DispatcherTimer();
		private DateTime currentTime=new DateTime();   
		public MainWindow()
		{
			this.InitializeComponent();
			mw=this;
			timer.Interval=TimeSpan.FromMilliseconds(1000);//时间间隔1秒
			timer.Tick+=new EventHandler(timerarrive);//时间到事件
			this.danxuanhjtext.Text="0";
			this.duoxuanhjtext.Text="0";
			this.panduanhjtext.Text="0";
			this.zongfenhjtext.Text="0";
			this.Timertb.Text="00:00:00";
			this.frame.Source=new Uri("/Page1.xaml", UriKind.Relative);
		}

		private void timerarrive(object sender,EventArgs e){
			int h=System.Math.Abs(System.DateTime.Now.Hour-currentTime.Hour);
			int m=System.Math.Abs(System.DateTime.Now.Minute-currentTime.Minute);
			int s=System.Math.Abs(System.DateTime.Now.Second-currentTime.Second);
			this.Timertb.Text=(h<10? "0"+h.ToString():h.ToString())+":"
			             +(m<10? "0"+m.ToString():m.ToString())+":"
			             +(s<10? "0"+s.ToString():s.ToString());
		}
		
		private void button1_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			timer.Start();
			currentTime=System.DateTime.Now;//取当前时间
		}

		private void button2_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			timer.Stop();
		}
    
		private void danxuanti_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			this.frame.Source=new Uri("/Page1.xaml", UriKind.Relative);
		}

		private void duoxuanti_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			this.frame.Source=new Uri("/Page2.xaml", UriKind.Relative);
		}

		private void panduanti_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			this.frame.Source=new Uri("/Page3.xaml", UriKind.Relative);
		}

		private void chaxun_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			this.frame.Source=new Uri("/Page4.xaml", UriKind.Relative);
		}		
	}
}