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
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Diagnostics;

namespace TestWPFCS
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlConnection sqlConnection = null;

        private SqlDataAdapter adapter = null;

        public static void fun()
        {
            ThreadTestClass.fun1(99999);
            ThreadTestClass.fun1(999999);
            ThreadTestClass.fun1(9999999);
            ThreadTestClass.fun1(99999999);
            ThreadTestClass.fun1(999999999);
        }

        public MainWindow()
        {
            InitializeComponent();

            DateTime dateTime1 = new DateTime();
            DateTime dateTime2 = new DateTime();

            dateTime1 = DateTime.Now;

            fun();

            dateTime2 = DateTime.Now;
            Trace.WriteLine(String.Format("Затраченое общее время без многопоточности = " + dateTime2.Subtract(dateTime1)));

            Thread tread1 = new Thread(() => ThreadTestClass.fun1(99999));
            Thread tread2 = new Thread(() => ThreadTestClass.fun1(999999));
            Thread tread3 = new Thread(() => ThreadTestClass.fun1(9999999));
            Thread tread4 = new Thread(() => ThreadTestClass.fun1(99999999));
            Thread tread5 = new Thread(() => ThreadTestClass.fun1(999999999));

            dateTime1 = DateTime.Now;

            tread1.Start();
            tread2.Start();
            tread3.Start();
            tread4.Start();
            tread5.Start();

            dateTime2 = DateTime.Now;
            Trace.WriteLine(String.Format("Затраченое общее время с многопоточностью = " + dateTime2.Subtract(dateTime1)));
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void OldSelect()
        {
            sqlConnection = new SqlConnection(@"Data Source=DESKTOP-LEONID\SQLEXPRESS;Initial Catalog=Dispatcher;Integrated Security=True");

            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "SELECT * FROM Batch";

            adapter = new SqlDataAdapter(sqlCommand.CommandText, sqlConnection);

            DataTable table = new DataTable();

            adapter.Fill(table);
            TestDG.ItemsSource = table.DefaultView;
        }

        private void UltimateSQLInsert()
        {
            string connectionString = Properties.Settings.Default.SqlConnectionString;
            string sqlExpression = "INSERT INTO Units (Name) VALUES ('vbcn')";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery(); //Выполнение запроса без возращения данных
            }
        }

        private void UltimateSQLSelect()
        {
            string connectionString = Properties.Settings.Default.SqlConnectionString;
            string sqlExpression = "SELECT * FROM Units";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                adapter = new SqlDataAdapter(sqlExpression, connectionString);
                DataTable table = new DataTable();
                adapter.Fill(table);
                TestDG.ItemsSource = table.DefaultView;
            }
        }

        private void UltimateSQLDelite(int id)
        {
            string connectionString = Properties.Settings.Default.SqlConnectionString;
            string sqlExpression = String.Format( "DELETE FROM Units WHERE ID={0}", id);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery(); //Выполнение запроса без возращения данных
            }
        }

        private void UltimateSQLUpdate(int id, string name)
        {
            string connectionString = Properties.Settings.Default.SqlConnectionString;
            string sqlExpression = String.Format("UPDATE Units SET Name='{0}' WHERE ID={1}", name, id);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery(); //Выполнение запроса без возращения данных
            }
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            UltimateSQLInsert();
        }

        private void GreenButton_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)GreenButton.IsChecked)
                GreenRectangle.Visibility = Visibility.Visible;
            else
                GreenRectangle.Visibility = Visibility.Collapsed;
        }

        private void RedButton_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)RedButton.IsChecked)
                RedRectangle.Visibility = Visibility.Visible;
            else
                RedRectangle.Visibility = Visibility.Collapsed;
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            UltimateSQLSelect();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            UltimateSQLDelite(Convert.ToInt32(IDTextBox.Text));
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            UltimateSQLUpdate(Convert.ToInt32(IDTextBox.Text), NameTextBox.Text);
        }
    }
}
