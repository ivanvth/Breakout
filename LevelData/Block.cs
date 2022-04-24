using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.LevelData
{
    public class Block : Entity
    {
        int Value;
        int Health;

        IBaseImage damagedImage;
        public Block(Shape shape, IBaseImage image, IBaseImage damaged) : base(shape, image)
        {
            damagedImage = damaged;
        }

        private void DamageBlock() {
            //this.Shape = new StationaryShape(Shape.Position.X, Shape.Position.Y, Shape.Extent.X, Shape.Extent.Y);
            this.Image = damagedImage;
        }
    }
}