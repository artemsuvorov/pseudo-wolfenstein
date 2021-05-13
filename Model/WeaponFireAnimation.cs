using System.Collections.Generic;
using System.Linq;

namespace PseudoWolfenstein.Model
{
    public class WeaponFireAnimation : Animation
    {
        public bool IsFireFrame => AnimationFrameIndex == fireFrameIndex;

        private readonly int fireFrameIndex = 0;

        public WeaponFireAnimation(IReadOnlyList<int> animationFrames) : this(animationFrames, 0) { }
        public WeaponFireAnimation(IReadOnlyList<int> animationFrames, int fireFrame) : base(animationFrames)
        {
            this.fireFrameIndex = animationFrames.First(frame => frame == fireFrame);
        }
    }

    public static class WeaponAnimations
    {
        private static readonly IReadOnlyDictionary<WeaponType, WeaponFireAnimation> animationFramesByWeapon;

        static WeaponAnimations()
        {
            animationFramesByWeapon = new Dictionary<WeaponType, WeaponFireAnimation>()
            {
                [WeaponType.Knife] = new WeaponFireAnimation(new[] { 0, 2, 3, 0 }, fireFrame:3),
                [WeaponType.Pistol] = new WeaponFireAnimation(new[] { 0, 1, 2, 0 }, fireFrame:2),
                [WeaponType.MachineGun] = new WeaponFireAnimation(new[] { 0, 1, 2, 1, 0 }, fireFrame:2),
                [WeaponType.Chaingun] = new WeaponFireAnimation(new[] { 0, 1, 2, 0 }, fireFrame: 2),
                [WeaponType.FlameThrower] = new WeaponFireAnimation(new[] { 0, 1, 2, 3, 2, 1, 0 }, fireFrame: 2),
                [WeaponType.RocketLauncher] = new WeaponFireAnimation(new[] { 0, 1, 2, 3, 0 }, fireFrame: 3),
            };
        }

        public static WeaponFireAnimation GetAnimation(WeaponType weapon)
        {
            return animationFramesByWeapon[weapon];
        }
    }
}