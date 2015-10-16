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

namespace WPFThumb
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		double canvas1Width,canvas1Height,thumb1Width,thumb1Height;
		public MainWindow()
		{
			this.InitializeComponent();
			canvas1Width=this.canvas1.Width;
			canvas1Height=this.canvas1.Height;
			thumb1Width=this.thumb1.Width;
			thumb1Height=this.thumb1.Height;
		}
        //鼠标拖动开始(鼠标按下)
		private void thumb1_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
		{
			this.thumb1.Background=Brushes.Red;
		}
        //拖动结束
		private void thumb1_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
		{
			this.thumb1.Background=Brushes.Blue;
		}
        //拖动
		private void thumb1_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
		{
			double eW=this.canvas1.Width+e.HorizontalChange;
			double eH=this.canvas1.Height+e.HorizontalChange;
			if (eW>0.5*thumb1Width&&eH>0.5*thumb1Height&&eH<=canvas1Height)
			{
			    this.canvas1.Width=eW;
			    this.canvas1.Height=eH;
			    this.grid1.Width=this.canvas1.Width;
			    this.grid1.Height=this.canvas1.Height;
			}
		}
	}
}