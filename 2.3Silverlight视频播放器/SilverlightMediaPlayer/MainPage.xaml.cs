using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using System.Windows.Threading;
namespace SilverlightMediaPlayer
{
	public partial class MainPage : UserControl
	{
		private DispatcherTimer timer=new  DispatcherTimer();
		string uri,videoname;//视频源地址和文件位置信息变量
		public MainPage()
		{
			InitializeComponent();
			this.Esctext.Visibility=Visibility.Collapsed;//Esc键提示关闭
			this.cp.Visibility=Visibility.Collapsed;//进度条当前位置显示文本框关闭
			this.grid4.Visibility=Visibility.Collapsed;//预览窗口关闭
			timer.Interval=TimeSpan.FromMilliseconds(500);//定时值
			timer.Tick+=new EventHandler(timerarrive);//定时访问
			Canvas.SetLeft(this.vernier,0);//设置视频播放进度游标初始位置
			Canvas.SetTop(this.vernier,0);			
		}
		//定时器定时访问程序
		double melength=0,currentvalue;//视频时间总长度和视频进度条当前值变量
		double movevalue=0;//鼠标悬停位置
		double times=0,previewtimes=0;//悬停不动计数和预览时间计数
		bool move=false;//鼠标移动
		private void timerarrive(object sender,EventArgs e){
			melength=me.NaturalDuration.TimeSpan.TotalSeconds;//视频时间总长度
			//视频进度条当前值=视频进度条最大值*视频当前位置时间/视频时间总长度
			currentvalue=this.progressbar.ActualWidth*me.Position.TotalSeconds/melength;
			Canvas.SetLeft(this.vernier,currentvalue);//设置视频进度游标位置
		    int h=me.Position.Hours;//获取视频当前位置时间小时值
			int m=me.Position.Minutes;//分值
			int s=me.Position.Seconds;//秒值
			this.textblock3.Text="播放时间进度："+(h<10? "0"+h.ToString():h.ToString())+":"
			             +(m<10? "0"+m.ToString():m.ToString())+":"
			             +(s<10? "0"+s.ToString():s.ToString());
			//预览视频
			if (times<4)
			    times++;//计数控制+1（位置不变的次数）
			if (times==1){//取当前鼠标X位置
			    movevalue=progressbarX;}
			if (move && movevalue==progressbarX){//如果鼠标滑动在进度条且X坐标没有变化
				if (times==4){//计数达4次
				    this.grid4.Visibility=Visibility.Visible;//开启预览窗口
					//如果预览窗口当前处于播放状态
					if (this.preview.CurrentState==MediaElementState.Playing){
						previewtimes++;//预览时间控制计数
						if (previewtimes==4)//达到预览时间
							this.preview.Pause();//暂停预览					
					}else{
					this.preview.Position=current;//设置预览视频位置
					this.preview.Play();//启动视频预览
					previewtimes=0;}//预览时间控制计数回0
				}
			}else{
			    this.grid4.Visibility=Visibility.Collapsed;//关闭预览窗口
				times=0;}//位置不变的次数计数回0			
		}
        //播放"机场"视频
		private void image1_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			timer.Stop();//定时器停止
			this.progressbar.Value=0;//下载进度值回0
			//获取当前浏览位置定位(含网页文件名称),如：http://localhost:2277/Default.html
			uri=System.Windows.Browser.HtmlPage.Document.DocumentUri.ToString();
			//LastIndexOf("/"):搜寻定位中右边最后1个"/"的位置，其左侧是不含网页文件的定位			
			//形成服务器视频文件的位置信息
			videoname=uri.Substring(0,uri.LastIndexOf("/")+1)+"video/video1.wmv";
			//设置视频播放控件的视频源
			this.me.Source=new Uri(videoname,UriKind.RelativeOrAbsolute);
			this.preview.Source=this.me.Source;//设置预览视频源
			this.preview.Stop();//关闭预览
			//设置视频文件打开后的事件
			this.me.MediaOpened+=new RoutedEventHandler(meOpened);
			//设置下载进度变化事件
			this.me.DownloadProgressChanged+=new RoutedEventHandler(meDownload);
			this.me.BufferingTime=TimeSpan.FromMilliseconds(500);//设置下载缓冲时间
			//设置下载缓冲变化的事件
			this.me.BufferingProgressChanged+=new RoutedEventHandler(me_BufferingProgressChanged);
		}
		//文件打开后定时器启动
		private void meOpened(object sender, System.Windows.RoutedEventArgs e)
		{
			timer.Start();
		}
		//下载进度显示
		private void meDownload(object sender, System.Windows.RoutedEventArgs e)
		{
			//DownloadProgress是下载进度，变化范围0-1
			this.progressbar.Value=this.me.DownloadProgress*this.progressbar.Maximum;
		}
		//下载缓冲变化显示
		double bp=0;
		private void me_BufferingProgressChanged(object sender,RoutedEventArgs e){
			//BufferingProgress：缓冲变化值0-1
			bp=this.me.BufferingProgress*100;
			this.textblock1.Text="下载缓冲进度:"+bp.ToString()+"%";
		   //MediaElement 的当前状态为下列值之一：Buffering、Closed、Opening、Paused、Playing 或 Stopped。默认值为 Closed。
			this.textblock2.Text="播放器当前状态:"+me.CurrentState.ToString();
		}
        //播放"魔手"视频
		private void image2_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			timer.Stop();
			this.progressbar.Value=0;
			uri=System.Windows.Browser.HtmlPage.Document.DocumentUri.ToString();
			videoname=uri.Substring(0,uri.LastIndexOf("/")+1)+"video/video2.wmv";
			this.me.Source=new Uri(videoname,UriKind.RelativeOrAbsolute); 
			this.me.MediaOpened+=new RoutedEventHandler(meOpened);
			this.me.DownloadProgressChanged+=new RoutedEventHandler(meDownload);
			this.preview.Source=this.me.Source;
			this.preview.Stop();
			this.me.BufferingTime=TimeSpan.FromMilliseconds(500);//默认值是5秒	
			this.me.BufferingProgressChanged+=new RoutedEventHandler(me_BufferingProgressChanged);
		}
        //播放"商场"视频
		private void image3_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			timer.Stop();
			this.progressbar.Value=0;
			uri=System.Windows.Browser.HtmlPage.Document.DocumentUri.ToString();
			videoname=uri.Substring(0,uri.LastIndexOf("/")+1)+"video/video3.wmv";
			this.me.MediaOpened+=new RoutedEventHandler(meOpened);
			this.me.DownloadProgressChanged+=new RoutedEventHandler(meDownload);
			this.me.Source=new Uri(videoname,UriKind.RelativeOrAbsolute);
			this.preview.Source=this.me.Source;
			this.preview.Stop();
			this.me.BufferingTime=TimeSpan.FromMilliseconds(500);//默认值是5秒	
			this.me.BufferingProgressChanged+=new RoutedEventHandler(me_BufferingProgressChanged);
		}
        //播放"马路"视频
		private void image4_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			timer.Stop();
			this.progressbar.Value=0;
			uri=System.Windows.Browser.HtmlPage.Document.DocumentUri.ToString();
			videoname=uri.Substring(0,uri.LastIndexOf("/")+1)+"video/video4.wmv";
			this.me.MediaOpened+=new RoutedEventHandler(meOpened);
			this.me.DownloadProgressChanged+=new RoutedEventHandler(meDownload);
			this.me.Source=new Uri(videoname,UriKind.RelativeOrAbsolute); 
			this.preview.Source=this.me.Source;
			this.preview.Stop();
			this.me.BufferingTime=TimeSpan.FromMilliseconds(500);//默认值是5秒	
			this.me.BufferingProgressChanged+=new RoutedEventHandler(me_BufferingProgressChanged);
		}
        //播放"silverlight"视频
		private void image5_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			timer.Stop();
			this.progressbar.Value=0;
			uri=System.Windows.Browser.HtmlPage.Document.DocumentUri.ToString();
			videoname=uri.Substring(0,uri.LastIndexOf("/")+1)+"video/silverlight.wmv";
			this.me.MediaOpened+=new RoutedEventHandler(meOpened);
			this.me.DownloadProgressChanged+=new RoutedEventHandler(meDownload);
			this.me.Source=new Uri(videoname,UriKind.RelativeOrAbsolute); 
			this.preview.Source=this.me.Source;
			this.preview.Stop();
			this.me.BufferingTime=TimeSpan.FromMilliseconds(500);//默认值是5秒	
			this.me.BufferingProgressChanged+=new RoutedEventHandler(me_BufferingProgressChanged);
		}
		//进度条当前位置时间显示
		double currentlength,progressbarX;//当前位置时间值和鼠标X坐标
		TimeSpan current;//进度条鼠标悬停处的时间
		private void progressbar_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			ProgressBar pb=sender as ProgressBar;
			//this.cp.Visibility=Visibility.Visible;
			melength=me.NaturalDuration.TimeSpan.TotalSeconds;//视频时间总长度
			//进度条当前位置时间值
			currentlength=melength*e.GetPosition(this.progressbar).X/this.progressbar.ActualWidth;
			int h=(int)currentlength/3600;
			int m=(int)((currentlength%3600)/60);
			int s=(int)((currentlength%3600)%60);
			Canvas.SetLeft(this.cp,e.GetPosition(this.progressbar).X-30);//设置视频进度游标位置
			Canvas.SetLeft(this.grid4,e.GetPosition(this.progressbar).X-54);//设置视频进度游标位置
			this.cp.Visibility=Visibility.Visible;
			this.cp.Content=(h<10? "0"+h.ToString():h.ToString())+":"
				           +(m<10? "0"+m.ToString():m.ToString())+":"
			               +(s<10? "0"+s.ToString():s.ToString());
			current=TimeSpan.Parse(this.cp.Content.ToString()); 
			move=true;
			progressbarX=e.GetPosition(this.progressbar).X;            
		}
		//鼠标离开进度条
		private void progressbar_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			this.grid4.Visibility=Visibility.Collapsed;
			this.cp.Visibility=Visibility.Collapsed;
			move=false;
			times=0;
			movevalue=0;
			this.preview.Stop();
			previewtimes=0;
		}
        //选择视频播放点
		private void progressbar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			this.me.Pause();
			this.me.Position=TimeSpan.FromSeconds(currentlength);
			this.me.Play();
		}
        //音量控制
		private void volume_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			this.me.Volume=e.GetPosition(this.volume).X/this.volume.ActualWidth;			
		}
        //静音控制
		private void mute_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			this.me.IsMuted=!this.me.IsMuted;
			if (this.me.IsMuted)
				this.grid5.Visibility=Visibility.Visible;
			else
				this.grid5.Visibility=Visibility.Collapsed;
		}
        //暂停按钮控制
		private void pause_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			this.Storyboard1.Begin();
		}
		private void pause_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			this.Storyboard1.Stop();
		}
		private void pause_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			this.me.Pause();
		}
        //播放按钮控制
		private void play_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			this.Storyboard2.Begin();
		}
		private void play_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			this.Storyboard2.Stop();
		}
		private void play_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			this.me.Play();
		}
        //停止按钮控制
		private void stop_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			this.Storyboard3.Begin();
		}
		private void stop_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			this.Storyboard3.Stop();
		}
		private void stop_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			this.me.Stop();
		}
        //重播按钮控制
		private void replay_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			this.Storyboard4.Begin();
		}
		private void replay_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			this.Storyboard4.Stop();
		}

		private void replay_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			this.me.Stop();
			this.me.Play();
		}
        //全屏按钮控制
		private void fullscreen_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			this.Storyboard5.Begin();
		}
		private void fullscreen_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			this.Storyboard5.Stop();
		}
        double canvas1W,canvas1H,meW,meH,meLeft,meTop;
		private void fullscreen_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
            //视频播放时使用全屏操作
			if (this.me.CurrentState==MediaElementState.Playing){
			canvas1W=this.canvas1.ActualWidth;//Canvas（me控件的容器）的原始大小
			canvas1H=this.canvas1.ActualHeight;
			meW=this.me.ActualWidth;//me控件的原始大小
			meH=this.me.ActualHeight;
			meLeft=Canvas.GetLeft(me);//me控件的原始位置
			meTop=Canvas.GetTop(me);
			//布局控件放大到当前应用程序（在浏览器中）界面大小
			this.canvas1.Width=Application.Current.Host.Content.ActualWidth;
			this.canvas1.Height=Application.Current.Host.Content.ActualHeight;
			//this.canvas1.Width=(double)System.Windows.Browser.HtmlPage.Window.Eval("screen.height");//桌面窗口大小
			//this.canvas1.Height=(double)System.Windows.Browser.HtmlPage.Window.Eval("screen.width");
			Canvas.SetZIndex(me,20);//设置me控件层次到上层
			//视频控件也放大到当前应用程序界面大小
			this.me.Width=this.canvas1.Width;
			this.me.Height=this.canvas1.Height;
			Canvas.SetLeft(me,0);//视频控件位置
			Canvas.SetTop(me,0);
			this.Esctext.Visibility=Visibility.Visible;//按Esc键提示
			Canvas.SetLeft(Esctext,0);//按Esc键提示框位置
			Canvas.SetTop(Esctext,0);
			Canvas.SetZIndex(Esctext,21);//按Esc键提示框放置最上层
			//设置按Esc键事件（自定义）
			Application.Current.RootVisual.KeyDown += new KeyEventHandler(ESC_Down);
			//使用Silverlight的全屏控制
			Application.Current.Host.Content.IsFullScreen=!Application.Current.Host.Content.IsFullScreen;
			}
		}
		private void ESC_Down(object sender, KeyEventArgs e)
		{
			if (e.Key.ToString()=="Escape"){//如果按Esc键
				this.Esctext.Visibility=Visibility.Collapsed;//Esc键提示框关闭				
			    this.canvas1.Width=canvas1W;//恢复布局原始大小
			    this.canvas1.Height=canvas1H;
				this.me.Width=meW;//恢复me控件原始大小
				this.me.Height=meH;
				Canvas.SetZIndex(me,0);//置于底层
			    Canvas.SetLeft(me,meLeft);//恢复原始位置
				Canvas.SetTop(me,meTop);
			}
		}
	}
}