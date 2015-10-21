using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;

namespace SilverlightChildWindow
{
    public partial class Page1 : Page
    {
		public Page1()
        {
            InitializeComponent();			   
        }
        // 当用户导航到此网页时执行。
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
        //返回首页MainPage
        private void tb1_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        	this.Content=new MainPage();
        }
		//“登录”
        private void tb2_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        	//"登录"使用被禁止
			this.tb2.IsHitTestVisible=false;
			ChildWindow1 mycw=new ChildWindow1();
			mycw.Show();	
			//设置子窗口关闭事件代码
			mycw.Closed += new EventHandler(mycw_Closed);  	
        }
		//子窗口登录关闭事件
        private void mycw_Closed(object sender, EventArgs e)   
        {   
			ChildWindow1 cwsender = (ChildWindow1)sender;
    		if ((bool)cwsender.DialogResult)   
            {   
                this.textblock1.Text = "用户“"+cwsender.textbox1.Text.ToString()+"”登录成功！"; 
				this.image.Visibility=Visibility.Visible;
                this.Storyboard1.Begin();   
            }   
            else   
            {   
                System.Windows.MessageBox.Show("取消登录！");
				this.tb2.IsHitTestVisible=true;
            }   
        }   
    }
}
