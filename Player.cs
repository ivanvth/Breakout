using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout
{

    public class Player : IGameEventProcessor
    {
        private Entity playerEntity;
        private DynamicShape playerShape;

        private float moveLeft = 0.0f;
        private float moveRight = 0.0f;
        private const float MOVEMENT_SPEED = 0.02f;
        private float minX;
        private float maxX;

        private AnimationContainer playerAnimation;
        private List<Image> playerStrides;

        public Player(DynamicShape playerShape)
        {
            this.playerShape = playerShape;

            playerAnimation = new AnimationContainer(1);
            playerStrides = ImageStride.CreateStrides(3, Path.Combine("..", "Breakout", "Assets", "Images", "playerStride.png"));
            
            playerEntity = new Entity(this.playerShape, new ImageStride(180, playerStrides));

            GameBus.GetBus().Subscribe(GameEventType.PlayerEvent, this);

            minX = 0f;
            maxX = 1f - this.playerShape.Extent.X;
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

        public void Move() {
            playerShape.Move();
            if (playerShape.Position.X < minX) {
                playerShape.Position.X = minX;
            }
            else if (playerShape.Position.X > maxX) {
                playerShape.Position.X = maxX;
            }
        }
        private void MoveLeft(bool shouldMove) {
            moveLeft = shouldMove ? MOVEMENT_SPEED : 0.0f;
            UpdateDirection();
        }

        private void MoveRight(bool shouldMove) {
            moveRight = shouldMove ? MOVEMENT_SPEED : 0.0f;
            UpdateDirection();
        }

        private void UpdateDirection() {
            playerShape.ChangeDirection(new Vec2F(moveRight-moveLeft, 0.0f));
        }
        public void Render() {
            Move();
            playerEntity.RenderEntity();
        }
    }
}