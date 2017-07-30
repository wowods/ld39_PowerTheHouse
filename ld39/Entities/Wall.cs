using ld39.Entities.Behaviour;
using Microsoft.Xna.Framework;

namespace ld39.Entities
{
    public class Wall : Entity, IBlockElectricity
    {
        public Wall(int x, int y)
        {
            CODE_ENTITY = 6;
            Texture = Art.Wall;
            Color = Color.White;
            Position = new Point(x, y);
            Scale = 2f;
            Animator = null;
        }
    }
}
