using GlobalHotKey;
using System.Windows;
using TryCounter.Models;
using TryCounter.Views;

namespace TryCounter
{
    public partial class MainWindow : Window
    {
        public static MainWindow singletone;
        public MainWindow()
        {
            InitializeComponent();
            CounterAPI.Init();
            MainFrame.Navigate(new MainPage());
            this.Closing += delegate
            {
                CounterAPI.Save();
            };
            singletone = this;
        }
    }
}
