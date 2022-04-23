namespace Breakout.LevelData
{
    public class Level
    {
        List<string> levelLines;
        string name;

        public Level(string name, List<string> levelLines)
        {
            this.levelLines = levelLines;
            this.name = name;
        }
    }
}