using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using System.Windows.Media.Imaging;//for BitmapImage
namespace SilverlightChildWindow
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			this.childw.HasCloseButton=false;
			this.childw.Title="用户登录";
			this.childw.Visibility=Visibility.Collapsed;
		}

		private void tb1_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{ 
		    this.childw.Visibility=Visibility.Visible;
			this.Storyboard1.Begin();
			//“登录”不能使用
			this.tb1.IsHitTestVisible=false;
		}

		private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (this.textbox1.Text.Trim()==""||this.pwbox.Password.Trim()==""){
				ShowChildWindow();
				return;
			}
			this.childw.Visibility=Visibility.Collapsed;
			this.textblock1.Text="用户“"+this.textbox1.Text.Trim()+"”登录成功！";
			this.image1.Visibility=Visibility.Visible;
			this.Storyboard2.Begin();
		}

		private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.childw.Visibility=Visibility.Collapsed;
			System.Windows.MessageBox.Show("取消登录！");
			//恢复使用
			this.tb1.IsHitTestVisible=true;
		}

		private void tb2_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			this.Content=new Page1();
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