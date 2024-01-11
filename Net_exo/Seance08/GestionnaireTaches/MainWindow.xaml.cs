using System;
using System.Collections.Generic;
using System.Data;
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

namespace GestionnaireTaches
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IList<Tache> placesList;

        public MainWindow()
        {
            InitializeComponent();
            placesList = new List<Tache>();

            dgTaches.ItemsSource = placesList;
        }

        private void Button_Click_Clean(object sender, RoutedEventArgs e)
        {
            placesList.Clear();
            dgTaches.Items.Refresh();
 
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            ComboBoxItem priorityItem = (ComboBoxItem) dropdownPriority.SelectedItem;
            string descriptionText = textBoxDescription.Text;
            bool? status = checkboxStatus.IsChecked;
            DateTime limitTime = datePicker.DisplayDate;

            

            if (priorityItem != null &&
                descriptionText != "")
            {
                placesList.Add(new Tache(descriptionText, Int32.Parse(priorityItem.Content.ToString()), (bool)status, limitTime));
            }
            dgTaches.Items.Refresh();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (dgTaches.SelectedItem != null)
            {
                placesList.Remove((Tache)dgTaches.SelectedItem);
            }
            dgTaches.Items.Refresh();
        }
    }
}
