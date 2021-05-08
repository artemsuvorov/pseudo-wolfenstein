using PseudoWolfenstein.Core;
using PseudoWolfenstein.Utils;
using System.Drawing;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    public class Enemy : RotatingPane
    {
        public int Health { get; private set; } = 2;

        public Enemy(char name, Vector2 position, Image texture, RectangleF srcRect)
            : base(name, position, texture, srcRect)
        { }

        public void OnPlayerShot(object sender, Player player)
        {
            var shootDistance = player.Weaponry.SelectedWeapon.Distance;
            var hitEnd = player.Position + Vector2.UnitX.RotateClockwise(-player.Rotation) * shootDistance;
            var hit = MathF2D.AreSegmentsCrossing(player.Position, hitEnd, Vertices[0], Vertices[1], out _);
            if (hit)
                 ApplyDamage(player.Weaponry.SelectedWeapon.DamageAmount);
        }

        public void ApplyDamage(int damage)
        {
            if (damage <= 0) return;
            var newHealth = Health - damage;
            Health = newHealth > 0 ? newHealth : 0;
            if (Health == 0) Destroy();
        }
    }
}