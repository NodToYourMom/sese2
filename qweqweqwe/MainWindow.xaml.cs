using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace qweqweqwe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void CommandLoad(string com)
        {
            string connect = @"data source=vc-stud-mssql1;user id=user92_db;password=user92;MultipleActiveResultSets=True;App=EntityFramework";
            SqlConnection myConnection = new SqlConnection(@connect);
            SqlCommand Mycommand = new SqlCommand(com, myConnection);
            myConnection.Open();
            Mycommand.ExecuteNonQuery();
            SqlDataAdapter dapter = new SqlDataAdapter(Mycommand);
            DataTable Table = new DataTable("Product");
            dapter.Fill(Table);
            Imput.ItemsSource = Table.DefaultView;
            myConnection.Close();
        }
        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string com = "Select Id ,Title, Cost, IsActive, ManufacturerID," +
                "MainImagePath From Product where Title like '" + Search.Text + "%' or Cost like '" + Search.Text + "%' or IsActive like '" + Search.Text + "%' or ManufacturerID" +
                " like '" + Search.Text + "%' or MainImagePath like'" + Search.Text + "%'";
            CommandLoad(com);
        }

        private void Manuf(object sender, SelectionChangedEventArgs e)
        {

                
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string com = "Select Id ,Title, Cost, IsActive, ManufacturerID," +
                " MainImagePath From Product";
            CommandLoad(com);
        }

        private void manuf_DropDownClosed(object sender, EventArgs e)
        {
            string com = "Select Id ,Title, Cost, IsActive, ManufacturerID," +
                " MainImagePath From Product where ManufacturerID ='" + manuf.Text + "'";
            CommandLoad(com);
        }

        private void Desc_Click(object sender, RoutedEventArgs e)
        {
            string com = "Select Id ,Title, Cost, IsActive, ManufacturerID," +
                " MainImagePath From Product Order by Cost Desc";
            CommandLoad(com);
        }

        private void Asc_Click(object sender, RoutedEventArgs e)
        {
            string com = "Select Id ,Title, Cost, IsActive, ManufacturerID," +
                " MainImagePath From Product Order by Cost Asc";
            CommandLoad(com);
        }

        private void Imput_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView dataRowView = Imput.SelectedValue as DataRowView;
            string Title = dataRowView[0].ToString();
            Product_window pw = new Product_window(Title);
            pw.Show();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView data = Imput.SelectedValue as DataRowView;
            MessageBoxResult result = MessageBox.Show("Подтвердите удаление!", "Удаление товара",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                string com = "Delete from Product where Id ='" + data[0] + "';" + "Select Title, Cost, IsActive, ManufacturerID, MainImagePath from Product";
                CommandLoad(com);
            }
        }
    }
}
