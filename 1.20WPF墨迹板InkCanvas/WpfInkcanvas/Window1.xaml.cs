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

using System.Globalization;
namespace WpfInkcanvas
{
	/// <summary>
	/// Window1.xaml 的交互逻辑
	/// </summary>
	public partial class Window1 : Window
	{
		public Window1()
		{
			this.InitializeComponent();
			
			// 在此点之下插入创建对象所需的代码。
		}

		private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			Image myImage = new Image();
            FormattedText text = new FormattedText("ABC",
                    new CultureInfo("en-us"),
                    FlowDirection.LeftToRight,
                    new Typeface(this.FontFamily, FontStyles.Normal, FontWeights.Normal, new FontStretch()),
                    this.FontSize,
                    this.Foreground);

            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            drawingContext.DrawText(text, new Point(2, 2));
            drawingContext.Close();

            RenderTargetBitmap bmp = new RenderTargetBitmap(180, 180, 120, 96, PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);
            myImage.Source = bmp;

            // Add Image to the UI
            StackPanel myStackPanel = new StackPanel();
            myStackPanel.Children.Add(myImage);
            this.Content = myStackPanel;
		}
	}
}