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

namespace WPFExpander
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			this.InitializeComponent();

			// 在此点下面插入创建对象所需的代码。
		}

		private void expander1_Expanded(object sender, System.Windows.RoutedEventArgs e)
		{
			Grid.SetZIndex(this.expander1,1);
			this.expander2.IsExpanded=false;
			this.me.Stop();
		}

		private void expander1_Collapsed(object sender, System.Windows.RoutedEventArgs e)
		{
			Grid.SetZIndex(this.expander1,0);
		}
		private void expander2_Expanded(object sender, System.Windows.RoutedEventArgs e)
		{
			Grid.SetZIndex(this.expander2,1);
			this.me.Play();
		}

		private void expander2_Collapsed(object sender, System.Windows.RoutedEventArgs e)
		{
			Grid.SetZIndex(this.expander2,0);
		}

	}
}