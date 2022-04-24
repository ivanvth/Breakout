using DIKUArcade.Events;

namespace Breakout
{
    public static class GameBus
    {
        private static GameEventBus eventBus;

        public static GameEventBus GetBus() {
            return eventBus ?? (eventBus = new GameEventBus());
        }

        public static void DeleteBus() {
            eventBus = null;
        }
    }
}