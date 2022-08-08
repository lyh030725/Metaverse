using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    #region Stat

    [Serializable]
    public class Stat
    {
        public int level;
        public int hp;
        public int attack;
    }
    [Serializable]
    public class StatData : ILoder<int, Stat>
    {
        List<Stat> stats = new List<Stat>();
        public Dictionary<int, Stat> MakeDict()
        {
            Dictionary<int, Stat> dict = new Dictionary<int, Stat>();

            foreach (Stat stat in stats)
                dict.Add(stat.level, stat);

            return dict;
        }
    }

    #endregion
}
