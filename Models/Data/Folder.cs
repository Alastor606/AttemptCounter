using System.Collections.Generic;
using System.Linq;

namespace TryCounter.Models.Data
{
    public class Folder
    {
        public string Name { get; set; }
        public List<Counter> Counters { get; set; }

        public Folder(string name)
        {
            Name = name;
            Counters = new List<Counter>();
        }

        public Folder()
        {
            Counters = new List<Counter>();
        }

        public Counter GetCounter(string name)
        {
            if (Counters.FirstOrDefault(x => x.Name == name) == null) return null;
            return Counters.First(x => x.Name == name);
        }
    }
}
