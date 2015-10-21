using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverlightCookie
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			// 为初始化变量所必需
			InitializeComponent();
		}
        //写Cookie
		private void b1_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (this.t1.Text.Length==0||this.t2.Text.Length==0){
				this.textblock1.Text="输入有缺项！";
			    return;}
			//通用协调时(UTC, Universal Time Coordinated),网络常用,北京时间超前UTC时间8小时
			DateTime ex = DateTime.UtcNow + TimeSpan.FromDays(30);
			//写入cookie的字符串，关键字expires不要修改
            string cookiestring = String.Format("{0}={1};expires={2}",
            this.t1.Text,this.t2.Text,ex.ToString("R"));
			//SetProperty()中使用"cookie"关键字标识,将连续写入不同名称的cookie系列,本机调试时看到的写入位置和txt文件是:
			//C:\Documents and Settings\Local Settings\Temporary Internet Files\administrator@localhost
			//内存Cookie：如果"cookie"改为其它名称，或关键字expires被修改，只在内存保存,写入时间无效，运行时可以指定名称读，退出或页面刷新自动消失
            System.Windows.Browser.HtmlPage.Document.SetProperty("cookie", cookiestring);
			this.textblock1.Text=cookiestring;
		}
        //读cookie全部数据
		private void b2_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			//读全部Cookie字符串，格式：Cookie名1=值; Cookie名2=值;……的组合串(注意分割符是;和1个空格)
			string readCookie = System.Windows.Browser.HtmlPage.Document.GetProperty("cookie") as String;
			this.textblock1.Text=readCookie;
		}		
        //删除指定的Cookie，只要修改时间就行
	    private void b3_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (this.t1.Text.Length==0){
				this.textblock1.Text="请输入Cookie名称！";
			    return;}
			//读以;分割的Cookie到字符串数组cookieString中
			string[] cookieString=System.Windows.Browser.HtmlPage.Document.Cookies.Split(';');
			bool yes=false;
			string[] ss;
			foreach (string ck in cookieString)
            {
               //对每个cookieString元素，读以=分割的字符到ss数组
			   ss = ck.Split('=');
			   //System.Windows.MessageBox.Show("["+ss[0]+"]长度:"+ss[0].Length.ToString());
			   //压缩字符开始的空格,ss[0]前有个空格
			   if (this.t1.Text.Trim()==ss[0].TrimStart()){
				yes=true;
			    break;}
	        }
			if (!yes){
			   System.Windows.MessageBox.Show("没有你选择的Cookie！");
			   return;
			}
			DateTime ex = DateTime.UtcNow + TimeSpan.FromMilliseconds(1);//只保留1毫秒
			string cookies = String.Format("{0}=;expires={2}", this.t1.Text,this.t2.Text, ex.ToString("R"));    
			System.Windows.Browser.HtmlPage.Document.SetProperty("cookie", cookies);
			this.textblock1.Text="删除“"+this.t1.Text.Trim()+"”Cookie成功！";
		}
		
	}
}