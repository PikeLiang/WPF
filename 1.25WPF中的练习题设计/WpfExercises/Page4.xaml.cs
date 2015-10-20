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

using System.Xml;
namespace WpfExercises
{
	public partial class Page4
	{
		public Page4()
		{
			this.InitializeComponent();
		}
        		
		private void datagrid1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			this.deletecurrent.IsEnabled=true;
		}	
		
		private void deletecurrent_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			//取当前选择行date属性值
			string cell=this.datagrid1.Columns[0].GetCellContent(this.datagrid1.SelectedItem).GetValue(TextBlock.TextProperty).ToString();
		    System.Windows.Data.XmlDataProvider jiluy =this.FindResource("ExercisesDataSource") as XmlDataProvider;
			XmlNode selectnode =jiluy.Document.SelectSingleNode("exercises");//xml文件的根结点
			XmlNodeList nodelist =jiluy.Document.SelectSingleNode("exercises").ChildNodes;//获得xml文件根结点下的所有子节点
            //遍历所有节点
			foreach (XmlNode nodem in nodelist){
			  //转换xml文件抽象型节点为行元素型节点，便于行数据属性访问
			  XmlElement nodex = (XmlElement)nodem;
			  if (nodex.GetAttribute("date") == cell)//如果节点date属性为选择项
              {
                //移除满足条件节点
				selectnode.RemoveChild(nodex);
				jiluy.Document.Save("xml/Exercises.xml");
				this.deletecurrent.IsEnabled=false;
              }
			}	
		}
		
		private void deleteall_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			var key=System.Windows.MessageBox.Show("删除全部数据吗？","确认",MessageBoxButton.OKCancel,MessageBoxImage.Question,MessageBoxResult.Cancel);
			if (key.ToString()!="OK"){
			return;}
			System.Windows.Data.XmlDataProvider jiluy =this.FindResource("ExercisesDataSource") as XmlDataProvider;
			XmlNode selectnode =jiluy.Document.SelectSingleNode("exercises");//xml文件的根节点
			selectnode.RemoveAll();
			jiluy.Document.Save("xml/Exercises.xml");
			this.deleteall.IsEnabled=false;
		}
		
		private void save_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			System.Windows.Data.XmlDataProvider jiluy =this.FindResource("ExercisesDataSource") as XmlDataProvider;
			XmlNode selectnode =jiluy.Document.SelectSingleNode("exercises");//xml文件的根节点
            XmlElement newnode = jiluy.Document.CreateElement ("exercise");//设置节点名
			DateTime datetime=DateTime.Now;
			newnode.SetAttribute("date",datetime.ToString("f"));//不显示秒
			newnode.SetAttribute("danxuanti",MainWindow.mw.danxuanhjtext.Text);
			newnode.SetAttribute("xz1",MainWindow.mw.danxuanEnter.Text);
			newnode.SetAttribute("rt1",MainWindow.mw.danxuanRight.Text);
			newnode.SetAttribute("duoxuanti",MainWindow.mw.duoxuanhjtext.Text);
			newnode.SetAttribute("xz2",MainWindow.mw.danxuanEnter.Text);
			newnode.SetAttribute("rt2",MainWindow.mw.duoxuanRight.Text);
			newnode.SetAttribute("panduanti",MainWindow.mw.panduanhjtext.Text);
			newnode.SetAttribute("xz3",MainWindow.mw.panduanEnter.Text);
			newnode.SetAttribute("rt3",MainWindow.mw.panduanRight.Text);
			newnode.SetAttribute("zongfen",MainWindow.mw.zongfenhjtext.Text);
			newnode.SetAttribute("time",MainWindow.mw.Timertb.Text);
			selectnode.AppendChild(newnode);
			jiluy.Document.Save("xml/Exercises.xml");	
			System.Windows.MessageBox.Show("xml/Exercises.xml文件保存成功！");
			this.deleteall.IsEnabled=true;
		}
	}
}