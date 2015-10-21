using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using System.Windows.Browser;//for HtmlPage
using System.ServiceModel;//for BasicHttpBinding
namespace SilverlightLoginSQL
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			// 为初始化变量所必需
			InitializeComponent();
            this.text2.Text = GetCookieValue();
		}
		string nm,pw1;
  		//浏览服务器端SQL数据
		private void b2_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Uri uri = new Uri(Application.Current.Host.Source, "Service.svc");
            BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            ServiceReference1.ServiceClient client = new ServiceReference1.ServiceClient(binding, new EndpointAddress(uri));
			client.GetDataCompleted += (ss, se) =>
                {
                    if (se.Error == null)
                    {
                        this.datagrid.ItemsSource = se.Result;
                        this.textblock1.Text ="通讯成功！";
                    }
                    else
                    {
                        HtmlPage.Window.Alert("WCF通讯出错！");
                        this.textblock1.Text = se.Error.Message;
                        return;
                      }
                };
            client.GetDataAsync();
            client.CloseAsync();
		}
        //验证用户名是否存在
		private void text1_LostFocus(object sender, System.Windows.RoutedEventArgs e)
		{
			nm=this.text1.Text.Trim();
			int nmlength=nm.Length;
			//汉字长度处理
			nmlength=getlength(nm,nmlength);
			if (nmlength<6){
			   System.Windows.MessageBox.Show("注册用户名长度不能少于6个字符！");
			   return;
			}
			Uri uri = new Uri(Application.Current.Host.Source, "Service.svc");
            BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            ServiceReference1.ServiceClient client = new ServiceReference1.ServiceClient(binding, new EndpointAddress(uri));
			client.CheckNameCompleted += (ss, se) =>
                {
                    if (se.Error == null)
                    {
                        this.textblock1.Text ="通讯成功！";
						if (se.Result=="Yes"){
			               var k=System.Windows.MessageBox.Show("注册用户名已经存在,请重新选择!","确认",MessageBoxButton.OKCancel);	
			               if (k.ToString()=="OK")
				           this.text1.Focus();
			               }                        
                    }
                    else
                    {
                        HtmlPage.Window.Alert("WCF通讯出错！");
                        this.textblock1.Text = se.Error.Message;
                        return;
                      }
                };
            client.CheckNameAsync(nm);
            client.CloseAsync();
		}
		//注册新用户
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
			Uri uri = new Uri(Application.Current.Host.Source, "Service.svc");
            BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            ServiceReference1.ServiceClient client = new ServiceReference1.ServiceClient(binding, new EndpointAddress(uri));
			client.TianjiaCompleted += (ss, se) =>
                {
                    if (se.Error == null)
                    {
                        this.textblock1.Text ="通讯成功！";
						HtmlPage.Window.Alert(se.Result);			                               
                    }
                    else
                    {
                        HtmlPage.Window.Alert("WCF通讯出错！");
                        this.textblock1.Text = se.Error.Message;
                        return;
                      }
                };
            client.TianjiaAsync(nm,pw1);
            client.CloseAsync();
        }
		//登录处理
		private void text2_LostFocus(object sender, RoutedEventArgs e)
        {
            nm=this.text2.Text.Trim();
			int nmlength=nm.Length;
			//汉字长度处理,C#中使用的unicode编码格式，默认一个汉字为一个字符。
			nmlength=getlength(nm,nmlength);
			if (nmlength<6){
			   System.Windows.MessageBox.Show("登录用户名长度不能少于6个字符！");
			   return;
			}
			Uri uri = new Uri(Application.Current.Host.Source, "Service.svc");
            BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            ServiceReference1.ServiceClient client = new ServiceReference1.ServiceClient(binding, new EndpointAddress(uri));
			client.CheckNameCompleted += (ss, se) =>
                {
                    if (se.Error == null)
                    {
                        this.textblock1.Text ="通讯成功！";
						if (se.Result=="No"){
			               var k=System.Windows.MessageBox.Show("登录用户名不存在,重新输入?","确认",MessageBoxButton.OKCancel);	
			               if (k.ToString()=="OK")
				           this.text2.Focus();
			               }                        
                    }
                    else
                    {
                        HtmlPage.Window.Alert("WCF通讯出错！");
                        this.textblock1.Text = se.Error.Message;
                        return;
                      }
                };
            client.CheckNameAsync(nm);
            client.CloseAsync();
        }
		//登录按钮
		private void b3_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	nm=this.text2.Text.Trim();
			int nmlength=nm.Length;
			//汉字长度处理
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
			Uri uri = new Uri(Application.Current.Host.Source, "Service.svc");
            BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            ServiceReference1.ServiceClient client = new ServiceReference1.ServiceClient(binding, new EndpointAddress(uri));
			client.CheckPasswordCompleted += (ss, se) =>
                {
                    if (se.Error == null)
                    {
                        this.textblock1.Text ="通讯成功！";
						if (se.Result=="No"){
			               var k=System.Windows.MessageBox.Show("密码出错,重新输入吗？","确认",MessageBoxButton.OKCancel);	
			               if (k.ToString()=="OK"){
							  this.passwordbox3.SelectAll();
				              this.passwordbox3.Focus();
						    }
			            }else{
						    DateTime ex = DateTime.UtcNow + TimeSpan.FromDays(30);
                            string cookiestring = String.Format("{0}={1};expires={2}","Login",
                                                  this.text2.Text,ex.ToString("R"));
				            System.Windows.Browser.HtmlPage.Document.SetProperty("cookie", cookiestring);
				            System.Windows.MessageBox.Show("登录成功！");
			                this.Content=new Page1();
						}                        
                    }
                    else
                    {
                        HtmlPage.Window.Alert("WCF通讯出错！");
                        this.textblock1.Text = se.Error.Message;
                        return;
                      }
                };
            client.CheckPasswordAsync(pw1);
            client.CloseAsync();
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