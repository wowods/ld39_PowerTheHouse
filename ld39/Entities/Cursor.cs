using Microsoft.Xna.Framework;
using ld39.Entities.Behaviour;

namespace ld39.Entities
{
    public class Cursor : Entity, IOverlap
    {
        private static Cursor instance;
        public static Cursor Instance
        {
            get
            {
                if (instance == null)
                    instance = new Cursor();
                return instance;
            }
        }

        public bool IsSelected { get; private set; }

        private Cursor()
        {
            Texture = Art.Cursor;
            Color = Color.White;
            Position = new Point(0, 0);
            Scale = 2f;
            Animator = null;

            IsSelected = false;
        }

        public override void Update(float dt)
        {
            if (GameMain.State.Equals(GameMain.GAMESTATE.Playing))
                Position = Position.Clamp(0, 9, 0, 9);
            base.Update(dt);
        }

        public void ChangeSelectedState()
        {
            if (IsSelected)
                IsSelected = false;
            else
                IsSelected = true;
        }
    }
}
