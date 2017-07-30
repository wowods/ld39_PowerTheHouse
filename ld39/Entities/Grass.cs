using Microsoft.Xna.Framework;
using ld39.Entities.Behaviour;
using System;

namespace ld39.Entities
{
    public class Grass : Entity, IOverlap, IElectrified
    {
        public bool IsElectrified { get; set; }
        public int RangeElectrified { get; private set; }

        public Grass(int x, int y)
        {
            CODE_ENTITY = 1;
            Texture = Art.Grass;
            Color = Color.White;
            Position = new Point(x, y);
            Scale = 2f;
            Animator = null;

            IsElectrified = false;
            RangeElectrified = 1;
        }
    }
}
