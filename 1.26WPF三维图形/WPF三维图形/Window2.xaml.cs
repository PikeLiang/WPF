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


using System.Windows.Media.Animation;//for Storyboard
using System.Windows.Media.Media3D;//for 3D
//using WPF三维图形;//for FollowMouse3D
namespace WPF三维图形
{
	public partial class Window2 : Window
	{
		//拆卸故事板
		Storyboard mystoryboard1=new Storyboard();
		Storyboard mystoryboard2=new Storyboard();
		Storyboard mystoryboard3=new Storyboard();
		Storyboard mystoryboard4=new Storyboard();
		Storyboard mystoryboard5=new Storyboard();
		Storyboard mystoryboard6=new Storyboard();
		Storyboard mystoryboard7=new Storyboard();
		Storyboard mystoryboard8=new Storyboard();
		Storyboard mystoryboard9=new Storyboard();
		Storyboard mystoryboard10=new Storyboard();
		Storyboard mystoryboard11=new Storyboard();
		//装配故事板
		Storyboard mystoryboard12=new Storyboard();
		Storyboard mystoryboard13=new Storyboard();
		Storyboard mystoryboard14=new Storyboard();
		Storyboard mystoryboard15=new Storyboard();
		Storyboard mystoryboard16=new Storyboard();
		Storyboard mystoryboard17=new Storyboard();
		Storyboard mystoryboard18=new Storyboard();
		Storyboard mystoryboard19=new Storyboard();
		Storyboard mystoryboard20=new Storyboard();
		Storyboard mystoryboard21=new Storyboard();
		//声明“鼠标轨迹球”效果对象
		FollowMouse3D fm3d=new FollowMouse3D();
		//定义鼠标位置记忆变量
		Point mouseLastPosition;
		//定义变量，记忆相机位置坐标
		double cameraX,cameraY,cameraZ;
		//定义变量，记忆三维对象变换组中子变换数
		int transforms;
		//声明三维对象的三维变换组对象
		Transform3DGroup GroupTF3D;
		//定义变量，由于拆卸或装配的自动、手动方式识别
		bool auto1=false,auto2=false;
		public Window2()
		{
			this.InitializeComponent();
			//记忆相机的初始位置
            cameraX=ppc.Position.X;
			cameraY=ppc.Position.Y;
			cameraZ=ppc.Position.Z;
			//声明拆卸故事板资源，并定义相应故事板完成事件
			mystoryboard1=(Storyboard)this.FindResource("Storyboard1");
			mystoryboard2=(Storyboard)this.FindResource("Storyboard2");
			this.mystoryboard2.Completed+=new System.EventHandler(mystoryboard2_Completed);
			mystoryboard3=(Storyboard)this.FindResource("Storyboard3");
			this.mystoryboard3.Completed+=new System.EventHandler(mystoryboard3_Completed);
			mystoryboard4=(Storyboard)this.FindResource("Storyboard4");
			this.mystoryboard4.Completed+=new System.EventHandler(mystoryboard4_Completed);
			mystoryboard5=(Storyboard)this.FindResource("Storyboard5");
			this.mystoryboard5.Completed+=new System.EventHandler(mystoryboard5_Completed);;
			mystoryboard6=(Storyboard)this.FindResource("Storyboard6");
			this.mystoryboard6.Completed+=new System.EventHandler(mystoryboard6_Completed);
			mystoryboard7=(Storyboard)this.FindResource("Storyboard7");
			this.mystoryboard7.Completed+=new System.EventHandler(mystoryboard7_Completed);
			mystoryboard8=(Storyboard)this.FindResource("Storyboard8");
			this.mystoryboard8.Completed+=new System.EventHandler(mystoryboard8_Completed);
			mystoryboard9=(Storyboard)this.FindResource("Storyboard9");
			this.mystoryboard9.Completed+=new System.EventHandler(mystoryboard9_Completed);
			mystoryboard10=(Storyboard)this.FindResource("Storyboard10");
			this.mystoryboard10.Completed+=new System.EventHandler(mystoryboard10_Completed);
			mystoryboard11=(Storyboard)this.FindResource("Storyboard11");
			this.mystoryboard11.Completed+=new System.EventHandler(mystoryboard11_Completed);
			//声明装配故事板资源，并定义相应故事板完成事件
			mystoryboard12=(Storyboard)this.FindResource("Storyboard12");
			this.mystoryboard12.Completed+=new System.EventHandler(mystoryboard12_Completed);
			mystoryboard13=(Storyboard)this.FindResource("Storyboard13");
			this.mystoryboard13.Completed+=new System.EventHandler(mystoryboard13_Completed);
			mystoryboard14=(Storyboard)this.FindResource("Storyboard14");
			this.mystoryboard14.Completed+=new System.EventHandler(mystoryboard14_Completed);
			mystoryboard15=(Storyboard)this.FindResource("Storyboard15");
			this.mystoryboard15.Completed+=new System.EventHandler(mystoryboard15_Completed);
			mystoryboard16=(Storyboard)this.FindResource("Storyboard16");
			this.mystoryboard16.Completed+=new System.EventHandler(mystoryboard16_Completed);
			mystoryboard17=(Storyboard)this.FindResource("Storyboard17");
			this.mystoryboard17.Completed+=new System.EventHandler(mystoryboard17_Completed);
			mystoryboard18=(Storyboard)this.FindResource("Storyboard18");
			this.mystoryboard18.Completed+=new System.EventHandler(mystoryboard18_Completed);
			mystoryboard19=(Storyboard)this.FindResource("Storyboard19");
			this.mystoryboard19.Completed+=new System.EventHandler(mystoryboard19_Completed);
			mystoryboard20=(Storyboard)this.FindResource("Storyboard20");
			this.mystoryboard20.Completed+=new System.EventHandler(mystoryboard20_Completed);
			mystoryboard21=(Storyboard)this.FindResource("Storyboard21");
			this.mystoryboard21.Completed+=new System.EventHandler(mystoryboard21_Completed);
			this.mystoryboard1.Begin();	
			//声明或获取当前World的三维变换组（xaml中）Transform3DGroup
			GroupTF3D = World.Transform as Transform3DGroup;
			//记录三维变换组中子变换的总数
			transforms=GroupTF3D.Children.Count;
			//所有按钮禁止使用，复位按钮除外
			IsEnable1();
			IsEnable2();
			this.rotat.IsEnabled=false;
		}

