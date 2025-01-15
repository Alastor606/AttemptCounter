using GlobalHotKey;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using TryCounter.Models;
using TryCounter.Models.Data;


namespace TryCounter.Views
{

    public partial class CounterPage : Page
    {
        public Counter CurrentCounter;
        public FolderPage BackPage;
        public Action<string> OnChange, OnSwapCounter;

        public CounterPage()
        {
            InitializeComponent();
            CounterAPI.BindActions(new List<Action<KeyPressedEventArgs>>()
            {
                ShowOverlay,
                Add,
                Remove,
                CounterNext
            });
            MainWindow.singletone.Closing += delegate
            {
                if(CurrentCounter != null && CounterName.Text != string.Empty)CurrentCounter.Name = CounterName.Text;
            };
        }

        public CounterPage(Counter counter, FolderPage backPage)
        {
            InitializeComponent();
            Init(counter,backPage);
        }

        public void Init(Counter counter, FolderPage backPage)
        {
            
            CurrentCounter = counter;
            BackPage = backPage;
            ShowAllAttempts.IsChecked = CounterAPI.Settings.ShowFolderCounts;
            TextPositionInput.ItemsSource = Enum.GetValues(typeof(TextPosition)).Cast<TextPosition>().ToList();
            TextPositionInput.SelectedIndex = (int)CounterAPI.Settings.TextPos;
            Render();
            AdditionalValue.TextChanged += delegate
            {
                if (!int.TryParse(AdditionalValue.Text, out int value))
                {
                    AdditionalValue.Text = string.Empty;
                    return;
                }
                CurrentCounter.Additional = value;
            };
            CounterName.TextChanged += delegate
            {
                CurrentCounter.Name = CounterName.Text;
                PlaceHolder();
            };
            CounterName.GotFocus += delegate
            {
                CounterName.Text = string.Empty;
            };
            ShowAllAttempts.Checked += delegate
            {
                CounterAPI.Settings.ShowFolderCounts = (bool)ShowAllAttempts.IsChecked;
            };
            TextPositionInput.SelectionChanged += delegate
            {
                CounterAPI.Settings.TextPos = (TextPosition)TextPositionInput.SelectedItem;
            };

            CounterAPI.Bind(new HotKey(Key.A, ModifierKeys.Alt));
            CounterAPI.Bind(new HotKey(Key.E, ModifierKeys.Alt));
            CounterAPI.Bind(new HotKey(Key.Q, ModifierKeys.Alt));
            CounterAPI.Bind(new HotKey(Key.E, ModifierKeys.Control | ModifierKeys.Alt));

            OverlayFontSize.Text = CounterAPI.Settings.FontSize.ToString();
            colorDisplay.Fill = new System.Windows.Media.SolidColorBrush(CounterAPI.Settings.FontColor);

            OverlayFontSize.TextChanged += delegate
            {
                if (!int.TryParse(OverlayFontSize.Text, out int value))
                {
                    OverlayFontSize.Text = "14";
                    CounterAPI.Settings.FontSize = 14;
                    return;
                }
                CounterAPI.Settings.FontSize = value;
            };
        }

        private void PlaceHolder()
        {
            if (CounterName.Text != "Введите название счетчика" && CounterName.Text == string.Empty && !CounterName.IsFocused) CounterName.Text = "Введите название счетчика";
        }

        private void Render()
        {
            CounterName.Text = CurrentCounter.Name;
            CounterTB.Text = CurrentCounter.Count.ToString();
            AdditionalValue.Text = CurrentCounter.Additional.ToString();
        }

        public void CounterNext(KeyPressedEventArgs e)
        {
            if (e.HotKey.Key != Key.E || e.HotKey.Modifiers != (ModifierKeys.Control | ModifierKeys.Alt)) return;
            var id = BackPage.CurrentFolder.Counters.FindIndex(x=>x.Name == CurrentCounter.Name);
            if (id == BackPage.CurrentFolder.Counters.Count - 1) CurrentCounter = BackPage.CurrentFolder.Counters[0];
            else if (BackPage.CurrentFolder.Counters.Count > 1)CurrentCounter = BackPage.CurrentFolder.Counters[id + 1];

            if(!(bool)ShowAllAttempts.IsChecked)OnSwapCounter?.Invoke($"{CurrentCounter.Name} : {CurrentCounter.Count}");
            else OnSwapCounter?.Invoke($"{BackPage.CurrentFolder.Name} : {BackPage.CurrentFolder.FullCount}\n{CurrentCounter.Name} : {CurrentCounter.Count}");
            Render();
        }

        public void Remove(KeyPressedEventArgs e)
        {
            if (e.HotKey.Key != Key.E || e.HotKey.Modifiers != ModifierKeys.Alt) return;
            RemoveButton_Click(null,null);
            if (!(bool)ShowAllAttempts.IsChecked) OnChange?.Invoke($"{CurrentCounter.Name} : {CurrentCounter.Count}");
            else OnChange?.Invoke($"{BackPage.CurrentFolder.Name} : {BackPage.CurrentFolder.FullCount}\n{CurrentCounter.Name} : {CurrentCounter.Count}");
        }

