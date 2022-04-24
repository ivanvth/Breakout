using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;

namespace Breakout
{

    public class Player : IGameEventProcessor
    {
        private Entity playerEntity;
        //private DynamicShape playerShape;

        private AnimationContainer playerAnimation;
        private List<Image> playerStrides;

        public Player(DynamicShape playerShape)
        {
            playerAnimation = new AnimationContainer(1);
            playerStrides = ImageStride.CreateStrides(3, Path.Combine("..", "Breakout", "Assets", "Images", "playerStride.png"));
            
            playerEntity = new Entity(playerShape, new ImageStride(80, playerStrides));

            GameBus.GetBus().Subscribe(GameEventType.PlayerEvent, this);
        }

        public void ProcessEvent(GameEvent gameEvent)
        {
            switch (gameEvent.StringArg1) {
                case "START_MOVE":
                    switch (gameEvent.StringArg2) {
                        case "LEFT":
                            MoveLeft(true);
                            break;
                        case "RIGHT":
                            MoveRight(true);
                            break;
                    }
                    break;
                case "STOP_MOVE":
                    switch (gameEvent.StringArg2) {
                        case "LEFT":
                            MoveLeft(false);
                            break;
                        case "RIGHT":
                            MoveRight(false);
                            break;
                    }
                    break;
            }
        }

        private void MoveLeft(bool shouldMove) {
            System.Console.WriteLine(shouldMove ? "start move left" : "stop move left"); 
        }

        private void MoveRight(bool shouldMove) {
            System.Console.WriteLine(shouldMove ? "start move right" : "stop move right");
        }
        public void Render() {
            playerEntity.RenderEntity();
        }
    }
}