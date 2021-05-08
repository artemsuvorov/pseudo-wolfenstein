using System.Drawing;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    public class Enemy : RotatingPane
    {
        public int Health { get; private set; } = 4;

        public Enemy(char name, Vector2 position, Image texture, RectangleF srcRect)
            : base(name, position, texture, srcRect)
        { }

        public void ApplyDamage(int damage)
        {
            if (damage <= 0) return;
            var newHealth = Health - damage;
            Health = newHealth > 0 ? newHealth : 0;
        }
    }
}