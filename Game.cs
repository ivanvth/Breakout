using DIKUArcade;
using DIKUArcade.GUI;

using Breakout.LevelData;

namespace Breakout
{
    public class Game : DIKUGame
    {
        LevelManager levelManager;

        public Game(WindowArgs windowArgs) : base(windowArgs) {
            levelManager = new LevelManager("level2.txt");
            
        }

        public override void Update() {

        }

        public override void Render() {
            levelManager.CurrentLevel.Render();
        }
    }
}