using DIKUArcade.GUI;

namespace Breakout {
    public class Program {

        static void Main(string[] args)
        {
            var windowArgs = new WindowArgs() { Title = "Breakout", Resizable = false };
            Game game = new Game(windowArgs);
            game.Run();
        }
    }
}