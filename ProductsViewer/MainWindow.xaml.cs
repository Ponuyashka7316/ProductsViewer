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


namespace ProductsViewer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
           // InitializeComponent();
        }
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            string advworksConnectionString =
"server=localhost;async=true;integrated security=true;database=AdventureWorks;";
            SqlConnection advworksConnection = new SqlConnection(advworksConnectionString);
            advworksConnection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand
            ("SELECT LargePhotoFileName FROM Production.ProductPhoto WHERE "
            + "ProductPhotoID <= 100", advworksConnection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "PhotoList");
            productsListBox.DataContext = dataSet;

        }

        private void GetPhoto(object sender, SelectionChangedEventArgs args)
        {
            string photoFileName;
            object selected = productsListBox.SelectedItem;
            DataRow row = ((DataRowView)selected).Row;
            photoFileName = row.ItemArray[0].ToString();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"E:\Labfiles\Images\" + photoFileName);
            bi.EndInit();
            productImage.Source = bi;

        }



    }
}
