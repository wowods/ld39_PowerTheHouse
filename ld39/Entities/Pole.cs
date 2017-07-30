using Microsoft.Xna.Framework;
using ld39.Entities.Behaviour;
using System;

namespace ld39.Entities
{
    public class Pole : Entity, IElectrified, IMoveable, INeedPower
    {
        public bool IsElectrified { get; set; }
        public int RangeElectrified { get; private set; }

        public Pole(int x, int y)
        {
            CODE_ENTITY = 4;
            Texture = Art.Pole;
            Color = Color.White;
            Position = new Point(x, y);
            Scale = 2f;
            Animator = null;

            RangeElectrified = 1;
            IsElectrified = false;
        }

        public void Moving()
        {
            Position = Cursor.Instance.Position;
            Color = Art.SelectedTint;
        }
    }
}
