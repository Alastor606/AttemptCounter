using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace TryCounter
{
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Создаем главное окно приложения
            MainWindow mainWindow = new MainWindow();

            // Получаем информацию о всех экранах
            Screen[] screens = Screen.AllScreens;

            // Проверяем, есть ли хотя бы два экрана
            if (screens.Length > 1)
            {
                // Указываем, на каком экране открывать окно (например, второй экран)
                Screen screen = screens[0]; // Индекс 0 - первый экран, индекс 1 - второй экран

                // Устанавливаем позицию окна
                mainWindow.Left = screen.Bounds.X + (screen.Bounds.Width - mainWindow.Width) / 2;
                mainWindow.Top = screen.Bounds.Y + (screen.Bounds.Height - mainWindow.Height) / 2;
            }

            mainWindow.Show();
        }
    }
}
