using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverlightEncoderandMediaPlayer
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			// 为初始化变量所必需
			InitializeComponent();
		}
        string uri,videoname;
		private void image1_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			//获取当前浏览位置定位(含网页文件名称),如：http://localhost:2277/Default.html
			uri=System.Windows.Browser.HtmlPage.Document.DocumentUri.ToString();
			//LastIndexOf("/"):搜寻定位中右边最后1个"/"的位置，其左侧是不含网页文件的定位			
			//形成服务器视频文件的位置信息
			videoname=uri.Substring(0,uri.LastIndexOf("/")+1)+"video/video1/Default.html";
			//导航链接
			System.Windows.Browser.HtmlPage.Window.Navigate(new Uri(videoname, UriKind.RelativeOrAbsolute));
		}

		private void image2_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			uri=System.Windows.Browser.HtmlPage.Document.DocumentUri.ToString();
			videoname=uri.Substring(0,uri.LastIndexOf("/")+1)+"video/video2/Default.html";
			System.Windows.Browser.HtmlPage.Window.Navigate(new Uri(videoname, UriKind.RelativeOrAbsolute));
		}

		private void image3_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			uri=System.Windows.Browser.HtmlPage.Document.DocumentUri.ToString();
			videoname=uri.Substring(0,uri.LastIndexOf("/")+1)+"video/video3/Default.html";
			System.Windows.Browser.HtmlPage.Window.Navigate(new Uri(videoname, UriKind.RelativeOrAbsolute));
		}

		private void image4_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			uri=System.Windows.Browser.HtmlPage.Document.DocumentUri.ToString();
			videoname=uri.Substring(0,uri.LastIndexOf("/")+1)+"video/video4/Default.html";
			System.Windows.Browser.HtmlPage.Window.Navigate(new Uri(videoname, UriKind.RelativeOrAbsolute));
		}

		private void image5_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			uri=System.Windows.Browser.HtmlPage.Document.DocumentUri.ToString();
			videoname=uri.Substring(0,uri.LastIndexOf("/")+1)+"video/silverlight/Default.html";
			System.Windows.Browser.HtmlPage.Window.Navigate(new Uri(videoname, UriKind.RelativeOrAbsolute));
		}
	}
}