using GlobalHotKey;
using System.ComponentModel;
using System.Threading;
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
            singletone = this;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            CounterAPI.Save();
            base.OnClosing(e);
        }
    }
}
