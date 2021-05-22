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
            if (ammoAmount <= 0) return;
            player.Weaponry.Ammo += ammoAmount;
            base.Collect(player);
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
            if (healAmount <= 0) return;
            player.Heal(healAmount);
            base.Collect(player);
            healAmount -= healAmount;
        }
    }

    public class Knife : Collectable
    {
        private bool isAdded = false;

        public Knife(char name, Vector2 position, Image texture)
            : base(name, position, texture)
        { }

        protected override void Collect(Player player)
        {
            if (isAdded) return;
            player.Weaponry.Equip(WeaponType.Knife);
            base.Collect(player);
            isAdded = true;
        }
    }

    public class Pistol : Collectable
    {
        private bool isAdded = false;

        public Pistol(char name, Vector2 position, Image texture)
            : base(name, position, texture)
        { }

        protected override void Collect(Player player)
        {
            if (isAdded) return;
            player.Weaponry.Equip(WeaponType.Pistol);
            base.Collect(player);
            isAdded = true;
        }
    }

    public class MachineGun : Collectable
    {
        private bool isAdded = false;

        public MachineGun(char name, Vector2 position, Image texture)
            : base(name, position, texture)
        { }

        protected override void Collect(Player player)
        {
            if (isAdded) return;
            player.Weaponry.Equip(WeaponType.MachineGun);
            base.Collect(player);
            isAdded = true;
        }
    }

    public class ChainGun : Collectable
    {
        private bool isAdded = false;

        public ChainGun(char name, Vector2 position, Image texture)
            : base(name, position, texture)
        { }

        protected override void Collect(Player player)
        {
            if (isAdded) return;
            player.Weaponry.Equip(WeaponType.Chaingun);
            base.Collect(player);
            isAdded = true;
        }
    }

    public class FlameThrower : Collectable
    {
        private bool isAdded = false;

        public FlameThrower(char name, Vector2 position, Image texture)
            : base(name, position, texture)
        { }

        protected override void Collect(Player player)
        {
            if (isAdded) return;
            player.Weaponry.Equip(WeaponType.FlameThrower);
            base.Collect(player);
            isAdded = true;
        }
    }

    public class RocketLauncher : Collectable
    {
        private bool isAdded = false;

        public RocketLauncher(char name, Vector2 position, Image texture)
            : base(name, position, texture)
        { }

        protected override void Collect(Player player)
        {
            if (isAdded) return;
            player.Weaponry.Equip(WeaponType.RocketLauncher);
            base.Collect(player);
            isAdded = true;
        }
    }
}