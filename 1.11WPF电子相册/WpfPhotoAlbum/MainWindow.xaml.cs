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

using System.Windows.Threading;//for Timer
namespace WpfPhotoAlbum
{

	public partial class MainWindow : Window
	{
		private DispatcherTimer timer = new DispatcherTimer();
		int count=0;
		public MainWindow()
		{
		   this.InitializeComponent();
		   this.textblock1.Text="图片1：淡入淡出";
		   timer.Interval = TimeSpan.FromMilliseconds(1000);
           timer.Tick += new EventHandler(timerTick);
		   timer.Start();
		}

		private void Rectangle_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Application.Current.Shutdown();
		}
		
		void timerTick(object sender, EventArgs e){
			count++;
			switch (count){
				case 1:
				  this.textblock1.Text="图片1：淡入淡出";					
				  break;
				case 4:
				  this.textblock1.Text="图片2：二维旋转";	
				  break;
				case 8:
				  this.textblock1.Text="图片3：三维正交相机推拉";
				  break;
				case 15:
				  this.textblock1.Text="图片4：三维旋转";
				  break;
				case 24:
				  this.textblock1.Text="图片5：三维环境灯光变换";
				  break;
				case 35:
				  this.textblock1.Text="图片6：三维照射灯光扫描";
				  break;
				case 44:
				  this.textblock1.Text="图片7：二维缩放";
				  break;
				case 49:
				  this.textblock1.Text="图片8、9：三维移动、转动";
				  break;
				case 55:
				  this.textblock1.Text="图片10：淡出";
				  break;
				case 59:
				  this.textblock1.Text="图片1：淡入淡出";
				  break;
				case 60:
                  count=0;	
				  break;	
			}
		}
	}
}