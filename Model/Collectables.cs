using System.Drawing;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    public class Ammo : Collectable
    {
        private int ammoAmount = 15;

        public Ammo(char name, Vector2 position, Image texture)
            : base(name, position, texture)
        { }

        protected override void Collect(Player player)
        {
            base.Collect(player);
            if (ammoAmount <= 0) return;
            player.Weaponry.Ammo += ammoAmount;
            ammoAmount -= ammoAmount;
        }
    }

    public class HealPack : Collectable
    {
        private int healAmount = 40;

        public HealPack(char name, Vector2 position, Image texture)
            : base(name, position, texture)
        { }

        protected override void Collect(Player player)
        {
            base.Collect(player);
            if (healAmount <= 0) return;
            player.Heal(healAmount);
            healAmount -= healAmount;
        }
    }
}