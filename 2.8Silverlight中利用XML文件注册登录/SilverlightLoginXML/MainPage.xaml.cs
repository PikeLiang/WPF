using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using System.Xml.Linq;//for XDocument
//using System.Linq;
using System.Net;// for WebClient
using System.IO;//for Stream
using System.Windows.Browser;//for HtmlPage
using System.Text;//for  Encoding
namespace SilverlightLoginXML
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			// 为初始化变量所必需
			InitializeComponent();
			string readCookie = System.Windows.Browser.HtmlPage.Document.GetProperty("cookie") as String;
			this.text2.Text=GetCookieValue();
		}
        //注册
		string nm=null;
		string pw1=null;
		private void text1_LostFocus(object sender, System.Windows.RoutedEventArgs e)
		{
			nm=this.text1.Text.Trim();
			int nmlength=nm.Length;
			//汉字长度处理,C#中使用的unicode编码格式，默认一个汉字为一个字符。
			nmlength=getlength(nm,nmlength);
			if (nmlength<6){
			   System.Windows.MessageBox.Show("注册用户名长度不能少于6个字符！");
			   return;
			}
			//HtmlPage.Document.DocumentUri是Default.html的定位
			string uri=System.Windows.Browser.HtmlPage.Document.DocumentUri.ToString();
			//创建Uri，指向服务器端的数据流上传处理程序
			uri=uri.Substring(0,uri.LastIndexOf("/")+1)+"CheckHandler.ashx";
			UriBuilder builder=new UriBuilder(uri);
			//创建WebClient通讯对象
			WebClient client1=new WebClient();
			//通讯完成后的事件处理
			client1.UploadStringCompleted+=new UploadStringCompletedEventHandler(client1_UploadStringCompleted);
			//启动通讯
			client1.UploadStringAsync(builder.Uri,nm);	　
		}
        void client1_UploadStringCompleted(object sender,UploadStringCompletedEventArgs e){
			//System.Windows.MessageBox.Show(e.Result);
			if (e.Result=="Yes"){
			  var k=System.Windows.MessageBox.Show("注册用户名已经存在,请重新选择!","确认",MessageBoxButton.OKCancel);	
			  if (k.ToString()=="OK")
				this.text1.Focus();
			}
		}
		private void b1_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			nm=this.text1.Text.Trim();
			int nmlength=nm.Length;
			//汉字长度处理,C#中使用的unicode编码格式，默认一个汉字为一个字符。
			nmlength=getlength(nm,nmlength);
			if (nmlength<6){
			   System.Windows.MessageBox.Show("注册用户名长度不能少于6个字符！");
			   return;
			}
			pw1=this.passwordbox1.Password.Trim();
			string pw2=this.passwordbox2.Password.Trim();
			if (pw1.Length<6){
			   System.Windows.MessageBox.Show("密码长度不能少于6个字符！");
			   return;
			}
			if (!pw1.Equals(pw2)){
			   System.Windows.MessageBox.Show("两个密码不等！");
			   return;
			}
			string　uploadstring=nm+"/"+pw1;
			//HtmlPage.Document.DocumentUri是Default.html的定位
			string uri=System.Windows.Browser.HtmlPage.Document.DocumentUri.ToString();
			//创建Uri，指向服务器端的数据流上传处理程序
			uri=uri.Substring(0,uri.LastIndexOf("/")+1)+"UploadHandler.ashx";//网站发布（设置虚拟目录）也成功
			UriBuilder builder=new UriBuilder(uri);//ok
			//this.textblock1.Text = builder.Uri.ToString();
			WebClient client=new WebClient();
			client.UploadStringCompleted+=new　UploadStringCompletedEventHandler(client_UploadStringCompleted);
			client.UploadStringAsync(builder.Uri,uploadstring);	　
		}
        //
		void client_UploadStringCompleted(object sender,UploadStringCompletedEventArgs e){
			System.Windows.MessageBox.Show(e.Result);				
		}
        //浏览服务器端XML文件
		private void b2_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			//设置文件路径,HtmlPage.Document.DocumentUri是Default.html的定位
			Uri file=new Uri(HtmlPage.Document.DocumentUri,"/loginXML/zhuce.xml");//或者
			WebClient client=new WebClient();
			client.DownloadStringCompleted+=new DownloadStringCompletedEventHandler(client1_DownloadStringCompleted);
			//使用DownloadStringAsync方法下载文件数据读文本内容
			client.DownloadStringAsync(file);
		}
		void client1_DownloadStringCompleted(object sender,DownloadStringCompletedEventArgs e){
		   if (e.Error!=null){
			    System.Windows.MessageBox.Show("下载出错了！");
				return;
			}
			if(e.Result!=null){
			    //显示XML文件内容 			    
				this.textblock1.Text="XML文件长度："+e.Result.Length.ToString();
				Stream st =new MemoryStream(Encoding.UTF8.GetBytes(e.Result)); 
				XDocument XDoc =XDocument.Load(st);;
				this.textblock2.Text=XDoc.ToString();				
			}
		}
        //登录
		private void text2_LostFocus(object sender, System.Windows.RoutedEventArgs e)
		{
			nm=this.text2.Text.Trim();
			int nmlength=nm.Length;
			//汉字长度处理,C#中使用的unicode编码格式，默认一个汉字为一个字符。
			nmlength=getlength(nm,nmlength);
			if (nmlength<6){
			   System.Windows.MessageBox.Show("登录用户名长度不能少于6个字符！");
			   return;
			}
			//HtmlPage.Document.DocumentUri是Default.html的定位
			string uri=System.Windows.Browser.HtmlPage.Document.DocumentUri.ToString();
			//创建Uri，指向服务器端的数据流上传处理程序
			uri=uri.Substring(0,uri.LastIndexOf("/")+1)+"CheckHandler.ashx";
			UriBuilder builder=new UriBuilder(uri);
			//创建WebClient通讯对象
			WebClient client2=new WebClient();
			//通讯完成后的事件处理
			client2.UploadStringCompleted+=new UploadStringCompletedEventHandler(client2_UploadStringCompleted);
			//启动通讯
			client2.UploadStringAsync(builder.Uri,nm);	　
		}
		void client2_UploadStringCompleted(object sender,UploadStringCompletedEventArgs e){
			if (e.Result!="Yes"){
			    var k=System.Windows.MessageBox.Show("登录用户名不存在,需要修改?","确认",MessageBoxButton.OKCancel);
				if (k.ToString()=="OK")
				this.text2.Focus();
			}				
		}
    
		private void b3_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			nm=this.text2.Text.Trim();
			int nmlength=nm.Length;
			//汉字长度处理,C#中使用的unicode编码格式，默认一个汉字为一个字符。
			nmlength=getlength(nm,nmlength);
			if (nmlength<6){
			   System.Windows.MessageBox.Show("登录用户名长度不能少于6个字符！");
			   return;
			}
			pw1=this.passwordbox3.Password.Trim();
			if (pw1.Length<6){
			   System.Windows.MessageBox.Show("密码长度不能少于6个字符！");
			   return;
			}
			//HtmlPage.Document.DocumentUri是Default.html的定位
			string uri=System.Windows.Browser.HtmlPage.Document.DocumentUri.ToString();
			//创建Uri，指向服务器端的数据流上传处理程序
			uri=uri.Substring(0,uri.LastIndexOf("/")+1)+"LoginHandler.ashx";//网站发布（设置虚拟目录）也成功
			UriBuilder builder=new UriBuilder(uri);//ok
			//this.textblock1.Text = builder.Uri.ToString();
			WebClient client3=new WebClient();
			client3.UploadStringCompleted+=new　UploadStringCompletedEventHandler(client3_UploadStringCompleted);
			client3.UploadStringAsync(builder.Uri,pw1);	
		}
		void client3_UploadStringCompleted(object sender,UploadStringCompletedEventArgs e){
			
			if (e.Result!="Yes"){
			    var k=System.Windows.MessageBox.Show("密码不对,重新输入吗?","确认",MessageBoxButton.OKCancel);
				if (k.ToString()=="OK")
				this.passwordbox3.SelectAll();
				this.passwordbox3.Focus();
			}else{
				DateTime ex = DateTime.UtcNow + TimeSpan.FromDays(30);
                string cookiestring = String.Format("{0}={1};expires={2}","Login",
                                     this.text2.Text,ex.ToString("R"));
				System.Windows.Browser.HtmlPage.Document.SetProperty("cookie", cookiestring);
				System.Windows.MessageBox.Show("登录成功！");
			    this.Content=new Page1();
			}			
		}
		//汉字长度处理
        private int getlength(string nm,int nmlength){
            int i;
            char[] chars = nm.ToCharArray();
            for (i = 0; i <chars.Length; i++)
           {
            //汉字编码值大于255
			if (System.Convert.ToInt32(chars[i]) > 255)
            {
                //如果是汉字,长度+1
				nmlength++;
            }
           }
           return nmlength;
		}
		//获取Cookie
        private string GetCookieValue(){
            string[] cookieString=System.Windows.Browser.HtmlPage.Document.Cookies.Split(';');
			string[] ss;
			foreach (string ck in cookieString)
            {
			   ss = ck.Split('=');
               //System.Windows.MessageBox.Show("["+ss[0]+"]长度:"+ss[0].Length.ToString());
			   //压缩字符开始的空格,ss[0]前有个空格
			   if (ss[0].TrimStart()=="Login"){
				return ss[1];
               }
	        }
			return "";
		}
        private void b4_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	string readCookie = System.Windows.Browser.HtmlPage.Document.GetProperty("cookie") as String;
			this.textblock2.Text=readCookie;
        }
	}
}