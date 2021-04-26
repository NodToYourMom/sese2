using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace qweqweqwe
{
    /// <summary>
    /// Interaction logic for Product_window.xaml
    /// </summary>
    public partial class Product_window : Window
    {
        public string str;
        string imag;
        public Product_window(string Title)
        {
            str = Title;
            InitializeComponent();
            avoid();
        }
        public void avoid()
        {
            string connect = @"data source=vc-stud-mssql1;user id=user92_db;password=user92;MultipleActiveResultSets=True;App=EntityFramework";
            string command = "Select * from Product where ID='" + str + "'";
            SqlConnection myConnection = new SqlConnection(@connect);
            SqlCommand myCommand = new SqlCommand(command, myConnection);
            myConnection.Open();
            SqlDataReader rd = myCommand.ExecuteReader();
            string Title = "null";
            string Cost = "null";
            string MainImagePath = "null";
            string ManufacturerID = "null";
            string desc = "null";
            while (rd.Read())//Чтение данных
            {
                Title = rd[1].ToString();
                Cost = rd[2].ToString();
                desc = rd[3].ToString();
                MainImagePath = rd[4].ToString();
                ManufacturerID = rd[6].ToString();
            }
            product_name_Copy.Text = str;
            product_name.Text = Title;
            product_cost.Text = Cost;
            product_manufactorer.Text = ManufacturerID;
            product_name_Copy1.Text = desc;
            Uri uri = new Uri(MainImagePath);
            ImageSource im = new BitmapImage(uri);
            ima.Source = im;
            myConnection.Close();
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void act(string command)
        {
            string connect = @"data source=vc-stud-mssql1;user id=user88_db;password=user88;MultipleActiveResultSets=True;App=EntityFramework";
            SqlConnection myConnection = new SqlConnection(@connect);
            SqlCommand Mycommand = new SqlCommand(command, myConnection);
            myConnection.Open();
            Mycommand.ExecuteNonQuery();
        }

        private void clean_Click(object sender, RoutedEventArgs e)
        {
            product_name_Copy.Text = "null";
            product_name.Text = "null";
            product_cost.Text = "null";
            product_manufactorer.Text = "null";
            product_name_Copy1.Text = "null";
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Вы уверены, что хотите удалить данный товар?", "Удаление", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                string command = "Delete From Product where ID ='" + product_name_Copy.Text + "'";
                act(command);
                product_name_Copy.Text = null;
                product_name.Text = null;
                product_cost.Text = null;
                product_manufactorer.Text = null;
                product_name_Copy1.Text = null;
                ima.Source = null;
            }
            else
            {
                System.Windows.MessageBox.Show("Удаление отменено!");
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image Files (*.jpg;*.png)|*.jpg;*.png|all files (*.*)|*.*";
            try
            {
                openFileDialog1.ShowDialog();
                Uri imgUri = new Uri(openFileDialog1.FileName);
                ImageSource i = new BitmapImage(imgUri);
                ima.Source = i;
                imag = openFileDialog1.FileName;
            }
            catch
            {
                System.Windows.MessageBox.Show(string.Format("Невозможно открыть выбранный файл!"), "Сообщение");
            }
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            string nam = product_name.Text;
            string coost = product_cost.Text;
            string manufacturer = product_manufactorer.Text;
            string description = product_name_Copy1.Text;
            string pic = imag;
            string command = "Update Product set Title = '" + nam + "' and Cost = '" + coost + "' and Description = '" + description + "' and Manufacturer = '" + manufacturer + " and MainImagePath = '" + pic + "''";
            act(command);
            System.Windows.MessageBox.Show(string.Format("Данные успешно изменены!"), "Сообщение");
        }

        private void adding_Click(object sender, RoutedEventArgs e)
        {
            string connect = @"data source=vc-stud-mssql1;user id=user92_db;password=user92;MultipleActiveResultSets=True;App=EntityFramework";
            SqlConnection myConnection = new SqlConnection(@connect);
            string command = "Insert into Product (Title,Cost,Description,MainImagePath,Manufacturer) values ('{0}','{1}','{2}','{3}','{4}')";
            myConnection.Open();
            string nam = product_name.Text;
            string coost = product_cost.Text;
            string description = product_name_Copy1.Text;
            string pic = imag;
            string manufacturer = product_manufactorer.Text;
            string sInsSotr = string.Format(command, nam, coost, description, pic, manufacturer);
            SqlCommand cmdIns = new SqlCommand(sInsSotr, myConnection);
            cmdIns.ExecuteNonQuery();
            product_name_Copy.Text = null;
            product_name.Text = null;
            product_cost.Text = null;
            product_manufactorer.Text = null;
            product_name_Copy1.Text = null;
            ima.Source = null;
        }
    }
}
