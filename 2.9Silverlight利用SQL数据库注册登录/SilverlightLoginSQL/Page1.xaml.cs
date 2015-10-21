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

namespace SilverlightLoginSQL
{
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        // 当用户导航到此网页时执行。
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void tb1_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        	this.Content=new MainPage();
        }
    }
}
