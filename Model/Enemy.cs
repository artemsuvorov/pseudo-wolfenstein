using PseudoWolfenstein.Core;
using PseudoWolfenstein.Utils;
using System.Drawing;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    public class Enemy : RotatingPane
    {
        public int Health { get; private set; } = 2;
        
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
            if (isShot)
            {
                Texture = Repository.Textures.ShotFritz;
                isShot = false;
            }
            else if (isDead)
                Texture = Repository.Textures.DeadFritz;
            else
                Texture = Repository.Textures.IdleFritz;
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