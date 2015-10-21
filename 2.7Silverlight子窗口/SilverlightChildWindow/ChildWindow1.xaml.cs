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

using System.Windows.Media.Imaging;//for BitmapImage
namespace SilverlightChildWindow
{
    public partial class ChildWindow1 : ChildWindow
    {
        public ChildWindow1()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
			if (this.textbox1.Text.Trim()==""||this.pwbox.Password.Trim()==""){
			   ShowChildWindow();
				return;
			}
  			this.DialogResult = true;//成功返回，关闭
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;//取消返回，关闭
        }
        //
		private void childWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
        	this.Storyboard1.Begin();
        }
		//子窗口定义
		private void ShowChildWindow(){
		    ChildWindow cwshow=new ChildWindow();
            Image im=new Image();
			im.Width=20;
			im.Height=20;
			im.Source=new BitmapImage(new Uri("sucai/jlb.png", UriKind.Relative));
			TextBlock text=new TextBlock();
			text.Text="提示";
			StackPanel titlepanel = new StackPanel();
            titlepanel.Orientation = Orientation.Horizontal;
            titlepanel.Children.Add(im);
            titlepanel.Children.Add(text);
			cwshow.Title=titlepanel;
			cwshow.Content="用户名或密码不能为空！"+"\r\n"+"请核查输入。";
			cwshow.FontSize=24;
			cwshow.Width=300;
			cwshow.Height=150;
			cwshow.Cursor=Cursors.Hand;
			cwshow.Show();
		}
    }
}

