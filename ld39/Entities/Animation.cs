using System;

namespace ld39.Entities
{
    public class Animation
    {
        public int[] Frames { get; private set; }
        public int FramePerSecond { get; private set; }
        public bool IsLooping { get; private set; }

        public Animation(int[] frames, int fps, bool isLoop)
        {
            Frames = frames;
            FramePerSecond = fps;
            IsLooping = isLoop;
        }
    }
}
