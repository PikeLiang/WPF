using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;

using System.Windows.Printing;//for PrintDocument
//using System.Windows.Media.Imaging;//for BitmapImage
namespace SilverlightPrint
{
    public partial class Page1 : Page
    {
		PrintDocument printImage,printText;
        public Page1()
        {
            InitializeComponent();
			printImage = new PrintDocument();
            printImage.PrintPage += new EventHandler<PrintPageEventArgs>(printImage_PrintPage);
			printText = new PrintDocument();
            printText.PrintPage += new EventHandler<PrintPageEventArgs>(printText_PrintPage);
        }

        // 当用户导航到此网页时执行。
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void tb1_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        	this.Content=new MainPage();
        }
		
		private void BTprintImage_Click(object sender, System.Windows.RoutedEventArgs e)
        {

			printImage.Print("iamge1");
        }
		
		void printImage_PrintPage(object sender, PrintPageEventArgs e)
       {
		    //确认打印,打印对象
            e.PageVisual = image1;		    
        }
	    StackPanel printarea;
		private void BTprintText_Click(object sender, System.Windows.RoutedEventArgs e)
        {
			int length=this.textblock1.Text.TrimEnd().Length;
			int w=18;
			printarea=new StackPanel();
			for (int i=0;i<=length;i=i+w)
			    {
			        if (this.textblock1.Text.Trim().Substring(i).Length>=w){
				       printarea.Children.Add(new TextBlock(){Text=this.textblock1.Text.TrimEnd().Substring(i,w), 
                       FontFamily=new System.Windows.Media.FontFamily("Arial"),FontSize=12}); 
		            }else
			       {
			           printarea.Children.Add(new TextBlock(){Text=this.textblock1.Text.TrimEnd().Substring(i), 
                       FontFamily=new System.Windows.Media.FontFamily("Arial"),FontSize=12}); 
			        }
			    }
			printText.Print("文本打印");//启动打印			
        }
		void printText_PrintPage(object sender, PrintPageEventArgs e)
        {
		    e.PageVisual =printarea;//开始打印
        }
    }
}
