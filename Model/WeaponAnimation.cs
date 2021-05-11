using System.Collections.Generic;
using System.Linq;

namespace PseudoWolfenstein.Model
{
    public class WeaponAnimation
    {
        public int Frame { get; private set; } = 0;
        
        public bool IsContinuing => animationFrameIndex < animationFrames.Count;
        public bool IsFireFrame => animationFrameIndex == fireFrameIndex;

        private int animationFrameIndex = 0;
        private readonly int fireFrameIndex = 0;
        private readonly IReadOnlyList<int> animationFrames;

        public WeaponAnimation(IReadOnlyList<int> animationFrames) : this(animationFrames, 0) { }
        public WeaponAnimation(IReadOnlyList<int> animationFrames, int fireFrame)
        {
            this.fireFrameIndex = animationFrames.First(frame => frame == fireFrame);
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
            Frame = animationFrames[0];
        }
    }

    public static class WeaponAnimations
    {
        private static readonly IReadOnlyDictionary<WeaponType, WeaponAnimation> animationFramesByWeapon;

        static WeaponAnimations()
        {
            animationFramesByWeapon = new Dictionary<WeaponType, WeaponAnimation>()
            {
                [WeaponType.Knife] = new WeaponAnimation(new[] { 0, 2, 3, 0 }, fireFrame:3),
                [WeaponType.Pistol] = new WeaponAnimation(new[] { 0, 1, 2, 0 }, fireFrame:2),
                [WeaponType.MachineGun] = new WeaponAnimation(new[] { 0, 1, 2, 1, 0 }, fireFrame:2),
                [WeaponType.Chaingun] = new WeaponAnimation(new[] { 0, 1, 2, 0 }, fireFrame: 2),
                [WeaponType.FlameThrower] = new WeaponAnimation(new[] { 0, 1, 2, 3, 2, 1, 0 }, fireFrame: 2),
                [WeaponType.RocketLauncher] = new WeaponAnimation(new[] { 0, 1, 2, 3, 0 }, fireFrame: 3),
            };
        }

        public static WeaponAnimation GetAnimation(WeaponType weapon)
        {
            return animationFramesByWeapon[weapon];
        }
    }
}