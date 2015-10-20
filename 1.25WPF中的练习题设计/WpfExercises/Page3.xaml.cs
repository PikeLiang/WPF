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

namespace WpfExercises
{
	public partial class Page3
	{
		ClassVariable fenx=new ClassVariable();
		private int i,j;
		public Page3()
		{
			this.InitializeComponent();
            this.tb1.DataContext=fenx;
		}
		 //第1题
		private void dt1_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			RadioButton rb=sender as RadioButton; 
			MainWindow.panduanYesNo[1]="×";
			this.grid1.IsEnabled=false;
			i=1;
			switch (rb.Name){
				case "dt1_1":
				this.panduanjfen();
				  MainWindow.panduanYesNo[1]="√";
			      j=1;
				  break;
				case "dt1_2":
			      j=2;
				  break;
			}
			this.pdt1.Text=MainWindow.panduanYesNo[1];
			this.panduanjs();
		}
        //第2题
		private void dt2_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			RadioButton rb=sender as RadioButton; 
			MainWindow.panduanYesNo[2]="×";
			this.grid2.IsEnabled=false;
			i=2;
			switch (rb.Name){
				case "dt2_1":				  
			      j=1;
				  break;
				case "dt2_2":
				  this.panduanjfen();
				  MainWindow.panduanYesNo[2]="√";
			      j=2;
				  break;
			}
			this.pdt2.Text=MainWindow.panduanYesNo[2];
			this.panduanjs();
		}
        //第3题
		private void dt3_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			RadioButton rb=sender as RadioButton; 
			MainWindow.panduanYesNo[3]="×";
			this.grid3.IsEnabled=false;
			i=3;
			switch (rb.Name){
				case "dt3_1":				  
			      j=1;
				  break;
				case "dt3_2":
				  this.panduanjfen();
				  MainWindow.panduanYesNo[3]="√";
			      j=2;
				  break;
			}
			this.pdt3.Text=MainWindow.panduanYesNo[3];
			this.panduanjs();
		}
        //第4题
		private void dt4_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			RadioButton rb=sender as RadioButton; 
			MainWindow.panduanYesNo[4]="×";
			this.grid4.IsEnabled=false;
			i=4;
			switch (rb.Name){
				case "dt4_1":				  
			      j=1;
				  break;
				case "dt4_2":
				  this.panduanjfen();
				  MainWindow.panduanYesNo[4]="√";
			      j=2;
				  break;
			}
			this.pdt4.Text=MainWindow.panduanYesNo[4];
			this.panduanjs();
		}
        private void panduanjfen(){
			fenx.Panduanfenxj();
			fenx.NotifyPropertyChanged("Panduanfenx");
			//fenx.NotifyPropertyChanged("Zongfenx");
			MainWindow.mw.panduanhjtext.Text=WpfExercises.ClassVariable.Panduanfenx.ToString();
			MainWindow.mw.zongfenhjtext.Text=WpfExercises.ClassVariable.Zongfenx.ToString();
			MainWindow.panduanright=MainWindow.panduanright+1;//正确题计数
			MainWindow.mw.panduanRight.Text=MainWindow.panduanright.ToString();
		}
		private void panduanjs(){
			MainWindow.panduanenter=MainWindow.panduanenter+1;//已做题计数
			MainWindow.mw.panduanEnter.Text=MainWindow.panduanenter.ToString();
			MainWindow.panduanFinished[i]=1;//已做题标志
			MainWindow.panduanSelected[i]=j;//选择的题号标志
		}
		private void Page_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			//第1题选择判断********
			if(MainWindow.panduanFinished[1]==1){
			  this.grid1.IsEnabled=false;
			  this.pdt1.Text=MainWindow.panduanYesNo[1];
			  if (MainWindow.panduanSelected[1]==1){
				  this.dt1_1.IsChecked=true;}
			  else
				  this.dt1_2.IsChecked=true;
			}
			//第2题选择判断********
			if(MainWindow.panduanFinished[2]==1){
			  this.grid2.IsEnabled=false;
			  this.pdt2.Text=MainWindow.panduanYesNo[2];
			  if (MainWindow.panduanSelected[2]==1){
				  this.dt2_1.IsChecked=true;}
			  else
				  this.dt2_2.IsChecked=true;
			}
			//第3题选择判断********
			if(MainWindow.panduanFinished[3]==1){
			  this.grid3.IsEnabled=false;
			  this.pdt3.Text=MainWindow.panduanYesNo[3];
			  if (MainWindow.panduanSelected[3]==1){
				  this.dt3_1.IsChecked=true;}
			  else
				  this.dt3_2.IsChecked=true;
			}
			//第4题选择判断********
			if(MainWindow.panduanFinished[4]==1){
			  this.grid4.IsEnabled=false;
			  this.pdt4.Text=MainWindow.panduanYesNo[4];
			  if (MainWindow.panduanSelected[4]==1){
				  this.dt4_1.IsChecked=true;}
			  else
				  this.dt4_2.IsChecked=true;
			}
		}
       
				
	}
}