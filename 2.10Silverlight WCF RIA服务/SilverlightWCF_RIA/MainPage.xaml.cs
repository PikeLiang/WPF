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

//添加引用
using SilverlightWCF_RIA.Web;// for DomainServiceRIA
using System.ServiceModel.DomainServices.Client;//for LoadOperation
using System.IO;//for Stream
using System.Windows.Media.Imaging;//for BitmapImage
using System.Windows.Browser;//for HtmlPage
namespace SilverlightWCF_RIA
{
    public partial class MainPage : UserControl		
    {
        DomainServiceRIA DSRia=new DomainServiceRIA();
		student sd;
		public MainPage()
        {
			InitializeComponent();
			this.datagrid.RowHeight=25;			
			GetStudentList();
        }
		///更新表中直接修改数据
		private void b1_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			var k=System.Windows.MessageBox.Show("更新修改的数据吗？","确认",MessageBoxButton.OKCancel);	
			if (k.ToString()=="OK"){
				DSRia.SubmitChanges();
				System.Windows.MessageBox.Show("更新完成！");
			}
		}
		 //取消更新
		private void b2_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			var k=System.Windows.MessageBox.Show("取消对表中数据的修改吗？","确认",MessageBoxButton.OKCancel);			
			if (k.ToString()=="OK")
			  DSRia.RejectChanges();
		}
		//修改记录（利用文本框输入的数据）
		private void b9_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			string xhss=this.textbox1.Text.Trim();
			if (xhss.Length==0){
				HtmlPage.Window.Alert("需要输入编号！");
			    return;
			}	            
			if (Checkbianhao(xhss)){
			   HtmlPage.Window.Alert("编号只能是数字，输入有非法字符，请检查！");
			   return;
			}
			int ids=int.Parse(xhss);
			int res =(from ss in DSRia.students where ss.id ==ids select ss).Count();
			if (res==0){
			    HtmlPage.Window.Alert("没有你要修改的记录");
			    return;
			}
			string kfs=this.textbox5.Text.Trim();
			if (Checkkaofen(kfs)){
			   HtmlPage.Window.Alert("输入的考分中有非法字符，请检查！");
			   return;
			}
			var k=System.Windows.MessageBox.Show("修改“"+this.textbox2.Text.Trim()+"”的数据吗？","确认",MessageBoxButton.OKCancel);			
			if (k.ToString()!="OK"){
			   return;
			}
			sd =(from ss in DSRia.students where ss.id ==ids select ss).First();
			sd.xingming=this.textbox2.Text.Trim();
			sd.xingbie=this.textbox3.Text.Trim();
			sd.zhuanye=this.textbox4.Text.Trim();
			sd.kaofen=float.Parse(kfs);
			sd.dianhua=this.textbox6.Text.Trim();
			sd.jiguan=this.textbox7.Text.Trim();
			//sd.zhaopian=null;
			DSRia.SubmitChanges();
		}			
        //添加新纪录
		private void b3_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			string xhss=this.textbox1.Text.Trim();
			string kfs=this.textbox5.Text.Trim();
			if (xhss.Length==0){
				HtmlPage.Window.Alert("需要输入编号！");
			    return;
			}
			if (kfs.Length ==0){
				HtmlPage.Window.Alert("需要输入考分！");
			    return;
			}
			if (Checkbianhao(xhss)){
			   HtmlPage.Window.Alert("编号只能是数字，输入有非法字符，请检查！");
			   return;
			}
			int ids=int.Parse(xhss);
			int res =(from ss in DSRia.students where ss.id ==ids select ss).Count();
			if (res!=0){
			    HtmlPage.Window.Alert("编号重复，请重新输入！");
			    return;
			}
			//string kfs=this.textbox5.Text.Trim();
			if (Checkkaofen(kfs)){
			   HtmlPage.Window.Alert("输入的考分中有非法字符，请检查！");
			   return;
			}
			//student是在建立数据库连接时自动创建的类
			student sd=new student();
			sd.id=ids;
			sd.xingming=this.textbox2.Text.Trim();
			sd.xingbie=this.textbox3.Text.Trim();
			sd.zhuanye=this.textbox4.Text.Trim();
			sd.kaofen=float.Parse(kfs);
			sd.dianhua=this.textbox6.Text.Trim();
			sd.jiguan=this.textbox7.Text.Trim();
			DSRia.students.Add(sd);
			//计算要插入、更新或删除的已修改对象的集，并执行相应命令以实现对数据库的更改。
			DSRia.SubmitChanges();            
			System.Windows.MessageBox.Show("添加完成,选择【重新加载】查看结果！");
		}
		//删除记录
		private void b4_Click(object sender, System.Windows.RoutedEventArgs e)
		{
		    string xhss=this.textbox1.Text.Trim();
			if (xhss.Length==0){
				HtmlPage.Window.Alert("需要输入编号！");
			    return;
			}	            
			if (Checkbianhao(xhss)){
			   HtmlPage.Window.Alert("编号只能是数字，输入有非法字符，请检查！");
			   return;
			}
			int ids=int.Parse(xhss);
			int res =(from ss in DSRia.students where ss.id ==ids select ss).Count();
			if (res==0){
			    HtmlPage.Window.Alert("没有你要删除的记录");
			    return;
			}
			var k=System.Windows.MessageBox.Show("删除“"+ids.ToString()+"”号的数据吗？","确认",MessageBoxButton.OKCancel);			
			if (k.ToString()!="OK")
				return;
			//DSRia.students.Remove(this.datagrid.SelectedItem as student);	//或使用下面的3条语句
			student shanchu =(from ss in DSRia.students where ss.id ==ids select ss).First();
			DSRia.students.Remove(shanchu);
			DSRia.SubmitChanges();
			System.Windows.MessageBox.Show("删除完成,选择【重新加载】查看结果！");   
		}
		//重新加载		
		private void b5_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			GetStudentList();
			//return;
			//LoadOperation<student> loadstudent = DSRia.Load(DSRia.GetStudentQuery());
			//this.datagrid.ItemsSource = loadstudent.Entities;
			//this.listbox.ItemsSource = loadstudent.Entities;
		}
		//高分查询
		private void b6_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			string kfs=this.textbox5.Text.Trim();
			if (Checkkaofen(kfs)){
			   HtmlPage.Window.Alert("输入的考分中有非法字符，请检查！");
			   return;
			}
			float kf=float.Parse(kfs);
			var studentList =from ss in DSRia.students where ss.kaofen>kf select ss;
			this.datagrid.ItemsSource=studentList;
		}
        //取消查询
		private void b7_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			var studentList =from ss in DSRia.students select ss;
			this.datagrid.ItemsSource=studentList;
		}
		//获取数据库表数据
		private void GetStudentList(){
			LoadOperation<student> loadstudent = DSRia.Load(DSRia.GetStudentQuery());
			DSRia.Load(DSRia.GetStudentQuery()).Completed += (SS,EE) => {
				this.datagrid.ItemsSource = loadstudent.Entities;				
				this.tb1.Text="数据库表记录总数: "+DSRia.students.Count.ToString();
				this.listbox.ItemsSource=loadstudent.Entities;
			};			
		}                
		//考分输入校验
		private bool Checkkaofen(string st){
            int i;
            char[] chars = st.ToCharArray();
			for (i = 0; i <chars.Length; i++){
			int j=System.Convert.ToInt32(chars[i]);
            //字符编码值（0和9的ASCII码分别是48、57，46是小数点，否则是非法字符）
			if (j > 57||j<48&&j!=46)
            {
                return true;
            }
           }
           return false;
		}
		//编号输入校验
		private bool Checkbianhao(string st){
            int i;
            char[] chars = st.ToCharArray();
			for (i = 0; i <chars.Length; i++){
			  int j=System.Convert.ToInt32(chars[i]);
            //字符编码值（0和9的ASCII码分别是48、57，否则是非法字符）
			  if (j > 57||j < 48)
              {
                return true;//有非法字符
              }
            }
           	return false;//无
		}
        //添加照片		
		private void b8_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			string xhss=this.textbox1.Text.Trim();
			if (xhss.Length==0){
				HtmlPage.Window.Alert("需要输入编号！");
			    return;
			}	            
			if (Checkbianhao(xhss)){
			   HtmlPage.Window.Alert("编号只能是数字，输入有非法字符，请检查！");
			   return;
			}
			int ids=int.Parse(xhss);
			int res =(from ss in DSRia.students where ss.id ==ids select ss).Count();
			if (res==0){
			    HtmlPage.Window.Alert("没有你要添加照片的记录");
			    return;
			}
			OpenFileDialog openjpg = new OpenFileDialog();
			openjpg.Filter="选择照片 *.jpg|*.jpg";
			if (openjpg.ShowDialog()==true){
			   if (openjpg.File.Name!=""){
				  this.tb1.Text="选择的照片:"+openjpg.File.Name;
				  Stream jpgstream=openjpg.File.OpenRead();
				  byte[] bytes = new byte[jpgstream.Length]; 
                  jpgstream.Read(bytes, 0, bytes.Length); 
			      BitmapImage bi=new BitmapImage();
				  bi.SetSource(jpgstream);
                  this.image.Source=bi;
				  student sd =(from ss in DSRia.students where ss.id ==ids select ss).First();
				  //字节数据加入，没有显示
				  sd.zhaopian=bytes;
				  DSRia.SubmitChanges();
			   }
			}
		}
		//选择记录
		private void listbox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			sd=this.listbox.SelectedItem as student;
            this.textbox1.Text=sd.id.ToString();
			this.textbox2.Text=sd.xingming;
			this.textbox3.Text=sd.xingbie;
			this.textbox4.Text=sd.zhuanye;
			this.textbox5.Text=sd.kaofen.ToString();
			this.textbox6.Text=sd.dianhua;
			this.textbox7.Text=sd.jiguan;
			sd =(from ss in DSRia.students where ss.id ==sd.id select ss).First();
			if (sd.zhaopian!=null){
				byte [] bytes=sd.zhaopian;
			    MemoryStream ms = new MemoryStream(bytes);
			    BitmapImage bi=new BitmapImage();
				bi.SetSource(ms);
                this.image.Source=bi;
			}else{
				this.image.Source=null;
			}
			this.datagrid.SelectedIndex=this.listbox.SelectedIndex;
			this.datagrid.Focus();
		}
        
    }
}