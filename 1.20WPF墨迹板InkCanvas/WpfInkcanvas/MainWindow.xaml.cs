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

using System.Windows.Ink; //for DrawingAttributes
using Microsoft.Win32;    //for SaveFileDialog/OpenFileDialog
using System.IO;  //for FileStream
using System.Reflection; //for  PropertyInfo
using System.Windows.Threading;//for timer
namespace WpfInkcanvas
{	
	public partial class MainWindow : Window
	{
		Brush brush;
		byte red=0,green=0,blue=0;
		DrawingAttributes DAink = new DrawingAttributes();
		public MainWindow()
		{
			this.InitializeComponent();
			brush = new SolidColorBrush(Color.FromArgb(255,red,green,blue));//alpha、红、绿、蓝分量
			this.rect_color.Fill=brush;//颜色标记
			DAink.Color=Color.FromArgb(255,red,green,blue);//默认笔迹颜色：黑色
			this.inkcanvas.DefaultDrawingAttributes=DAink;//赋予笔迹板			
		}
		 //鼠标移动选择笔迹大小
		private void pen_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			//Point curpos=e.GetPosition(null);//鼠标坐标
			Ellipse pen=sender as Ellipse;//事件对象作为椭圆
			//黄色矩形位置
			Canvas.SetLeft(this.rectangle1,Canvas.GetLeft(pen)-(this.rectangle1.Width/2-pen.Width/2));
			Canvas.SetTop(this.rectangle1,Canvas.GetTop(pen)-(this.rectangle1.Height/2-pen.Height/2));
		}
        //笔迹大小选择确认******************************		
		private void pen_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Ellipse pen=sender as Ellipse;//鼠标点击的对象
			DAink.Width=pen.Width;
			DAink.Height=DAink.Width;
			this.inkcanvas.DefaultDrawingAttributes=DAink; //墨迹板笔迹属性设置
			Canvas.SetLeft(this.rectangle2,Canvas.GetLeft(this.rectangle1));
			Canvas.SetTop(this.rectangle2,Canvas.GetTop(this.rectangle1));
		}       
		//笔迹颜色选择******************************        
		private void color_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
		{
			Slider colorvalue=sender as Slider;
			byte cv=(byte)colorvalue.Value;
			switch (colorvalue.Name){
				case "Red_slider":
				  red=cv;
				  break;
				case "Green_slider":
                  green=cv;
				  break;
				case "Blue_slider":
                  blue=cv;
				  break;
			}
			brush = new SolidColorBrush(Color.FromArgb(255,red,green,blue));
			this.rect_color.Fill=brush;
			DAink.Color=Color.FromArgb(255,red,green,blue);//笔迹颜色
			this.inkcanvas.DefaultDrawingAttributes=DAink;//赋予笔迹板
		}
        //笔迹模式选择  笔迹描绘************************************
		private void Ink_radiobutton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.inkcanvas.EditingMode=InkCanvasEditingMode.Ink;
		}
        //擦除接触部分
		private void Point_radiobutton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.inkcanvas.EditingMode=InkCanvasEditingMode.EraseByPoint;
			//this.inkcanvas.EraserShape = new EllipseStylusShape(20,10);//擦除光标是椭圆
		}
        //擦除线段
		private void Stroke_radiobutton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.inkcanvas.EditingMode=InkCanvasEditingMode.EraseByStroke;
		}
        //选择笔迹板中的元素。
		private void Select_radiobutton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.inkcanvas.EditingMode=InkCanvasEditingMode.Select;
		}
		//无操作
		private void None_radiobutton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.inkcanvas.EditingMode=InkCanvasEditingMode.None;
		}
		//无墨迹，但接受笔迹数据
		private void GestureOnly__radiobutton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.inkcanvas.EditingMode=InkCanvasEditingMode.GestureOnly;
		}
		//有墨迹，同时接受笔迹数据
		private void InkAndGesture__radiobutton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.inkcanvas.EditingMode=InkCanvasEditingMode.InkAndGesture;
		}
		//收集笔迹板的笔迹点集合
		StylusPointCollection spc=new StylusPointCollection();
		Stroke myStroke;
		StylusPoint stylusPoint1;
		Point curpos;//记忆鼠标位置
		private void inkcanvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			//如果按下鼠标左键
			if (e.LeftButton== MouseButtonState.Pressed){
				curpos=e.GetPosition(this.border1);
				StylusPoint stylusPoint = new StylusPoint(curpos.X, curpos.Y);
				spc.Add(stylusPoint);
				this.textblock1.Text="Points: "+spc.Count.ToString();
			}
			//如果鼠标左键抬起
			if (e.LeftButton== MouseButtonState.Released){
				//鼠标左键按下到抬起自动产生1个Stroke
			    this.textblock2.Text="Strokes: "+this.inkcanvas.Strokes.Count.ToString();
			}
		}
		//显示从笔迹板收集的笔迹点集合
		private void ShowPoint_Click(object sender, System.Windows.RoutedEventArgs e)
		{	
			if (spc.Count>1){
			   this.inkcanvas.Strokes.Clear();			   
			   myStroke = new Stroke(spc,DAink);
			   this.inkcanvas.Strokes.Add(myStroke);
			}
			this.textblock1.Text="Points: "+spc.Count.ToString();
			this.textblock2.Text="Strokes: "+this.inkcanvas.Strokes.Count.ToString();
		}
		//动画显示从笔迹板收集的笔迹点集合
		StylusPointCollection spc1;
		DispatcherTimer timer2 = new DispatcherTimer();//定时器
		int count;		
		private void PointAnimation_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (spc.Count>1){
			   this.inkcanvas.Strokes.Clear();//清除墨迹板笔迹
			   spc1=new StylusPointCollection();//清除spc1
			   count=0;
			   timer2.Interval=TimeSpan.FromMilliseconds(20);
			   timer2.Tick+=new EventHandler(timer2Tick);
			   timer2.Start();	
			}
		}
		void timer2Tick(object sender, EventArgs e)
       {
			if (count<spc.Count){
			  spc1.Add(spc[count]);
			  myStroke = new Stroke(spc1,DAink);
			  this.inkcanvas.Strokes.Add(myStroke);
			  count++;				
			}
			else{
			  timer2.Stop();
			}
	    }		
       //动画删除从笔迹板收集的点集合
		DispatcherTimer timer1 = new DispatcherTimer();//定时器
		private void ErasePoint_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.inkcanvas.Strokes.Clear();	
			myStroke = new Stroke(spc,DAink);
			this.inkcanvas.Strokes.Add(myStroke);
			timer1.Interval=TimeSpan.FromMilliseconds(20);
			timer1.Tick+=new EventHandler(timer1Tick);
			timer1.Start();
		}
		void timer1Tick(object sender, EventArgs e)
       {
		    int j=this.spc.Count;
			if (j>1){
			  this.spc.RemoveAt(j-1);//最后1个点不能清除
			}else{
			  timer1.Stop();			  
			}
			this.textblock1.Text="Points: "+spc.Count.ToString();
			this.textblock2.Text="Strokes: "+this.inkcanvas.Strokes.Count.ToString();
	    }
        //逐条删除刚刚涂抹的墨迹线段（这是笔迹板的原始笔迹）
		private void EraseStroke_Click(object sender, System.Windows.RoutedEventArgs e)
		{			
			int i=this.inkcanvas.Strokes.Count;//Strokes.Count从0开始编号
			if (i>0){
			this.inkcanvas.Strokes.RemoveAt(i-1);
            this.textblock2.Text="Strokes: "+this.inkcanvas.Strokes.Count.ToString();
			}
		}
        //保存文件***************************************
		private void Save_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "墨迹文件 (*.isf)|*.isf|" +"Bitmap文件 (*.bmp)|*.bmp";
            if (sfd.ShowDialog()==true && sfd.FileName!=""){
                FileStream fs = new FileStream(sfd.FileName,FileMode.Create, FileAccess.Write);
                  //墨迹文件格式
                if (sfd.FilterIndex == 1){
                    this.inkcanvas.Strokes.Save(fs);
                    fs.Close();}
				else{ 
					//bitmap文件格式	Width自动时非数字，而ActualWidth是数字，和自动方式无关
					RenderTargetBitmap rtb = new RenderTargetBitmap((int)this.inkcanvas.ActualWidth,
                    (int)this.inkcanvas.ActualHeight,0,0, PixelFormats.Default);//位图宽、高、水平、竖直分辨率DPI和格式	
					rtb.Render(this.inkcanvas);//InkCanvas必须放在Border布局控件中，否则生成的尺寸不对，Margin均为0
                    BmpBitmapEncoder bbe = new BmpBitmapEncoder();					
                    bbe.Frames.Add(BitmapFrame.Create(rtb));
                    bbe.Save(fs);
                    fs.Close();	}
             }
		}
        //打开文件************************
		private void Open_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
            //ofd.CheckFileExists = true;
            ofd.Filter = "墨迹文件格式 (*.isf)|*.isf|" + "所有文件 (*.*)|*.*";
            if (ofd.ShowDialog()==true && ofd.FileName!="")
            {
                this.inkcanvas.Strokes.Clear();
                    if (!ofd.FileName.ToLower().EndsWith(".isf")){
                    System.Windows.MessageBox.Show("选择的文件类型不属于墨迹文件，不能使用！","重新选择！");
                }else{
					FileStream fs = new FileStream(ofd.FileName,FileMode.Open, FileAccess.Read);
                    this.inkcanvas.Strokes = new StrokeCollection(fs);
                    fs.Close();
                }
            }
		}
        //删除所有
		private void DeleteAll_Click(object sender, System.Windows.RoutedEventArgs e)
		{
		   this.inkcanvas.Strokes.Clear();
		   this.textblock2.Text="Strokes: "+this.inkcanvas.Strokes.Count.ToString();
		   spc=new StylusPointCollection();
		   spc1=new StylusPointCollection();
		   this.textblock1.Text="Points: "+spc.Count.ToString();
		}
        //选择所有
		private void SelectAll_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.inkcanvas.Select(this.inkcanvas.Strokes);
		}
        //删除选择的笔迹
		private void DeleteSelect_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (this.inkcanvas.GetSelectedStrokes().Count > 0)
            {
                foreach (Stroke s in this.inkcanvas.GetSelectedStrokes())
                    this.inkcanvas.Strokes.Remove(s); 
            }
			else
				System.Windows.MessageBox.Show("没有可以删除的笔迹！");
		}
        //剪切选择的笔迹
		private void Cut_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (this.inkcanvas.GetSelectedStrokes().Count > 0)
                this.inkcanvas.CutSelection();
			else
				System.Windows.MessageBox.Show("没有选择剪切的笔迹！");
		}
       //复制笔迹
		private void Copy_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (this.inkcanvas.GetSelectedStrokes().Count > 0)
                this.inkcanvas.CopySelection();
			else
				System.Windows.MessageBox.Show("没有可以复制的笔迹！");
		}
        //粘贴笔迹
		private void Paste_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			 if (this.inkcanvas.CanPaste())
                this.inkcanvas.Paste();
			else
				System.Windows.MessageBox.Show("没有可以粘贴的笔迹！");
		}
        //系统画刷颜色选择
		private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{   //获取系统画刷信息
			//PropertyInfo[] colorsys = typeof(Brushes).GetProperties(BindingFlags.Public | BindingFlags.Static);
			PropertyInfo[] colorsys = typeof(Brushes).GetProperties();
            // 创建颜色按钮
            foreach (PropertyInfo pinfo in colorsys)
            {
                Button colorbt = new Button();//定义按钮
                colorbt.Background = (SolidColorBrush)pinfo.GetValue(null, null);//获得颜色值作为按钮背景
                colorbt.Click += new RoutedEventHandler(colorbt_Click);//设置公共程序
                this.syscolor.Children.Add(colorbt);//UniformGrid控件容器中添加按钮元素（自动平均尺寸从左到右排列）
            }
		}
		private void colorbt_Click(object sender, RoutedEventArgs e)
        {
            Button mybt=sender as Button;//作为按钮
			SolidColorBrush scb=(SolidColorBrush)mybt.Background;//获取按钮背景作为颜色刷
            DAink.Color = scb.Color;//作为画笔颜色
			this.inkcanvas.DefaultDrawingAttributes=DAink;//赋予笔迹板
        }
		//笔迹动画
        StrokeCollection sc;
		DispatcherTimer timer3 = new DispatcherTimer();//定时器
		int ct;
		private void SavePoint_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.inkcanvas.Strokes.Clear();
			FileStream fs = new FileStream("myisf.isf",FileMode.Open,FileAccess.Read);
			sc = new StrokeCollection(fs);
			fs.Close();
			ct=0;
			timer3.Interval=TimeSpan.FromMilliseconds(300);
			timer3.Tick+=new EventHandler(timer3Tick);
			timer3.Start();	
			this.textblock2.Text="Strokes: "+sc.Count.ToString();			
		}
		void timer3Tick(object sender, EventArgs e)
       {
			if (ct<sc.Count){
			 this.inkcanvas.Strokes.Add(sc[ct]);
			  ct++;
			}
			else{
			  timer3.Stop();}
	    }

	}
}