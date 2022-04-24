using DIKUArcade;
using DIKUArcade.GUI;

using Breakout.LevelData;

namespace Breakout
{
    public class Game : DIKUGame
    {
        LevelManager levelReader;

        public Game(WindowArgs windowArgs) : base(windowArgs) {
            levelReader = new LevelManager("level2.txt");
            
        }

        public override void Update() {

        }

        public override void Render() {

        }
    }
}