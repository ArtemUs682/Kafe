using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;

namespace Kafe
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : System.Windows.Window
    {
        Workers NewWorker = new Workers();
        public AdminWindow()
        {
            byte[] mas = null;
            string imageLoc = @"..\..\Images\default-user-image.png";
            FileStream fs = new FileStream(imageLoc, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            mas = br.ReadBytes((int)fs.Length);
            NewWorker.Id = 0;
            NewWorker.Surname = "Новый";
            NewWorker.Firstname = "сотрудник";
            NewWorker.Middlename = " ";
            NewWorker.Pass = " ";
            NewWorker.RoleId = 1;
            NewWorker.Login = " ";
            NewWorker.ProfilePhoto = mas;
            NewWorker.ContractPhoto = mas;
            InitializeComponent();
            Vivod();
        }

        gr682_uat3Entities1 db = new gr682_uat3Entities1();
        gr682_uat3Entities2 db1 = new gr682_uat3Entities2();
        byte[] ProfilePhotoMas = null;
        byte[] ContractPhotoMas = null;
        private void EmployeesGridBtn_Click(object sender, RoutedEventArgs e)
        {
            EmployeesGrid.Visibility = Visibility.Visible;
            OffersGrid.Visibility = Visibility.Hidden;
            ShiftsGrid.Visibility = Visibility.Hidden;
            ReportsGrid.Visibility = Visibility.Hidden;
            EmployeesGridBtn.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            OffersGridBtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            ShiftsGridBtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            ReportsGridBtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
        }

        private void OffersGridBtn_Click(object sender, RoutedEventArgs e)
        {
            EmployeesGrid.Visibility = Visibility.Hidden;
            OffersGrid.Visibility = Visibility.Visible;
            ShiftsGrid.Visibility = Visibility.Hidden;
            ReportsGrid.Visibility = Visibility.Hidden;
            EmployeesGridBtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            OffersGridBtn.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            ShiftsGridBtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            ReportsGridBtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
        }

        private void ShiftsGridBtn_Click(object sender, RoutedEventArgs e)
        {
            EmployeesGrid.Visibility = Visibility.Hidden;
            OffersGrid.Visibility = Visibility.Hidden;
            ShiftsGrid.Visibility = Visibility.Visible;
            ReportsGrid.Visibility = Visibility.Hidden;
            EmployeesGridBtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            OffersGridBtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            ShiftsGridBtn.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            ReportsGridBtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
        }

        private void ReportsGridBtn_Click(object sender, RoutedEventArgs e)
        {
            EmployeesGrid.Visibility = Visibility.Hidden;
            OffersGrid.Visibility = Visibility.Hidden;
            ShiftsGrid.Visibility = Visibility.Hidden;
            ReportsGrid.Visibility = Visibility.Visible;
            EmployeesGridBtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            OffersGridBtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            ShiftsGridBtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            ReportsGridBtn.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        private void Combooo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Combooo.Text != "" || Combooo.SelectedItem != null)
            {
                DolznostLabel.Visibility = Visibility.Hidden;
            }
            else
            {
                DolznostLabel.Visibility = Visibility.Visible;
            }
        }

        private void Passss_GotFocus(object sender, RoutedEventArgs e)
        {
            PassLabel.Visibility = Visibility.Hidden;
        }

        private void Passss_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Passss.Password != null && Passss.Password != "")
            {
                PassLabel.Visibility = Visibility.Hidden;
            }
            else
            {
                PassLabel.Visibility = Visibility.Visible;
            }
        }

        private void SurnameBOX_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SurnameBOX.Text == "Фамилия")
            {
                SurnameBOX.Text = "";
            }
        }

        private void SurnameBOX_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SurnameBOX.Text == "" || SurnameBOX.Text == null)
            {
                SurnameBOX.Text = "Фамилия";
            }
        }

        private void NameBOX_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NameBOX.Text == "Имя")
            {
                NameBOX.Text = "";
            }
        }

        private void NameBOX_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NameBOX.Text == "" || NameBOX.Text == null)
            {
                NameBOX.Text = "Имя";
            }
        }

        private void LastNameBOX_GotFocus(object sender, RoutedEventArgs e)
        {
            if (LastNameBOX.Text == "Отчество")
            {
                LastNameBOX.Text = "";
            }
        }

        private void LastNameBOX_LostFocus(object sender, RoutedEventArgs e)
        {
            if (LastNameBOX.Text == "" || LastNameBOX.Text == null)
            {
                LastNameBOX.Text = "Отчество";
            }
        }

        private void LoginBOX_GotFocus(object sender, RoutedEventArgs e)
        {
            if (LoginBOX.Text == "Логин")
            {
                LoginBOX.Text = "";
            }
        }

        private void LoginBOX_LostFocus(object sender, RoutedEventArgs e)
        {
            if (LoginBOX.Text == "" || LoginBOX.Text == null)
            {
                LoginBOX.Text = "Логин";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dld = new OpenFileDialog();
            dld.Filter = "JPG Files (.jpg)|.jpg|PNG Files (.png)|.png|JPEG Files (.jpeg)|.jpeg";
            dld.Title = "Выберите фотографию пользователя";
            if (dld.ShowDialog() == true)
            {
                string imageLoc = dld.FileName.ToString();
                PhotoProfileBox.Source = new BitmapImage(new Uri(imageLoc));
                FileStream fs = new FileStream(imageLoc, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                ProfilePhotoMas = br.ReadBytes((int)fs.Length);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dld = new OpenFileDialog();
            dld.Filter = "JPG Files (.jpg)|.jpg|PNG Files (.png)|.png|JPEG Files (.jpeg)|.jpeg";
            dld.Title = "Выберите фотографию договора";
            if (dld.ShowDialog() == true)
            {
                string imageLoc = dld.FileName.ToString();
                ContractPhotoBox.Source = new BitmapImage(new Uri(imageLoc));
                FileStream fs = new FileStream(imageLoc, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                ContractPhotoMas = br.ReadBytes((int)fs.Length);
            }
        }

        private void StackPanel_Drop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            string imageLoc = System.IO.Path.GetFullPath(files[0]).ToString();
            PhotoProfileBox.Source = new BitmapImage(new Uri(imageLoc));
            FileStream fs = new FileStream(imageLoc, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            ProfilePhotoMas = br.ReadBytes((int)fs.Length);
        }

        private void StackPanel_Drop_1(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            string imageLoc = System.IO.Path.GetFullPath(files[0]).ToString();
            ContractPhotoBox.Source = new BitmapImage(new Uri(imageLoc));
            FileStream fs = new FileStream(imageLoc, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            ContractPhotoMas = br.ReadBytes((int)fs.Length);
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)//добавить нового
        {
            NewWorker.Id = db.Workers.Select(w => w.Id).Max() + 1;
            db.Workers.Add(NewWorker);
            db.SaveChanges();
            Vivod();
            SotrudnikiLV.SelectedIndex = SotrudnikiLV.Items.IndexOf(NewWorker);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)//сохранить изменения
        {
            Workers worker = (Workers)SotrudnikiLV.SelectedItem;
            worker.ProfilePhoto = ProfilePhotoMas;
            worker.ContractPhoto = ContractPhotoMas;
            worker.Surname = SurnameBOX.Text;
            worker.Firstname = NameBOX.Text;
            worker.Middlename = LastNameBOX.Text;
            worker.RoleId = Combooo.SelectedIndex + 1;
            worker.Login = LoginBOX.Text;
            worker.Pass = Passss.Password;
            db.SaveChanges();
            if(NewWorker.Id != 0)
            {
                NewWorker.Id = 0;
            }
            Vivod();
        }

        public class ItemsForTblesLV
        {
            public string Id { get; set; }
            public string WaiterId { get; set; }
            public string WaiterName { get; set; }
        }

        public List<ItemsForTblesLV> itemsforTablesLV = new List<ItemsForTblesLV>();

        public class ItemsForCombooSotr
        {
            public string Id { get; set; }
            public string SurFir { get; set; }
        }

        public List<ItemsForCombooSotr> itemsforCombooSotr = new List<ItemsForCombooSotr>();
        public List<string> surfirs_forcomboo = new List<string>();

        public class ItemsForCombooShiftSotr
        {
            public int Id { get; set; }
            public string SurFir { get; set; }
            public string RoleName { get; set; }
        }
        public List<ItemsForCombooShiftSotr> itemsforAddSotrShifts = new List<ItemsForCombooShiftSotr>();
        public List<ItemsForCombooShiftSotr> itemsforRemoveSotrShifts = new List<ItemsForCombooShiftSotr>();

        private void Vivod()
        {
            SotrudnikiLV.ItemsSource = db.Workers.ToList();
            //SqlConnection con = new SqlConnection("Data Source=mssql;Initial Catalog=gr682_uat3;Integrated Security=True");
            //con.Open();
            //string query = "SELECT Surname, Firstname, Middlename, Name, ProfilePhoto FROM Workers INNER JOIN Roles ON Workers.RoleId = Roles.Id";
            //SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            //DataTable dataTable = new DataTable();
            //adapter.Fill(dataTable);
            //SotrudnikiLV.ItemsSource = dataTable.DefaultView;
            //adapter.Update(dataTable);
            //con.Close();

            Combooo.Items.Clear();
            for (int i = 1; i <= db.Roles.Count(); i++)
            {
                var row = db.Roles.Where(w => w.Id == i).FirstOrDefault();
                Combooo.Items.Add(row.Name);
            }

            OffersLV.ItemsSource = db.Orders.ToList();
            ////
            itemsforTablesLV.Clear();
            for (int i = 1; i <= db.Tables.Select(w => w.Id).Max(); i++)
            {
                var row = db.Tables.Where(w => w.Id == i).FirstOrDefault();
                if(row != null)
                {
                    if (row.WorkerId != null)
                    {
                        string sur = db.Workers.Where(w => w.Id == row.WorkerId).FirstOrDefault().Surname;
                        string fir = db.Workers.Where(w => w.Id == row.WorkerId).FirstOrDefault().Firstname;
                        ItemsForTblesLV tblesLV = new ItemsForTblesLV();
                        tblesLV.Id = row.Id.ToString();
                        tblesLV.WaiterId = row.WorkerId.ToString();
                        tblesLV.WaiterName = sur + " " + fir;
                        itemsforTablesLV.Add(tblesLV);
                    }
                    else
                    {
                        ItemsForTblesLV tblesLV = new ItemsForTblesLV();
                        tblesLV.Id = row.Id.ToString();
                        tblesLV.WaiterId = "";
                        tblesLV.WaiterName = "Пусто";
                        itemsforTablesLV.Add(tblesLV);
                    }
                }
            }
            TablesLV.ItemsSource = itemsforTablesLV.ToList();
            ////
            surfirs_forcomboo.Clear();
            itemsforCombooSotr.Clear();
            for (int i = 1; i <= db.Workers.Select(w => w.Id).Max(); i++)
            {
                var row = db.Workers.Where(w => w.Id == i).FirstOrDefault();
                if (row != null)
                {
                    if (row.RoleId == 1)
                    {
                        string sur = row.Surname;
                        string fir = row.Firstname;
                        ItemsForCombooSotr items = new ItemsForCombooSotr();
                        items.Id = row.Id.ToString();
                        items.SurFir = sur + " " + fir;
                        surfirs_forcomboo.Add(items.SurFir);
                        itemsforCombooSotr.Add(items);
                    }
                }
            }
            ComboooSotr.ItemsSource = surfirs_forcomboo;
            /////
            itemsforAddSotrShifts.Clear();
            for (int i = 1; i <= db.Workers.Select(w => w.Id).Max(); i++)
            {
                var row = db.Workers.Where(w => w.Id == i).FirstOrDefault();
                if (row != null)
                {
                    string sur = row.Surname;
                    string fir = row.Firstname;
                    ItemsForCombooShiftSotr items = new ItemsForCombooShiftSotr();
                    items.Id = row.Id;
                    items.SurFir = sur + " " + fir;
                    items.RoleName = db.Roles.Where(w => w.Id == row.RoleId).FirstOrDefault().Name;
                    if (itemsforRemoveSotrShifts.Select(w => w.Id).Contains(items.Id) == false)
                    {
                        itemsforAddSotrShifts.Add(items);
                    }
                }
            }
            SotrShiftsAddGrid.ItemsSource = itemsforAddSotrShifts.ToList();
            SotrShiftsRemoveGrid.ItemsSource = itemsforRemoveSotrShifts.ToList();
            /////
            ShiftsLV.ItemsSource = db.Shifts.ToList();
        }

        private void SotrudnikiLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NewWorker.Id != 0 && SotrudnikiLV.SelectedIndex != SotrudnikiLV.Items.IndexOf(NewWorker))
            {
                MessageBox.Show("Закончите создание нового пользователя!");
                SotrudnikiLV.SelectedIndex = SotrudnikiLV.Items.IndexOf(NewWorker);
            }
            else
            {
                if (SotrudnikiLV.SelectedIndex == SotrudnikiLV.Items.IndexOf(NewWorker))
                {
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.UriSource = new Uri(@"..\..\Images\default-user-image.png", UriKind.Relative);
                    image.EndInit();
                    PhotoProfileBox.Source = image;
                    BitmapImage image1 = new BitmapImage();
                    image1.BeginInit();
                    image1.UriSource = new Uri(@"..\..\Images\imgonline-com-ua-Blur-7S7J1nOCckLMxcrt.jpg", UriKind.Relative);
                    image1.EndInit();
                    ContractPhotoBox.Source = image1;
                    SurnameBOX.Text = "Фамилия";
                    NameBOX.Text = "Имя";
                    LastNameBOX.Text = "Отчество";
                    Combooo.SelectedIndex = -1;
                    DolznostLabel.Visibility = Visibility.Visible;
                    LoginBOX.Text = "Логин";
                    Passss.Password = "";
                    PassLabel.Visibility = Visibility.Visible;
                }
                else
                {
                    Workers worker = (Workers)SotrudnikiLV.SelectedItem;
                    byte[] image = worker.ProfilePhoto;
                    MemoryStream ms = new MemoryStream(image);
                    PhotoProfileBox.Source = BitmapFrame.Create(ms);
                    image = worker.ContractPhoto;
                    ms = new MemoryStream(image);
                    ContractPhotoBox.Source = BitmapFrame.Create(ms);
                    SurnameBOX.Text = worker.Surname;
                    NameBOX.Text = worker.Firstname;
                    LastNameBOX.Text = worker.Middlename;
                    Combooo.SelectedIndex = worker.RoleId - 1;
                    DolznostLabel.Visibility = Visibility.Hidden;
                    LoginBOX.Text = worker.Login;
                    Passss.Password = worker.Pass;
                    PassLabel.Visibility = Visibility.Hidden;
                }
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Workers worker = (Workers)SotrudnikiLV.SelectedItem;
            db.Workers.Remove(worker);
            db.SaveChanges();
            Vivod();
            NewWorker.Id = 0;
        }

        public class ItemsForOrderDishDG
        {
            public string Name { get; set; }
            public string Price { get; set; }
            public string Count { get; set; }

            public ItemsForOrderDishDG(string Name, string Price, string Count)
            {
                this.Name = Name;
                this.Price = Price;
                this.Count = Count;
            }
        }
        

        private void OffersLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Orders orderr = (Orders)OffersLV.SelectedItem;
            IdOdrerBox.Text = "Заказ № " + orderr.Id.ToString();
            DateOrderBox.Text = orderr.Date.ToShortDateString();
            WaiterOrderBox.Text = "Оф-ант: " + db.Workers.Where(w => w.Id == orderr.WorkerID).FirstOrDefault().Surname + " " + db.Workers.Where(w => w.Id == orderr.WorkerID).FirstOrDefault().Firstname;

            SqlConnection con = new SqlConnection("Data Source=mssql;Initial Catalog=gr682_uat3;Integrated Security=True");
            con.Open();
            string query = "CREATE TABLE #View10 " +
                           "(Name nvarchar(50), "+
                           "Price nvarchar(50), "+
                           "Count nvarchar(50)) "+
                           "INSERT INTO #View10 "+
                           "SELECT Name, Price, Count FROM OrderDish INNER JOIN Dishes ON DishId = Id WHERE OrderDish.OrderId = " + orderr.Id.ToString() + "; " +
                           "INSERT INTO #View10 VALUES ('ИТОГО', (SELECT sum(Price * Count) FROM OrderDish INNER JOIN Dishes ON DishId = Id WHERE OrderDish.OrderId = " + orderr.Id.ToString() + "), '');";
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

            CustomersCountOrderBox.Text = "Кол-во гостей: " + orderr.CustumerCount.ToString();
            TableOrderBox.Text = "Столик № " + orderr.TableID.ToString();
            StatusBox.Text = "Статус: " + db.Statuses.Where(w => w.Id == orderr.StatusId).FirstOrDefault().Name;
        }

        private void TablesLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ItemsForTblesLV item = (ItemsForTblesLV)TablesLV.SelectedItem;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (ComboooSotr.SelectedItem != null || ComboooSotr.SelectedIndex != -1)
            {
                int ind = Convert.ToInt32(itemsforCombooSotr.Where(w => w.SurFir == ComboooSotr.Text).FirstOrDefault().Id);
                ItemsForTblesLV table = (ItemsForTblesLV)TablesLV.SelectedItem;
                int i = Convert.ToInt32(table.Id);
                Tables tb = db.Tables.Where(w => w.Id == i).FirstOrDefault();
                tb.WorkerId = ind;
                db.SaveChanges();
                Vivod();
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            ItemsForTblesLV table = (ItemsForTblesLV)TablesLV.SelectedItem;
            int i = Convert.ToInt32(table.Id);
            Tables tb = db.Tables.Where(w => w.Id == i).FirstOrDefault();
            tb.WorkerId = null;
            db.SaveChanges();
            Vivod();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            object tag = (sender as FrameworkElement).Tag;
            ItemsForCombooShiftSotr item = itemsforAddSotrShifts.Where(w => w.Id == Convert.ToInt32(tag)).FirstOrDefault();
            itemsforRemoveSotrShifts.Add(item);
            itemsforAddSotrShifts.Remove(item);
            Vivod();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            object tag = (sender as FrameworkElement).Tag;
            ItemsForCombooShiftSotr item = itemsforRemoveSotrShifts.Where(w => w.Id == Convert.ToInt32(tag)).FirstOrDefault();
            itemsforAddSotrShifts.Add(item);
            itemsforRemoveSotrShifts.Remove(item);
            Vivod();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            Shifts shift = new Shifts();
            if (db.Shifts.Count() > 0)
            {
                shift.Id = db.Shifts.Select(w => w.Id).Max() + 1;
            }
            else
            {
                shift.Id = 1;
            }
            shift.Date = CalendarBoxShifts.SelectedDate.Value;
            db.Shifts.Add(shift);
            db.SaveChanges();
            for(int i = 0; i < itemsforRemoveSotrShifts.Count(); i++)
            {
                ShiftWorker shiftWorker = new ShiftWorker();
                shiftWorker.ShiftId = shift.Id;
                shiftWorker.WorkerId = itemsforRemoveSotrShifts[i].Id;
                db1.ShiftWorker.Add(shiftWorker);
                db1.SaveChanges();
            }
            itemsforRemoveSotrShifts.Clear();
            Vivod();
        }

        private void ShiftsLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Shifts shift = (Shifts)ShiftsLV.SelectedItem;
            SqlConnection con = new SqlConnection("Data Source=mssql;Initial Catalog=gr682_uat3;Integrated Security=True");
            con.Open();
            string query = "SELECT Surname, Firstname, Name FROM ShiftWorker INNER JOIN Workers ON WorkerId = Workers.Id INNER JOIN Roles ON Workers.RoleId = Roles.Id WHERE ShiftId = " + shift.Id.ToString();
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            System.Data.DataTable dataTable = new System.Data.DataTable();
            adapter.Fill(dataTable);
            ShiftWorkerLv.ItemsSource = dataTable.DefaultView;
            adapter.Update(dataTable);
            con.Close();
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            if(CalendarBoxReports.SelectedDate == null)
            {
                MessageBox.Show("Не выбрана дата!");
            }
            else
            {
                var report = db.Orders.Where(x => x.Date == CalendarBoxReports.SelectedDate).ToList();
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Выберите место для сохранения отчета";
                saveFileDialog.FileName = "Отчет за " + CalendarBoxReports.SelectedDate.Value.ToShortDateString() + " - PDF";
                saveFileDialog.Filter = "PDF Files |*.pdf";
                if (saveFileDialog.ShowDialog() == true)
                {
                    var application = new Word.Application();

                    double summ = 0;

                    Word.Document document = application.Documents.Add();

                    Word.Paragraph paragraph1 = document.Paragraphs.Add();
                    Word.Range range1 = paragraph1.Range;
                    range1.Text = "Отчет по заказам за " + CalendarBoxReports.SelectedDate.Value.ToShortDateString();
                    paragraph1.set_Style("Заголовок");
                    paragraph1.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    range1.Font.Size = 18;
                    range1.InsertParagraphAfter();

                    Word.Paragraph tableparagraph = document.Paragraphs.Add();
                    Word.Range tablerange = tableparagraph.Range;
                    Word.Table paymentstable = document.Tables.Add(tablerange, report.Count() + 1, 5);
                    paymentstable.Borders.InsideLineStyle = paymentstable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                    paymentstable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    Word.Range cellRange;

                    cellRange = paymentstable.Cell(1, 1).Range;
                    cellRange.Text = "Дата заказа";
                    cellRange = paymentstable.Cell(1, 2).Range;
                    cellRange.Text = "Номер стола";
                    cellRange = paymentstable.Cell(1, 3).Range;
                    cellRange.Text = "Официант";
                    cellRange = paymentstable.Cell(1, 4).Range;
                    cellRange.Text = "Статус заказа";
                    cellRange = paymentstable.Cell(1, 5).Range;
                    cellRange.Text = "Стоимость";

                    paymentstable.Rows[1].Range.Bold = 1;
                    paymentstable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                    for (int i = 0; i < report.Count(); i++)
                    {
                        var orders = report[i];

                        cellRange = paymentstable.Cell(i + 2, 1).Range;
                        cellRange.Text = orders.Date.ToString("dd.MM.yyyy");

                        cellRange = paymentstable.Cell(i + 2, 2).Range;
                        cellRange.Text = orders.TableID.ToString();

                        cellRange = paymentstable.Cell(i + 2, 3).Range;
                        cellRange.Text = orders.Workers.Surname + " " + orders.Workers.Firstname + " " + orders.Workers.Middlename;

                        cellRange = paymentstable.Cell(i + 2, 4).Range;
                        cellRange.Text = orders.Statuses.Name;

                        cellRange = paymentstable.Cell(i + 2, 5).Range;
                        cellRange.Text = orders.Price.ToString();
                        summ += orders.Price;
                    }

                    cellRange.InsertParagraphAfter();
                    tablerange.InsertParagraphAfter();

                    Word.Paragraph paragraph2 = document.Paragraphs.Add();
                    Word.Range range2 = paragraph2.Range;
                    range2.Text = "Общая выручка - " + string.Format("{0:0.00}", summ) + " руб.";
                    paragraph2.set_Style("Заголовок");
                    paragraph2.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    range2.Font.Size = 18;
                    range2.InsertParagraphAfter();

                    document.SaveAs2(saveFileDialog.FileName, Word.WdExportFormat.wdExportFormatPDF);
                    MessageBox.Show("Отчет сохранен!");
                }
            }
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            var report = db.Orders.Where(x => x.Date.ToShortDateString() == DateTime.Now.ToShortDateString()).ToList();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Выберите место для сохранения отчета";
            saveFileDialog.FileName = "Отчет за " + DateTime.Now.ToShortDateString() + " - PDF";
            saveFileDialog.Filter = "PDF Files |*.pdf";
            if (saveFileDialog.ShowDialog() == true)
            {
                var application = new Word.Application();

                double summ = 0;

                Word.Document document = application.Documents.Add();

                Word.Paragraph paragraph1 = document.Paragraphs.Add();
                Word.Range range1 = paragraph1.Range;
                range1.Text = "Отчет по заказам за " + DateTime.Now.ToShortDateString();
                paragraph1.set_Style("Заголовок");
                paragraph1.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                range1.Font.Size = 18;
                range1.InsertParagraphAfter();

                Word.Paragraph tableparagraph = document.Paragraphs.Add();
                Word.Range tablerange = tableparagraph.Range;
                Word.Table paymentstable = document.Tables.Add(tablerange, report.Count() + 1, 5);
                paymentstable.Borders.InsideLineStyle = paymentstable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                paymentstable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                Word.Range cellRange;

                cellRange = paymentstable.Cell(1, 1).Range;
                cellRange.Text = "Дата заказа";
                cellRange = paymentstable.Cell(1, 2).Range;
                cellRange.Text = "Номер стола";
                cellRange = paymentstable.Cell(1, 3).Range;
                cellRange.Text = "Официант";
                cellRange = paymentstable.Cell(1, 4).Range;
                cellRange.Text = "Статус заказа";
                cellRange = paymentstable.Cell(1, 5).Range;
                cellRange.Text = "Стоимость";

                paymentstable.Rows[1].Range.Bold = 1;
                paymentstable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                for (int i = 0; i < report.Count(); i++)
                {
                    var orders = report[i];

                    cellRange = paymentstable.Cell(i + 2, 1).Range;
                    cellRange.Text = orders.Date.ToString("dd.MM.yyyy");

                    cellRange = paymentstable.Cell(i + 2, 2).Range;
                    cellRange.Text = orders.TableID.ToString();

                    cellRange = paymentstable.Cell(i + 2, 3).Range;
                    cellRange.Text = orders.Workers.Surname + " " + orders.Workers.Firstname + " " + orders.Workers.Middlename;

                    cellRange = paymentstable.Cell(i + 2, 4).Range;
                    cellRange.Text = orders.Statuses.Name;

                    cellRange = paymentstable.Cell(i + 2, 5).Range;
                    cellRange.Text = orders.Price.ToString();
                    summ += orders.Price;
                    //cellRange.InsertParagraphAfter();
                }

                cellRange.InsertParagraphAfter();
                tablerange.InsertParagraphAfter();

                Word.Paragraph paragraph2 = document.Paragraphs.Add();
                Word.Range range2 = paragraph2.Range;
                range2.Text = "Общая выручка - " + string.Format("{0:0.00}", summ) + " руб.";
                paragraph2.set_Style("Заголовок");
                paragraph2.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                range2.Font.Size = 18;
                range2.InsertParagraphAfter();

                document.SaveAs2(saveFileDialog.FileName, Word.WdExportFormat.wdExportFormatPDF);
                MessageBox.Show("Отчет сохранен!");
            }
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            if (CalendarBoxReports.SelectedDate == null)
            {
                MessageBox.Show("Не выбрана дата!");
            }
            else
            {
                var report = db.Orders.Where(x => x.Date == CalendarBoxReports.SelectedDate).ToList();
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Выберите место для сохранения отчета";
                saveFileDialog.FileName = "Отчет за " + CalendarBoxReports.SelectedDate.Value.ToShortDateString() + " - PDF";
                saveFileDialog.Filter = "Excel Files |*.xlsx";
                if (saveFileDialog.ShowDialog() == true)
                {
                    var application = new Excel.Application();

                    application.SheetsInNewWorkbook = 1;

                    Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);

                    int startRowIndex = 1;

                    Excel.Worksheet worksheet = application.Worksheets.Item[1];

                    worksheet.Name = "Отчет по заказам за " + CalendarBoxReports.SelectedDate.Value.ToShortDateString();

                    worksheet.Cells[1][startRowIndex] = "Дата заказа";
                    worksheet.Cells[2][startRowIndex] = "Номер стола";
                    worksheet.Cells[3][startRowIndex] = "Официант";
                    worksheet.Cells[4][startRowIndex] = "Статус заказа";
                    worksheet.Cells[5][startRowIndex] = "Стоимость";

                    startRowIndex++;

                    foreach (var orders in report)
                    {

                        worksheet.Cells[1][startRowIndex] = orders.Date.ToShortDateString();
                        worksheet.Cells[2][startRowIndex] = orders.TableID;
                        worksheet.Cells[3][startRowIndex] = orders.Workers.Surname + " " + orders.Workers.Firstname + " " + orders.Workers.Middlename;
                        worksheet.Cells[4][startRowIndex] = orders.Statuses.Name;
                        worksheet.Cells[5][startRowIndex] = orders.Price;
                        startRowIndex++;
                    }

                    Excel.Range sumRange = worksheet.Range[worksheet.Cells[1][startRowIndex], worksheet.Cells[4][startRowIndex]];
                    sumRange.Merge();
                    sumRange.Value = "Итого:";
                    sumRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

                    worksheet.Cells[5][startRowIndex].Formula = $"=SUM(E{startRowIndex - report.Count()}:" + $"E{startRowIndex - 1})";

                    sumRange.Font.Bold = worksheet.Cells[5][startRowIndex].Font.Bold = true;
                    //worksheet.Cells[6][startRowIndex].NumberFormat = "#,###.00";

                    startRowIndex++;

                    Excel.Range rangeBorders = worksheet.Range[worksheet.Cells[1][1], worksheet.Cells[5][startRowIndex - 1]];
                    rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                    rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                    rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                    rangeBorders.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle =
                    rangeBorders.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;


                    worksheet.Columns.AutoFit();

                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Отчет сохранен!");
                }
            }
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            var report = db.Orders.Where(x => x.Date.ToShortDateString() == DateTime.Now.ToShortDateString()).ToList();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Выберите место для сохранения отчета";
            saveFileDialog.FileName = "Отчет за " + DateTime.Now.ToShortDateString() + " - PDF";
            saveFileDialog.Filter = "Excel Files |*.xlsx";
            if (saveFileDialog.ShowDialog() == true)
            {
                var application = new Excel.Application();

                application.SheetsInNewWorkbook = 1;

                Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);

                int startRowIndex = 1;

                Excel.Worksheet worksheet = application.Worksheets.Item[1];

                worksheet.Name = "Отчет по заказам за " + DateTime.Now.ToShortDateString();

                worksheet.Cells[1][startRowIndex] = "Дата заказа";
                worksheet.Cells[2][startRowIndex] = "Номер стола";
                worksheet.Cells[3][startRowIndex] = "Официант";
                worksheet.Cells[4][startRowIndex] = "Статус заказа";
                worksheet.Cells[5][startRowIndex] = "Стоимость";

                startRowIndex++;

                foreach (var orders in report)
                {

                    worksheet.Cells[1][startRowIndex] = orders.Date.ToShortDateString();
                    worksheet.Cells[2][startRowIndex] = orders.TableID;
                    worksheet.Cells[3][startRowIndex] = orders.Workers.Surname + " " + orders.Workers.Firstname + " " + orders.Workers.Middlename;
                    worksheet.Cells[4][startRowIndex] = orders.Statuses.Name;
                    worksheet.Cells[5][startRowIndex] = orders.Price;
                    startRowIndex++;
                }

                Excel.Range sumRange = worksheet.Range[worksheet.Cells[1][startRowIndex], worksheet.Cells[4][startRowIndex]];
                sumRange.Merge();
                sumRange.Value = "Итого:";
                sumRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

                worksheet.Cells[5][startRowIndex].Formula = $"=SUM(E{startRowIndex - report.Count()}:" + $"E{startRowIndex - 1})";

                sumRange.Font.Bold = worksheet.Cells[5][startRowIndex].Font.Bold = true;
                //worksheet.Cells[6][startRowIndex].NumberFormat = "#,###.00";

                startRowIndex++;

                Excel.Range rangeBorders = worksheet.Range[worksheet.Cells[1][1], worksheet.Cells[5][startRowIndex - 1]];
                rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                rangeBorders.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle =
                rangeBorders.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;


                worksheet.Columns.AutoFit();

                workbook.SaveAs(saveFileDialog.FileName);
                MessageBox.Show("Отчет сохранен!");
            }
        }
    }
}
