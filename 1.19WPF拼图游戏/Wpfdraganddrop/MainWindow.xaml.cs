using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Windows.Media.Animation;
namespace Wpfdraganddrop
{
	public partial class MainWindow : Window
	{
		Point curpos;//记忆鼠标位置
		bool mousemoving=false;	//拖动判断
		//图片元素拖动允许
		bool p1moving=true,p2moving=true,p3moving=true,p4moving=true;
		bool p5moving=true,p6moving=true,p7moving=true,p8moving=true,p9moving=true;
		//图片元素目标坐标和原位置坐标变量
		double canvas1Left,canvas1Top,p1Left,p1Top,canvas2Left,canvas2Top,p2Left,p2Top;
		double canvas3Left,canvas3Top,p3Left,p3Top,canvas4Left,canvas4Top,p4Left,p4Top;
		double canvas5Left,canvas5Top,p5Left,p5Top,canvas6Left,canvas6Top,p6Left,p6Top;
		double canvas7Left,canvas7Top,p7Left,p7Top,canvas8Left,canvas8Top,p8Left,p8Top;
		double canvas9Left,canvas9Top,p9Left,p9Top;
		DoubleAnimation da=null;//定义动画用于图片元素返回原位置
		public MainWindow()
		{
			this.InitializeComponent();
            canvas1Left=Canvas.GetLeft(canvas1);//图片元素目标位置坐标
			canvas1Top=Canvas.GetTop(canvas1);
			p1Left=Canvas.GetLeft(p1);//图片元素当前位置坐标
			p1Top=Canvas.GetTop(p1);
			canvas2Left=Canvas.GetLeft(canvas2);
			canvas2Top=Canvas.GetTop(canvas2);
			p2Left=Canvas.GetLeft(p2);
			p2Top=Canvas.GetTop(p2);
			canvas3Left=Canvas.GetLeft(canvas3);
			canvas3Top=Canvas.GetTop(canvas3);
			p3Left=Canvas.GetLeft(p3);
			p3Top=Canvas.GetTop(p3);
			canvas4Left=Canvas.GetLeft(canvas4);
			canvas4Top=Canvas.GetTop(canvas4);
			p4Left=Canvas.GetLeft(p4);
			p4Top=Canvas.GetTop(p4);
			canvas5Left=Canvas.GetLeft(canvas5);
			canvas5Top=Canvas.GetTop(canvas5);
			p5Left=Canvas.GetLeft(p5);
			p5Top=Canvas.GetTop(p5);
			canvas6Left=Canvas.GetLeft(canvas6);
			canvas6Top=Canvas.GetTop(canvas6);
			p6Left=Canvas.GetLeft(p6);
			p6Top=Canvas.GetTop(p6);
			canvas7Left=Canvas.GetLeft(canvas7);
			canvas7Top=Canvas.GetTop(canvas7);
			p7Left=Canvas.GetLeft(p7);
			p7Top=Canvas.GetTop(p7);
			canvas8Left=Canvas.GetLeft(canvas8);
			canvas8Top=Canvas.GetTop(canvas8);
			p8Left=Canvas.GetLeft(p8);
			p8Top=Canvas.GetTop(p8);
			canvas9Left=Canvas.GetLeft(canvas9);
			canvas9Top=Canvas.GetTop(canvas9);
			p9Left=Canvas.GetLeft(p9);
			p9Top=Canvas.GetTop(p9);
		}

		private void p1_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			curpos=e.GetPosition(null);//获取鼠标当前坐标
			mousemoving=true;//允许拖动
			this.p1.CaptureMouse();//鼠标捕获允许
		}

