using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TryCounter.Models.Data
{
    public class MainData
    {
        public List<Folder> Folders { get; set; }

        public MainData()
        {
            Folders = new List<Folder>();
        }

        public bool TryGetFolder(string folderName, out Folder folder)
        {
            if(Folders.FirstOrDefault(x=>x.Name == folderName) == null)
            {
                folder = null;
                return false;
            }
            folder = Folders.FirstOrDefault(x => x.Name == folderName);
            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var item in Folders)
            {
                sb.Append($"--{item.Name}--\n");
                for (int i = 0; i < item.Counters.Count; i++)
                {
                    sb.Append($"{i}. {item.Counters[i]}\n");
                }
            }
            return sb.ToString();
        }
    }
}
