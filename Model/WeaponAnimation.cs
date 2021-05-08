using System.Collections.Generic;

namespace PseudoWolfenstein.Model
{
    public class WeaponAnimation
    {
        public int Frame { get; private set; } = 0;
        
        public bool IsContinuing => animationFrameIndex < animationFrames.Count;

        private int animationFrameIndex = 0;
        private readonly IReadOnlyList<int> animationFrames;

        public WeaponAnimation(IReadOnlyList<int> animationFrames)
        {
            this.animationFrames = animationFrames;
        }

        public void NextFrame()
        {
            Frame = animationFrames[animationFrameIndex];
            animationFrameIndex++;
        }

        public void Reset()
        {
            animationFrameIndex = 0;
        }

        public static implicit operator WeaponAnimation(int[] frames)
        {
            return new WeaponAnimation(frames);
        }
    }

    public static class WeaponAnimations
    {
        private static readonly IReadOnlyDictionary<Weapon, WeaponAnimation> animationFramesByWeapon;

        static WeaponAnimations()
        {
            animationFramesByWeapon = new Dictionary<Weapon, WeaponAnimation>()
            {
                [Weapon.Knife] = new int[] { 0, 2, 3, 0 },
                [Weapon.Pistol] = new int[] { 0, 1, 2, 0 },
                [Weapon.MachineGun] = new int[] { 0, 1, 2, 1, 0 },
                [Weapon.Chaingun] = new int[] { 0, 1, 2, 0 },
                [Weapon.FlameThrower] = new int[] { 0, 1, 2, 3, 2, 1, 0 },
                [Weapon.RocketLauncher] = new int[] { 0, 1, 2, 3, 0 },
            };
        }

        public static WeaponAnimation GetAnimation(Weapon weapon)
        {
            return animationFramesByWeapon[weapon];
        }
    }
}