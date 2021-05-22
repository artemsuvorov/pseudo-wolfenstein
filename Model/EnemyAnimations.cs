using System;

namespace PseudoWolfenstein.Model
{
    internal class EnemyAnimations
    {
        public readonly int FireAnimationFrame = 9;

        public readonly Animation IdleAnimation1 = new(new[] { 0 }) { Looped = true };
        public readonly Animation IdleAnimation2 = new(new[] { 10 }) { Looped = true };
        public readonly Animation IdleAnimation3 = new(new[] { 11 }) { Looped = true };
        public readonly Animation IdleAnimation4 = new(new[] { 12 }) { Looped = true };
        public readonly Animation IdleAnimation5 = new(new[] { 13 }) { Looped = true };
        public readonly Animation IdleAnimation6 = new(new[] { 14 }) { Looped = true };
        public readonly Animation IdleAnimation7 = new(new[] { 15 }) { Looped = true };
        public readonly Animation IdleAnimation8 = new(new[] { 16 }) { Looped = true };

        public readonly Animation ShotAnimation = new(new[] { 1, 1, 1, 1 }) { Looped = true };
        public readonly Animation FireAnimation = new(new[] { 7, 7, 7, 8, 8, 8, 8, 9, 8, 8, 7 }) { Looped = true };
        public readonly Animation WalkAnimation = new(new[] { 17, 17, 18, 18, 19, 19, 20, 20 }) { Looped = true };
        public readonly Animation DeadAnimation = new(new[] { 2, 2, 3, 3, 4, 4, 5, 5, 6 });

        public EnemyAnimations() { }

        public Animation GetIdleAnimation(float angle)
        {
            angle = MathF.Abs(angle) % (2f * MathF.PI);
            if (angle >= -1f * MathF.PI / 8f && angle < 1f * MathF.PI / 8f) return IdleAnimation1;
            else if (angle >= 1f * MathF.PI / 8f && angle < 3f * MathF.PI / 8f) return IdleAnimation2;
            else if (angle >= 3f * MathF.PI / 8f && angle < 5f * MathF.PI / 8f) return IdleAnimation3;
            else if (angle >= 5f * MathF.PI / 8f && angle < 7f * MathF.PI / 8f) return IdleAnimation4;
            else if (angle >= 7f * MathF.PI / 8f && angle < 9f * MathF.PI / 8f) return IdleAnimation5;
            else if (angle >= 9f * MathF.PI / 8f && angle < 11f * MathF.PI / 8f) return IdleAnimation6;
            else if (angle >= 11f * MathF.PI / 8f && angle < 13f * MathF.PI / 8f) return IdleAnimation7;
            else return IdleAnimation8;
        }
    }
}