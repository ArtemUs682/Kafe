using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace Kafe
{
    /// <summary>
    /// Логика взаимодействия для CookWindow.xaml
    /// </summary>
    public partial class CookWindow : Window
    {
        gr682_uat3Entities1 db = new gr682_uat3Entities1();
        public CookWindow()
        {
            InitializeComponent();
            OrderLV.ItemsSource = db.Orders.ToList();
            OrderStatusBox.Items.Clear();
            for (int i = 0; i < db.Statuses.Count(); i++)
            {
                var row = db.Statuses.ToList()[i];
                OrderStatusBox.Items.Add(row.Name);
            }
        }

        private void OrderLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Orders order = (Orders)OrderLV.SelectedItem;
            OrderIdBox.Text = order.Id.ToString();
            OrderDateBox.Text = order.Date.ToShortDateString();
            OrderWorkerBox.Text = order.CustumerCount.ToString();

            SqlConnection con = new SqlConnection("Data Source=mssql;Initial Catalog=gr682_uat3;Integrated Security=True");
            con.Open();
            string query = "CREATE TABLE #View10 " +
                           "(Name nvarchar(50), " +
                           "Count nvarchar(50)) " +
                           "INSERT INTO #View10 " +
                           "SELECT Name AS Блюдо, Count AS 'Кол-во' FROM OrderDish INNER JOIN Dishes ON DishId = Id WHERE OrderDish.OrderId = " + order.Id.ToString() + "; ";
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            adapter.SelectCommand.ExecuteNonQuery();
            query = "SELECT Name AS Блюдо, Count AS 'Кол-во' FROM #View10";
            adapter = new SqlDataAdapter(query, con);
            System.Data.DataTable dataTable = new System.Data.DataTable();
            adapter.Fill(dataTable);
            OrderDG.ItemsSource = dataTable.DefaultView;
            adapter.Update(dataTable);
            query = "DROP TABLE #View10";
            adapter = new SqlDataAdapter(query, con);
            adapter.SelectCommand.ExecuteNonQuery();
            con.Close();

            OrderStatusBox.SelectedIndex = OrderStatusBox.Items.IndexOf(order.Statuses.Name);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Orders order = (Orders)OrderLV.SelectedItem;
            order.StatusId = db.Statuses.Where(w => w.Name == OrderStatusBox.Text).FirstOrDefault().Id;
            db.SaveChanges();
            OrderLV.ItemsSource = db.Orders.ToList();
            CookWindow window = new CookWindow();
            window.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Orders order = (Orders)OrderLV.SelectedItem;
            order.StatusId = db.Statuses.Where(w => w.Name == "Готов").FirstOrDefault().Id;
            db.SaveChanges();
            OrderLV.ItemsSource = db.Orders.ToList();
            CookWindow window = new CookWindow();
            window.Show();
            this.Close();
        }
    }
}
