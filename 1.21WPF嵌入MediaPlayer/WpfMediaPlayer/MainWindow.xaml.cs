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

using AxWMPLib;
using WMPLib;
using Microsoft.Win32;
namespace WpfMediaPlayer
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑

		
	public partial class MainWindow : Window
	{
	    // 实例化  AxWindowsMediaPlayer
		public  AxWindowsMediaPlayer wmp=new AxWindowsMediaPlayer();
		public MainWindow()
		{
			this.InitializeComponent();
 		}

		private void button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "选择WMV文件|*.wmv";
            openfile.Title = "选择WMV文件";
            if (openfile.ShowDialog() == true)
            {
                if (openfile.FileName != "")
                {
					wmp.URL=openfile.FileName;
                }
            }
		}

		private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			// 创建 wfh 对象需要程序集 System.Windows.Forms.dll支持,否则报错
		   System.Windows.Forms.Integration.WindowsFormsHost wfh = new System.Windows.Forms.Integration.WindowsFormsHost();
			// 嵌入wmp对象
            wfh.Child = wmp;
			// wfh 对象嵌入boder
            this.border.Child=wfh;
			string wmvf=System.Environment.CurrentDirectory+@"\silverlight.wmv";
			wmp.URL=wmvf;
		}

	}
}