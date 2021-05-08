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

    public static class Weapons
    {
        private static readonly IReadOnlyDictionary<WeaponType, Weapon> weapons;

        static Weapons()
        {
            weapons = new Dictionary<WeaponType, Weapon>
            {
                [WeaponType.Knife]          = new Weapon(WeaponType.Knife, id: 0, damage: 1, blocksDistance: 1f),
                [WeaponType.Pistol]         = new Weapon(WeaponType.Knife, id: 1, damage: 1, blocksDistance: 6f),
                [WeaponType.MachineGun]     = new Weapon(WeaponType.Knife, id: 2, damage: 1, blocksDistance: 6f),
                [WeaponType.Chaingun]       = new Weapon(WeaponType.Knife, id: 3, damage: 1, blocksDistance: 6f),
                [WeaponType.FlameThrower]   = new Weapon(WeaponType.Knife, id: 4, damage: 1, blocksDistance: 6f),
                [WeaponType.RocketLauncher] = new Weapon(WeaponType.Knife, id: 5, damage: 1, blocksDistance: 6f)
            };
        }

        public static Weapon GetWeapon(WeaponType type)
        {
            return weapons[type];
        }
    }
}