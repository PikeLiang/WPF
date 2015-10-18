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

using System.Windows.Forms;//for PictureBox
using System.Drawing;//for Graphic
using System.Drawing.Imaging;//for ImageAttributes 
namespace WpfGraphicDrawing
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		PictureBox picturebox = new PictureBox();//实例化对象picturebox
		//设定图像源(应该先放到bin\debug文件夹中)
		System.Drawing.Image images = System.Drawing.Image.FromFile("image.jpg");
		System.Windows.Media.Brush brush;//定义画刷
		byte red=0,green=0,blue=0;//三基色初始化值
		public MainWindow()
		{
			this.InitializeComponent();
			//设置图形放入图形盒后自动压缩或拉伸使适合图形盒大小
            this.picturebox.SizeMode = PictureBoxSizeMode.StretchImage;
			//设置picturebox停靠在父控件并跟随调节大小
			this.picturebox.Dock = DockStyle.Fill;            
			this.picturebox.Image=images;//设置图形盒的图形源
		    this.wfh.Child=this.picturebox;//放入接口控件中
			//后面4个参数对应alpha、红、绿、蓝
			brush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255,red,green,blue));
			this.rect_color.Fill=brush;//填充颜色，作为RGB参数合成后的颜色标记
		}
        //原图像
		private void button1_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.picturebox.Paint += new System.Windows.Forms.PaintEventHandler(picturebox_Paint1); 
		    this.picturebox.Refresh();//触发picturebox_Paint1事件			
		}
		System.Drawing.Rectangle rect;
		private void picturebox_Paint1(object sender, System.Windows.Forms.PaintEventArgs e)
        {
		    int w=(int)this.wfh.ActualWidth;
		    int h=(int)this.wfh.ActualHeight;
		   //定义绘制图像矩形，前2个参数是左上角坐标,后面是宽、高，即矩形大小
	        rect = new System.Drawing.Rectangle(0, 0, w, h);
            e.Graphics.DrawImage(images, rect);	//以矩形描图
        }	
		//颜色设置
	    private void color_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
		{
			Slider colorvalue=sender as Slider;//引发事件对象作为滑块
			byte cv=(byte)colorvalue.Value;//获取滑块值
			switch (colorvalue.Name){
				case "Red_slider":
				  red=cv;//红色参数
				  break;
				case "Green_slider":
                  green=cv;//绿色参数
				  break;
				case "Blue_slider":
                  blue=cv;//蓝色参数
				  break;
			}
			//画刷颜色
			brush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255,red,green,blue));
			this.rect_color.Fill=brush;//填充
		}
		//转图像色
		ColorMatrix matrix= new ColorMatrix();//定义颜色矩阵
		ImageAttributes attributes = new ImageAttributes();//描绘图像时的着色属性
	    private void button2_Click(object sender, System.Windows.RoutedEventArgs e)
	    {			
			//定义事件处理
			this.picturebox.Paint += new System.Windows.Forms.PaintEventHandler(picturebox_Paint2); 
		    this.picturebox.Refresh();//刷新重绘，触发picturebox_Paint2事件，执行相应程序
	    }
		private void picturebox_Paint2(object sender, System.Windows.Forms.PaintEventArgs e)
        {
		    matrix[0, 0] = red/256f  ; matrix[0, 1] =0f; matrix[0, 2] =0f;//红色分量
		    matrix[1, 0] = 0f ; matrix[1, 1] = green/256f; matrix[1, 2] = 0f;//绿色分量
		    matrix[2, 0] = 0f ; matrix[2, 1] = 0f; matrix[2, 2] = blue/256f;//蓝色分量
			//着色属性设置，设置颜色调整矩阵
            attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
			int w=(int)this.wfh.ActualWidth;//接口控件实际宽
		    int h=(int)this.wfh.ActualHeight;//接口控件实际高			
		   //定义绘制图像矩形，前2个参数是左上角坐标,后面是宽、高，即矩形大小
	        rect = new System.Drawing.Rectangle(0, 0, w, h);
			//根据着色属性和矩形尺寸对图像描绘
            e.Graphics.DrawImage(images, rect, 0, 0, w, h, GraphicsUnit.Pixel, attributes);				
        }	
        //灰色扫描
		int i,j,k,wh,clip ;
		private void button3_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.picturebox.Paint += new System.Windows.Forms.PaintEventHandler(picturebox_Paint1); 
		    this.picturebox.Refresh();//触发picturebox_Paint1事件,恢复原图			
            attributes.SetGamma(1.0F);//透明度参数设置(可省)
		    this.picturebox.Paint += new System.Windows.Forms.PaintEventHandler(wfh_Paint31); 
		    wh=(int)this.wfh.ActualHeight;
		    clip=10;//扫描像素,横向或纵向
		    for (i =0; i < wh+clip; i += clip){
		       this.picturebox.Refresh();//触发wfh_Paint31事件
		       System.Threading.Thread.Sleep(20);//延时20毫秒
		    }
		    this.picturebox.Paint += new System.Windows.Forms.PaintEventHandler(wfh_Paint32); 
		    wh=(int)this.wfh.ActualWidth;
			for (j =0; j < wh+clip; j += clip){
		       this.picturebox.Refresh();//触发wfh_Paint32事件
		       System.Threading.Thread.Sleep(20);
		    }
		}
		private void wfh_Paint31(object sender, System.Windows.Forms.PaintEventArgs e)
       {
		    matrix[0, 0] = 0.3f  ; matrix[0, 1] = 0.3f; matrix[0, 2] = 0.3f;//红色分量灰度
		    matrix[1, 0] = 0.6f  ; matrix[1, 1] = 0.6f; matrix[1, 2] = 0.6f;//绿色分量灰度
		    matrix[2, 0] = 0.2f  ; matrix[2, 1] = 0.2f; matrix[2, 2] = 0.2f;//蓝色分量灰度
			//颜色属性设置
            attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
		    int w=(int)this.wfh.ActualWidth;
		    rect = new System.Drawing.Rectangle(0, 0, w, i);
		    e.Graphics.DrawImage(images, rect, 0, 0, w, i, GraphicsUnit.Pixel, attributes);
	    }	
	    private void wfh_Paint32(object sender, System.Windows.Forms.PaintEventArgs e)
       {
	        int h=(int)this.wfh.ActualHeight;
		    rect = new System.Drawing.Rectangle(0, 0, j, h);
		   //不用颜色属性设置，重绘原图像
		    e.Graphics.DrawImage(images, rect, 0, 0, j, h, GraphicsUnit.Pixel);
	    }
        //三色扫描
	    private void button4_Click(object sender, System.Windows.RoutedEventArgs e)
	    {
	    	this.picturebox.Paint += new System.Windows.Forms.PaintEventHandler(picturebox_Paint1); 
		    this.picturebox.Refresh();//触发picturebox_Paint1事件,恢复原图
            attributes.SetGamma(1.0F);//透明度参数设置(可省)
		    this.picturebox.Paint += new System.Windows.Forms.PaintEventHandler(wfh_Paint41); 
		    wh=(int)this.wfh.ActualHeight;
		    clip=10;
		    for (i =0; i < wh/2; i+= clip){
			    this.picturebox.Refresh();
			    System.Threading.Thread.Sleep(50);			   
		    }			
			this.picturebox.Paint += new System.Windows.Forms.PaintEventHandler(wfh_Paint42); 
		    wh=(int)this.wfh.ActualWidth;
			for (j =0; j < (wh+clip)/2; j+= clip){
		       this.picturebox.Refresh();
		       System.Threading.Thread.Sleep(50);}
			this.picturebox.Paint += new System.Windows.Forms.PaintEventHandler(wfh_Paint43); 
		    wh=(int)this.wfh.ActualWidth;
			for (k =0; k < wh+clip; k+= clip){
		       this.picturebox.Refresh();
		       System.Threading.Thread.Sleep(20);
		    }
	    }
		private void wfh_Paint41(object sender, System.Windows.Forms.PaintEventArgs e)
       {
		    int w=(int)this.wfh.ActualWidth;
		    int h=(int)this.wfh.ActualHeight; 
		    matrix[0, 0] =1f  ; matrix[0, 1] = 0f; matrix[0, 2] = 0f;//红色分量
		    matrix[1, 0] =0f  ; matrix[1, 1] = 0f; matrix[1, 2] = 0f;//绿色分量
		    matrix[2, 0] =0f  ; matrix[2, 1] = 0f; matrix[2, 2] = 0f;//蓝色分量
		    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
		    rect = new System.Drawing.Rectangle(0, 0, w, i);
		    e.Graphics.DrawImage(images, rect, 0, 0, w, i, GraphicsUnit.Pixel, attributes);
		    matrix[0, 0] =0f  ; matrix[0, 1] = 0f; matrix[0, 2] = 0f;//红色分量
		    matrix[1, 0] =0f  ; matrix[1, 1] = 1f; matrix[1, 2] = 0f;//绿色分量
		    matrix[2, 0] =0f  ; matrix[2, 1] = 0f; matrix[2, 2] = 0f;//蓝色分量
		    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
			rect = new System.Drawing.Rectangle(0, h-i, w, i);
		    e.Graphics.DrawImage(images, rect, 0, h-i, w, i, GraphicsUnit.Pixel, attributes);
	    }
	    private void wfh_Paint42(object sender, System.Windows.Forms.PaintEventArgs e)
       {
		    matrix[0, 0] =0f  ; matrix[0, 1] = 0f; matrix[0, 2] = 0f;//红色分量
		    matrix[1, 0] =0f  ; matrix[1, 1] = 0f; matrix[1, 2] = 0f;//绿色分量
		    matrix[2, 0] =0f  ; matrix[2, 1] = 0f; matrix[2, 2] = 1f;//蓝色分量
		    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap); 
		    int w=(int)this.wfh.ActualWidth;
		    int h=(int)this.wfh.ActualHeight;
		    rect = new System.Drawing.Rectangle(w/2-j, 0, j, h);
		    e.Graphics.DrawImage(images, rect, w/2-j, 0, j, h, GraphicsUnit.Pixel, attributes);
		    rect = new System.Drawing.Rectangle(w/2, 0, j, h);
		    e.Graphics.DrawImage(images, rect, w/2, 0, j, h, GraphicsUnit.Pixel, attributes);
	    }
	   private void wfh_Paint43(object sender, System.Windows.Forms.PaintEventArgs e)
       {
		    int w=(int)this.wfh.ActualWidth;
		    int h=(int)this.wfh.ActualHeight;
		    rect = new System.Drawing.Rectangle(0, 0, k, h);
		    e.Graphics.DrawImage(images, rect, 0, 0, k, h, GraphicsUnit.Pixel);
	    }
	}
}