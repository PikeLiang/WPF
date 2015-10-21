using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverlightHtmlHost
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			// 为初始化变量所必需
			InitializeComponent();
			this.host.SourceUri=new Uri("/文件/shufa.swf",UriKind.Relative);//播放Flash
		}
        //嵌入并播放swf文件
		private void tb1_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			this.host.SourceUri=new Uri("/文件/huajuan.swf",UriKind.Relative);
		}
        //嵌入并显示PDF文件
		private void tb2_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			this.host.SourceUri=new Uri("/文件/虚拟现实技术.pdf",UriKind.Relative);
		}
        //嵌入并显示Html文件
		private void tb3_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			this.host.SourceUri=new Uri("/文件/blend/blend.html",UriKind.Relative);
		}
        //嵌入并链接网站
		private void tb4_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			this.host.SourceUri=new Uri("http://www.divelements.co.uk/silverlight/tools.aspx");
		}
        //下载或本机寻找播放器播放Flv文件
		private void tb5_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			this.host.SourceUri=new Uri("/文件/video1.flv",UriKind.Relative);
		}
         //下载文件
		private void tb6_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			this.host.SourceUri=new Uri("/文件/blend.rar",UriKind.Relative);
		}
	}
}