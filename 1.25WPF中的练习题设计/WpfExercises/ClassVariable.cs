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

using System.ComponentModel;//for INotifyPropertyChanged
namespace WpfExercises
{
	public class ClassVariable:INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public void NotifyPropertyChanged(string propertyname){
		   if (PropertyChanged!=null){
			  PropertyChanged(this,new PropertyChangedEventArgs(propertyname));
		   }
		}
		private static int danxuanfenx=0,duoxuanfenx=0,panduanfenx=0,zongfenx=0;
		public static int Danxuanfenx{    //定义属性Danxuanfenx
		   get {return danxuanfenx;}
		   set {danxuanfenx=value;
		        //NotifyPropertyChanged("Danxuanfenx");//这仅对非静态属性，对于静态属性需实例化对象调用,不能在此用
		       }
		}
	    public static int Duoxuanfenx{    //定义属性Duoxuanfenx
		   get {return duoxuanfenx;}
		   set {duoxuanfenx=value;}
		}
		public static int Panduanfenx{    //定义属性Panduanfenx
		   get {return panduanfenx;}
		   set {panduanfenx=value;}
		}
		public static int Zongfenx{    //定义属性Zongfenx
		   get {return zongfenx;}
		   set {zongfenx=value;}
		}
		
		//构造函数，属性值初始化
		public ClassVariable(){
		    Danxuanfenx=danxuanfenx;
		    Duoxuanfenx=duoxuanfenx;
		    Panduanfenx=panduanfenx;
		    Zongfenx=zongfenx;
		}
		//单选题分和总分增加
		public int Danxuanfenxj(){
		   Danxuanfenx=Danxuanfenx+MainWindow.fenmt;
		   Zongfenx=Zongfenx+MainWindow.fenmt;
		   return Zongfenx;
		}
		//多选题分和总分增加
		public int Duoxuanfenxj(){
		   Duoxuanfenx=Duoxuanfenx+MainWindow.fenmt;
		   Zongfenx=Zongfenx+MainWindow.fenmt;
		   return Zongfenx;
		}
		//判断题分和总分增加
		public int Panduanfenxj(){
		   Panduanfenx=Panduanfenx+MainWindow.fenmt;
		   Zongfenx=Zongfenx+MainWindow.fenmt;
		   return Zongfenx;
		}
	}
}