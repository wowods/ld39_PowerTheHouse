using ld39.Entities.Behaviour;
using Microsoft.Xna.Framework;

namespace ld39.Entities
{
    public class Water : Entity, IBlockElectricity
    {
        public Water(int x, int y)
        {
            CODE_ENTITY = 7;
            Texture = Art.Water;
            Color = Color.White;
            Position = new Point(x, y);
            Scale = 2f;

            Animation waterAnimation = new Animation(new int[] { 0, 1, 2, 3, 4, 5 }, 4, true);
            Animator = new Animator(32, 32, 1, 6);
            Animator.SetAnimation(waterAnimation);
        }
    }
}
