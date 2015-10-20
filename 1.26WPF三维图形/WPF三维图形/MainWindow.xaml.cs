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
	
	public partial class MainWindow : Window
	{
		private ModelVisual3D World;//用于ModelVisual3D元素集合
  	    private Point mouseLastPosition;
		//定义鼠标跟随对象
		FollowMouse3D fm3d=new FollowMouse3D();
		//定义变量，记忆相机位置坐标
		double cameraX,cameraY,cameraZ;
		//定义变量,用于三维对象的旋转参数，轴和角度
		double vX=-1,vY=-1,vZ=-1,angle=45;
		//记忆三维变换组中的子变换数
		int transforms;
		public MainWindow()
		{
			this.InitializeComponent();
			this.viewport3d.Cursor=Cursors.Hand;
			cameraX=ppc.Position.X;
			cameraY=ppc.Position.Y;
			cameraZ=ppc.Position.Z;
            Create3D();
		}
		
		private void Create3D() {
			//定义网格
			MeshGeometry3D mesh = new MeshGeometry3D();
            //设置 MeshGeometry3D 的顶点位置的集合。Positions属性指定的点表示构成三维网格的三角形的顶点。
			//环绕顺序（指定构成网格每个三角形的 Position 的顺序）确定了给定面是正面还是背面。 
			//正面三角形以逆时针顺序环绕；背面三角形以顺时针顺序环绕。 
			//定义8个三维空间点
			mesh.Positions.Add(new Point3D(-10, -10, 10));
			mesh.Positions.Add(new Point3D(10, -10, 10));
			mesh.Positions.Add(new Point3D(10, 10, 10));
			mesh.Positions.Add(new Point3D(-10, 10, 10));

			mesh.Positions.Add(new Point3D(-10, -10, -10));
			mesh.Positions.Add(new Point3D(10, -10, -10));
			mesh.Positions.Add(new Point3D(10, 10, -10));
			mesh.Positions.Add(new Point3D(-10, 10, -10));

			//TriangleIndices:获取或设置 MeshGeometry3D 的三角形索引的集合。（组成3D可视形状）
			//对于给定的三维网格而言，指定三角形的顶点位置的顺序确定了此三角形面是正面还是背面。 
            //Windows Presentation Foundation三维实现采用逆时针环绕顺序；也就是说，当从网格的正面看时，应以逆时针顺序指定用于确定正面网格三角形的位置的点。 
            //对 TriangleIndices 属性的设置为可选操作。 如果不指定索引，则以非索引的方式绘制三角形。 每个组的三个位置将成为一个三角形。 
			// 前面 逆时针从0号点开始，又回到0号点，组成2个三角形		
			mesh.TriangleIndices.Add(0);
			mesh.TriangleIndices.Add(1);
			mesh.TriangleIndices.Add(2);			
			mesh.TriangleIndices.Add(2);
			mesh.TriangleIndices.Add(3);
			mesh.TriangleIndices.Add(0);

			// 背面
			mesh.TriangleIndices.Add(6);
			mesh.TriangleIndices.Add(5);
			mesh.TriangleIndices.Add(4);
			mesh.TriangleIndices.Add(4);
			mesh.TriangleIndices.Add(7);
			mesh.TriangleIndices.Add(6);

			// 右面
			mesh.TriangleIndices.Add(1);
			mesh.TriangleIndices.Add(5);
			mesh.TriangleIndices.Add(2);
			mesh.TriangleIndices.Add(5);
			mesh.TriangleIndices.Add(6);
			mesh.TriangleIndices.Add(2);

			// 上面
			mesh.TriangleIndices.Add(2);
			mesh.TriangleIndices.Add(6);
			mesh.TriangleIndices.Add(3);
			mesh.TriangleIndices.Add(3);
			mesh.TriangleIndices.Add(6);
			mesh.TriangleIndices.Add(7);

			// 底面
			mesh.TriangleIndices.Add(5);
			mesh.TriangleIndices.Add(1);
			mesh.TriangleIndices.Add(0);
			mesh.TriangleIndices.Add(0);
			mesh.TriangleIndices.Add(4);
			mesh.TriangleIndices.Add(5);

			// 左面
			mesh.TriangleIndices.Add(4);
			mesh.TriangleIndices.Add(0);
			mesh.TriangleIndices.Add(3);
			mesh.TriangleIndices.Add(3);
			mesh.TriangleIndices.Add(7);
			mesh.TriangleIndices.Add(4);

			//父元素对象
			World=new ModelVisual3D();//
			// 创建三维几何图形
			GeometryModel3D m3DGeometry = new GeometryModel3D();
			m3DGeometry.Geometry=mesh;
			//漫射材料
			m3DGeometry.Material=new DiffuseMaterial(Brushes.Red);			
			ModelVisual3D box01=new ModelVisual3D();
			box01.Content=m3DGeometry;
			
			//环境光元素设置
			ModelVisual3D ambientlight=new ModelVisual3D();//
			AmbientLight abl=new AmbientLight();
			abl.Color=Colors.Gray;			
			ambientlight.Content=abl;//
			
			ModelVisual3D directionallight=new ModelVisual3D();//
		    DirectionalLight dl = new DirectionalLight();
            dl.Color = Colors.White;
			dl.Direction = new Vector3D(0, 0, -1);			
			directionallight.Content=dl;		
			
			//定义三维变换
			Transform3DGroup traneform3dgroup=new Transform3DGroup();//
			TranslateTransform3D tlt3d=new TranslateTransform3D();
			tlt3d.OffsetX=0;
			tlt3d.OffsetY=0;
			tlt3d.OffsetZ=0;
			traneform3dgroup.Children.Add(tlt3d);
			//尺寸大小变换设置
			ScaleTransform3D st3d=new ScaleTransform3D();
			st3d.CenterX=0;
			st3d.CenterY=0;
			st3d.CenterZ=0;
			//缩小
			st3d.ScaleX=1;
			st3d.ScaleY=1;
			st3d.ScaleZ=1;
			traneform3dgroup.Children.Add(st3d);
			//旋转变换设置
			RotateTransform3D rtf3d=new RotateTransform3D();
			rtf3d.CenterX=0;
			rtf3d.CenterY=0;
			rtf3d.CenterZ=0;
			//设置绕指定轴进行指定角度的三维旋转
			AxisAngleRotation3D aar=new AxisAngleRotation3D();
			aar.Angle=angle;
			//旋转轴X分量、Y分量和Z分量组成的矢量
			aar.Axis= new Vector3D(vX,vY,vZ);
			rtf3d.Rotation=aar;
			traneform3dgroup.Children.Add(rtf3d);
			World.Transform=traneform3dgroup;//
			//环境光线加入3D模型组合体 
			World.Children.Add(ambientlight);
			//定向光线加入3D模型组合体   
			World.Children.Add(directionallight);
			World.Children.Add(box01);//
			//三维变换组中的子变换数
			transforms=traneform3dgroup.Children.Count;
			//赋予3D控件
			this.viewport3d.Children.Add(World);			
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
			Transform3DGroup group3D = World.Transform as Transform3DGroup;	
			int j=group3D.Children.Count;
			//保留原来的变换数,其余全部删除
			if (j>transforms){			
			    for (int k=j-1;k>transforms-1;){
			       group3D.Children.RemoveAt(k);
			       k=group3D.Children.Count-1;
			    }
			}
			//重新定义三维对象的旋转变换
			//World.Transform=new Transform3DGroup();
			//Transform3DGroup group3D = World.Transform as Transform3DGroup;	
			//三维旋转轴,三维向量
			//Vector3D axis = new Vector3D(vX,vY,vZ);
			//定义四元数的旋转转换
			//QuaternionRotation3D qr = new QuaternionRotation3D(new Quaternion(axis, angle));
			//group3D.Children.Add(new RotateTransform3D(qr));
		}

		private void viewport3d_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
		{
			double s=240;
			ppc.Position = new Point3D(ppc.Position.X, ppc.Position.Y, ppc.Position.Z - e.Delta /s);
		}
	}
}