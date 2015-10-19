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

using System.Diagnostics;//for Process
using System.Runtime.InteropServices; //for DllImport
using System.Windows.Interop;//for WindowInteropHelper
namespace WpfEXE
{	
	public partial class MainWindow : Window
	{
		//声明调用user32.dll中的函数
		[DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
		[DllImport("user32.dll")]
        static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow); 
		//定义变量
		private IntPtr prsmwh;//外部EXE文件运行句柄
		private Process app;//外部exe文件对象
		public MainWindow()
		{
			this.InitializeComponent();			
		}

		private void excel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			//获取当前窗口句柄
			IntPtr handle = new WindowInteropHelper(this).Handle;
			app = Process.Start("excel.exe");
			prsmwh = app.MainWindowHandle;
            while ( prsmwh ==IntPtr.Zero)
            {
				prsmwh = app.MainWindowHandle;
            }
			//设置父窗口
			SetParent(prsmwh,handle);
			ShowWindowAsync(prsmwh,3);//子窗口最大化 
		}
		
		 private void notepad_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	//获取当前窗口句柄
			IntPtr handle = new WindowInteropHelper(this).Handle;
			app = System.Diagnostics.Process.Start("NOTEPAD.EXE");
			prsmwh = app.MainWindowHandle;
            while ( prsmwh ==IntPtr.Zero)
            {
				prsmwh = app.MainWindowHandle;
            }
			//设置父窗口
			SetParent(prsmwh,handle);
			ShowWindowAsync(prsmwh,3);//子窗口最大化 
        }
		
        private void storm_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			//获取当前窗口句柄
			IntPtr handle = new WindowInteropHelper(this).Handle;
            string path=Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)+@"\StormII\Storm.exe";
			app = Process.Start(path,"video1.flv");
			prsmwh = app.MainWindowHandle;
            while ( prsmwh ==IntPtr.Zero)
            {
 				prsmwh = app.MainWindowHandle;
            }
			//设置父窗口
			SetParent(prsmwh,handle);
			ShowWindowAsync(prsmwh,3);//子窗口最大化 
		}
		
		private void flash_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			//获取当前窗口句柄
			IntPtr handle = new WindowInteropHelper(this).Handle;
            string path=System.Environment.CurrentDirectory + @"\FlashPlayer.exe";
			app = Process.Start(path);
			prsmwh = app.MainWindowHandle;
            while ( prsmwh ==IntPtr.Zero)
            {
 				prsmwh = app.MainWindowHandle;
            }
			//设置父窗口
			SetParent(prsmwh,handle);
			ShowWindowAsync(prsmwh,3);//子窗口最大化 
		}

        private void Window_Closed(object sender, System.EventArgs e)
		{
			if (app.CloseMainWindow()){
				app.Kill();
				app.Close();				
			}
		}		
	}
}