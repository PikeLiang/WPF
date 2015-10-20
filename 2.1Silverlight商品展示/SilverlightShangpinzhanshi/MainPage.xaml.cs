using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using System.Windows.Threading;//添加的命名空间,用于定时器
namespace SilverlightShangpinzhanshi
{
	public partial class MainPage : UserControl
	{
		int i=0;//图标计数
	    DispatcherTimer timers=new DispatcherTimer();//定时器
		public MainPage()
		{
			InitializeComponent();
			this.Storyboard1.Begin();//启动故事板
			this.path.StrokeThickness=0;//看不到圆轨迹
			//图片文件路径字符串转换为ImageSource类型
			this.image.Source=new ImageSourceConverter().ConvertFromString("image/size2/p1.png") as ImageSource;
			timers.Interval=new TimeSpan(0,0,2);//定时器间隔2秒
			timers.Tick+=new EventHandler(timetick);//定时器事件
			timers.Start();//启动定时器
		}
		private void timetick(object sender,EventArgs e){
	      	i++;
			if (i>10){
				i=1;
			}
			string ii=i.ToString();
			string pict="image/size2/p"+ii+".png";//组合图片文件路径字符串
			this.image.Source=new ImageSourceConverter().ConvertFromString(pict) as ImageSource;
	    }
		private void Menter_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			this.Storyboard1.Pause();//故事板暂停
			timers.Stop();
			Image sp=sender as Image;//小图片是事件发起
			string pict="image/size2/"+sp.Name+".png";
			this.image.Source=new ImageSourceConverter().ConvertFromString(pict) as ImageSource;			
		}

		private void Mlease_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			this.Storyboard1.Resume();//故事板恢复
			timers.Start();
		}
	}
}