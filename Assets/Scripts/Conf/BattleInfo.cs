using System.Collections.Generic;
using Core;

namespace Conf
{
    public abstract class BattleInfo
    {
        public class Entry
        {
            public TeamFlag Flag;
            public UnitInfo Info;
            public int SpawnX;
            public int SpawnY;
        }
        
        public readonly List<Entry> Units = new List<Entry>();

        protected void AddBlue(UnitInfo info, int spawnX, int spawnY)
        {
            Add(TeamFlag.Blue, info, spawnX, spawnY);
        }

        protected void AddRed(UnitInfo info, int spawnX, int spawnY)
        {
            Add(TeamFlag.Red, info, spawnX, spawnY);
        }
        
        private void Add(TeamFlag flag, UnitInfo info, int spawnX, int spawnY)
        {
            Units.Add(new Entry
            {
                Flag = flag,
                Info = info,
                SpawnX = spawnX,
                SpawnY = spawnY
            });
        }
    }
}