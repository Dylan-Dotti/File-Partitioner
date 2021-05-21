using System;
using System.Collections.Generic;
using System.Linq;

namespace FileSplitter
{
    public static class DictUtils
    {
        public static T GetMinKeyByValue<T>(this Dictionary<T, long> dict)
        {
            return dict.Aggregate((l, r) => l.Value <= r.Value ? l : r).Key;
        }

        public static T GetMinKeyByValue<T>(this Dictionary<T, int> dict)
        {
            return dict.Aggregate((l, r) => l.Value <= r.Value ? l : r).Key;
        }

        public static T GetMaxKeyByValue<T>(this Dictionary<T, long> dict)
        {
            return dict.Aggregate((l, r) => l.Value <= r.Value ? r : l).Key;
        }

        public static T GetMaxKeyByValue<T>(this Dictionary<T, int> dict)
        {
            return dict.Aggregate((l, r) => l.Value <= r.Value ? r : l).Key;
        }
    }
}
