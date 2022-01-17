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
    /// Логика взаимодействия для WaiterWindow.xaml
    /// </summary>
    public partial class WaiterWindow : Window
    {
        gr682_uat3Entities1 db = new gr682_uat3Entities1();
        gr682_uat3Entities2 db1 = new gr682_uat3Entities2();

        Orders NewOrder = new Orders();
        public static int authorizedID = 0;

        public class ItemsForDishesLV
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public List<ItemsForDishesLV> itemsforDishesLV = new List<ItemsForDishesLV>();
        public class ItemsForOrderDishesLV
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Price { get; set; }
            public string Count { get; set; }
        }
        public List<ItemsForOrderDishesLV> itemsforOrderDishesLV = new List<ItemsForOrderDishesLV>();

        public static void GetUserId(int i)
        {
            authorizedID = i;
        }
        public WaiterWindow()
        {
            InitializeComponent();
            NewOrder.Id = 0;
            NewOrder.Date = DateTime.Now;
            NewOrder.WorkerID = authorizedID;
            NewOrder.TableID = 1;
            NewOrder.CustumerCount = 1;
            NewOrder.Price = 0;
            NewOrder.StatusId = 1;
            Vivod();
        }

        private void Vivod()
        {
            OrdersLV.ItemsSource = db.Orders.ToList();

            ///////////////////////

            itemsforDishesLV.Clear();
            for (int i = 1; i <= db.Dishes.Select(w => w.Id).Max(); i++)
            {
                var row = db.Dishes.Where(w => w.Id == i).FirstOrDefault();
                if (row != null)
                {
                    
                    ItemsForDishesLV item = new ItemsForDishesLV();
                    item.Id = row.Id;
                    item.Name = row.Name;
                    itemsforDishesLV.Add(item);
                    
                }
            }
            DishesLV.ItemsSource = itemsforDishesLV.ToList();

            OrderWorkerBox.Items.Clear();
            for (int i = 0; i < db.Workers.Where(w => w.RoleId == 1).Count(); i++)
            {
                var row = db.Workers.Where(w=> w.RoleId == 1).ToList()[i];
                OrderWorkerBox.Items.Add(row.Surname + " " + row.Firstname);
            }

            OrderTableBox.Items.Clear();
            for (int i = 0; i < db.Tables.Count(); i++)
            {
                var row = db.Tables.ToList()[i];
                OrderTableBox.Items.Add(row.Id);
            }

            OrderStatusBox.Items.Clear();
            for (int i = 0; i < db.Statuses.Count(); i++)
            {
                var row = db.Statuses.ToList()[i];
                OrderStatusBox.Items.Add(row.Name);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (NewOrder.Id == 0)
            {
                NewOrder.Id = db.Orders.Select(w => w.Id).Max() + 1;
                db.Orders.Add(NewOrder);
                db.SaveChanges();
                Vivod();
                OrdersLV.SelectedIndex = OrdersLV.Items.IndexOf(NewOrder);
                OrderIdBox.IsReadOnly = false;
                OrderDateBox.IsEnabled = true;
                OrderCustCountBox.IsReadOnly = false;
                OrderTableBox.IsEnabled = true;
                OrderWorkerBox.IsEnabled = true;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (NewOrder.Id != 0)
            {
                object tag = (sender as FrameworkElement).Tag;
                ItemsForDishesLV dish = itemsforDishesLV.Where(w => w.Id == Convert.ToInt32(tag)).FirstOrDefault();
                if (itemsforOrderDishesLV.Select(w => w.Id).Contains(dish.Id))
                {
                    var orderDish = itemsforOrderDishesLV.Where(w => w.Id == dish.Id).FirstOrDefault();
                    orderDish.Count = (Convert.ToInt32(orderDish.Count) + 1).ToString();
                }
                else
                {
                    ItemsForOrderDishesLV orderDish = new ItemsForOrderDishesLV();
                    orderDish.Id = dish.Id;
                    orderDish.Name = dish.Name;
                    orderDish.Count = "1";
                    orderDish.Price = db.Dishes.Where(w => w.Id == dish.Id).FirstOrDefault().Price;
                    itemsforOrderDishesLV.Add(orderDish);
                }
                ItemsForOrderDishesLV itogo = new ItemsForOrderDishesLV();
                itogo.Id = 0;
                itogo.Name = "ИТОГО";
                itogo.Price = 0;
                for (int i = 0; i < itemsforOrderDishesLV.Count(); i++)
                {
                    itogo.Price += itemsforOrderDishesLV[i].Price * Convert.ToInt32(itemsforOrderDishesLV[i].Count);
                }
                itogo.Count = "";
                itemsforOrderDishesLV.Add(itogo);
                DishesOrderList.ItemsSource = itemsforOrderDishesLV.ToList();
                itemsforOrderDishesLV.Remove(itogo);
            }
            else
            {
                MessageBox.Show("Создайте новый заказ!");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (NewOrder.Id != 0)
            {
                object tag = (sender as FrameworkElement).Tag;
            ItemsForDishesLV dish = itemsforDishesLV.Where(w => w.Id == Convert.ToInt32(tag)).FirstOrDefault();
            if (itemsforOrderDishesLV.Select(w => w.Id).Contains(dish.Id))
            {
                var orderDish = itemsforOrderDishesLV.Where(w => w.Id == dish.Id).FirstOrDefault();
                if (orderDish.Count == "1")
                {
                    itemsforOrderDishesLV.Remove(orderDish);
                }
                else
                {
                    orderDish.Count = (Convert.ToInt32(orderDish.Count) - 1).ToString(); ;
                }
            }
            ItemsForOrderDishesLV itogo = new ItemsForOrderDishesLV();
            itogo.Id = 0;
            itogo.Name = "ИТОГО";
            itogo.Price = 0;
            for (int i = 0; i < itemsforOrderDishesLV.Count(); i++)
            {
                itogo.Price += itemsforOrderDishesLV[i].Price * Convert.ToInt32(itemsforOrderDishesLV[i].Count);
            }
            itogo.Count = "";
            itemsforOrderDishesLV.Add(itogo);
            DishesOrderList.ItemsSource = itemsforOrderDishesLV.ToList();
            itemsforOrderDishesLV.Remove(itogo);
        }
            else
            {
                MessageBox.Show("Создайте новый заказ!");
            }
}

        private void OrdersLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NewOrder.Id != 0 && OrdersLV.SelectedIndex != OrdersLV.Items.IndexOf(NewOrder))
            {
                MessageBox.Show("Закончите создание нового заказа!");
                OrdersLV.SelectedIndex = OrdersLV.Items.IndexOf(NewOrder);
            }
            else
            {
                if (OrdersLV.SelectedIndex == OrdersLV.Items.IndexOf(NewOrder))
                {
                    OrderIdBox.Text = NewOrder.Id.ToString();
                    OrderDateBox.SelectedDate = DateTime.Now;
                    OrderCustCountBox.Text = "1";
                    OrderTableBox.SelectedIndex = -1;
                    DishesOrderList.ItemsSource = null;
                    OrderStatusBox.SelectedIndex = OrderStatusBox.Items.IndexOf("Принят");
                    var row = db.Workers.Where(w => w.Id == authorizedID).FirstOrDefault();
                    OrderWorkerBox.SelectedIndex = OrderWorkerBox.Items.IndexOf(row.Surname + " " + row.Firstname);
                }
                else
                {
                    Orders order = (Orders)OrdersLV.SelectedItem;
                    OrderIdBox.Text = order.Id.ToString();
                    OrderDateBox.SelectedDate = order.Date;
                    OrderCustCountBox.Text = order.CustumerCount.ToString();
                    OrderTableBox.SelectedIndex = order.TableID - 1;

                    SqlConnection con = new SqlConnection("Data Source=mssql;Initial Catalog=gr682_uat3;Integrated Security=True");
                    con.Open();
                    string query = "CREATE TABLE #View10 " +
                                   "(Name nvarchar(50), " +
                                   "Price nvarchar(50), " +
                                   "Count nvarchar(50)) " +
                                   "INSERT INTO #View10 " +
                                   "SELECT Name, Price, Count FROM OrderDish INNER JOIN Dishes ON DishId = Id WHERE OrderDish.OrderId = " + order.Id.ToString() + "; " +
                                   "INSERT INTO #View10 VALUES ('ИТОГО', (SELECT sum(Price * Count) FROM OrderDish INNER JOIN Dishes ON DishId = Id WHERE OrderDish.OrderId = " + order.Id.ToString() + "), '');";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    adapter.SelectCommand.ExecuteNonQuery();
                    query = "SELECT * FROM #View10";
                    adapter = new SqlDataAdapter(query, con);
                    System.Data.DataTable dataTable = new System.Data.DataTable();
                    adapter.Fill(dataTable);
                    DishesOrderList.ItemsSource = dataTable.DefaultView;
                    adapter.Update(dataTable);
                    query = "DROP TABLE #View10";
                    adapter = new SqlDataAdapter(query, con);
                    adapter.SelectCommand.ExecuteNonQuery();
                    con.Close();

                    OrderStatusBox.SelectedIndex = OrderStatusBox.Items.IndexOf(order.Statuses.Name);
                    OrderWorkerBox.SelectedIndex = OrderWorkerBox.Items.IndexOf(order.Workers.Surname + " " + order.Workers.Firstname);
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Orders order = (Orders)OrdersLV.SelectedItem;
            order.Date = OrderDateBox.SelectedDate.Value;
            order.CustumerCount = Convert.ToInt32(OrderCustCountBox.Text);
            order.WorkerID = db.Workers.Where(w => w.Surname + " " + w.Firstname == OrderWorkerBox.Text).FirstOrDefault().Id;
            order.TableID = Convert.ToInt32(OrderTableBox.Text);
            order.StatusId = db.Statuses.Where(w => w.Name == OrderStatusBox.Text).FirstOrDefault().Id;
            if (NewOrder.Id != 0)
            {
                order.Price = 0;
                for (int i = 0; i < itemsforOrderDishesLV.Count(); i++)
                {
                    order.Price += itemsforOrderDishesLV[i].Price * Convert.ToInt32(itemsforOrderDishesLV[i].Count);
                    OrderDish orderDish = new OrderDish();
                    orderDish.OrderId = order.Id;
                    orderDish.DishId = itemsforOrderDishesLV[i].Id;
                    orderDish.Count = Convert.ToInt32(itemsforOrderDishesLV[i].Count);
                    db.OrderDish.Add(orderDish);
                }
            }
            db.SaveChanges();
            OrdersLV.ItemsSource = db.Orders.ToList();
            if (NewOrder.Id != 0)
            {
                NewOrder.Id = 0;
            }
            WaiterWindow.GetUserId(authorizedID);
            WaiterWindow window = new WaiterWindow();
            window.Show();
            this.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

            Checkov.Text = "Кассовый чек" + Environment.NewLine;
            Checkov.Text += "ООО Кафе" + Environment.NewLine;
            if (MessageBox.Show("Наличный расчёт?", "Способ оплаты", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                Checkov.Text += "Безналичный расчёт" + Environment.NewLine;
            }
            else
            {
                Checkov.Text += "Наличный расчёт" + Environment.NewLine;
            }
            int i = Convert.ToInt32(OrderIdBox.Text);
            var row = db.Orders.Where(w => w.Id == i).FirstOrDefault();
            Checkov.Text += "Дата " + row.Date.ToShortDateString() + Environment.NewLine;
            Checkov.Text += "==========================" + Environment.NewLine;
            for(int j = 0; j < db.OrderDish.Where(w=>w.OrderId == row.Id).ToList().Count(); j++)
            {
                var rowd = db.OrderDish.Where(w => w.OrderId == row.Id).ToList()[j];
                Checkov.Text += rowd.Dishes.Name + " " + rowd.Count + "x" + rowd.Dishes.Price + " = " + (rowd.Count * rowd.Dishes.Price).ToString() + Environment.NewLine;
            }
            Checkov.Text += "ИТОГО: " + row.Price + Environment.NewLine;
            Checkov.Text += "==========================" + Environment.NewLine;
            Checkov.Text += "Приятного абрикоса и хорошего помидора!" + Environment.NewLine;
        }
    }
}
