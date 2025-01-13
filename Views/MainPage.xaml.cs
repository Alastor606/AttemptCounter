using System.Windows.Controls;
using System.Windows.Navigation;
using TryCounter.Models;
using TryCounter.Models.Data;

namespace TryCounter.Views
{

    public partial class MainPage : Page
    {
        private static ListBox Folders;
        public MainPage()
        {
            InitializeComponent();
            if (CounterAPI.Data.Folders.Count < 1) return;
            FolderList.ItemsSource = CounterAPI.Data.Folders;
            Folders = FolderList;
        }

        public static void Refresh()
        {
            CounterAPI.Refresh();
            Folders.ItemsSource = CounterAPI.Data.Folders;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new FolderPage());
        }

        private void FolderList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new FolderPage(FolderList.SelectedItem as Folder));
        }
    }
}
