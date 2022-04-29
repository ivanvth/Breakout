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
        public string Name {get; private set;}

        private Dictionary<string, string> metaData = new();

        public int blockHeightOfScreen { get; private set; }
        public int blockWidthOfScreen { get; private set; }
        public float blockHeight { get; private set; }
        public float blockWidth { get; private set; }
        public Level(string name, string[,] map, Dictionary<string, string> metaData)
        {
            this.mapChars = map;
            this.Name = name;
            this.metaData = metaData;
            MapToBlocks();
        }

        public void RememberToDelete() {
            blocks.Iterate(block => block.DamageBlock(50));
            System.Console.WriteLine(blocks.CountEntities());
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

        public override string ToString() {
            string s = "Level Name: " + Name;

            foreach (KeyValuePair<string, string> entry in metaData) {
                if (!entry.Key.Equals("Name")) {
                    s += "\n" + $"{entry.Key, 20} - {entry.Value, 10}";
                }
            }

            return s;
        }
    }
}