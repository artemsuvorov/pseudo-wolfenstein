using PseudoWolfenstein.Core;
using PseudoWolfenstein.Utils;
using System.Drawing;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    public class Enemy : RotatingPane
    {
        public int Health { get; private set; } = 3;

        private readonly Animation idleAnimation = new Animation(new[] { 0 });
        private readonly Animation shotAnimation = new Animation(new[] { 1, 1, 0 });
        private readonly Animation deadAnimation = new Animation(new[] { 2, 3, 4, 5, 6 });
        private Animation currentAnimation;

        private bool IsAnimating => isShot || isDead;
        private bool isShot = false;
        private bool isDead = false;

        public Enemy(char name, Vector2 position, Image texture, RectangleF srcRect)
            : base(name, position, texture, srcRect)
        { }

        public void OnPlayerShot(object sender, GameEventArgs e)
        {
            var shootDistance = e.Player.Weaponry.SelectedWeapon.Distance;
            var hitEnd = e.Player.Position + Vector2.UnitX.RotateClockwise(-e.Player.Rotation) * shootDistance;
            if (!MathF2D.AreSegmentsCrossing(e.Player.Position, hitEnd, Vertices[0], Vertices[1], out _))
                return;
            BeginShotAnimation();
            ApplyDamage(e.Player.Weaponry.SelectedWeapon.DamageAmount);
        }

        public void Animate()
        {
            if (isShot && !isDead)
                currentAnimation = shotAnimation;
            else if (isDead)
                currentAnimation = deadAnimation;
            else
                currentAnimation = idleAnimation;

            if (IsAnimating && currentAnimation.IsContinuing)
            {
                var frame = currentAnimation.NextFrame();
                Texture = Repository.Textures.EnemyFrames[frame];
            }
            else
            {
                isShot = false;
            }
        }

        private void BeginShotAnimation()
        {
            isShot = true;
        }

        public void ApplyDamage(int damage)
        {
            if (damage <= 0) return;
            var newHealth = Health - damage;
            Health = newHealth > 0 ? newHealth : 0;
            if (Health == 0) Die();
        }

        private void Die()
        {
            isDead = true;
        }
    }
}