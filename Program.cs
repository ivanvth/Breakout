using DIKUArcade.GUI;

namespace Breakout {
    public class Program {

        static void Main(string[] args)
        {
            var windowArgs = new WindowArgs() { Title = "Breakout", AspectRatio = WindowAspectRatio.Aspect_4X3, Resizable = false };
            Game game = new Game(windowArgs);
            game.Run();
        }
    }
}