        public void Rebind() =>
            CounterAPI.Bind(new HotKey(Key.Q, ModifierKeys.Alt));
        

        private void ShowOverlay(KeyPressedEventArgs e)
        {
            if (e.HotKey.Key != Key.Q || !MainWindow.singletone.IsVisible) return;
            ShowOverlay(null, null);
        }

        public void Add(KeyPressedEventArgs e)
        {
            if(e.HotKey.Key == Key.A)
            {
                AddButton_Click(null, null);
                if (!(bool)ShowAllAttempts.IsChecked) OnChange?.Invoke($"{CurrentCounter.Name} : {CurrentCounter.Count}");
                else OnChange?.Invoke($"{BackPage.CurrentFolder.Name} : {BackPage.CurrentFolder.FullCount}\n{CurrentCounter.Name} : {CurrentCounter.Count}");
            }
        }

        private void AddButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CurrentCounter.Add();
            CounterTB.Text = CurrentCounter.Count.ToString();
        }

        private void RemoveButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CurrentCounter.Remove();
            CounterTB.Text = CurrentCounter.Count.ToString();
        }

        private void Back(object sender, System.Windows.RoutedEventArgs e)
        {
            CurrentCounter = null;
            BackPage.Refresh();
            NavigationService.GoBack();
            CounterAPI.UnBind(new HotKey(Key.A, ModifierKeys.Alt));
            CounterAPI.UnBind(new HotKey(Key.E, ModifierKeys.Alt));
            CounterAPI.UnBind(new HotKey(Key.Q, ModifierKeys.Alt));
            CounterAPI.UnBind(new HotKey(Key.E, ModifierKeys.Control | ModifierKeys.Alt));
            AdditionalValue.TextChanged -= delegate
            {
                if (!int.TryParse(AdditionalValue.Text, out int value))
                {
                    AdditionalValue.Text = string.Empty;
                    return;
                }
                CurrentCounter.Additional = value;
            };
            CounterName.TextChanged -= delegate
            {
                CurrentCounter.Name = CounterName.Text;
                PlaceHolder();
            };
            CounterName.GotFocus -= delegate
            {
                CounterName.Text = string.Empty;
            };
            ShowAllAttempts.Checked -= delegate
            {
                CounterAPI.Settings.ShowFolderCounts = (bool)ShowAllAttempts.IsChecked;
            };
            TextPositionInput.SelectionChanged -= delegate
            {
                CounterAPI.Settings.TextPos = (TextPosition)TextPositionInput.SelectedItem;
            };
            OverlayFontSize.TextChanged -= delegate
            {
                if (!int.TryParse(OverlayFontSize.Text, out int value))
                {
                    OverlayFontSize.Text = "14";
                    CounterAPI.Settings.FontSize = 14;
                    return;
                }
                CounterAPI.Settings.FontSize = value;
            };
        }

        private void ShowOverlay(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.singletone.Hide();
            CounterAPI.UnBind(new HotKey(Key.Q, ModifierKeys.Alt));
            var overlay = new OverlayWindow(this);
            var screens = Screen.AllScreens;

            if (screens.Length > 1)
            {
                var secondScreen = screens[0];
                var workingArea = secondScreen.WorkingArea;

                overlay.Left = workingArea.Left;
                overlay.Top = workingArea.Top;
            }

            overlay.Show();

            if (!(bool)ShowAllAttempts.IsChecked) OnChange?.Invoke($"{CurrentCounter.Name} : {CurrentCounter.Count}");
            else OnChange?.Invoke($"{BackPage.CurrentFolder.Name} : {BackPage.CurrentFolder.FullCount}\n{CurrentCounter.Name} : {CurrentCounter.Count}");
            overlay.Closed += delegate
            {
                CounterAPI.Save();
            }; 
        }

        private void PickColor(object sender, System.Windows.RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                System.Drawing.Color selectedColor = colorDialog.Color;

                colorDisplay.Fill = new System.Windows.Media.SolidColorBrush(
                    System.Windows.Media.Color.FromArgb(selectedColor.A, selectedColor.R, selectedColor.G, selectedColor.B));
                CounterAPI.Settings.FontColor = System.Windows.Media.Color.FromArgb(selectedColor.A, selectedColor.R, selectedColor.G, selectedColor.B);
            }
        }

        private void RemoveCounter(object sender, System.Windows.RoutedEventArgs e)
        {
            CounterAPI.UnBind(new HotKey(Key.A, ModifierKeys.Alt));
            CounterAPI.UnBind(new HotKey(Key.E, ModifierKeys.Alt));
            CounterAPI.UnBind(new HotKey(Key.Q, ModifierKeys.Alt));
            CounterAPI.UnBind(new HotKey(Key.E, ModifierKeys.Control | ModifierKeys.Alt));
            CounterAPI.RemoveCounter(BackPage.CurrentFolder.Name,CurrentCounter.Name);
            NavigationService.GoBack();
            BackPage.Refresh();
        }
    }
}
