using PseudoWolfenstein.Core;
using System.Drawing;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    public class Enemy : Shotable
    {
        public int Health { get; private set; } = 3;

        private readonly Animation idleAnimation = new Animation(new[] { 0 }) { Looped = true };
        private readonly Animation shotAnimation = new Animation(new[] { 1, 1, 1, 1, 0 }) { Looped = true };
        private readonly Animation deadAnimation = new Animation(new[] { 2, 3, 4, 5, 6 });
        private Animation currentAnimation;

        private bool IsAnimating => isShot || isDead;
        private bool isShot = false;
        private bool isDead = false;

        public Enemy(char name, Vector2 position, Image texture, RectangleF srcRect)
            : base(name, position, texture, srcRect)
        { }

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
                if (currentAnimation.Looped)
                    currentAnimation.Reset();
                isShot = false;
            }
        }

        protected override void OnShot(object sender, GameEventArgs e)
        {
            BeginShotAnimation();
            ApplyDamage(e.Player.Weaponry.SelectedWeapon.DamageAmount);
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