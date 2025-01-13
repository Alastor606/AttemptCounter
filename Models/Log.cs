using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryCounter.Models
{
    internal static class Log
    {
        public static void Add(string message)
        {
            File.AppendAllText(CounterAPI.OverlaySettingsPath, $"[{DateTime.Now.ToString("dd.MM.HH.mm.ss")}]"+  message + "\n");
        }
    }
}
