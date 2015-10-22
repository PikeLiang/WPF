using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using System.Windows.Printing;//for PrintDocument
using System.Windows.Media.Imaging;//for BitmapImage
namespace SilverlightPrint
{
	public partial class MainPage : UserControl
	{
		PrintDocument printer;
		string prn;
		double w,h,left,top;//打印区对象尺寸和坐标
		double x,y;//记录打印对象控件坐标
		SolidColorBrush scb=new SolidColorBrush(Colors.Orange);
		string ztselected;//字体选择
		int zhselected;//字号选择
		RichTextBox newtb=new RichTextBox();//富文本框
		WriteableBitmap wb;//截取视频
		public MainPage()
		{
			InitializeComponent();
			this.ChildView.HasCloseButton=false;
			//子窗口标题栏设置，定义布局
			StackPanel titlepanel=new StackPanel();
			titlepanel.Orientation=Orientation.Horizontal;
			titlepanel.Cursor=Cursors.Hand;
			Image im=new Image();
			im.Width=20;
			im.Height=20;
			im.Source=new BitmapImage(new Uri("sucai/icon16.png", UriKind.Relative));
			titlepanel.Children.Add(im);
			TextBlock text=new TextBlock();
			text.Text="  打印预览……";
			text.Width=this.ChildView.Width;
			text.FontSize=12;
			titlepanel.Children.Add(text);
			this.ChildView.Title=titlepanel;
			//打印预览子窗口打印区设置
			this.combobox1.SelectedIndex=0;//默认字体Arial
			this.combobox2.SelectedIndex=1;//默认字号12
			this.printarea.Background=scb;
			w=this.printarea.Width;
			h=this.printarea.Height;
			left=Canvas.GetLeft(this.printarea)-7;
			top=Canvas.GetTop(this.printarea)-7;
			SetRecPosition();
			//设置打印对象
			printer = new PrintDocument();
            printer.PrintPage += new EventHandler<PrintPageEventArgs>(printer_PrintPage);
			printer.EndPrint+= new EventHandler<EndPrintEventArgs>(printer_EndPrintPage);			
		}
		//图片打印预览
		private void BTprintImage_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            prn="image";
			this.ChildView.Visibility=Visibility.Visible;
			this.printarea.Children.Clear();
        }
        //文本打印预览
	    private void BTprintText_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	prn="text";			
			//打印区控件清空
			this.ChildView.Visibility=Visibility.Visible;
			this.printarea.Children.Clear();
        }
		//截取视频图片
		private void BTprintVideo_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			wb = new WriteableBitmap(me, null);//快照截图
			prn="video";
			this.ChildView.Visibility=Visibility.Visible;
			this.printarea.Children.Clear();
		}
        //预览判断和预览
		string subs;
        private void view_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	this.printarea.Children.Clear();
			this.rec1.Visibility=Visibility.Visible;
			this.rec2.Visibility=Visibility.Visible;
			this.rec3.Visibility=Visibility.Visible;
			this.rec4.Visibility=Visibility.Visible;
			newtb.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
			newtb.BorderThickness=new Thickness(1,1,1,1);
			this.printarea.Background=scb;		
			switch (prn){
				case "image":
				  this.printarea.Children.Add(new Image(){Source=new BitmapImage(new Uri("sucai/im2.jpg", UriKind.Relative)),
				     Stretch=Stretch.Uniform});
				  break;
				case "video":
				  this.printarea.Children.Add(new Image(){Source=wb,
				     Stretch=Stretch.Uniform});
				  break;
				case "text":
					//字号
				   zhselected=Convert.ToInt32(this.combobox2.SelectionBoxItem.ToString());
					//字体
				   ztselected=this.combobox1.SelectionBoxItem.ToString();
				   newtb.Blocks.Clear();
			       newtb.Width=this.printarea.Width;
			       newtb.Height=this.printarea.Height;
			       Run run1 = new Run();
			       run1.FontSize=zhselected;
			       run1.FontFamily=new System.Windows.Media.FontFamily(ztselected);
                   run1.Text=this.textblock1.Text;
			       Paragraph paragraph = new Paragraph();
                   paragraph.Inlines.Add(run1);
			       newtb.Blocks.Add(paragraph);
			       this.printarea.Children.Add(newtb); 
				   break;
		    }
        }
		
		//启动打印
        private void print_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	//显示打印选择对话框,选择打印机，确认打印
			this.rec1.Visibility=Visibility.Collapsed;
			this.rec2.Visibility=Visibility.Collapsed;
			this.rec3.Visibility=Visibility.Collapsed;
			this.rec4.Visibility=Visibility.Collapsed;
			newtb.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
			newtb.BorderThickness=new Thickness(0,0,0,0);
			this.printarea.Background=null;
			printer.Print("打印……");
        }
		//开始打印
		void printer_PrintPage(object sender, PrintPageEventArgs e)
        {
		    e.PageVisual =canvasprint;
        }
		//结束打印(送出打印最后1页信息)
		void printer_EndPrintPage(object sender, EndPrintEventArgs e)
        {
		    this.rec1.Visibility=Visibility.Visible;
			this.rec2.Visibility=Visibility.Visible;
			this.rec3.Visibility=Visibility.Visible;
			this.rec4.Visibility=Visibility.Visible;
			newtb.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
			newtb.BorderThickness=new Thickness(1,1,1,1);
			this.printarea.Background=scb;
			//if (e.Error!=null){
			//	System.Windows.MessageBox.Show("打印出错："+e.Error.Message);
			//}
        }
		//退出预览窗口
        private void exit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	this.ChildView.Visibility=Visibility.Collapsed;
        }

        private void tb1_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        	this.Content=new Page1();
        }
        Point curpos;//记忆鼠标位置
		bool mousemoving=false;	//拖动判断
		//移动标rec1按下
        private void rec1_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        	curpos=e.GetPosition(null);//获取鼠标当前坐标
			mousemoving=true;//允许拖动
			this.rec1.CaptureMouse();//鼠标捕获允许
        }
       //移动标rec1移动,移动打印对象
        private void rec1_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
        	if (mousemoving){
				double addX=e.GetPosition(null).X-curpos.X;//鼠标X方向增加值
				double addY=e.GetPosition(null).Y-curpos.Y;//鼠标Y方向增加值
				Canvas.SetLeft(printarea,addX+Canvas.GetLeft(printarea));//对象新位置坐标
				Canvas.SetTop(printarea,addY+Canvas.GetTop(printarea));
				left=Canvas.GetLeft(this.printarea)-7;
			    top=Canvas.GetTop(this.printarea)-7;
				SetRecPosition();
				curpos=e.GetPosition(null);	
			}
        }
        //移动标rec1停止
        private void rec1_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        	mousemoving=false;//禁止拖动		
			this.rec1.ReleaseMouseCapture();//停止捕获			
        }
		//移动标rec2按下
		private void rec2_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			curpos=e.GetPosition(null);//获取鼠标当前坐标
			y=Canvas.GetTop(printarea);
			top=y-7;
			mousemoving=true;//允许拖动
			this.rec2.CaptureMouse();//鼠标捕获允许
		}
        //移动标rec2移动改变打印区宽度
		private void rec2_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (mousemoving){
				double addX=e.GetPosition(null).X-curpos.X;//鼠标X方向增加值
				//double addY=e.GetPosition(null).Y-curpos.Y;//鼠标Y方向增加值
				Canvas.SetLeft(printarea,addX+Canvas.GetLeft(printarea));//对象新位置坐标
				Canvas.SetTop(printarea,y);
				this.printarea.Width=this.printarea.Width-addX;
				this.printarea.Background=scb;
				left=Canvas.GetLeft(this.printarea)-7;				
				w=this.printarea.Width;
				SetRecPosition();
				newtb.Width=w;
				curpos=e.GetPosition(null);	
			}
		}
        //移动标rec2移动停止
		private void rec2_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			mousemoving=false;//禁止拖动		
			this.rec2.ReleaseMouseCapture();//停止捕获
		}
        //移动标rec4按下
		private void rec4_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			curpos=e.GetPosition(null);//获取鼠标当前坐标
			y=Canvas.GetTop(printarea);
			x=Canvas.GetLeft(this.printarea);
			mousemoving=true;//允许拖动
			this.rec4.CaptureMouse();//鼠标捕获允许
		}
        //移动标rec4移动改变打印区宽度
		private void rec4_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (mousemoving){
				double addX=e.GetPosition(null).X-curpos.X;//鼠标X方向增加值
				//double addY=e.GetPosition(null).Y-curpos.Y;//鼠标Y方向增加值
				Canvas.SetLeft(printarea,x);//对象新位置坐标
				Canvas.SetTop(printarea,y);
				this.printarea.Width=this.printarea.Width+addX;
				this.printarea.Background=scb;
				left=x-7;
				top=y-7;
				w=this.printarea.Width;
				SetRecPosition();
				newtb.Width=w;
				curpos=e.GetPosition(null);	
		    }
        }
		private void rec4_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			mousemoving=false;//禁止拖动		
			this.rec4.ReleaseMouseCapture();//停止捕获
		}

		private void rec3_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			curpos=e.GetPosition(null);//获取鼠标当前坐标
			x=Canvas.GetLeft(printarea);
			y=Canvas.GetTop(printarea);
			top=y-7;
			w=this.printarea.Width;
			mousemoving=true;//允许拖动
			this.rec3.CaptureMouse();//鼠标捕获允许
		}
         //移动标rec3移动改变打印区高度
		private void rec3_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (mousemoving){
				//double addX=e.GetPosition(null).X-curpos.X;//鼠标X方向增加值
				double addY=e.GetPosition(null).Y-curpos.Y;//鼠标Y方向增加值
				Canvas.SetLeft(printarea,x);//对象新位置坐标
				Canvas.SetTop(printarea,y);
				//this.printarea.Width=this.printarea.Width-addX;
				this.printarea.Height=this.printarea.Height+addY;
				this.printarea.Background=scb;
				left=Canvas.GetLeft(this.printarea)-7;				
				h=this.printarea.Height;
				SetRecPosition();
				newtb.Height=h;
				curpos=e.GetPosition(null);	
			}
		}
         //移动标rec3移动停止
		private void rec3_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			mousemoving=false;//禁止拖动		
			this.rec3.ReleaseMouseCapture();//停止捕获
		}
        //设置移动标位置
		private void SetRecPosition(){
		    Canvas.SetLeft(rec1,left+w/2);
			Canvas.SetTop(rec1,top);
			Canvas.SetLeft(rec2,left);
			Canvas.SetTop(rec2,top+h/2);
			Canvas.SetLeft(rec3,left+w/2);
			Canvas.SetTop(rec3,top+h);
			Canvas.SetLeft(rec4,left+w);
			Canvas.SetTop(rec4,top+h/2);
		}
        //视频再启动播放
		private void me_MediaEnded(object sender, System.Windows.RoutedEventArgs e)
		{
			this.me.Stop();
			this.me.Play();
		}

	}
}