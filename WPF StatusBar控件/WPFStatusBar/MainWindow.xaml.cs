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

namespace WPFStatusBar
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		int items;
		public MainWindow()
		{
			this.InitializeComponent();
			this.sbt1.Content="今天日期："+System.DateTime.Now.ToLongDateString()+"  星期："+System.DateTime.Now.DayOfWeek.ToString();
			items=this.statusbar.Items.Count;
		}

		private void tuichu_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			//Application.Current.Shutdown();
			this.Close();
		}

		private void menu11_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			TextBlock tb=new TextBlock();
			tb.Text="课程教学计划安排，学时分配，实习实验指导等……";
			tb.Foreground=Brushes.White;
			this.statusbar.Items.Add(tb);
		}

		private void menu11_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)			
		{
			this.statusbar.Items.RemoveAt(items);
		}

		private void menu12_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			TextBlock tb=new TextBlock();
			tb.Text="介绍课程学习预备知识、课程内容、学习方法等……";
			tb.Foreground=Brushes.White;
			this.statusbar.Items.Add(tb);
		}

		private void menu12_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			this.statusbar.Items.RemoveAt(items);
		}

		private void tuichu_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			TextBlock tb=new TextBlock();
			tb.Text="退出本系统……";
			tb.Foreground=Brushes.White;
			this.statusbar.Items.Add(tb);
		}

		private void tuichu_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			this.statusbar.Items.RemoveAt(items);
		}

		private void statusbar_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			//System.Windows.MessageBox.Show("MouseEnter");
		}

		private void statusbar_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			//System.Windows.MessageBox.Show("MouseLeave");
		}

		private void statusbar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			//System.Windows.MessageBox.Show("MouseLeftButtonDown");
		}
	}
}