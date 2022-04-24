using System.Collections.Generic;

namespace Breakout.LevelData
{
    public class Level
    {
        string[,] mapChars;
        string name;

        public Level(string name, string[,] map)
        {
            this.mapChars = map;
            this.name = name;
        }
    }
}