using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using TryCounter.Models;
using TryCounter.Models.Data;


namespace TryCounter.Views
{

    public partial class FolderPage : Page
    {
        public Folder CurrentFolder;
        private bool _isNewFolder = false;
        private string folderName;
        private CounterPage _page;


        public FolderPage()
        {
            InitializeComponent();
            CurrentFolder = new Folder();
            _isNewFolder = true;
            _page = new CounterPage();
            FolderName.LostFocus += delegate
            {
                CurrentFolder.Name = FolderName.Text;
                PlaceHolder();
            };
            FolderName.TextChanged += delegate
            {
                PlaceHolder();
            };
            FolderName.GotFocus += delegate
            {
                FolderName.Text = string.Empty;
            };
        }

        public FolderPage(Folder folder)
        {
            InitializeComponent();
            _page = new CounterPage();
            CurrentFolder = folder;
            FolderName.Text = folder.Name;
            CountersList.ItemsSource = folder.Counters;
            folderName = folder.Name;
            FolderName.TextChanged += delegate
            {
                PlaceHolder();
            };
            FolderName.GotFocus += delegate
            {
                FolderName.Text = string.Empty;
            };
        }

        private void PlaceHolder()
        {
            if (FolderName.Text != "Введите название раздела" && FolderName.Text == string.Empty && !FolderName.IsFocused) FolderName.Text = "Введите название раздела";
        }

        public void Refresh()
        {
            CountersList.ItemsSource = null;
            CountersList.ItemsSource = CurrentFolder.Counters;
        }

        private void AddCounter(object sender, System.Windows.RoutedEventArgs e)
        {
            CurrentFolder.Counters.Add(new Counter("New Counter"));
            if (_isNewFolder) CounterAPI.AddFolder(CurrentFolder);
            Refresh();
        }

        private void Back(object sender, System.Windows.RoutedEventArgs e)
        {
            if (CurrentFolder.Name == string.Empty) return;
            if(_isNewFolder)CounterAPI.AddFolder(CurrentFolder);
            else
            {
                CounterAPI.EditFolderName(folderName, FolderName.Text);
            }
            MainPage.Refresh();
            NavigationService.GoBack();
        }

        private void CountersList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (CountersList.SelectedItem == null) return;
            _page.Init(CountersList.SelectedItem as Counter, this);
            NavigationService.Navigate(_page);
        }
    }
}
