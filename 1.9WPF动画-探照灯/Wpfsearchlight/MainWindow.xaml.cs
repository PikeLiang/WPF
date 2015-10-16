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

namespace Wpfsearchlight
{
	
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			this.InitializeComponent();

		}

		private void exit_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}
	}
}