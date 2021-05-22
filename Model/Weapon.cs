using System.Collections.Generic;

namespace PseudoWolfenstein.Model
{
    public enum WeaponType
    {
        Knife,
        Pistol,
        MachineGun,
        Chaingun,
        FlameThrower,
        RocketLauncher
    }

    public class Weapon
    {
        public WeaponType Type { get; private set; }
        public int Id { get; private set; }
        public int DamageAmount { get; private set; }
        public float Distance { get; private set; }

        public Weapon(WeaponType type, int id, int damage, float blocksDistance)
        {
            Type = type;
            Id = id;
            DamageAmount = damage;
            Distance = blocksDistance * Settings.WorldWallSize;
        }
    }

    public class Weapons
    {
        private readonly IReadOnlyDictionary<WeaponType, Weapon> weapons;

        public Weapons()
        {
            weapons = new Dictionary<WeaponType, Weapon>
            {
                [WeaponType.Knife]          = new Weapon(WeaponType.Knife, id: 0, damage: 1, blocksDistance: 1f),
                [WeaponType.Pistol]         = new Weapon(WeaponType.Pistol, id: 1, damage: 1, blocksDistance: 15f),
                [WeaponType.MachineGun]     = new Weapon(WeaponType.MachineGun, id: 2, damage: 1, blocksDistance: 15f),
                [WeaponType.Chaingun]       = new Weapon(WeaponType.Chaingun, id: 3, damage: 1, blocksDistance: 15f),
                [WeaponType.FlameThrower]   = new Weapon(WeaponType.FlameThrower, id: 4, damage: 1, blocksDistance: 4f),
                [WeaponType.RocketLauncher] = new Weapon(WeaponType.RocketLauncher, id: 5, damage: 1, blocksDistance: 15f)
            };
        }

        public Weapon GetWeapon(WeaponType type)
        {
            return weapons[type];
        }
    }
}