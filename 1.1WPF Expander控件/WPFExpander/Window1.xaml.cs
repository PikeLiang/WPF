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
	/// Window1.xaml 的交互逻辑
	/// </summary>
	public partial class Window1 : Window
	{
		public Window1()
		{
			this.InitializeComponent();
			
			// 在此点之下插入创建对象所需的代码。
		}

		private void rb1_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.expander.ExpandDirection=ExpandDirection.Down;
		}

		private void rb2_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.expander.ExpandDirection=ExpandDirection.Up;
		}

		private void rb3_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.expander.ExpandDirection=ExpandDirection.Left;
		}

		private void rb4_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.expander.ExpandDirection=ExpandDirection.Right;
		}

		

	}
}