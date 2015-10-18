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

namespace WpfDynamicIcon
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			this.InitializeComponent();
		}
		
		double curposeX,curposeY;
		private void dockpanel_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			curposeX=e.GetPosition(this.dockpanel).X;
			curposeY=e.GetPosition(this.dockpanel).Y;
		}

		private void dockpanel_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			double mouseX=e.GetPosition(this.dockpanel).X;
			double mouseY=e.GetPosition(this.dockpanel).Y;
			if (curposeX!=mouseX||curposeY!=mouseY){
			    curposeX=mouseX;
				curposeY=mouseY;
				transform();}
		}
		
		ScaleTransform st;
		double scalecoefficient=1.8;
		private void transform(){
			double childWs = 0;//X方向位置
			double w,h;//记录图标的宽、高
			int	i=0,j=0;//用于相邻图标标识
			double hchange=this.dockpanel.Height/2;
			foreach (UIElement child in this.dockpanel.Children){	//遍历所有图标
				//从左边第1个图标开始
			   if (child.IsMouseOver){
				j=i;//记录位置
			   }
			  i++;
			}
			i=0;			
			foreach (UIElement child in this.dockpanel.Children){			
			    st=new ScaleTransform();
			    w=child.DesiredSize.Width;
			    h=child.DesiredSize.Height;
				child.RenderTransformOrigin = new Point(0.5, 0.5);	
			 if (j==i){	//如果是鼠标悬停的图标
				child.Arrange(new Rect(childWs,-hchange*0.5, w, h));
			    st.ScaleX=scalecoefficient;
				st.ScaleY=scalecoefficient;
				child.RenderTransform=st;
			  }	else{
				child.Arrange(new Rect(childWs,this.dockpanel.Height/2, w, h));
				st.ScaleX=1;
				st.ScaleY=1;
				child.RenderTransform=st;
			  }
			  if (i==j+1&&(j+1)<this.dockpanel.Children.Count||(j-1)>-1&&i==(j-1)){
				child.Arrange(new Rect(childWs,0, w, h));
			    st.ScaleX=scalecoefficient*0.75;
				st.ScaleY=scalecoefficient*0.75;
				child.RenderTransform=st;
			  }
			  if (i==j+2&&(j+2)<this.dockpanel.Children.Count||(j-2)>-1&&i==(j-2)){
				child.Arrange(new Rect(childWs,hchange*0.5, w, h));
			    st.ScaleX=scalecoefficient*0.6;
				st.ScaleY=scalecoefficient*0.6;
				child.RenderTransform=st;
			  }
			 if (i==j+3&&(j+3)<this.dockpanel.Children.Count||(j-3)>-1 && i==(j-3)){
				child.Arrange(new Rect(childWs,hchange*0.8, w, h));
			  }
			  childWs=childWs+w;
			  i++;
			}			
		}

		private void dockpanel_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			this.dockpanel.InvalidateArrange();
			st=new ScaleTransform();
			foreach (UIElement child in this.dockpanel.Children){
				st.ScaleX=1;
				st.ScaleY=1;
			   child.RenderTransform=st;
			}	
		}

		private void image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Image im=sender as Image;			
			this.textblock1.Text="这是第“"+im.Name.Substring(5,im.Name.Length-5)+"”个图标！";			
		}		
	}
}