		private void p1_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (mousemoving && p1moving){
				double addX=e.GetPosition(null).X-curpos.X;//鼠标X方向增加值
				double addY=e.GetPosition(null).Y-curpos.Y;//鼠标Y方向增加值
				Canvas.SetLeft(p1,addX+Canvas.GetLeft(p1));//对象新位置坐标
				Canvas.SetTop(p1,addY+Canvas.GetTop(p1));
				curpos=e.GetPosition(null);	
			}
		}

		private void p1_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			mousemoving=false;//禁止拖动		
			this.p1.ReleaseMouseCapture();//停止捕获
			if (Math.Abs(Canvas.GetLeft(p1)-canvas1Left)<10 && Math.Abs(Canvas.GetTop(p1)-canvas1Top)<10){
				p1moving=false;
				Canvas.SetLeft(p1,canvas1Left);
				Canvas.SetTop(p1,canvas1Top);
			}else{
                da= new DoubleAnimation();
				da.Completed+=new EventHandler(da1Completed);
				da.From=Canvas.GetLeft(p1);
			    da.To=p1Left;
			    da.Duration=TimeSpan.FromMilliseconds(1000);
				p1.BeginAnimation(Canvas.LeftProperty, da);
				da.From=Canvas.GetTop(p1);
			    da.To=p1Top;
			    da.Duration=TimeSpan.FromMilliseconds(1000);
				p1.BeginAnimation(Canvas.TopProperty, da);	
			}
		}
				
		void da1Completed(object sender, EventArgs e){
			    p1.BeginAnimation(Canvas.LeftProperty, null);
			    p1.BeginAnimation(Canvas.TopProperty, null);
			    Canvas.SetLeft(p1,p1Left);
			    Canvas.SetTop(p1,p1Top);
		}

		private void p2_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			curpos=e.GetPosition(null);
			mousemoving=true;
			this.p2.CaptureMouse();
		}

		private void p2_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (mousemoving && p2moving){
				double addX=e.GetPosition(null).X-curpos.X;
				double addY=e.GetPosition(null).Y-curpos.Y;
				Canvas.SetLeft(p2,addX+Canvas.GetLeft(p2));
				Canvas.SetTop(p2,addY+Canvas.GetTop(p2));
				curpos=e.GetPosition(null);
			}
		}

		private void p2_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			mousemoving=false;
			this.p2.ReleaseMouseCapture();
			if (Math.Abs(Canvas.GetLeft(p2)-canvas2Left)<10 && Math.Abs(Canvas.GetTop(p2)-canvas2Top)<10){
				p2moving=false;
				Canvas.SetLeft(p2,canvas2Left);
				Canvas.SetTop(p2,canvas2Top);
			}else{
                da= new DoubleAnimation();
				da.Completed+=new EventHandler(da2Completed);
				da.From=Canvas.GetLeft(p2);
			    da.To=p2Left;
			    da.Duration=TimeSpan.FromMilliseconds(1000);
				p2.BeginAnimation(Canvas.LeftProperty, da);
				da.From=Canvas.GetTop(p2);
			    da.To=p2Top;
			    da.Duration=TimeSpan.FromMilliseconds(1000);
				p2.BeginAnimation(Canvas.TopProperty, da);	
			}
		}
		void da2Completed(object sender, EventArgs e){
			    p2.BeginAnimation(Canvas.LeftProperty, null);
			    p2.BeginAnimation(Canvas.TopProperty, null);
			    Canvas.SetLeft(p2,p2Left);
			    Canvas.SetTop(p2,p2Top);
		}

		private void p3_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			curpos=e.GetPosition(null);
			mousemoving=true;
			this.p3.CaptureMouse();
		}

		private void p3_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (mousemoving && p3moving){
				double addX=e.GetPosition(null).X-curpos.X;
				double addY=e.GetPosition(null).Y-curpos.Y;
				Canvas.SetLeft(p3,addX+Canvas.GetLeft(p3));
				Canvas.SetTop(p3,addY+Canvas.GetTop(p3));
				curpos=e.GetPosition(null);
			}
		}

		private void p3_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			mousemoving=false;
			this.p3.ReleaseMouseCapture();
			if (Math.Abs(Canvas.GetLeft(p3)-canvas3Left)<10 && Math.Abs(Canvas.GetTop(p3)-canvas3Top)<10){
				p3moving=false;
				Canvas.SetLeft(p3,canvas3Left);
				Canvas.SetTop(p3,canvas3Top);
			}else{
                da= new DoubleAnimation();
				da.Completed+=new EventHandler(da3Completed);
				da.From=Canvas.GetLeft(p3);
			    da.To=p3Left;
			    da.Duration=TimeSpan.FromMilliseconds(1000);
				p3.BeginAnimation(Canvas.LeftProperty, da);
				da.From=Canvas.GetTop(p3);
			    da.To=p3Top;
			    da.Duration=TimeSpan.FromMilliseconds(1000);
				p3.BeginAnimation(Canvas.TopProperty, da);	
			}
		}
		void da3Completed(object sender, EventArgs e){
			    p3.BeginAnimation(Canvas.LeftProperty, null);
			    p3.BeginAnimation(Canvas.TopProperty, null);
			    Canvas.SetLeft(p3,p3Left);
			    Canvas.SetTop(p3,p3Top);
		}
		private void p4_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			curpos=e.GetPosition(null);
			mousemoving=true;
			this.p4.CaptureMouse();
		}

		private void p4_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (mousemoving && p4moving){
				double addX=e.GetPosition(null).X-curpos.X;
				double addY=e.GetPosition(null).Y-curpos.Y;
				Canvas.SetLeft(p4,addX+Canvas.GetLeft(p4));
				Canvas.SetTop(p4,addY+Canvas.GetTop(p4));
				curpos=e.GetPosition(null);
			}
		}

		private void p4_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			mousemoving=false;
			this.p4.ReleaseMouseCapture();
			if (Math.Abs(Canvas.GetLeft(p4)-canvas4Left)<10 && Math.Abs(Canvas.GetTop(p4)-canvas4Top)<10){
				p4moving=false;
				Canvas.SetLeft(p4,canvas4Left);
				Canvas.SetTop(p4,canvas4Top);
			}else{
                da= new DoubleAnimation();
				da.Completed+=new EventHandler(da4Completed);
				da.From=Canvas.GetLeft(p4);
			    da.To=p4Left;
			    da.Duration=TimeSpan.FromMilliseconds(1000);
				p4.BeginAnimation(Canvas.LeftProperty, da);
				da.From=Canvas.GetTop(p4);
			    da.To=p4Top;
			    da.Duration=TimeSpan.FromMilliseconds(1000);
				p4.BeginAnimation(Canvas.TopProperty, da);	
			}
		}
		void da4Completed(object sender, EventArgs e){
			    p4.BeginAnimation(Canvas.LeftProperty, null);
			    p4.BeginAnimation(Canvas.TopProperty, null);
			    Canvas.SetLeft(p4,p4Left);
			    Canvas.SetTop(p4,p4Top);
		}
		private void p5_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			curpos=e.GetPosition(null);
			mousemoving=true;
			this.p5.CaptureMouse();
		}

		private void p5_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (mousemoving && p5moving){
				double addX=e.GetPosition(null).X-curpos.X;
				double addY=e.GetPosition(null).Y-curpos.Y;
				Canvas.SetLeft(p5,addX+Canvas.GetLeft(p5));
				Canvas.SetTop(p5,addY+Canvas.GetTop(p5));
				curpos=e.GetPosition(null);
			}
		}

		private void p5_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			mousemoving=false;
			this.p5.ReleaseMouseCapture();
			if (Math.Abs(Canvas.GetLeft(p5)-canvas5Left)<10 && Math.Abs(Canvas.GetTop(p5)-canvas5Top)<10){
				p5moving=false;
				Canvas.SetLeft(p5,canvas5Left);
				Canvas.SetTop(p5,canvas5Top);
			}else{
                da= new DoubleAnimation();
				da.Completed+=new EventHandler(da5Completed);
				da.From=Canvas.GetLeft(p5);
			    da.To=p5Left;
			    da.Duration=TimeSpan.FromMilliseconds(1000);
				p5.BeginAnimation(Canvas.LeftProperty, da);
				da.From=Canvas.GetTop(p5);
			    da.To=p5Top;
			    da.Duration=TimeSpan.FromMilliseconds(1000);
				p5.BeginAnimation(Canvas.TopProperty, da);	
			}
		}
		void da5Completed(object sender, EventArgs e){
			    p5.BeginAnimation(Canvas.LeftProperty, null);
			    p5.BeginAnimation(Canvas.TopProperty, null);
			    Canvas.SetLeft(p5,p5Left);
			    Canvas.SetTop(p5,p5Top);
		}
	private void p6_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			curpos=e.GetPosition(null);
			mousemoving=true;
			this.p6.CaptureMouse();
		}

		private void p6_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (mousemoving && p6moving){
				double addX=e.GetPosition(null).X-curpos.X;
				double addY=e.GetPosition(null).Y-curpos.Y;
				Canvas.SetLeft(p6,addX+Canvas.GetLeft(p6));
				Canvas.SetTop(p6,addY+Canvas.GetTop(p6));
				curpos=e.GetPosition(null);
			}
		}

		private void p6_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			mousemoving=false;
			this.p6.ReleaseMouseCapture();
			if (Math.Abs(Canvas.GetLeft(p6)-canvas6Left)<10 && Math.Abs(Canvas.GetTop(p6)-canvas6Top)<10){
				p6moving=false;
				Canvas.SetLeft(p6,canvas6Left);
				Canvas.SetTop(p6,canvas6Top);
			}else{
                da= new DoubleAnimation();
				da.Completed+=new EventHandler(da6Completed);
				da.From=Canvas.GetLeft(p6);
			    da.To=p6Left;
			    da.Duration=TimeSpan.FromMilliseconds(1000);
				p6.BeginAnimation(Canvas.LeftProperty, da);
				da.From=Canvas.GetTop(p6);
			    da.To=p6Top;
			    da.Duration=TimeSpan.FromMilliseconds(1000);
				p6.BeginAnimation(Canvas.TopProperty, da);	
			}
		}
		void da6Completed(object sender, EventArgs e){
			    p6.BeginAnimation(Canvas.LeftProperty, null);
			    p6.BeginAnimation(Canvas.TopProperty, null);
			    Canvas.SetLeft(p6,p6Left);
			    Canvas.SetTop(p6,p6Top);
		}
	   private void p7_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			curpos=e.GetPosition(null);
			mousemoving=true;
			this.p7.CaptureMouse();
		}

		private void p7_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (mousemoving && p7moving){
				double addX=e.GetPosition(null).X-curpos.X;
				double addY=e.GetPosition(null).Y-curpos.Y;
				Canvas.SetLeft(p7,addX+Canvas.GetLeft(p7));
				Canvas.SetTop(p7,addY+Canvas.GetTop(p7));
				curpos=e.GetPosition(null);
			}
		}

		private void p7_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			mousemoving=false;
			this.p7.ReleaseMouseCapture();
			if (Math.Abs(Canvas.GetLeft(p7)-canvas7Left)<10 && Math.Abs(Canvas.GetTop(p7)-canvas7Top)<10){
				p7moving=false;
				Canvas.SetLeft(p7,canvas7Left);
				Canvas.SetTop(p7,canvas7Top);
			}else{
                da= new DoubleAnimation();
				da.Completed+=new EventHandler(da7Completed);
				da.From=Canvas.GetLeft(p7);
			    da.To=p7Left;
			    da.Duration=TimeSpan.FromMilliseconds(1000);
				p7.BeginAnimation(Canvas.LeftProperty, da);
				da.From=Canvas.GetTop(p7);
			    da.To=p7Top;
			    da.Duration=TimeSpan.FromMilliseconds(1000);
				p7.BeginAnimation(Canvas.TopProperty, da);	
			}
		}
		void da7Completed(object sender, EventArgs e){
			    p7.BeginAnimation(Canvas.LeftProperty, null);
			    p7.BeginAnimation(Canvas.TopProperty, null);
			    Canvas.SetLeft(p7,p7Left);
			    Canvas.SetTop(p7,p7Top);
		}
	  private void p8_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			curpos=e.GetPosition(null);
			mousemoving=true;
			this.p8.CaptureMouse();
		}

		private void p8_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (mousemoving && p8moving){
				double addX=e.GetPosition(null).X-curpos.X;
				double addY=e.GetPosition(null).Y-curpos.Y;
				Canvas.SetLeft(p8,addX+Canvas.GetLeft(p8));
				Canvas.SetTop(p8,addY+Canvas.GetTop(p8));
				curpos=e.GetPosition(null);
			}
		}

		private void p8_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			mousemoving=false;
			this.p8.ReleaseMouseCapture();
			if (Math.Abs(Canvas.GetLeft(p8)-canvas8Left)<10 && Math.Abs(Canvas.GetTop(p8)-canvas8Top)<10){
				p8moving=false;
				Canvas.SetLeft(p8,canvas8Left);
				Canvas.SetTop(p8,canvas8Top);
			}else{
                da= new DoubleAnimation();
				da.Completed+=new EventHandler(da8Completed);
				da.From=Canvas.GetLeft(p8);
			    da.To=p8Left;
			    da.Duration=TimeSpan.FromMilliseconds(1000);
				p8.BeginAnimation(Canvas.LeftProperty, da);
				da.From=Canvas.GetTop(p8);
			    da.To=p8Top;
			    da.Duration=TimeSpan.FromMilliseconds(1000);
				p8.BeginAnimation(Canvas.TopProperty, da);	
			}
		}
		void da8Completed(object sender, EventArgs e){
			    p8.BeginAnimation(Canvas.LeftProperty, null);
			    p8.BeginAnimation(Canvas.TopProperty, null);
			    Canvas.SetLeft(p8,p8Left);
			    Canvas.SetTop(p8,p8Top);
		}
	private void p9_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			curpos=e.GetPosition(null);
			mousemoving=true;
			this.p9.CaptureMouse();
		}

		private void p9_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (mousemoving && p9moving){
				double addX=e.GetPosition(null).X-curpos.X;
				double addY=e.GetPosition(null).Y-curpos.Y;
				Canvas.SetLeft(p9,addX+Canvas.GetLeft(p9));
				Canvas.SetTop(p9,addY+Canvas.GetTop(p9));
				curpos=e.GetPosition(null);
			}
		}

		private void p9_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			mousemoving=false;
			this.p9.ReleaseMouseCapture();
			if (Math.Abs(Canvas.GetLeft(p9)-canvas9Left)<10 && Math.Abs(Canvas.GetTop(p9)-canvas9Top)<10){
				p9moving=false;
				Canvas.SetLeft(p9,canvas9Left);
				Canvas.SetTop(p9,canvas9Top);
			}else{
                da= new DoubleAnimation();
				da.Completed+=new EventHandler(da9Completed);
				da.From=Canvas.GetLeft(p9);
			    da.To=p9Left;
			    da.Duration=TimeSpan.FromMilliseconds(1000);
				p9.BeginAnimation(Canvas.LeftProperty, da);
				da.From=Canvas.GetTop(p9);
			    da.To=p9Top;
			    da.Duration=TimeSpan.FromMilliseconds(1000);
				p9.BeginAnimation(Canvas.TopProperty, da);	
			}
		}
		void da9Completed(object sender, EventArgs e){
			    p9.BeginAnimation(Canvas.LeftProperty, null);
			    p9.BeginAnimation(Canvas.TopProperty, null);
			    Canvas.SetLeft(p9,p9Left);
			    Canvas.SetTop(p9,p9Top);
		}
	}
}