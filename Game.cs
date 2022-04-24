using DIKUArcade;
using DIKUArcade.GUI;

using Breakout.LevelData;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;

namespace Breakout
{
    public class Game : DIKUGame, IGameEventProcessor
    {
        LevelManager levelManager;
        Level currentLevel;
        Player player;

        public Game(WindowArgs windowArgs) : base(windowArgs) {
            // events
            window.SetKeyEventHandler(KeyHandler);
            GameBus.GetBus().InitializeEventBus(new List<GameEventType> 
            { 
                GameEventType.GameStateEvent,
                GameEventType.PlayerEvent 
            });
            GameBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            levelManager = new LevelManager("level2.txt");
            currentLevel = levelManager.CurrentLevel;
            CreatePlayer();
        }

        public void KeyHandler(KeyboardAction action, KeyboardKey key) {
            GameEvent gameEvent = new GameEvent();
            switch (action) {
                case KeyboardAction.KeyPress:
                    
                    switch (key) {
                        case KeyboardKey.Escape:
                            gameEvent.EventType = GameEventType.GameStateEvent;
                            gameEvent.StringArg1 = "QUIT";
                            break;
                        case KeyboardKey.Left:
                            gameEvent.EventType = GameEventType.PlayerEvent;
                            gameEvent.StringArg1 = "START_MOVE";
                            gameEvent.StringArg2 = "LEFT";
                            break;
                        case KeyboardKey.Right:
                            gameEvent.EventType = GameEventType.PlayerEvent;
                            gameEvent.StringArg1 = "START_MOVE";
                            gameEvent.StringArg2 = "RIGHT";
                            break;
                    }
                    break;
                case KeyboardAction.KeyRelease:
                    
                    switch (key) {
                        case KeyboardKey.Left:
                            gameEvent.EventType = GameEventType.PlayerEvent;
                            gameEvent.StringArg1 = "STOP_MOVE";
                            gameEvent.StringArg2 = "LEFT";
                            break;
                        case KeyboardKey.Right:
                            gameEvent.EventType = GameEventType.PlayerEvent;
                            gameEvent.StringArg1 = "STOP_MOVE";
                            gameEvent.StringArg2 = "RIGHT";
                            break;
                    }
                    break;
                default:
                    System.Console.WriteLine($"Something else happened - action: {action}, key: {key}.");
                    break;
            }
            GameBus.GetBus().RegisterEvent(gameEvent);
        }
        private void CreatePlayer() {
            float posX = currentLevel.blockWidth * (currentLevel.blockWidthOfScreen * 0.45f);
            float posY = currentLevel.blockHeight;
            float width = currentLevel.blockWidth * 1.5f;
            float height = currentLevel.blockHeight * 0.8f;
            player = new Player(new DynamicShape(posX, posY, width, height));
        }
        public override void Update() {
            GameBus.GetBus().ProcessEventsSequentially();
        }

        public override void Render() {
            currentLevel.Render();
            player.Render();
        }

        public void ProcessEvent(GameEvent gameEvent)
        {
            switch (gameEvent.StringArg1) {
                case "QUIT":
                    window.CloseWindow();
                    break;
                default:
                    System.Console.WriteLine("Something happened - Game.ProcessEvent() ");
                    break;
            }
        }
    }
}