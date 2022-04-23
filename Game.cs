using DIKUArcade;
using DIKUArcade.GUI;

using Breakout.LevelData;

namespace Breakout
{
    public class Game : DIKUGame
    {
        LevelReader levelReader;

        public Game(WindowArgs windowArgs) : base(windowArgs) {
            levelReader = new LevelReader("central-mass.txt");
        }

        public override void Update() {

        }

        public override void Render() {

        }
    }
}