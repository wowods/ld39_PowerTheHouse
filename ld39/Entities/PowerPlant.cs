using Microsoft.Xna.Framework;
using ld39.Entities.Behaviour;

namespace ld39.Entities
{
    public class PowerPlant : Entity, IElectrified, IMoveable
    {
        public bool IsElectrified
        {
            // For Power Plant, it's always Electrified
            get { return true; }
            set { }
        }
        public int RangeElectrified { get; private set; }

        public PowerPlant(int x, int y)
        {
            CODE_ENTITY = 5;
            Texture = Art.PowerPlant;
            Color = Color.White;
            Position = new Point(x, y);
            Scale = 2f;

            Animation powerAnimation = new Animation(new int[] { 0, 1, 2, 3, 2, 1 }, 4, true);
            Animator = new Animator(32, 32, 1, 4);
            Animator.SetAnimation(powerAnimation);

            RangeElectrified = 3;
        }

        public void Moving()
        {
            Position = Cursor.Instance.Position;
            Color = Art.SelectedTint;
        }
    }
}
