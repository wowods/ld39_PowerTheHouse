using Microsoft.Xna.Framework;

namespace ld39.Entities
{
    public class Blocked : Entity
    {
        public Blocked(int x, int y)
        {
            CODE_ENTITY = 2;
            Texture = Art.Blocked;
            Color = Color.White;
            Position = new Point(x, y);
            Scale = 2f;
            Animator = null;
        }
    }
}
