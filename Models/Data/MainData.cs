using System.Collections.Generic;
using System.Linq;


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
    }
}
