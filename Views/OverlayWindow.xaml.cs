using GlobalHotKey;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;
using TryCounter.Models;
using TryCounter.Models.Data;

namespace TryCounter.Views
{
    public partial class OverlayWindow : Window
    {
        private CounterPage CurrentCounter;
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOMOVE = 0x0002;

        public OverlayWindow(CounterPage currentCounter)
        {
            InitializeComponent();
            CounterAPI.Bind(new HotKey(Key.Q, ModifierKeys.Alt), ShowSelf);

            CounterText.FontSize = CounterAPI.Settings.FontSize;
            CounterText.Foreground = new System.Windows.Media.SolidColorBrush(CounterAPI.Settings.FontColor);
            CurrentCounter = currentCounter;
            CurrentCounter.OnChange += x =>
            {
                CounterText.Text = x;
            };

            CurrentCounter.OnSwapCounter += x =>
            {
                CounterText.Text = x;
            };

            this.Topmost = true;

            Loaded += (s, e) =>
            {
                SetWindowPos(new System.Windows.Interop.WindowInteropHelper(this).Handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
            };
            SetTextPos();
        }

        private void SetTextPos()
        {
            switch (CounterAPI.Settings.TextPos)
            {
                case TextPosition.leftTop:
                    break;
                case TextPosition.leftCenter:
                    CounterText.VerticalAlignment = VerticalAlignment.Center;
                    break;
                case TextPosition.leftBot:
                    CounterText.VerticalAlignment = VerticalAlignment.Bottom;
                    break;
                case TextPosition.rightTop:
                    CounterText.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    CounterText.VerticalAlignment = VerticalAlignment.Top;
                    break;
                case TextPosition.rightCenter:
                    CounterText.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    CounterText.VerticalAlignment = VerticalAlignment.Center;
                    break;
                case TextPosition.rightBot:
                    CounterText.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    CounterText.VerticalAlignment = VerticalAlignment.Bottom;
                    break;
                case TextPosition.midBot:
                    CounterText.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    CounterText.VerticalAlignment = VerticalAlignment.Bottom;
                    break;
                case TextPosition.midCenter:
                    CounterText.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    CounterText.VerticalAlignment = VerticalAlignment.Center;
                    break;
                case TextPosition.midTop:
                    CounterText.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    CounterText.VerticalAlignment = VerticalAlignment.Top;
                    break;
            }
            
        }

        private void ShowSelf(KeyPressedEventArgs e)
        {
            if (e.HotKey.Key != Key.Q || !this.IsVisible) return;
            
            CurrentCounter.OnChange -= x =>
            {
                CounterText.Text = x;
            };
            CounterAPI.UnBind(new HotKey(Key.Q, ModifierKeys.Alt));
            CounterAPI.Save();
            MainWindow.singletone.Show();
            CurrentCounter.Rebind();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var screens = Screen.AllScreens;

            if (screens.Length > 1)
            {
                var secondScreen = screens[0].WorkingArea;

                this.Left = secondScreen.Left;
                this.Top = secondScreen.Top;
                this.Width = secondScreen.Width;
                this.Height = secondScreen.Height;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }
    }
}
