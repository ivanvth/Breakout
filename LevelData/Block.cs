using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.LevelData
{
    public class Block : Entity
    {
        int Value = 100;
        int Health = 100;

        IBaseImage damagedImage;
        public Block(Shape shape, IBaseImage image, IBaseImage damaged) : base(shape, image)
        {
            damagedImage = damaged;
        }

        public void DamageBlock(int damage) {
            Health -= damage;
            if (Health <= 50) {
                this.Image = damagedImage;
            }
            if (Health <= 0) {
                this.DeleteEntity();
            }          
        }

    }
}