		private void viewport3d_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Point pos = Mouse.GetPosition(viewport3d);
			mouseLastPosition = new Point(pos.X - viewport3d.ActualWidth / 2, viewport3d.ActualHeight / 2 - pos.Y);
		}
        //鼠标移动电动机
		private void viewport3d_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (Mouse.LeftButton == MouseButtonState.Pressed){
			   mouseLastPosition=fm3d.MouseFollow(viewport3d,mouseLastPosition,World);
			}
		}
        //复位按钮
		private void reset_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Reset();
			this.bta1.IsEnabled=true;
			this.bta2.IsEnabled=true;
			this.rotat.IsEnabled=true;
		}
		//自定义复位方法
		private void Reset(){
		    ppc.Position = new Point3D(cameraX, cameraY,cameraZ);
			//获取当前的子变换总数
			int j=GroupTF3D.Children.Count;
			//保留原来的变换数,其余删除
			if (j>transforms){			
			    for (int k=j-1;k>transforms-1;){
			       GroupTF3D.Children.RemoveAt(k);
			       k=GroupTF3D.Children.Count-1;
			    }
			}
			this.mystoryboard1.Stop();
		}
		//自动拆卸
		private void bta1_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Reset();
			auto1=true;
			this.bta2.IsEnabled=false;
			this.reset.IsEnabled=false;
			this.rotat.IsEnabled=false;
			this.mystoryboard2.BeginTime=TimeSpan.FromSeconds(1);
			this.mystoryboard2.Begin();				
		}
		//旋转
		private void rotat_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.mystoryboard1.Begin();
		}
		//拆卸传动轴
		private void mystoryboard2_Completed(object sender, System.EventArgs e)
		{
			if (auto1)
			   this.mystoryboard3.Begin();
		}
		//拆卸前盖板
		private void mystoryboard3_Completed(object sender, System.EventArgs e)
		{
			if (auto1)
			   this.mystoryboard4.Begin();
		}
		//拆卸转子
		private void mystoryboard4_Completed(object sender, System.EventArgs e)
		{
			if (auto1)
			   this.mystoryboard5.Begin();
		}
		//拆卸线盒螺丝
		private void mystoryboard5_Completed(object sender, System.EventArgs e)
		{
			if (auto1)
			   this.mystoryboard6.Begin();
		}
		//拆卸线盒盖
		private void mystoryboard6_Completed(object sender, System.EventArgs e)
		{
			if (auto1)
			   this.mystoryboard7.Begin();
		}
		//旋转到-70
		private void mystoryboard7_Completed(object sender, System.EventArgs e)
		{
			if (auto1)
			   this.mystoryboard8.Begin();		
		}
		//拆卸风扇罩
		private void mystoryboard8_Completed(object sender, System.EventArgs e)
		{
			if (auto1){
			   this.mystoryboard9.BeginTime=TimeSpan.FromSeconds(1);
			   this.mystoryboard9.Begin();
			}
		}
        //拆卸风扇
		private void mystoryboard9_Completed(object sender, System.EventArgs e)
		{
			if (auto1)
			   this.mystoryboard10.Begin();
		}
		//拆卸后盖板
		private void mystoryboard10_Completed(object sender, System.EventArgs e)
		{
			if (auto1)
			   this.mystoryboard11.Begin();
		}
		//拆卸完成
		private void mystoryboard11_Completed(object sender, System.EventArgs e)
		{
			this.reset.IsEnabled=true;
			this.rotat.IsEnabled=true;
			this.bta1.IsEnabled=false;
			this.btb1.IsEnabled=true;
			this.btb2.IsEnabled=true;
		}
        //手动拆卸前盖螺丝
		private void bta2_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Reset();
			auto1=false;
			this.mystoryboard2.BeginTime=TimeSpan.FromSeconds(1);
			this.mystoryboard2.Begin();
			this.bta1.IsEnabled=false;
			this.reset.IsEnabled=false;
			this.rotat.IsEnabled=false;
			this.bta2.IsEnabled=false;
			this.bta3.IsEnabled=true;
		}
        //手动拆卸传动轴
		private void bta3_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.mystoryboard3.Begin();
			this.bta3.IsEnabled=false;
			this.bta4.IsEnabled=true;
		}
        //手动拆卸前盖板
		private void bta4_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.mystoryboard4.Begin();
			this.bta4.IsEnabled=false;
			this.bta5.IsEnabled=true;
		}
        //手动拆卸转子
		private void bta5_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.mystoryboard5.Begin();
			this.bta5.IsEnabled=false;
			this.bta6.IsEnabled=true;
		}
        //手动拆卸线盒螺丝
		private void bta6_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.mystoryboard6.Begin();
			this.bta6.IsEnabled=false;
			this.bta7.IsEnabled=true;
		}
        //手动拆卸线盒盖
		private void bta7_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.mystoryboard7.Begin();
			this.bta7.IsEnabled=false;
			this.bta8.IsEnabled=true;
		}
        //手动旋转
		private void bta8_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.mystoryboard8.Begin();
			this.bta8.IsEnabled=false;
			this.bta9.IsEnabled=true;
		}
        //手动拆卸风扇罩
		private void bta9_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.mystoryboard9.Begin();
			this.bta9.IsEnabled=false;
			this.bta10.IsEnabled=true;
		}
        //手动拆卸风扇
		private void bta10_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.mystoryboard10.Begin();
			this.bta10.IsEnabled=false;
			this.bta11.IsEnabled=true;
		}
        //手动拆卸后盖板
		private void bta11_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.mystoryboard11.Begin();
			this.bta11.IsEnabled=false;
		}
        //自动装配,从后盖板开始
		private void btb1_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Reset();
			auto2=true;
			this.mystoryboard12.BeginTime=TimeSpan.FromSeconds(1);
			this.mystoryboard12.Begin();
			this.btb1.IsEnabled=false;
			this.reset.IsEnabled=false;
			this.rotat.IsEnabled=false;
			this.btb2.IsEnabled=false;
		}
		//手动装配后盖板
		private void btb2_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Reset();
			auto2=false;
			this.mystoryboard12.Begin();
			this.btb2.IsEnabled=false;
			this.btb3.IsEnabled=true;
		}
        //手动装配风扇
		private void btb3_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.mystoryboard13.Begin();
			this.btb3.IsEnabled=false;
			this.btb4.IsEnabled=true;
		}
        //手动装配风扇罩
		private void btb4_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.mystoryboard14.Begin();
			this.btb4.IsEnabled=false;
			this.btb5.IsEnabled=true;
		}
        //底座转动,从-70到0
		private void btb5_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.mystoryboard15.Begin();
			this.btb5.IsEnabled=false;
			this.btb6.IsEnabled=true;
		}
        //手动装配线盒盖板
		private void btb6_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.mystoryboard16.Begin();
			this.btb6.IsEnabled=false;
			this.btb7.IsEnabled=true;
		}
        //手动装配线盒螺丝
		private void btb7_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.mystoryboard17.Begin();
			this.btb7.IsEnabled=false;
			this.btb8.IsEnabled=true;
		}
        //手动装配转子
		private void btb8_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.mystoryboard18.Begin();
			this.btb8.IsEnabled=false;
			this.btb9.IsEnabled=true;
		}
        //手动装配前盖板
		private void btb9_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.mystoryboard19.Begin();
			this.btb9.IsEnabled=false;
			this.btb10.IsEnabled=true;
		}
        //手动装配传动轴
		private void btb10_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.mystoryboard20.Begin();
			this.btb10.IsEnabled=false;
			this.btb11.IsEnabled=true;
		}
        //手动装配前盖螺丝
		private void btb11_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.mystoryboard21.Begin();
			this.btb11.IsEnabled=false;
		}
		//装配风扇
		private void mystoryboard12_Completed(object sender, System.EventArgs e)
		{
			if (auto2)
			    this.mystoryboard13.Begin();
		}
		//装配风扇罩
		private void mystoryboard13_Completed(object sender, System.EventArgs e)
		{
			if (auto2)
			    this.mystoryboard14.Begin();
		}
        //旋转
		private void mystoryboard14_Completed(object sender, System.EventArgs e)
		{
			if (auto2)
			   this.mystoryboard15.Begin();
		}
        //装配线盒盖
		private void mystoryboard15_Completed(object sender, System.EventArgs e)
		{
			if (auto2)
			   this.mystoryboard16.Begin();
		}
        //装配线盒螺丝
		private void mystoryboard16_Completed(object sender, System.EventArgs e)
		{
			if (auto2)
			   this.mystoryboard17.Begin();
		}
        //装配转子
		private void mystoryboard17_Completed(object sender, System.EventArgs e)
		{
			if (auto2)
			   this.mystoryboard18.Begin();
		}
        //装配前盖板
		private void mystoryboard18_Completed(object sender, System.EventArgs e)
		{
			if (auto2)
			   this.mystoryboard19.Begin();
		}
        //装配传动轴
		private void mystoryboard19_Completed(object sender, System.EventArgs e)
		{
			if (auto2)
			   this.mystoryboard20.Begin();
		}
        //装配前盖螺丝
		private void mystoryboard20_Completed(object sender, System.EventArgs e)
		{
			if (auto2)
			   this.mystoryboard21.Begin();
		}
		//装配完成
	    private void mystoryboard21_Completed(object sender, System.EventArgs e)
		{
			this.reset.IsEnabled=true;
			this.rotat.IsEnabled=true;
			this.bta1.IsEnabled=true;
			this.bta2.IsEnabled=true;
		}
		
		private void IsEnable1(){
			this.bta1.IsEnabled=false;
			this.bta2.IsEnabled=false;
			this.bta3.IsEnabled=false;
			this.bta4.IsEnabled=false;
			this.bta5.IsEnabled=false;
			this.bta6.IsEnabled=false;
			this.bta7.IsEnabled=false;
			this.bta8.IsEnabled=false;
			this.bta9.IsEnabled=false;
			this.bta10.IsEnabled=false;
			this.bta11.IsEnabled=false;
		}
		private void IsEnable2(){
			this.btb1.IsEnabled=false;
			this.btb2.IsEnabled=false;
			this.btb3.IsEnabled=false;
			this.btb4.IsEnabled=false;
			this.btb5.IsEnabled=false;
			this.btb6.IsEnabled=false;
			this.btb7.IsEnabled=false;
			this.btb8.IsEnabled=false;
			this.btb9.IsEnabled=false;
			this.btb10.IsEnabled=false;
			this.btb11.IsEnabled=false;
		}
        
	
	}
}