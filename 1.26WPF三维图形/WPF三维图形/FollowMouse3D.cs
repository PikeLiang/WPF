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

using System.Windows.Media.Media3D;
namespace WPF三维图形
{
	public class FollowMouse3D
	{
		public FollowMouse3D()
		{
			// 在此点下面插入创建对象所需的代码。
		}
		//自定义方法，三维对象跟随鼠标旋转		
		public Point MouseFollow(Viewport3D vp3d,Point mLPosition,ModelVisual3D m3D)
		{
				//获取鼠标窗口坐标
				Point Mpos = Mouse.GetPosition(vp3d);
				//鼠标相对于三维对象中心的坐标				
				Point mouseRelativePosition = new Point(Mpos.X - vp3d.ActualWidth / 2, vp3d.ActualHeight / 2 - Mpos.Y);
				//计算本次鼠标坐标与上次鼠标坐标的差
				double shiftX = mouseRelativePosition.X - mLPosition.X, shiftY = mouseRelativePosition.Y - mLPosition.Y;
                double mouseRotationAngle = 0;
				if(shiftX != 0 && shiftY != 0) {
					//计算鼠标转动的角度(三维空间近似计算)
					mouseRotationAngle = Math.Asin(Math.Abs(shiftY) / Math.Sqrt(Math.Pow(shiftX, 2) + Math.Pow(shiftY, 2)));
					//mouseRotationAngle = 0.2;
					//如果位于第2象限
					if(shiftX < 0 && shiftY > 0) mouseRotationAngle += Math.PI / 2;
					//如果位于第3象限
					if(shiftX < 0 && shiftY < 0) mouseRotationAngle += Math.PI;
					//如果位于第4象限
					if(shiftX > 0 && shiftY < 0) mouseRotationAngle += Math.PI * 1.5;
				}
				if(shiftX == 0 && shiftY != 0) mouseRotationAngle = Math.Sign(shiftY) > 0 ? Math.PI / 2 : Math.PI * 1.5;
				if(shiftX != 0 && shiftY == 0) mouseRotationAngle = Math.Sign(shiftX) > 0 ? 0 : Math.PI;
				//旋转轴和旋转面垂直，使用右手法则
				double axisRotationAngle = mouseRotationAngle + Math.PI / 2;                
				Vector3D RotationAxis = new Vector3D(Math.Cos(axisRotationAngle) * 10, Math.Sin(axisRotationAngle) * 10, 0);                
				double RotationAngle = 0.01 * Math.Sqrt(Math.Pow(shiftX, 2) + Math.Pow(shiftY, 2));                
				Transform3DGroup TF3DGroup = m3D.Transform as Transform3DGroup;
				//声明四元旋转对象,实现3D旋转,旋转中心是三维对象的中心点
				Quaternion quaternion=new Quaternion(RotationAxis, RotationAngle * 180 / Math.PI);
				QuaternionRotation3D Qr3D = new QuaternionRotation3D(quaternion);
				TF3DGroup.Children.Add(new RotateTransform3D(Qr3D));			
				return mouseRelativePosition;
		}
	}
}