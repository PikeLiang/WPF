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

namespace WPFRepeatButton
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		double Hmax;
		public MainWindow()
		{
			this.InitializeComponent();
			Hmax=Canvas.GetTop(this.lineh)-10;
			this.xuanniu.Angle=0;
		}
        //启动秒表
		private void rb1_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			double js=double.Parse(this.textblock1.Text);
			if (js<999.5){
			   js=js+0.1;
			}
			//取整数部分,舍去小数
			int js1=(int)System.Math.Floor(js);
			if (js1<10){
			   this.textblock1.Text="00"+js.ToString();
			   if (js==System.Math.Floor(js)){
			     this.textblock1.Text=this.textblock1.Text+".0";
			}
			   return;
			}
			if (js1<100){
			   this.textblock1.Text="0"+js.ToString();
			   if (js==System.Math.Floor(js)){
			     this.textblock1.Text=this.textblock1.Text+".0";
			}	
			}
			
		}
        //秒表复位
		private void rb2_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			 this.textblock1.Text="000.0";
		}
        //水平校准线下移
		private void repeatbuttonLeft_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (Canvas.GetTop(this.lineh)<Hmax*2){				
				Canvas.SetTop(this.lineh,Canvas.GetTop(this.lineh)+1);
				this.xuanniu.Angle=this.xuanniu.Angle-2.4;
			}
		}
        //水平校准线上移
		private void repeatbuttonRight_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (Canvas.GetTop(this.lineh)>20){				
			   Canvas.SetTop(this.lineh,Canvas.GetTop(this.lineh)-1);
			   this.xuanniu.Angle=this.xuanniu.Angle+2.4;
			}
		}
		
	}
}