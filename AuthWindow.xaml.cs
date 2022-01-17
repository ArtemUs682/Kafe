using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kafe
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }
        gr682_uat3Entities1 db = new gr682_uat3Entities1();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var row = db.Workers.Where(w => w.Login == LoginBox.Text).FirstOrDefault();
            if(row.Pass == PassBox.Password)
            {
                switch (row.RoleId)
                {
                    case 1: WaiterWindow.GetUserId(row.Id); WaiterWindow window = new WaiterWindow(); window.Show(); this.Close(); break;
                    case 2: AdminWindow window1 = new AdminWindow(); window1.Show(); this.Close(); break;
                    case 3: CookWindow window2 = new CookWindow(); window2.Show(); this.Close(); break;
                }

            }
        }
    }
}
