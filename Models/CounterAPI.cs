using GlobalHotKey;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using TryCounter.Models.Data;

namespace TryCounter.Models
{
    internal static class CounterAPI
    {
        public static string DataPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\CounterData\\",
            MainDataPath = DataPath + "data.json",
            OverlaySettingsPath = DataPath + "overlay.json";
        public static MainData Data;
        public static HotKeyManager Manager { get; private set; }
        public static OverlaySettings Settings;

        public static void Init()
        {
            if (!Directory.Exists(DataPath))
            {
                Directory.CreateDirectory(DataPath);
                File.Create(MainDataPath);
                Settings = (new OverlaySettings()
                {
                    FontSize = 14,
                    FontColor = Color.FromRgb(255, 255, 255),
                    ShowFolderCounts = false
                });
                File.WriteAllText(OverlaySettingsPath, JsonConvert.SerializeObject(Settings));
                Data = new MainData();
            }
            else
            {
                Settings = JsonConvert.DeserializeObject<OverlaySettings>(File.ReadAllText(OverlaySettingsPath));
                Data = JsonConvert.DeserializeObject<MainData>(File.ReadAllText(MainDataPath));
            }
            
            Manager = new HotKeyManager();
        }

        public static OverlaySettings GetOverlaySettings() =>
            JsonConvert.DeserializeObject<OverlaySettings>(File.ReadAllText(OverlaySettingsPath));
        

        public static void AddFolder(Folder folder)
        {
            if (Data.Folders.Contains(folder)) return;
            Data.Folders.Add(folder);
        }
            
        
        public static void AddCounter(Counter counter, string folderName)
        {
            if (!Data.TryGetFolder(folderName, out var folder)) return;
            folder.Counters.Add(counter);
        }

        public static void RemoveFolder(string folderName) => Data.Folders.Remove(Data.Folders.First(x => x.Name == folderName));
        public static void RemoveCounter(string folderName, string counterName)
        {
            
            if (!Data.TryGetFolder(folderName, out var folder)) return;
            var counter = folder.Counters.FirstOrDefault(x => x.Name == counterName);
            if(counter == null)
            {
                Log.Add("Try delete counter but it doesnt exsitsts");
                return;
            }
            folder.Counters.Remove(counter);
        }

        public static void EditFolderName(string lastFolderName, string newFolderName)
        {
            if (!Data.TryGetFolder(lastFolderName,out var folder)) return;
            folder.Name = newFolderName;
        }

        public static void EditCounterName(string folderName,string lastCounterName, string newCounterName)
        {
            if (!Data.TryGetFolder(folderName, out var folder)) return;
            var counter = folder.GetCounter(lastCounterName);
            counter.Name = newCounterName;
        }

        public static void Save()
        {
            try
            {
                File.WriteAllText(MainDataPath, JsonConvert.SerializeObject(Data));
                File.WriteAllText(OverlaySettingsPath, JsonConvert.SerializeObject(Settings));
            }
            catch (Exception ex)
            {
                File.AppendAllText(DataPath + "logs.txt", $"[{DateTime.Now.ToString("dd.MM.HH.mm.ss")}] " +ex.Message + "\n");
            }
        }

        public static void Refresh()
        {
            Save();
            Data = JsonConvert.DeserializeObject<MainData>(File.ReadAllText(MainDataPath));
        }

        public static void Bind(HotKey hotkey) =>
            Manager.Register(hotkey);
        
        
        public static void UnBind(HotKey hotkey) =>
            Manager.Unregister(hotkey);

        public static void BindActions(List<Action<KeyPressedEventArgs>> actions)
        {
            foreach (var item in actions) Manager.KeyPressed += (o,e) => item?.Invoke(e);
        }
        
    }
}
