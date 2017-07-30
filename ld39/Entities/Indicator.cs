using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ld39.Entities
{
    public class Indicator : Entity
    {
        public bool IsExpired { get; private set; }

        public Indicator(int x, int y)
        {
            Texture = Art.Indicator;
            Color = Color.White;
            Position = new Point(x, y);
            Scale = 2f;

            Animation indicatorAnimation = new Animation(new int[] { 0, 1 }, 2, true);
            Animator = new Animator(32, 32, 1, 2);
            Animator.SetAnimation(indicatorAnimation);
        }

        public void Reset(int x, int y)
        {
            IsExpired = false;
            Position = new Point(x, y);
        }

        public void Kill()
        {
            IsExpired = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!IsExpired)
                base.Draw(spriteBatch);
        }
    }
}
