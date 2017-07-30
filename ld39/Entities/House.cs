using Microsoft.Xna.Framework;
using ld39.Entities.Behaviour;

namespace ld39.Entities
{
    public class House : Entity, INeedPower
    {
        private Animation offAnimation = new Animation(new int[] { 0 }, 4, false);
        private Animation onAnimation = new Animation(new int[] { 1 }, 4, false);

        private bool isElectrified;
        public bool IsElectrified
        {
            get { return isElectrified; }
            set
            {
                isElectrified = value;
                if (isElectrified)
                    Animator.SetAnimation(onAnimation);
                else
                    Animator.SetAnimation(offAnimation);
            }
        }
        public int RangeElectrified { get; private set; }

        public House(int x, int y)
        {
            CODE_ENTITY = 3;
            Texture = Art.House;
            Color = Color.White;
            Position = new Point(x, y);
            Scale = 2f;

            Animator = new Animator(32, 32, 1, 2);
            Animator.SetAnimation(offAnimation);

            isElectrified = false;
            RangeElectrified = 1;
        }
    }
}
