using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryCounter.Models
{
    internal static class Extensions
    {
        public static string GetNames<T>(this List<T> self)
        {
            var sb = new StringBuilder();
            foreach (var item in self) sb.Append(item.ToString() + "\n");
            return sb.ToString();
        }
    }
}
