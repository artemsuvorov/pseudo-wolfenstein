using PseudoWolfenstein.Core;
using System;
using System.Drawing;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    public class NextLevelVase : Shotable, IAnimatable
    {
        public event EventHandler Triggered;

        private readonly Animation breakAnimation = new Animation(new[] { 0, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2 });
        private bool isBreaking;

        public NextLevelVase(char name, Vector2 position, Image texture)
            : base(name, position, texture, default)
        { }

        public void Animate()
        {
            if (isBreaking && breakAnimation.IsContinuing)
            {
                var frame = breakAnimation.NextFrame();
                Texture = Repository.Textures.VaseFrames[frame];
            }

            if (isBreaking && !breakAnimation.IsContinuing)
                Triggered?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnShot(object sender, GameEventArgs e)
        {
            BeginBreakAnimation();
        }

        private void BeginBreakAnimation()
        {
            isBreaking = true;
        }
    }
}