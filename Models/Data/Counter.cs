using System;

namespace TryCounter.Models.Data
{
    public class Counter
    {
        public string Name { get; set; }
        public int Count { get; private set; }
        public int Additional { get; set; }

        public void Add()
        {
            Count += Additional;
        }

        public void Remove()
        {
            Count -= Additional;
        }

        public Counter(string name, int count = 0, int additional = 1)
        {
            Name = name;
            Count = count;
            Additional = additional;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
