using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ld39.Entities
{
    public abstract class Entity
    {
        public int CODE_ENTITY;
        public Texture2D Texture;
        public Color Color;
        public Point Position;
        public float Scale;
        protected Animator Animator;

        private Vector2 size;

        public virtual void Update(float dt)
        {
            if (Animator != null)
                Animator.Update(dt);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Animator == null)
                spriteBatch.Draw(Texture, Position.GetPosition(), null, Color, 0f, Vector2.Zero, Scale, 0f, 0f);
            else
                spriteBatch.Draw(Texture, Position.GetPosition(), Animator.GetRectangle(), Color, 0f, Vector2.Zero, Scale, 0f, 0f);
        }
    }
}
