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

using System.Windows.Media.Media3D;//for 3D
namespace WPF三维图形
{
	/// <summary>
	/// Window1.xaml 的交互逻辑
	/// </summary>
	public partial class Window1 : Window
	{
		FollowMouse3D fm3d=new FollowMouse3D();
		Point mouseLastPosition;
		//定义变量，记忆相机位置坐标
		double cameraX,cameraY,cameraZ;
		//设置三维变换组变量
		Transform3DGroup GroupTF3D;
		//记忆三维变换组中的子变换数
		int transforms;
		public Window1()
		{
			this.InitializeComponent();
			cameraX=ppc.Position.X;
			cameraY=ppc.Position.Y;
			cameraZ=ppc.Position.Z;
			//声明或获取当前World的三维变换组（xaml中）Transform3DGroup
			GroupTF3D = World.Transform as Transform3DGroup;
			//记录三维变换组中子变换的总数
			transforms=GroupTF3D.Children.Count;
		}

		private void viewport3d_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Point pos = Mouse.GetPosition(viewport3d);
			mouseLastPosition = new Point(pos.X - viewport3d.ActualWidth / 2, viewport3d.ActualHeight / 2 - pos.Y);
		}

		private void viewport3d_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (Mouse.LeftButton == MouseButtonState.Pressed){
			   mouseLastPosition=fm3d.MouseFollow(viewport3d,mouseLastPosition,World);
			}
		}

		private void reset_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			//恢复相机初始位置
			ppc.Position = new Point3D(cameraX, cameraY,cameraZ);
			int j=GroupTF3D.Children.Count;
			//保留原来的变换数,其余删除
			if (j>transforms){			
			    for (int k=j-1;k>transforms-1;){
			       GroupTF3D.Children.RemoveAt(k);
			       k=GroupTF3D.Children.Count-1;
			    }
			}
		}

		private void viewport3d_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
		{
			double s=240;
			ppc.Position = new Point3D(ppc.Position.X, ppc.Position.Y, ppc.Position.Z - e.Delta /s);
		}
	}
}