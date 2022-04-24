using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.LevelData
{
    public class Level
    {
        string[,] mapChars;
        EntityContainer<Block> blocks = new EntityContainer<Block>();
        string name;

        public int blockHeightOfScreen { get; private set; }
        public int blockWidthOfScreen { get; private set; }
        public float blockHeight { get; private set; }
        public float blockWidth { get; private set; }
        public Level(string name, string[,] map)
        {
            this.mapChars = map;
            this.name = name;
            MapToBlocks();
        }

        private void MapToBlocks() {
            blockHeightOfScreen = mapChars.GetLength(0);
            blockWidthOfScreen = mapChars.GetLength(1);
            blockHeight = 1f / blockHeightOfScreen;
            blockWidth = 1f / blockWidthOfScreen;

            for (int i=0; i<mapChars.GetLength(0); i++) {
                for (int j=0; j<mapChars.GetLength(1); j++) {
                    string blockName = mapChars[i, j];
                    if (!blockName.Equals("none")) {
                        string[] splitBlockName = blockName.Split('.');
                        string damagedBlockName = splitBlockName[0] + "-damaged.png";
                        float posY = 1f - blockHeight - i * blockHeight;
                        float posX = j * blockWidth;
                        
                        blocks.AddEntity(new Block(
                            new StationaryShape(posX, posY, blockWidth, blockHeight),
                            new Image(Path.Combine("..", "Breakout", "Assets", "Images", blockName)),
                            new Image(Path.Combine("..", "Breakout", "Assets", "Images", damagedBlockName))
                        ));
                    }
                    
                }
            }
        }
        public void Render() {
            
            blocks.RenderEntities();
        }
    }
}