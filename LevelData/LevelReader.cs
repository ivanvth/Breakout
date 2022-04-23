using System.IO;

namespace Breakout.LevelData
{
    public class LevelReader
    {
        string levelPath;
        public LevelReader(string fileName)
        {
            levelPath = Path.Combine("..", "Breakout", "Assets", "Levels", fileName);
            
        }
    }
}