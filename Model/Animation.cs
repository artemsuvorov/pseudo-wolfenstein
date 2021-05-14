using System.Collections.Generic;

namespace PseudoWolfenstein.Model
{
    public class Animation
    {
        private readonly IReadOnlyList<int> animationFrames;

        protected int AnimationFrameIndex { get; set; } = 0;
        public int Frame { get; private set; } = 0;
        public bool Looped { get; set; } = false;

        public bool IsContinuing => AnimationFrameIndex < animationFrames.Count;

        public Animation(IReadOnlyList<int> animationFrames)
        {
            this.animationFrames = animationFrames;
        }

        public int NextFrame()
        {
            Frame = animationFrames[AnimationFrameIndex];
            AnimationFrameIndex++;
            return Frame;
        }

        public void Reset()
        {
            AnimationFrameIndex = 0;
            Frame = animationFrames[0];
        }
    }
}