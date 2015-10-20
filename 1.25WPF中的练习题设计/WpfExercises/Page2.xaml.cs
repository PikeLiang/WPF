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
	public partial class Page2
	{
		ClassVariable fenx=new ClassVariable();
		private int i;
		public Page2()
		{
			this.InitializeComponent();
            this.tb1.DataContext=fenx;
		}
		//第1题********************
		private void bt1_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			i=1;
			MainWindow.duoxuanYesNo[1]="×";
			if (this.cb1_1.IsChecked==true){
			 MainWindow.duoxuanSelected[0,0]=1;
			}
			if (this.cb1_2.IsChecked==true){
			 MainWindow.duoxuanSelected[0,1]=1;
			}
			if (this.cb1_3.IsChecked==true){
			 MainWindow.duoxuanSelected[0,2]=1;
			}
			if (this.cb1_4.IsChecked==true){
			 MainWindow.duoxuanSelected[0,3]=1;
			}
			this.duoxuanjs();
			this.grid1.IsEnabled=false;
			if (this.cb1_2.IsChecked==true && this.cb1_3.IsChecked==true && this.cb1_1.IsChecked==false && this.cb1_4.IsChecked==false){
			   this.duoxuanjfen();
			   MainWindow.duoxuanYesNo[1]="√";
			}
			this.dxan1.Text=MainWindow.duoxuanYesNo[1];
		}
        //第2题********************
		private void bt2_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			i=2;
			MainWindow.duoxuanYesNo[2]="×";
			if (this.cb2_1.IsChecked==true){
			 MainWindow.duoxuanSelected[1,0]=1;
			}
			if (this.cb2_2.IsChecked==true){
			 MainWindow.duoxuanSelected[1,1]=1;
			}
			if (this.cb2_3.IsChecked==true){
			 MainWindow.duoxuanSelected[1,2]=1;
			}
			if (this.cb2_4.IsChecked==true){
			 MainWindow.duoxuanSelected[1,3]=1;
			}
			this.duoxuanjs();
			this.grid2.IsEnabled=false;
			if (this.cb2_1.IsChecked==true && this.cb2_2.IsChecked==true && this.cb2_3.IsChecked==false && this.cb2_4.IsChecked==false){
			   this.duoxuanjfen(); 
			   MainWindow.duoxuanYesNo[2]="√";
			}
			this.dxan2.Text=MainWindow.duoxuanYesNo[2];
		}
        //第3题********************
		private void bt3_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			i=3;
			MainWindow.duoxuanYesNo[3]="×";
			if (this.cb3_1.IsChecked==true){
			 MainWindow.duoxuanSelected[2,0]=1;
			}
			if (this.cb3_2.IsChecked==true){
			 MainWindow.duoxuanSelected[2,1]=1;
			}
			if (this.cb3_3.IsChecked==true){
			 MainWindow.duoxuanSelected[2,2]=1;
			}
			if (this.cb3_4.IsChecked==true){
			 MainWindow.duoxuanSelected[2,3]=1;
			}
			this.duoxuanjs();
			this.grid3.IsEnabled=false;
			if (this.cb3_1.IsChecked==true && this.cb3_4.IsChecked==true && this.cb3_2.IsChecked==false && this.cb3_3.IsChecked==false){
			   this.duoxuanjfen(); 
			   MainWindow.duoxuanYesNo[3]="√";
			}
			this.dxan3.Text=MainWindow.duoxuanYesNo[3];
		}
        //第4题********************
		private void bt4_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			i=4;
			MainWindow.duoxuanYesNo[4]="×";
			if (this.cb4_1.IsChecked==true){
			 MainWindow.duoxuanSelected[3,0]=1;
			}
			if (this.cb4_2.IsChecked==true){
			 MainWindow.duoxuanSelected[3,1]=1;
			}
			if (this.cb4_3.IsChecked==true){
			 MainWindow.duoxuanSelected[3,2]=1;
			}
			if (this.cb4_4.IsChecked==true){
			 MainWindow.duoxuanSelected[3,3]=1;
			}
			this.duoxuanjs();
			this.grid4.IsEnabled=false;
			if (this.cb4_1.IsChecked==true && this.cb4_3.IsChecked==true && this.cb4_4.IsChecked==true && this.cb4_2.IsChecked==false){
			   this.duoxuanjfen();
			   MainWindow.duoxuanYesNo[4]="√";			   
			}
			this.dxan4.Text=MainWindow.duoxuanYesNo[4];
		}
        private void duoxuanjfen(){
			fenx.Duoxuanfenxj();
			fenx.NotifyPropertyChanged("Duoxuanfenx");
			//fenx.NotifyPropertyChanged("Zongfenx");
			WpfExercises.MainWindow.mw.duoxuanhjtext.Text=WpfExercises.ClassVariable.Duoxuanfenx.ToString();
			WpfExercises.MainWindow.mw.zongfenhjtext.Text=WpfExercises.ClassVariable.Zongfenx.ToString();
			MainWindow.duoxuanright=MainWindow.duoxuanright+1;//正确题计数
			MainWindow.mw.duoxuanRight.Text=MainWindow.duoxuanright.ToString();
		}
		private void duoxuanjs(){
			MainWindow.duoxuanenter=MainWindow.duoxuanenter+1;//已做题计数
			MainWindow.mw.duoxuanEnter.Text=MainWindow.duoxuanenter.ToString();
			MainWindow.duoxuanFinished[i]=1;//已做题标志
		}
		
		private void Page_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			//第1题选择判断********
			if(MainWindow.duoxuanFinished[1]==1){
			  this.grid1.IsEnabled=false;
			  if(MainWindow.duoxuanSelected[0,0]==1){
			      this.cb1_1.IsChecked=true;}
			  if(MainWindow.duoxuanSelected[0,1]==1){
			      this.cb1_2.IsChecked=true;}
			  if(MainWindow.duoxuanSelected[0,2]==1){
			      this.cb1_3.IsChecked=true;}
			  if(MainWindow.duoxuanSelected[0,3]==1){
			      this.cb1_4.IsChecked=true;}
			  this.dxan1.Text=MainWindow.duoxuanYesNo[1];
			}
			//第2题选择判断********
			if(MainWindow.duoxuanFinished[2]==1){
			  this.grid2.IsEnabled=false;
			  if(MainWindow.duoxuanSelected[1,0]==1){
			      this.cb2_1.IsChecked=true;}
			  if(MainWindow.duoxuanSelected[1,1]==1){
			      this.cb2_2.IsChecked=true;}
			  if(MainWindow.duoxuanSelected[1,2]==1){
			      this.cb2_3.IsChecked=true;}
			  if(MainWindow.duoxuanSelected[1,3]==1){
			      this.cb2_4.IsChecked=true;}
			   this.dxan2.Text=MainWindow.duoxuanYesNo[2];
			}
			//第3题选择判断********
			if(MainWindow.duoxuanFinished[3]==1){
			  this.grid3.IsEnabled=false;
			  if(MainWindow.duoxuanSelected[2,0]==1){
			      this.cb3_1.IsChecked=true;}
			  if(MainWindow.duoxuanSelected[2,1]==1){
			      this.cb3_2.IsChecked=true;}
			  if(MainWindow.duoxuanSelected[2,2]==1){
			      this.cb3_3.IsChecked=true;}
			  if(MainWindow.duoxuanSelected[2,3]==1){
			      this.cb3_4.IsChecked=true;}
			  this.dxan3.Text=MainWindow.duoxuanYesNo[3];
			}
			//第4题选择判断********
			if(MainWindow.duoxuanFinished[4]==1){
			  this.grid4.IsEnabled=false;
			  if(MainWindow.duoxuanSelected[3,0]==1){
			      this.cb4_1.IsChecked=true;}
			  if(MainWindow.duoxuanSelected[3,1]==1){
			      this.cb4_2.IsChecked=true;}
			  if(MainWindow.duoxuanSelected[3,2]==1){
			      this.cb4_3.IsChecked=true;}
			  if(MainWindow.duoxuanSelected[3,3]==1){
			      this.cb4_4.IsChecked=true;}
			   this.dxan4.Text=MainWindow.duoxuanYesNo[4];
			}	
		}
		
	}
}