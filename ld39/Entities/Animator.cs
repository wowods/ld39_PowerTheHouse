using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ld39.Entities
{
    public class Animator
    {
        // For animator
        private int width, height, row, column;

        // For animating
        private List<Rectangle> rectangles = new List<Rectangle>();
        private Animation currentAnimation;
        private float currentTime;
        private float duration;
        private int frame;

        public Animator(int spriteWidth, int spriteHeight, int row, int column)
        {
            width = spriteWidth;
            height = spriteHeight;
            this.row = row;
            this.column = column;

            generateRectangles();
        }

        private void generateRectangles()
        {
            for (int i = 0; i < row * column; i++)
            {
                Rectangle rect = new Rectangle()
                {
                    X = (i % column) * width,
                    Y = (int)Math.Floor((double)i / column) * height,
                    Width = width,
                    Height = height
                };
                rectangles.Add(rect);
            }
        }

        public void SetAnimation(Animation animation)
        {
            currentAnimation = animation;
            currentTime = 0f;
            duration = 1.0f / currentAnimation.FramePerSecond;
            frame = 0;
        }

        public void Update(float dt)
        {
            currentTime += dt;
            if (currentTime >= duration)
            {
                if (frame < currentAnimation.Frames.Length - 1)
                    frame++;
                else if (currentAnimation.IsLooping)
                    frame = 0;

                currentTime = 0f;
            }
        }

        public Rectangle GetRectangle()
        {
            return rectangles[currentAnimation.Frames[frame]];
        }
    }
}
