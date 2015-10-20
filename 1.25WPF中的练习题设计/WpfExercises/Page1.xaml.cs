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
	public partial class Page1
	{
		ClassVariable fenx=new ClassVariable();
		private int i,j;
		public Page1()
		{
			this.InitializeComponent();
			this.tb1.DataContext=fenx;
		}
		//第1题*****************************************
		private void dt1_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			RadioButton rb=sender as RadioButton; 
			MainWindow.danxuanYesNo[1]="×";
			this.grid1.IsEnabled=false;
			i=1;
			switch (rb.Name){
				case "dt1_1":
			      j=1;
				  break;
				case "dt1_2":
			      j=2;
				  break;
				case "dt1_3":
				  this.danxuanjfen();
			      MainWindow.danxuanYesNo[1]="√";
			      j=3;
				  break;
				case "dt1_4":
			      j=4;
				  break;
			}
			this.dxt1.Text=MainWindow.danxuanYesNo[1];
			this.danxuanjs();
		}
		
		//第2题*****************************************
		private void dt2_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			RadioButton rb=sender as RadioButton; 
			MainWindow.danxuanYesNo[2]="×";
			this.grid2.IsEnabled=false;
			i=2;
			switch (rb.Name){
				case "dt2_1":
				  this.danxuanjfen();
			      MainWindow.danxuanYesNo[2]="√";
			      j=1;
				  break;
				case "dt2_2":
			      j=2;
				  break;
				case "dt2_3":
			      j=3;
				  break;
				case "dt2_4":
			      j=4;
				  break;
			}
			this.dxt2.Text=MainWindow.danxuanYesNo[2];
			this.danxuanjs();			
		}
		//第3题*****************************************
		private void dt3_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			RadioButton rb=sender as RadioButton; 
			MainWindow.danxuanYesNo[3]="×";
			this.grid3.IsEnabled=false;
			i=3;
			switch (rb.Name){
				case "dt3_1":
			      j=1;
				  break;
				case "dt3_2":
				  this.danxuanjfen();
			      MainWindow.danxuanYesNo[3]="√";
			      j=2;
				  break;
				case "dt3_3":
			      j=3;
				  break;
				case "dt3_4":
			      j=4;
				  break;
			}
			this.dxt3.Text=MainWindow.danxuanYesNo[3];
			this.danxuanjs();			
		}
		
        //第4题*****************************************
		private void dt4_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			RadioButton rb=sender as RadioButton; 
			MainWindow.danxuanYesNo[4]="×";
			this.grid4.IsEnabled=false;
			i=4;
			switch (rb.Name){
				case "dt4_1":
			      j=1;
				  break;
				case "dt4_2":				  
			      j=2;
				  break;
				case "dt4_3":
			      j=3;
				  break;
				case "dt4_4":
				  this.danxuanjfen();
			      MainWindow.danxuanYesNo[4]="√";
			      j=4;
				  break;
			}
			this.dxt4.Text=MainWindow.danxuanYesNo[4];
			this.danxuanjs();	
		}
		//第5题*****************************************
		private void dt5_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			RadioButton rb=sender as RadioButton; 
			MainWindow.danxuanYesNo[5]="×";
			this.grid5.IsEnabled=false;
			i=5;
			switch (rb.Name){
				case "dt5_1":
				  this.danxuanjfen();
			      MainWindow.danxuanYesNo[5]="√";
			      j=1;
				  break;
				case "dt4_2":				  
			      j=2;
				  break;
				case "dt5_3":
			      j=3;
				  break;
				case "dt5_4":				  
			      j=4;
				  break;
			}
			this.dxt5.Text=MainWindow.danxuanYesNo[5];
			this.danxuanjs();	
		}
        //第6题*****************************************
		private void dt6_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			RadioButton rb=sender as RadioButton; 
			MainWindow.danxuanYesNo[6]="×";
			this.grid6.IsEnabled=false;
			i=6;
			switch (rb.Name){
				case "dt6_1":				  
			      j=1;
				  break;
				case "dt6_2":
				  this.danxuanjfen();
			      MainWindow.danxuanYesNo[6]="√";
			      j=2;
				  break;
				case "dt6_3":
			      j=3;
				  break;
				case "dt6_4":				  
			      j=4;
				  break;
			}
			this.dxt6.Text=MainWindow.danxuanYesNo[6];
			this.danxuanjs();
		}
		private void danxuanjfen(){
			fenx.Danxuanfenxj();
			fenx.NotifyPropertyChanged("Danxuanfenx");//单选题分更新
			//fenx.NotifyPropertyChanged("Zongfenx");//总分更新
			MainWindow.mw.danxuanhjtext.Text=WpfExercises.ClassVariable.Danxuanfenx.ToString();			
			MainWindow.mw.zongfenhjtext.Text=WpfExercises.ClassVariable.Zongfenx.ToString();
			MainWindow.danxuanright=MainWindow.danxuanright+1;//正确题计数
			MainWindow.mw.danxuanRight.Text=MainWindow.danxuanright.ToString();
		}
		private void danxuanjs(){
			MainWindow.danxuanenter=MainWindow.danxuanenter+1;//已做题计数
			MainWindow.mw.danxuanEnter.Text=MainWindow.danxuanenter.ToString();
			MainWindow.danxuanFinished[i]=1;//已做题标志
			MainWindow.danxuanSelected[i]=j;//选择的题号标志
		}
		
		private void Page_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			//第1题选择判断********
			if(MainWindow.danxuanFinished[1]==1){
			  this.grid1.IsEnabled=false;
			  this.dxt1.Text=MainWindow.danxuanYesNo[1];
			  switch (MainWindow.danxuanSelected[1]){
				case 1:
				    this.dt1_1.IsChecked=true;
				    break;
				case 2:
					this.dt1_2.IsChecked=true;
				    break;
				case 3:
					this.dt1_3.IsChecked=true;
				    break;
				case 4:
					this.dt1_4.IsChecked=true;
				    break;
			  }
			}
			//第2题选择判断********
			if(MainWindow.danxuanFinished[2]==1){
			  this.grid2.IsEnabled=false;
			  this.dxt2.Text=MainWindow.danxuanYesNo[2];
			  switch (MainWindow.danxuanSelected[2]){
				case 1:
				    this.dt2_1.IsChecked=true;
				    break;
				case 2:
					this.dt2_2.IsChecked=true;
				    break;
				case 3:
					this.dt2_3.IsChecked=true;
				    break;
				case 4:
					this.dt2_4.IsChecked=true;
				    break;
			  }
			}
			//第3题选择判断********
			if(MainWindow.danxuanFinished[3]==1){
			  this.grid3.IsEnabled=false;
			  this.dxt3.Text=MainWindow.danxuanYesNo[3];
			  //System.Windows.MessageBox.Show(MainWindow.danxuanSelected[3].ToString());
			  switch (MainWindow.danxuanSelected[3]){
				case 1:
				    this.dt3_1.IsChecked=true;
				    break;
				case 2:
					this.dt3_2.IsChecked=true;
				    break;
				case 3:
					this.dt3_3.IsChecked=true;
				    break;
				case 4:
					this.dt3_4.IsChecked=true;
				    break;
			  }
			}
			//第4题选择判断********
			if(MainWindow.danxuanFinished[4]==1){
			  this.grid4.IsEnabled=false;
			  this.dxt4.Text=MainWindow.danxuanYesNo[4];
			  switch (MainWindow.danxuanSelected[4]){
				case 1:
				    this.dt4_1.IsChecked=true;
				    break;
				case 2:
					this.dt4_2.IsChecked=true;
				    break;
				case 3:
					this.dt4_3.IsChecked=true;
				    break;
				case 4:
					this.dt4_4.IsChecked=true;
				    break;
			  }
			}
			//第5题选择判断********
			if(MainWindow.danxuanFinished[5]==1){
			  this.grid5.IsEnabled=false;
			  this.dxt5.Text=MainWindow.danxuanYesNo[5];
			  switch (MainWindow.danxuanSelected[5]){
				case 1:
				    this.dt5_1.IsChecked=true;
				    break;
				case 2:
					this.dt5_2.IsChecked=true;
				    break;
				case 3:
					this.dt5_3.IsChecked=true;
				    break;
				case 4:
					this.dt5_4.IsChecked=true;
				    break;
			  }
			}
			//第6题选择判断********
			if(MainWindow.danxuanFinished[6]==1){
			  this.grid6.IsEnabled=false;
			  this.dxt6.Text=MainWindow.danxuanYesNo[6];
			  switch (MainWindow.danxuanSelected[6]){
				case 1:
				    this.dt6_1.IsChecked=true;
				    break;
				case 2:
					this.dt6_2.IsChecked=true;
				    break;
				case 3:
					this.dt6_3.IsChecked=true;
				    break;
				case 4:
					this.dt6_4.IsChecked=true;
				    break;
			  }
			}	
		}
		
	}
}